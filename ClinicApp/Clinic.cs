using System;

namespace ClinicApp
{
    public class Clinic
    {
        public string Name { get; }
        public PatientManager Patients { get; }
        public DoctorManager Doctors { get; }
        public AppointmentManager Appointments { get; }

        public Clinic(string name)
        {
            Name = name;
            Patients = new PatientManager();
            Doctors = new DoctorManager();
            Appointments = new AppointmentManager(Patients, Doctors);
        }

        public void DisplaySchedule(DateTime date)
        {
            Console.WriteLine($"\n=== Розклад на {date:dd.MM.yyyy} ===");
            Appointment[] dailyApps = Appointments.GetByDate(date);
            Appointments.DisplayList(dailyApps);
        }

        // рапорт

        public void GenerateReport()
        {
            Appointment[] upcomingApps = Appointments.GetUpcoming();
            Doctor[] allDoctors = Doctors.GetAll();

            Console.WriteLine("\n╔═════════════════════════════════════════╗");
            Console.WriteLine($"║ Звіт — {Name,-32} ║");
            Console.WriteLine("╠═════════════════════════════════════════╣");
            Console.WriteLine($"║ Пацієнтів:        {Patients.Count,-21} ║");
            Console.WriteLine($"║ Лікарів:          {Doctors.Count,-21} ║");
            Console.WriteLine($"║ Майбутніх записів: {upcomingApps.Length,-20} ║");
            Console.WriteLine("╠═════════════════════════════════════════╣");
            Console.WriteLine("║ Навантаження лікарів (майбутні записи): ║");

            // навантаження 
            for (int i = 0; i < allDoctors.Length; i++)
            {
                Doctor doc = allDoctors[i];
                int count = 0;

                for (int j = 0; j < upcomingApps.Length; j++)
                {
                    if (upcomingApps[j].DoctorId == doc.Id)
                    {
                        count++;
                    }
                }

                string docLine = $"  {doc.FullName} ({doc.Speciality}): {count} записів";
                Console.WriteLine($"║ {docLine,-39} ║");
            }

            Console.WriteLine("╚═════════════════════════════════════════╝");
        }
    }
}