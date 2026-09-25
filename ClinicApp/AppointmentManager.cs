namespace ClinicApp;

public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private Appointment[] _appointments;
    private int _count;

    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count => _count;

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
        _appointments = new Appointment[MaxAppointments];
        _count = 0;
    }

    // ID
    private Appointment FindById(int id)
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
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Помилка: досягнуто ліміт записів.");
            return false;
        }

        Patient patient = _patients.FindById(patientId);
        if (patient == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID {patientId} не знайдено.");
            return false;
        }

        Doctor doctor = _doctors.FindById(doctorId);
        if (doctor == null)
        {
            Console.WriteLine($"Помилка: лікаря з ID {doctorId} не знайдено.");
            return false;
        }

        Appointment newAppointment = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);
        _appointments[_count] = newAppointment;
        _count++;

        Console.WriteLine($"Запис [{newAppointment.Id}] створено: {patient.FullName} -> {doctor.FullName} о {scheduledAt:dd.MM.yyyy HH:mm}");
        return true;
    }

    // 2. Скасування 
    public bool Cancel(int id, string reason = "")
    {
        Appointment App = FindById(id);
        if (App != null)
        {
            bool success = App.Cancel(reason);
            if (success)
            {
                Console.WriteLine($"Запис [{id}] скасовано.");
            }
            return success;
        }
        Console.WriteLine($"Запис з ID {id} не знайдено.");
        return false;
    }

    // 3. Завершення
    public bool Complete(int id)
    {
        Appointment app = FindById(id);
        if (app != null)
        {
            bool success = app.Complete();
            if (success)
            {
                Console.WriteLine($"Запис [{id}] завершено.");
            }
            return success;
        }
        Console.WriteLine($"Запис з ID {id} не знайдено.");
        return false;
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

        Patient patient = _patients.FindById(app.PatientId);
        Doctor doctor = _doctors.FindById(app.DoctorId);

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
}