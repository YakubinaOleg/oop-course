using System;

namespace ClinicApp
{
    public class GrowablePatientManager
    {
        private Patient[] _patients;
        private int _count;

        public int Count => _count;
        public int Capacity => _patients.Length; 
        public GrowablePatientManager()
        {
            _patients = new Patient[4];
            _count = 0;
        }

        private void Resize()
        {
            int oldSize = _patients.Length;
            int newSize = _patients.Length * 2;

            Patient[] newArr = new Patient[newSize];
            for (int i = 0; i < _count; i++)
            {
                newArr[i] = _patients[i];
            }

            _patients = newArr;
            Console.WriteLine($"Масив заповнений! Розширення: {oldSize} -> {newSize}");
        }

        // 5. Додати
        public void Add(Patient patient)
        {
            if (patient == null)
            {
                return;
            }
            if (_count == _patients.Length)
            {
                Resize();
            }
            _patients[_count++] = patient;
        }

        public Patient FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    return _patients[i];
                }
            }
            return null;
        }

        // видалити
        public bool Remove(int id)
        {
            for (int i = 0; i < _count;i++)
            {
                if ( _patients[i].Id == id)
                {
                    for (int j = i; j < _count - 1; j++)
                    {
                        _patients[j] = _patients[j + 1];
                    }
                    _patients[_count - 1] = null;
                    _count--;
                    return true;
                }
            }
            return false;
        }

        public void DisplayAll()
        {
            Console.WriteLine($"\n=== Список пацієнтів (Всього: {_count}, Ємність: {Capacity}) ===");
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine($"[{_patients[i].Id}] {_patients[i].FullName} | {_patients[i].Phone}");
            }
        }
    }
}