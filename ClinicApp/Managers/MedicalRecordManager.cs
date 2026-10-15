using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private readonly MedicalRecord[] _record = new MedicalRecord[1000];
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

    public Diagnosis[] GetDiagnoses(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            Diagnosis? diag = _record[i] as Diagnosis;
            if (diag != null && diag.PatientId == patientId)
            {
                matches++;
            }
        }

        Diagnosis[] result = new Diagnosis[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            Diagnosis? diag = _record[i] as Diagnosis;
            if (diag != null && diag.PatientId == patientId)
            {
                result[index++] = diag;
            }
        }
        return result;
    }

    public LabResult[] GetLabResults(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is LabResult lb && lb.PatientId == patientId)
            {
                matches++;
            }
        }
        LabResult[] result = new LabResult[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is LabResult lb && lb.PatientId == patientId)
            {
                result[index++] = lb;
            }
        }
        return result;
    }

    public Prescription[] GetPrescriptions(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is Prescription p && p.PatientId == patientId)
            {
                matches++;
            }
        }
        Prescription[] result = new Prescription[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is Prescription p && p.PatientId == patientId)
            {
                result[index++] = p;
            }
        }
        return result;
    }

    public Diagnosis[] GetChronicDiagnoses(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is Diagnosis d && d.PatientId == patientId && d.IsChronic)
            {
                matches++;
            }
        }
        Diagnosis[] result = new Diagnosis[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is Diagnosis d && d.PatientId == patientId && d.IsChronic)
            {
                result[index++] = d;
            }
        }
        return result;
    }

    public Prescription[] GetActivePrescriptions(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is Prescription p && p.PatientId == patientId && p.IsActive())
            {
                matches++;
            }
        }
        Prescription[] result = new Prescription[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_record[i] is Prescription p && p.PatientId == patientId && p.IsActive())
            {
                result[index++] = p;
            }
        }
        return result;
    }

    public void DisplayPatientSummary(int patientId)
    {
        int diagnosisCount = 0;
        int labResultCount = 0;
        int prescriptionCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_record[i].PatientId == patientId)
            {
                if (_record[i] is Diagnosis) diagnosisCount++;
                else if (_record[i] is LabResult) labResultCount++;
                else if (_record[i] is Prescription) prescriptionCount++;
            }
        }

        int totalRecords = diagnosisCount + labResultCount + prescriptionCount;

        if (totalRecords == 0)
        {
            Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");
            Console.WriteLine("Записів немає.");
            return;
        }

        Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");
        Console.WriteLine($"Всього записів: {totalRecords} (діагнозів: {diagnosisCount}, аналізів: {labResultCount}, рецептів: {prescriptionCount})");

        var chronicDiagnoses = GetChronicDiagnoses(patientId);
        if (chronicDiagnoses.Length > 0)
        {
            Console.WriteLine($"Хронічні діагнози ({chronicDiagnoses.Length}):");
            foreach (var d in chronicDiagnoses)
            {
                Console.WriteLine($"  {d}");
            }
        }

        var activePrescriptions = GetActivePrescriptions(patientId);
        if (activePrescriptions.Length > 0)
        {
            Console.WriteLine($"Активні рецепти ({activePrescriptions.Length}):");
            foreach (var p in activePrescriptions)
            {
                Console.WriteLine($"  {p} | до {p.ExpiresAt:dd.MM.yyyy}");
            }
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