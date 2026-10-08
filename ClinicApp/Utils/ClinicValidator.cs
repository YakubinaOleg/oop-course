namespace ClinicApp.Utils;
public static class ClinicValidator
{
    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new ArgumentException("Некоректне ім'я/прізвище, максимум 50 символів.", fieldName);
        }
    }
    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Номер телефону не може бути null.", nameof(phone));
        }
        if (phone.Length != 10)
        {
            throw new ArgumentException("Некоректний номер телефону, має бути 10 цифр.", nameof(phone));
        }
        foreach (var c in phone) 
        {
            if (!char.IsDigit(c))
            {
                throw new ArgumentException("Некоректний номер телефону, може містити тільки цифри.", nameof(phone));
            }
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
