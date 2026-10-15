using System.Text.RegularExpressions;

namespace ClinicApp.Utils;
public static class ClinicValidator
{
    private static readonly Regex PhoneRegex = new Regex(@"^[0-9]{10}\z", RegexOptions.Compiled);
    private static readonly Regex EmailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);
    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || !PhoneRegex.IsMatch(phone))
        {
            throw new ArgumentException("Некоректний номер телефону, має бути рівно 10 цифр.", nameof(phone));
        }
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return;

        if (!EmailRegex.IsMatch(email))
        {
            throw new ArgumentException("Некоректний формат email.", nameof(email));
        }
    }
    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new ArgumentException("Некоректне значення, максимум 50 символів.", fieldName);
        }
    }
    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Дата не може бути пізніше поточного часу.");
        }
        if (value.Year < 1900)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Дата не може бути раніше 1900 року.");
        }
    }
    public static void ValidatePositive(int value, string fieldName) 
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Значення має бути більше 0.");
        }
    }
}
