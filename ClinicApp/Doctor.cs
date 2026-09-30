using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Speciality Speciality { get; set; }
    public string LicenseNumber { get; set; }
    public string Phone { get; set; }
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
        string status = IsAvailableNow ? "доступний зараз" : "не в робочий час";
        string formattedSpec = ClinicFormatter.FormatSpeciality(Speciality);
        string formattedPhone = ClinicFormatter.FormatPhone(Phone);

        return $"[{Id}] {FullName} | {formattedSpec} | {LicenseNumber} | Тел: {formattedPhone} | {Schedule} | {status}";
    }
}