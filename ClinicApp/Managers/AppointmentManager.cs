using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Models;
using System.Runtime.InteropServices;

namespace ClinicApp.Managers;

public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private Appointment[] _appointments;
    private int _count;

    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count => _count;

    private bool AppointmentCr(int pId,int dId) {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Помилка: досягнуто ліміт записів.");
            return false;
        }

        Patient? patient = _patients.FindById(pId);
        if (patient == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID {pId} не знайдено.");
            return false;
        }

        Doctor? doctor = _doctors.FindById(dId);
        if (doctor == null)
        {
            Console.WriteLine($"Помилка: лікаря з ID {dId} не знайдено.");
            return false;
        }

        return true;
    }

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
        _appointments = new Appointment[MaxAppointments];
        _count = 0;
    }

    // ID
    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }
        return null;
    }

    // 1. Запис
    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        if (AppointmentCr(patientId, doctorId))
        {

            RegularAppointment newAppointment = new RegularAppointment(patientId, doctorId, scheduledAt, durationMinutes);
            _appointments[_count] = newAppointment;
            _count++;

            Console.WriteLine($"Створено запис [{newAppointment.Id}]: {newAppointment.GetDescription()} | Пацієнт ID: {patientId}, Лікар ID: {doctorId} о {scheduledAt:dd.MM.yyyy HH:mm}");
            return true;
        }
        return false;
    }

    // Терміновий запис
    public bool BookUrgent(int patientId, int doctorId, DateTime scheduledAt, string urgencyNote = "", int durationMinutes = 30)
    {
        if (!AppointmentCr(patientId, doctorId))
        {
            return false;
        }

        UrgentAppointment newAppointment = new UrgentAppointment(patientId, doctorId, scheduledAt, urgencyNote, durationMinutes);
        _appointments[_count] = newAppointment;
        _count++;

        Console.WriteLine($"Створено терміновий запис [{newAppointment.Id}]: {newAppointment.GetDescription()} | Пацієнт ID: {patientId}, Лікар ID: {doctorId} о {scheduledAt:dd.MM.yyyy HH:mm}");
        return true;
    }

    // Запис до спеціаліста
    public bool BookSpecialist(int patientId, int doctorId, DateTime scheduledAt, string topic = "", int durationMinutes = 45)
    {
        if (!AppointmentCr(patientId, doctorId))
        {
            return false;
        }

        SpecialistAppointment newAppointment = new SpecialistAppointment(patientId, doctorId, scheduledAt, topic, durationMinutes);
        _appointments[_count] = newAppointment;
        _count++;

        Console.WriteLine($"Створено запис до спеціаліста [{newAppointment.Id}]: {newAppointment.GetDescription()} | Пацієнт ID: {patientId}, Лікар ID: {doctorId} о {scheduledAt:dd.MM.yyyy HH:mm}");
        return true;
    }

    // 2. Скасування 
    public bool Cancel(int id, string reason = "")
    {
        Appointment? App = FindById(id);

        if (App == null)
        {
            Console.WriteLine($"Запис з ID {id} не знайдено.");
            return false;
        }
        bool success = App.Cancel(reason);
        if (success)
        {
            Console.WriteLine($"[Успіх] Запис [{id}] успішно скасовано.");
        }
        else
        {
            Console.WriteLine($"[Помилка] Запис [{id}] неможливо скасувати (поточний статус: {App.Status}).");
        }
        return success;
    }

    // 3. Завершення
    public bool Complete(int id)
    {
        Appointment? app = FindById(id);
        if (app == null)
        {
            Console.WriteLine($"Запис з ID {id} не знайдено.");
            return false;
        }
        bool success = app.Complete();
        if (success)
        {
            Console.WriteLine($"Запис [{id}] завершено.");
        }
        else
        {
            Console.WriteLine($"[Помилка] Запис [{id}] неможливо завершити (поточний статус: {app.Status}).");
        }
        return success;
    }

    // 4. всі записи пацика
    public Appointment[] GetByPatient(int patientId)
    {
        int Count = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                Count++;
            }
        }

        Appointment[] result = new Appointment[Count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[index++] = _appointments[i];
            }
        }
        return result;
    }

    // 5. всі записи лекаря
    public Appointment[] GetByDoctor(int doctorId)
    {
        int Count = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                Count++;
            }
        }

        Appointment[] result = new Appointment[Count];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[index++] = _appointments[i];
            }
        }
        return result;
    }

    // 6. всі записи на дату 
    public Appointment[] GetByDate(DateTime date)
    {
        int Count = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                Count++;
            }
        }

        Appointment[] result = new Appointment[Count];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[index++] = _appointments[i];
            }
        }
        return result;
    }

    public Appointment[] GetByDate(int year, int month, int day)
    {
        return GetByDate(new DateTime(year, month, day));
    }

    // 7. всі майбутні 
    public Appointment[] GetUpcoming()
    {
        int Count = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                Count++;
            }
        }

        Appointment[] result = new Appointment[Count];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[index++] = _appointments[i];
            }
        }
        return result;
    }

    // 8. запис з іменами
    public void DisplayAppointment(Appointment app)
    {
        if (app == null) return;

        Patient? patient = _patients.FindById(app.PatientId);
        Doctor? doctor = _doctors.FindById(app.DoctorId);

        string patientName = patient != null ? patient.FullName : $"Пацієнт #{app.PatientId}";
        string doctorName = doctor != null ? doctor.FullName : $"Лікар #{app.DoctorId}";

        string line = $"[{app.Id}] {patientName} -> {doctorName} | {app.ScheduledAt:dd.MM.yyyy HH:mm}-{app.EndsAt:HH:mm} | {app.Status}";

        if (app.Notes != null && app.Notes.Length > 0)
        {
            line += $" | {app.Notes}";
        }

        Console.WriteLine(line);
    }

    // 9. список записів
    public void DisplayList(Appointment[] list)
    {
        if (list == null || list.Length == 0)
        {
            Console.WriteLine("Записів не знайдено.");
            return;
        }

        for (int i = 0; i < list.Length; i++)
        {
            DisplayAppointment(list[i]);
        }
    }

    public Appointment? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) return null;
            return _appointments[index];
        }
    }

    public Appointment[] GetAll()
    {
        Appointment[] arr = new Appointment[_count];
        for(int i = 0; i < _count; i++)
        {
            arr[i] = _appointments[i];
        }
        return arr;
    }

    public static int CancelAll(ICancellable[] items, string reason = "")
    {
        int cancelledCount = 0;
        foreach (var item in items)
        {
            if (item.Cancel(reason))
            {
                cancelledCount++;
            }
        }
        return cancelledCount;
    }
}