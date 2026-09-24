namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count => _count;

    // 1. ДОДАВАННЯ
    public void Add(Doctor doctor)
    {
        if (_count < MaxDoctors)
        {
            _doctors[_count++] = doctor;
            Console.WriteLine($"Лікаря [{doctor.Id}] {doctor.FullName} додано.");
        }
        else
        {
            Console.WriteLine($"Не вдалося додати лікаря, ліміт ({MaxDoctors}) перевищено.");
        }
    }

    // 2. ID
    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
                return _doctors[i];
        }
        return null;
    }

    // 3. GetAll
    public Doctor[] GetAll()
    {
        Doctor[] doctors_copy = new Doctor[_count]; 

        for (int i = 0; i < _count;i++)
        {
            doctors_copy[i] = _doctors[i];
        }
        return doctors_copy;
    }

    // 4. ПОШУК ЗА СПЕЦІАЛЬНІСТЮ
    public Doctor[] FindBySpeciality(string speciality)
    {
        int count = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.Contains(speciality, StringComparison.OrdinalIgnoreCase))
            {
                count++;
            }
        }

        Doctor[] DoctorSpeciality = new Doctor[count];

        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.Contains(speciality, StringComparison.OrdinalIgnoreCase))
            {
                DoctorSpeciality[index++] = _doctors[i];
            }
        }
        return DoctorSpeciality;
    }

    // 5. ВИДАЛЕННЯ ЗА ID
    public bool Remove(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                for (int j = i; j < _count - 1; j++)
                {
                    _doctors[j] = _doctors[j + 1];
                }
                _count--;
                _doctors[_count] = null;
                return true;
            }
        }
        return false;
    }

    // 6. ВИВІД УСІХ
    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список лікарів порожній.");
            return;
        }

        Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i]);
        }
    }

    // 7. СТАТИСТИКА
    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Немає даних для статистики.");
            return;
        }

        int availableCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine("\n=== Статистика лікарів ===");
        Console.WriteLine($"Всього:          {_count}");
        Console.WriteLine($"Доступні зараз:  {availableCount}");
        Console.WriteLine("По спеціальностях:");

        for (int i = 0; i < _count; i++)
        {
            string currentSpec = _doctors[i].Speciality;

            bool alreadySeen = false;
            for (int j = 0; j < i; j++)
            {
                if (_doctors[j].Speciality.Equals(currentSpec, StringComparison.OrdinalIgnoreCase))
                {
                    alreadySeen = true;
                    break;
                }
            }

            if (!alreadySeen)
            {
                int specDoctorCount = 0;
                for (int k = 0; k < _count; k++)
                {
                    if (_doctors[k].Speciality.Equals(currentSpec, StringComparison.OrdinalIgnoreCase))
                    {
                        specDoctorCount++;
                    }
                }

                Console.WriteLine($"  {currentSpec}: {specDoctorCount}");
            }
        }
    }
}