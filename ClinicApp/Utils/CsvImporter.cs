using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils
{
    public class CsvImporter
    {
        private const int FieldCount = 5;

        public ImportResult ImportPatients(Clinic clinic, string filePath)
        {
            ImportResult result = new ImportResult();

            if (!File.Exists(filePath))
            {
                result.AddError(0, "Файл не знайдено.");
                return result;
            }

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            for (int i = 1; i < lines.Length; i++)
            {
                int lineNumber = i + 1;

                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                try
                {
                    string[] parts = lines[i].Split(',');

                    if (parts.Length != FieldCount)
                    {
                        result.AddError(lineNumber, $"Рядок має {parts.Length} {FieldWord(parts.Length)} замість {FieldCount}.");
                        continue;
                    }

                    string firstName = parts[0].Trim();
                    string lastName = parts[1].Trim();

                    if (firstName == "")
                    {
                        result.AddError(lineNumber, "Ім'я не може бути порожнім.");
                        continue;
                    }
                    if (lastName == "")
                    {
                        result.AddError(lineNumber, "Прізвище не може бути порожнім.");
                        continue;
                    }

                    DateTime birthDate = DateTime.ParseExact(parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());
                    string phone = parts[4].Trim();

                    Patient patient = new Patient(firstName, lastName, birthDate, bloodType, phone, "");
                    clinic.Patients.Add(patient);
                    result.AddSuccess();
                }
                catch (FormatException)
                {
                    result.AddError(lineNumber, "Некоректна дата (очікується дд.ММ.рррр).");
                }
                catch (ArgumentException ex)
                {
                    result.AddError(lineNumber, ex.Message);
                }
            }

            return result;
        }

        private static string FieldWord(int count)
        {
            if (count == 1) return "поле";
            if (count >= 2 && count <= 4) return "поля";
            return "полів";
        }
    }
}