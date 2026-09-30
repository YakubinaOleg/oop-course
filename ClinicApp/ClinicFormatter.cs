using System;

namespace ClinicApp
{
    public static class ClinicFormatter
    {
        public static string FormatBloodType(BloodType bloodType)
        {
            return bloodType switch
            {
                BloodType.APositive => "A+",
                BloodType.ANegative => "A-",
                BloodType.BPositive => "B+",
                BloodType.BNegative => "B-",
                BloodType.ABPositive => "AB+",
                BloodType.ABNegative => "AB-",
                BloodType.OPositive => "O+",
                BloodType.ONegative => "O-",
                _ => bloodType.ToString()
            };
        }

        public static string FormatSpeciality(Speciality speciality)
        {
            return speciality switch
            {
                Speciality.General => "Загальна медицина",
                Speciality.Cardiology => "Кардіологія",
                Speciality.Surgery => "Хірургія",
                Speciality.Pediatrics => "Педіатрія",
                Speciality.Neurology => "Неврологія",
                _ => speciality.ToString()
            };
        }

        public static string FormatAge(int age)
        {
            int lastTwoDigits = age % 100;
            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
            {
                return $"{age} років";
            }

            int lastDigit = age % 10;
            if (lastDigit == 1)
            {
                return $"{age} рік";
            }
            if (lastDigit >= 2 && lastDigit <= 4)
            {
                return $"{age} роки";
            }

            return $"{age} років";
        }

        public static string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return phone;

            // Залишаємо лише цифри
            string digits = new string(phone.Where(char.IsDigit).ToArray());

            // Якщо номер починається з 380 і має 12 цифр, беремо останні 10
            if (digits.Length == 12 && digits.StartsWith("380"))
            {
                digits = digits.Substring(2);
            }

            // Форматуємо 10-значний номер: 0971112233 -> (097) 111-2233
            if (digits.Length == 10)
            {
                return $"({digits.Substring(0, 3)}) {digits.Substring(3, 3)}-{digits.Substring(6)}";
            }

            return phone;
        }
    }
}