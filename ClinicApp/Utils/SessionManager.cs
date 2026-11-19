using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils
{
    public class SessionManager
    {
        private const string PatientsSection = "[PATIENTS]";
        private const string EndMarker = "[END]";

        private readonly string _sessionPath;

        public SessionManager(string sessionPath = "session.dat")
        {
            _sessionPath = sessionPath;
        }

        public bool Exists()
        {
            return File.Exists(_sessionPath);
        }

        public void Save(Clinic clinic)
        {
            Patient[] patients = clinic.Patients.GetAll();

            using (StreamWriter writer = new StreamWriter(_sessionPath, false, Encoding.UTF8))
            {
                writer.WriteLine(PatientsSection);
                for (int i = 0; i < patients.Length; i++)
                {
                    Patient p = patients[i];
                    writer.WriteLine($"{p.FirstName},{p.LastName},{p.DateOfBirth:dd.MM.yyyy},{p.BloodType},{p.Phone}");
                }
                writer.WriteLine(EndMarker);
            }
        }

        public int Load(Clinic clinic)
        {
            if (!File.Exists(_sessionPath))
                return 0;

            string[] lines = File.ReadAllLines(_sessionPath, Encoding.UTF8);
            string section = "";
            int added = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (line == "")
                    continue;

                if (line.StartsWith("["))
                {
                    section = line == EndMarker ? "" : line;
                    continue;
                }

                if (section != PatientsSection)
                    continue;

                try
                {
                    string[] parts = line.Split(',');
                    if (parts.Length != 5)
                        continue;

                    string firstName = parts[0].Trim();
                    string lastName = parts[1].Trim();
                    DateTime birthDate = DateTime.ParseExact(parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());
                    string phone = parts[4].Trim();

                    if (PatientExists(clinic, firstName, lastName, birthDate))
                        continue;

                    clinic.Patients.Add(new Patient(firstName, lastName, birthDate, bloodType, phone, ""));
                    added++;
                }
                catch (Exception)
                {
                }
            }

            return added;
        }

        private static bool PatientExists(Clinic clinic, string firstName, string lastName, DateTime birthDate)
        {
            Patient[] patients = clinic.Patients.GetAll();
            for (int i = 0; i < patients.Length; i++)
            {
                if (patients[i].FirstName == firstName &&
                    patients[i].LastName == lastName &&
                    patients[i].DateOfBirth.Date == birthDate.Date)
                {
                    return true;
                }
            }
            return false;
        }
    }
}