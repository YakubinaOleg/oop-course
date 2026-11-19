using System.Text;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Utils
{
    public class ClinicExporter
    {
        private readonly Clinic _clinic;
        private readonly string _baseDir;

        public ClinicExporter(Clinic clinic, string baseDir = "reports")
        {
            _clinic = clinic;
            _baseDir = baseDir;
        }

        public string ExportPatients()
        {
            Patient[] items = _clinic.Patients.GetAll();
            string[] lines = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
                lines[i] = items[i].ToString();

            return WriteReport("patients.txt", "Пацієнти — звіт", lines, $"Всього: {lines.Length}");
        }

        public string ExportAppointments()
        {
            Appointment[] items = _clinic.Appointments.GetAll();
            string[] lines = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
                lines[i] = FormatAppointment(items[i]);

            return WriteReport("appointments.txt", "Записи — звіт", lines, $"Всього: {lines.Length}");
        }

        public string ExportBilling()
        {
            IPayable[] items = _clinic.Billing.GetAllUnpaid();
            string[] lines = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] is Appointment a)
                    lines[i] = FormatAppointment(a);
                else
                    lines[i] = $"{items[i]} | {items[i].GetCost():F2} грн";
            }

            string summary = $"Неоплачених: {lines.Length}, загальний борг: {_clinic.Billing.GetTotalDebt():F2} грн";
            return WriteReport("billing.txt", "Рахунки — звіт", lines, summary);
        }

        public string ExportTreatmentPlans()
        {
            TreatmentPlan[] items = _clinic.TreatmentPlans.GetAll();
            string[] lines = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                lines[i] = $"[{items[i].Id}] Пацієнт #{items[i].PatientId} | {items[i].Diagnosis} | {items[i].DurationDays} дн. | {items[i].Status}";
            }

            return WriteReport("treatment_plans.txt", "Плани лікування — звіт", lines, $"Всього: {lines.Length}");
        }

        public string[] ExportAll()
        {
            return new string[]
            {
                ExportPatients(),
                ExportAppointments(),
                ExportBilling(),
                ExportTreatmentPlans()
            };
        }

        private string FormatAppointment(Appointment app)
        {
            Patient? patient = _clinic.Patients.FindById(app.PatientId);
            Doctor? doctor = _clinic.Doctors.FindById(app.DoctorId);

            string patientName = patient != null ? patient.FullName : $"Пацієнт #{app.PatientId}";
            string doctorName = doctor != null ? doctor.FullName : $"Лікар #{app.DoctorId}";

            return $"[{app.Id}] {app.GetDescription()} | {patientName} -> {doctorName} | " +
                   $"{app.ScheduledAt:dd.MM.yyyy HH:mm}-{app.EndsAt:HH:mm} | {app.Status} | {app.GetCost():F2} грн";
        }

        private string EnsureDirectory()
        {
            string dir = Path.Combine(_baseDir, DateTime.Now.ToString("yyyy-MM-dd"));
            Directory.CreateDirectory(dir);
            return dir;
        }

        private string WriteReport(string fileName, string title, string[] lines, string summary)
        {
            string path = Path.Combine(EnsureDirectory(), fileName);
            string separator = new string('-', 40);

            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine($"=== {title} ===");
                writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
                writer.WriteLine(separator);
                for (int i = 0; i < lines.Length; i++)
                {
                    writer.WriteLine($"{i + 1,3}. {lines[i]}");
                }
                writer.WriteLine(separator);
                writer.WriteLine(summary);
            }

            return path;
        }
    }
}