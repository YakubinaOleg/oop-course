using ClinicApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

PatientManager patientManager = new PatientManager();
DoctorManager doctorManager = new DoctorManager();
AppointmentManager appointmentManager = new AppointmentManager(patientManager, doctorManager);

patientManager.Add(new Patient("Олександр", "Коваленко", new DateTime(1990, 5, 14), "A+", "+380971112233", "kovalenko@gmail.com"));
patientManager.Add(new Patient("Марія", "Шевченко", new DateTime(2003, 11, 28), "O-", "+380504445566", "m.shevchenko@gmail.com"));
patientManager.Add(new Patient("Іван", "Бондаренко", new DateTime(1978, 3, 2), "B+", "+380637778899", "i.bond@gmail.com"));

doctorManager.Add(new Doctor("Андрій", "Мельник", "Кардіолог", "LIC-1001", "+380671110001", 8, 16));
doctorManager.Add(new Doctor("Олена", "Ткаченко", "Терапевт", "LIC-1002", "+380671110002", 9, 17));
doctorManager.Add(new Doctor("Сергій", "Кравченко", "Хірург", "LIC-1003", "+380671110003", 10, 18));

appointmentManager.Book(1, 1, DateTime.Now.AddDays(1).AddHours(2), 30);
appointmentManager.Book(2, 2, DateTime.Now.AddDays(2).AddHours(4), 45);
appointmentManager.Book(3, 3, DateTime.Now.AddDays(3).AddHours(1), 60);

ShowMainMenu(patientManager, doctorManager, appointmentManager);

static void ShowMainMenu(
    PatientManager patientManager,
    DoctorManager doctorManager,
    AppointmentManager appointmentManager)
{
    while (true)
    {
        Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ КЛІНІКИ ===");
        Console.WriteLine("1. Управління пацієнтами");
        Console.WriteLine("2. Управління лікарями");
        Console.WriteLine("3. Управління записами");
        Console.WriteLine("0. Вихід з програми");
        Console.Write("Оберіть розділ: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowPatientMenu(patientManager);
                break;

            case "2":
                ShowDoctorMenu(doctorManager);
                break;

            case "3":
                ShowAppointmentMenu(appointmentManager, patientManager, doctorManager);
                break;

            case "0":
                Console.WriteLine("Завершення роботи програми...");
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowPatientMenu(PatientManager patientManager)
{
    while (true)
    {
        Console.WriteLine("\n--- Управління пацієнтами ---");
        Console.WriteLine("1. Показати всіх пацієнтів");
        Console.WriteLine("2. Додати нового пацієнта");
        Console.WriteLine("3. Знайти пацієнта за ID");
        Console.WriteLine("4. Знайти пацієнта за ім'ям / прізвищем");
        Console.WriteLine("5. Видалити пацієнта за ID");
        Console.WriteLine("6. Переглянути статистику пацієнтів");
        Console.WriteLine("0. Повернутися в головне меню");
        Console.Write("Оберіть дію: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                patientManager.DisplayAll();
                break;

            case "2":
                Console.Write("Введіть ім'я: ");
                string firstName = Console.ReadLine();

                Console.Write("Введіть прізвище: ");
                string lastName = Console.ReadLine();

                Console.Write("Введіть дату народження (РРРР-ММ-ДД): ");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Введіть групу крові (наприклад, A+, O-): ");
                string bloodType = Console.ReadLine();

                Console.Write("Введіть номер телефону: ");
                string phone = Console.ReadLine();

                Console.Write("Введіть email: ");
                string email = Console.ReadLine();

                // Усі 6 параметрів строго за конструктором
                Patient newPatient = new Patient(firstName, lastName, birthDate, bloodType, phone, email);
                patientManager.Add(newPatient);
                break;

            case "3":
                Console.Write("Введіть ID пацієнта: ");
                int id = int.Parse(Console.ReadLine());
                Patient p = patientManager.FindById(id);
                if (p != null)
                {
                    Console.WriteLine($"Знайдено: {p}");
                }
                else
                {
                    Console.WriteLine($"Пацієнта з ID {id} не знайдено.");
                }
                break;

            case "4":
                Console.Write("Введіть ім'я або прізвище для пошуку: ");
                string name = Console.ReadLine();
                Patient[] found = patientManager.FindByName(name);
                if (found.Length == 0)
                {
                    Console.WriteLine("Пацієнтів не знайдено.");
                }
                else
                {
                    Console.WriteLine($"\nЗнайдено пацієнтів: {found.Length}");
                    for (int i = 0; i < found.Length; i++)
                    {
                        Console.WriteLine(found[i]);
                    }
                }
                break;

            case "5":
                Console.Write("Введіть ID пацієнта для видалення: ");
                int removeId = int.Parse(Console.ReadLine());
                if (patientManager.Remove(removeId))
                {
                    Console.WriteLine($"Пацієнта з ID {removeId} успішно видалено.");
                }
                else
                {
                    Console.WriteLine($"Не вдалося видалити: пацієнта з ID {removeId} не знайдено.");
                }
                break;

            case "6":
                patientManager.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowDoctorMenu(DoctorManager doctorManager)
{
    while (true)
    {
        Console.WriteLine("\n--- Управління лікарями ---");
        Console.WriteLine("1. Показати всіх лікарів");
        Console.WriteLine("2. Додати нового лікаря");
        Console.WriteLine("3. Знайти лікаря за ID");
        Console.WriteLine("4. Знайти лікарів за спеціальністю");
        Console.WriteLine("5. Видалити лікаря за ID");
        Console.WriteLine("6. Переглянути статистику лікарів");
        Console.WriteLine("0. Повернутися в головне меню");
        Console.Write("Оберіть дію: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                doctorManager.DisplayAll();
                break;

            case "2":
                Console.Write("Введіть ім'я: ");
                string firstName = Console.ReadLine();

                Console.Write("Введіть прізвище: ");
                string lastName = Console.ReadLine();

                Console.Write("Введіть спеціальність: ");
                string speciality = Console.ReadLine();

                Console.Write("Введіть номер ліцензії: ");
                string licenseNumber = Console.ReadLine();

                Console.Write("Введіть номер телефону: ");
                string phone = Console.ReadLine();

                Console.Write("Початок робочого дня (година, за замовчуванням 8): ");
                string startInput = Console.ReadLine();
                int startHour = string.IsNullOrWhiteSpace(startInput) ? 8 : int.Parse(startInput);

                Console.Write("Кінець робочого дня (година, за замовчуванням 17): ");
                string endInput = Console.ReadLine();
                int endHour = string.IsNullOrWhiteSpace(endInput) ? 17 : int.Parse(endInput);

                // Виклики згідно з конструктором
                Doctor newDoctor = new Doctor(firstName, lastName, speciality, licenseNumber, phone, startHour, endHour);
                doctorManager.Add(newDoctor);
                break;

            case "3":
                Console.Write("Введіть ID лікаря: ");
                int id = int.Parse(Console.ReadLine());
                Doctor doc = doctorManager.FindById(id);
                if (doc != null)
                {
                    Console.WriteLine($"Знайдено: {doc}");
                }
                else
                {
                    Console.WriteLine($"Лікаря з ID {id} не знайдено.");
                }
                break;

            case "4":
                Console.Write("Введіть спеціальність для пошуку: ");
                string spec = Console.ReadLine();
                Doctor[] found = doctorManager.FindBySpeciality(spec);
                if (found.Length == 0)
                {
                    Console.WriteLine("Лікарів за цією спеціальністю не знайдено.");
                }
                else
                {
                    Console.WriteLine($"\nЗнайдено лікарів: {found.Length}");
                    for (int i = 0; i < found.Length; i++)
                    {
                        Console.WriteLine(found[i]);
                    }
                }
                break;

            case "5":
                Console.Write("Введіть ID лікаря для видалення: ");
                int removeId = int.Parse(Console.ReadLine());
                if (doctorManager.Remove(removeId))
                {
                    Console.WriteLine($"Лікаря з ID {removeId} успішно видалено.");
                }
                else
                {
                    Console.WriteLine($"Не вдалося видалити: лікаря з ID {removeId} не знайдено.");
                }
                break;

            case "6":
                doctorManager.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowAppointmentMenu(
    AppointmentManager appointmentManager,
    PatientManager patientManager,
    DoctorManager doctorManager)
{
    while (true)
    {
        Console.WriteLine("\n--- Управління записами ---");
        Console.WriteLine("1. Створити новий запис");
        Console.WriteLine("2. Скасувати запис");
        Console.WriteLine("3. Завершити прийом");
        Console.WriteLine("4. Переглянути всі майбутні записи");
        Console.WriteLine("5. Переглянути записи за датою");
        Console.WriteLine("6. Переглянути записи пацієнта");
        Console.WriteLine("7. Переглянути записи лікаря");
        Console.WriteLine("0. Повернутися в головне меню");
        Console.Write("Оберіть дію: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // 1. Показуємо доступні списки пацієнтів та лікарів
                Console.WriteLine("\n=== Список пацієнтів ===");
                patientManager.DisplayAll();

                Console.WriteLine("\n=== Список лікарів ===");
                doctorManager.DisplayAll();

                // 2. Зчитуємо дані для запису
                Console.Write("\nВведіть ID пацієнта: ");
                int pId = int.Parse(Console.ReadLine());

                Console.Write("Введіть ID лікаря: ");
                int dId = int.Parse(Console.ReadLine());

                Console.Write("Введіть дату та час (РРРР-ММ-ДД ГГ:ХХ): ");
                DateTime dt = DateTime.Parse(Console.ReadLine());

                Console.Write("Введіть тривалість у хвилинах (за замовчуванням 30): ");
                string durInput = Console.ReadLine();
                int duration = string.IsNullOrWhiteSpace(durInput) ? 30 : int.Parse(durInput);

                appointmentManager.Book(pId, dId, dt, duration);
                break;

            case "2":
                Console.Write("Введіть ID запису для скасування: ");
                int cancelId = int.Parse(Console.ReadLine());

                Console.Write("Введіть причину скасування (опціонально): ");
                string reason = Console.ReadLine();

                appointmentManager.Cancel(cancelId, reason);
                break;

            case "3":
                Console.Write("Введіть ID запису для завершення: ");
                int completeId = int.Parse(Console.ReadLine());

                appointmentManager.Complete(completeId);
                break;

            case "4":
                Console.WriteLine("\n=== Майбутні записи ===");
                appointmentManager.DisplayList(appointmentManager.GetUpcoming());
                break;

            case "5":
                Console.Write("Введіть дату (РРРР-ММ-ДД): ");
                DateTime searchDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine($"\n=== Записи на {searchDate:dd.MM.yyyy} ===");
                appointmentManager.DisplayList(appointmentManager.GetByDate(searchDate));
                break;

            case "6":
                Console.Write("Введіть ID пацієнта: ");
                int searchPId = int.Parse(Console.ReadLine());

                Console.WriteLine($"\n=== Записи пацієнта #{searchPId} ===");
                appointmentManager.DisplayList(appointmentManager.GetByPatient(searchPId));
                break;

            case "7":
                Console.Write("Введіть ID лікаря: ");
                int searchDId = int.Parse(Console.ReadLine());

                Console.WriteLine($"\n=== Записи лікаря #{searchDId} ===");
                appointmentManager.DisplayList(appointmentManager.GetByDoctor(searchDId));
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}