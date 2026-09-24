namespace ClinicApp;

public class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count = 0;

    public int Count => _count;

    // 1. ДОДАВАННЯ 
    public void Add(Patient patient)
    {
        if (_count < MaxPatients)
        {
            _patients[_count++] = patient;
            Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
        }
        else
        {
            Console.WriteLine($"Не вдалося додати пацієнта [{patient.Id}] {patient.FullName}, перевищено ліміт ({MaxPatients}).");
        }
    }

    // 2. ID
    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; ++i)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }
        return null;
    }

    // 3. ІМ'Я АБО ПРІЗВИЩЕ 
    public Patient[] FindByName(string name)
    {
        int count = 0;
        for (int i = 0; i < _count; ++i)
        {
            if (_patients[i].FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                _patients[i].LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
            {
                count++;
            }
        }

        Patient[] result = new Patient[count];
        int index = 0;

        for (int i = 0; i < _count; ++i)
        {
            if (_patients[i].FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                _patients[i].LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
            {
                result[index++] = _patients[i];
            }
        }

        return result;
    }

    // 4. ВИДАЛЕННЯ ID
    public bool Remove(int id)
    {
        for (int i = 0; i < _count; ++i)
        {
            if (_patients[i].Id == id)
            {
                for (int j = i; j < _count - 1; ++j)
                {
                    _patients[j] = _patients[j + 1];
                }
                _count--;
                _patients[_count] = null;
                return true;
            }
        }
        return false;
    }

    // 5. ВИВІД УСІХ
    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
        }
        else
        {
            Console.WriteLine($"=== Пацієнти ({_count} / {MaxPatients}) ===");
            for (int i = 0; i < _count; ++i)
            {
                Console.WriteLine(_patients[i]);
            }
        }
    }

    // 6. СТАТИСТИКА
    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Немає даних для статистики.");
            return;
        }

        double sumAge = 0;
        int smallestOneIdx =  0;
        int biggestOneIdx = 0;
        int howManyAdults = 0;

        for (int i = 0; i < _count; ++i)
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

        double averageAge = sumAge / _count;

        Console.WriteLine("\n=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього: {_count}");
        Console.WriteLine($"Середній вік: {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший: {_patients[smallestOneIdx].FullName} ({_patients[smallestOneIdx].Age} р.)");
        Console.WriteLine($"Найстарший: {_patients[biggestOneIdx].FullName} ({_patients[biggestOneIdx].Age} р.)");
        Console.WriteLine($"Дорослих: {howManyAdults} з {_count}");
    }
}