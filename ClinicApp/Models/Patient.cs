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
            ClinicValidator.ValidateName(value, nameof(FirstName));
            _firstName = value; 
        }
    }
    private string _lastName = "";
    public string LastName { get => _lastName;
    set 
        {
            ClinicValidator.ValidateName(value, nameof(LastName));
            _lastName = value; 
        }
    }
    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            ClinicValidator.ValidateDate(value, nameof(DateOfBirth));
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
            ClinicValidator.ValidatePhone(value);
            _phone = value; 
        }
    }
    private string _email;

    public string Email
    {
        get => _email;
        set
        {
            ClinicValidator.ValidateEmail(value);
            _email = value;
        }
    }

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
