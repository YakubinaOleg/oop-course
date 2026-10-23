using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

public class Doctor : ISchedulable
{

    public bool CanSchedule(DateTime at)
    {
        return CanAcceptAt(at.Hour);
    }

    public DateTime[] GetAvailableSlots(DateTime date, int  SlotCount)
    {
        ClinicValidator.ValidatePositive(SlotCount, nameof(SlotCount));
        int smallest = Math.Min(SlotCount, Schedule.HoursPerDay);

        var slots = new DateTime[smallest];

        DateTime baseTime = date.Date.AddHours(Schedule.Start);

        for (int i = 0; i < smallest; i++)
        {
            slots[i] = baseTime.AddHours(i);
        }
        return slots;
    }
    private static int _nextId = 1;

    public int Id { get; }
    private string _firstName = "";

    public string FirstName
    {
        get => _firstName;
        set
        {
            ClinicValidator.ValidateName(value, nameof(FirstName));
            _firstName = value;
        }
    }
    private string _lastName = "";
    public string LastName
    {
        get => _lastName;
        set
        {
            ClinicValidator.ValidateName(value, nameof(LastName));
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
            ClinicValidator.ValidatePhone(value);
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