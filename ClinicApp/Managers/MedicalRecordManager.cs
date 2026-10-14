using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private MedicalRecord[] _record = new MedicalRecord[1000];
    private int _count = 0;
    public int Count => _count;

    // 1. ДОДАВАННЯ 
    public void Add(MedicalRecord record)
    {
        if (_count < _record.Length)
        {
            _record[_count++] = record;
            Console.WriteLine($"Запис #[{record.Id}] [{record.GetRecordType()}] успішно доданий.");
        }
        else
        {
            Console.WriteLine($"Максимальна кількість записів досягнута({_record.Length}).");
        }
    }

    // 2. Пошук 
    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_record[i].Id == id)
            {
                return _record[i];
            }
        }
        return null;
    }

    public MedicalRecord[] GetByPatientId(int id)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i].PatientId == id)
            {
                matches++;
            }
        }
        MedicalRecord[] result = new MedicalRecord[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i].PatientId == id)
            {
                result[index++] = _record[i];
            }
        }
        return result;
    }

    public MedicalRecord[] GetByDoctorId(int doctorId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i].DoctorId == doctorId)
            {
                matches++;
            }
        }
        MedicalRecord[] result = new MedicalRecord[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i].DoctorId == doctorId)
            {
                result[index++] = _record[i];
            }
        }
        return result;
    }

    public void DisplayAll()
    {
        if (Count == 0)
        {
            Console.WriteLine("Немає записів.");
            return;
        }
        Console.WriteLine("Список всіх записів:");
        for (int i = 0; i < Count; i++)
        {
            Console.WriteLine(_record[i]);
        }
    }
    public void DisplayList(MedicalRecord[] records)
    {
        if (records.Length == 0)
        {
            Console.WriteLine("Немає записів.");
            return;
        }
        Console.WriteLine("Список записів:");
        foreach (var record in records)
        {
            Console.WriteLine(record);
        }
    }

    public MedicalRecord? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count) return null;
            return _record[index];
        }
    }
        
}