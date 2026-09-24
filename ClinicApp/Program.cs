using ClinicApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

DoctorManager doctorManager = new DoctorManager();

// Початкові тестові дані для перевірки (із завдання)
doctorManager.Add(new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567", 8, 16));
doctorManager.Add(new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678", 9, 18));
doctorManager.Add(new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789", 8, 17));

bool exit = false;

while (!exit)
{
    Console.WriteLine("\n=== МЕНЮ «ЛІКАРІ» ===");
    Console.WriteLine("1. Показати всіх лікарів");
    Console.WriteLine("2. Додати лікаря");
    Console.WriteLine("3. Знайти лікаря за спеціальністю");
    Console.WriteLine("4. Видалити лікаря за ID");
    Console.WriteLine("5. Статистика");
    Console.WriteLine("0. Вихід");
    Console.Write("Оберіть опцію: ");

    string inputChoice = Console.ReadLine();
    string choice = inputChoice != null ? inputChoice : "";
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            doctorManager.DisplayAll();
            break;

        case "2":
            Console.WriteLine("--- Додавання нового лікаря ---");
            Console.Write("Ім'я: ");
            string inputFn = Console.ReadLine();
            string fn = inputFn != null ? inputFn : "";

            Console.Write("Прізвище: ");
            string inputLn = Console.ReadLine();
            string ln = inputLn != null ? inputLn : "";

            Console.Write("Спеціальність: ");
            string inputSpec = Console.ReadLine();
            string spec = inputSpec != null ? inputSpec : "";

            Console.Write("Номер ліцензії: ");
            string inputLic = Console.ReadLine();
            string lic = inputLic != null && inputLic != "" ? inputLic : "LIC-000";

            Console.Write("Телефон: ");
            string inputPhone = Console.ReadLine();
            string phone = inputPhone != null && inputPhone != "" ? inputPhone : "0000000000";

            Console.Write("Година початку роботи (0-23) [за замовчуванням 8]: ");
            string startInput = Console.ReadLine();
            int startHour = 8;
            if (startInput != null)
            {
                int.TryParse(startInput, out startHour);
            }

            Console.Write("Година кінця роботи (0-23) [за замовчуванням 17]: ");
            string endInput = Console.ReadLine();
            int endHour = 17;
            if (endInput != null)
            {
                int.TryParse(endInput, out endHour);
            }

            Doctor newDoctor = new Doctor(fn, ln, spec, lic, phone, startHour, endHour);
            doctorManager.Add(newDoctor);
            break;

        case "3":
            Console.Write("Введіть спеціальність для пошуку: ");
            string inputQuery = Console.ReadLine();
            string querySpec = inputQuery != null ? inputQuery : "";
            Doctor[] docs = doctorManager.FindBySpeciality(querySpec);

            if (docs.Length == 0)
            {
                Console.WriteLine("Лікарів такої спеціальності не знайдено.");
            }
            else
            {
                Console.WriteLine($"Знайдено лікарів ({docs.Length}):");
                for (int i = 0; i < docs.Length; i++)
                {
                    Console.WriteLine(docs[i]);
                }
            }
            break;

        case "4":
            Console.Write("Введіть ID лікаря для видалення: ");
            string removeInput = Console.ReadLine();
            int removeId;
            if (removeInput != null && int.TryParse(removeInput, out removeId))
            {
                if (doctorManager.Remove(removeId))
                {
                    Console.WriteLine($"Лікаря з ID {removeId} успішно видалено.");
                }
                else
                {
                    Console.WriteLine($"Лікаря з ID {removeId} не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
            break;

        case "5":
            doctorManager.DisplayStats();
            break;

        case "0":
            exit = true;
            Console.WriteLine("Роботу з меню лікарів завершено.");
            break;

        default:
            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            break;
    }
}