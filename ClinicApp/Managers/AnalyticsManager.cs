using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private readonly AppointmentManager _appointments;
    private readonly DoctorManager _doctors;
    private readonly PatientManager _patients;

    public AnalyticsManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        Doctor[] doctors = _doctors.GetAll();
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < doctors.Length; i++)
        {
            Doctor doctor = doctors[i];

            int count = 0;
            decimal total = 0m;
            DateTime last = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].DoctorId == doctor.Id)
                {
                    count++;
                    total += appointments[j].GetCost();
                    if (appointments[j].ScheduledAt > last)
                    {
                        last = appointments[j].ScheduledAt;
                    }
                }
            }
            yield return new DoctorStats(doctor.Id, doctor.FullName, count, total, last);
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        Patient[] patients = _patients.GetAll();
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < patients.Length; i++)
        {
            Patient patient = patients[i];

            int count = 0;
            decimal total = 0m;
            DateTime last = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].PatientId == patient.Id)
                {
                    count++;
                    total += appointments[j].GetCost();
                    if (appointments[j].ScheduledAt > last)
                    {
                        last = appointments[j].ScheduledAt;
                    }
                }
            }
            yield return new PatientStats(patient.Id, patient.FullName, count, total, last);
        }
    }
}