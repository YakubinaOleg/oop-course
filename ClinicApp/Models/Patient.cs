using ClinicApp.Enums;
using ClinicApp.Utils;
using System.IO.Pipes;
using System.Reflection.Metadata;

namespace ClinicApp.Models;

public class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
    private string _firstName = "";
    public string FirstName { get => _firstName;
        set 
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException("Некоректне ім'я/прізвище, максимум 50 символів.", nameof(FirstName));
            }
            _firstName = value; 
        }
    }
    private string _lastName = "";
    public string LastName { get => _lastName;
    set 
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException("Некоректне ім'я/прізвище, максимум 50 символів.", nameof(LastName));
            }
            _lastName = value; 
        }
    }
    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (value > DateTime.Today) { throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Дата народження не може бути в майбутньому."); }
            if (value.Year < 1900) { throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Дата народження не може бути раніше 1900 року."); }
            _dateOfBirth = value; 
        }
    }
    public BloodType BloodType { get; set; }
    private string _phone = "";
    public string Phone
    {
        get => _phone;
        set 
        {
            if (value == null || value.Length != 10) { throw new ArgumentException("Некоректний номер телефону, повинно бути 10 цифр.", nameof(Phone)); }
            foreach (var item in value) 
            {
                if (!char.IsDigit(item))
                {
                    throw new ArgumentException("Некоректний номер телефону, повинні бути лише цифри.", nameof(Phone));
                }
            }
            _phone = value; 
        }
    }
    public string Email { get; set; }

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }

    public bool IsAdult
    {
        get
        {
            return Age >= 18;
        }
    }


    public Patient(string firstName, string lastName, DateTime dateOfBirth, BloodType bloodType, string phone, string email)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = email;
    }

    public Patient(string firstName, string lastName) : this(firstName, lastName, new DateTime(2000, 3, 15), BloodType.Unknown, "0000000000", "") { }
    public Patient() : this("Невідомий", "Пацієнт")
    {
        BloodType = BloodType.Unknown;
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
        {
            return "дитина";
        }
        else if (Age >= 18 && Age < 60)
        {
            return "дорослий";
        }
        else
        {
            return "літній";
        }
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | Вік: {ClinicFormatter.FormatAge(Age)} | Гр. крові: {ClinicFormatter.FormatBloodType(BloodType)} | Тел: {ClinicFormatter.FormatPhone(Phone)}";
    }
}
