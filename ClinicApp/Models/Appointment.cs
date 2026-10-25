using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

public class Appointment : IPayable, ICancellable
{
    private static int _nextId = 1;

    public int Id { get; }
    public int PatientId { get; }
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; set; }
    private int _durationMinutes;

    public bool IsPaid { get; private set; } = false;

    public bool IsCancelled  => Status == AppointmentStatus.Cancelled;

    public string CancellationReason
    {
        get
        {
            if (Status == AppointmentStatus.Cancelled)
            {
                return Notes;
            }
            return "";
        }
    }

    public virtual decimal GetCost()
    {
        return DurationMinutes * 10m;
    }

    public virtual string GetDescription()
    {
        return "Прийом";
    }

    public int GetPriority()
    {
        return 3;
    }

    public void MarkPaid()
    {
        if(!IsCancelled)
        {
            IsPaid = true;
        }
    }

    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            ClinicValidator.ValidatePositive(value, nameof(DurationMinutes));
            _durationMinutes = value;
        }
    }
    public AppointmentStatus Status { get; private set; }
    public string Notes { get; private set; }

    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);
    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;

    public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        Id = _nextId++;
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
        Notes = "";
    }

    public Appointment() : this(0,0, DateTime.Now) { }

    public bool Cancel(string reason = "")
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Cancelled;
            Notes = reason;
            return true;
        }
        return false;
    }

    public bool Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
            return false;

        Status = AppointmentStatus.Completed;
        return true;
    }

    public override string ToString()
    {
        string result = $"{GetDescription()} [{Id}] Пацієнт #{PatientId} -> Лікар #{DoctorId} | {ScheduledAt:dd.MM.yyyy HH:mm}-{EndsAt:HH:mm} | {Status} | Вартість: {GetCost():F2} грн";

        if (!string.IsNullOrEmpty(Notes))
        {
            result += $" | {Notes}";
        }

        return result;
    }
}