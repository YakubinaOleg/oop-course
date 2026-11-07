using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Managers;

public class PatientManager
{
    private List<Patient> _patients = new List<Patient>();

    public Patient[] GetAll() => _patients.ToArray();

    public int Count => _patients.Count;

    // 1. ДОДАВАННЯ
    public void Add(Patient patient)
    {
        _patients.Add(patient);
        Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
    }

    // 2. ID
    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; ++i)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }
        return null;
    }

    public bool TryFindById(int id, out Patient? patient)
    {
        patient = FindById(id);
        return patient != null;
    }

    // 3. ІМ'Я АБО ПРІЗВИЩЕ
    public Patient[] FindByName(string name)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; ++i)
        {
            if (_patients[i].FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                _patients[i].LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    public bool RemoveById(int id)
    {
        int index = -1;
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine($"Пацієнта з ID {id} не знайдено.");
            return false;
        }

        string removedName = _patients[index].FullName;
        _patients.RemoveAt(index);

        Console.WriteLine($"Пацієнта [{id}] {removedName} успішно видалено.");
        return true;
    }

    // ПОШУК ЗА ГРУПОЮ КРОВІ
    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    // 5. ВИВІД УСІХ
    public void DisplayAll()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
        }
        else
        {
            Console.WriteLine($"=== Пацієнти ({_patients.Count}) ===");
            for (int i = 0; i < _patients.Count; ++i)
            {
                Console.WriteLine(_patients[i]);
            }
        }
    }

    // 6. СТАТИСТИКА
    public void DisplayStats()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("Немає даних для статистики.");
            return;
        }

        double sumAge = 0;
        int smallestOneIdx = 0;
        int biggestOneIdx = 0;
        int howManyAdults = 0;

        for (int i = 0; i < _patients.Count; ++i)
        {
            sumAge += _patients[i].Age;
            if (_patients[i].Age < _patients[smallestOneIdx].Age)
            {
                smallestOneIdx = i;
            }
            if (_patients[i].Age > _patients[biggestOneIdx].Age)
            {
                biggestOneIdx = i;
            }
            if (_patients[i].IsAdult)
            {
                howManyAdults++;
            }
        }

        double averageAge = sumAge / _patients.Count;

        Console.WriteLine("\n=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього: {_patients.Count}");
        Console.WriteLine($"Середній вік: {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший: {_patients[smallestOneIdx].FullName} ({_patients[smallestOneIdx].Age} р.)");
        Console.WriteLine($"Найстарший: {_patients[biggestOneIdx].FullName} ({_patients[biggestOneIdx].Age} р.)");
        Console.WriteLine($"Дорослих: {howManyAdults} з {_patients.Count}");
    }

    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _patients.Count) return null;
            return _patients[index];
        }
    }
}