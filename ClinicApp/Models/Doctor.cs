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
        get => _firstName; set => _firstName = value;
    }
    private string _lastName = "";
    public string LastName
    {
        get => _lastName; set => _lastName = value;
    }
    public Speciality Speciality { get; set; }
    
    private string _licenseNumber = "";
    public string LicenseNumber
    {
        get => _licenseNumber; set => _licenseNumber = value;
    }
    private string _phone = "";
    public string Phone
    {
        get => _phone; set => _phone = value;
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