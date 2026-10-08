using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    private string _firstName = "";

    public string FirstName
    {
        get => _firstName;
        set
        {
            if(string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException("Некоректне ім'я/прізвище, максимум 50 символів.", nameof(FirstName));
            }
            _firstName = value;
        }
    }
    private string _lastName = "";
    public string LastName
    {
        get => _lastName;
        set
        {
            if(string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException("Некоректне ім'я/прізвище, максимум 50 символів.", nameof(LastName));
            }
            _lastName = value;
        }
    }
    public Speciality Speciality { get; set; }
    
    private string _licenseNumber = "";
    public string LicenseNumber
    {
        get => _licenseNumber; set
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Некоректний номер ліцензії.", nameof(LicenseNumber));
            }
            _licenseNumber = value;
        }
    }
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
    public WorkSchedule Schedule { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public int WorkingHoursPerDay
    {
        get
        {
            return Schedule.End - Schedule.Start;
        }
    }

    public string WorkSchedule
    {
        get
        {
            return Schedule.Display;
        }
    }

    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }

    public bool IsAvailableNow
    {
        get
        {

            return Schedule.Contains(DateTime.Now.Hour);
        }
    }

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, WorkSchedule schedule)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;
    }

    public Doctor(string firstName, string lastName, Speciality speciality)
        : this(firstName, lastName, speciality, "LIC-000", "0000000000", new WorkSchedule(8, 17)) { }

    public Doctor()
        : this("Невідомий", "Лікар", Speciality.General) { }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | {ClinicFormatter.FormatSpeciality(Speciality)} | {LicenseNumber} | Тел: {ClinicFormatter.FormatPhone(Phone)} | {Schedule}";
    }
}