using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;
using System.Globalization;
using System.Security.Cryptography;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Clinic clinic = new Clinic("Медична Клініка");



clinic.Patients.Add(new Patient("Олександр", "Коваленко", new DateTime(1990, 5, 14), BloodType.APositive, "0971112233", "kovalenko@gmail.com"));
clinic.Patients.Add(new Patient("Марія", "Шевченко", new DateTime(2003, 11, 28), BloodType.ONegative, "0504445566", "m.shevchenko@gmail.com"));
clinic.Patients.Add(new Patient("Іван", "Бондаренко", new DateTime(1978, 3, 2), BloodType.BPositive, "0637778899", "i.bond@gmail.com"));

clinic.Doctors.Add(new Doctor("Андрій", "Мельник", Speciality.Cardiology, "LIC-1001", "0671110001", new WorkSchedule(8, 16)));
clinic.Doctors.Add(new Doctor("Олена", "Ткаченко", Speciality.General, "LIC-1002", "0671110002", new WorkSchedule(9, 17)));
clinic.Doctors.Add(new Doctor("Сергій", "Кравченко", Speciality.Surgery, "LIC-1003", "0671110003", new WorkSchedule(10, 18)));

clinic.Appointments.Book(1, 1, DateTime.Now.AddDays(1).AddHours(2), 30);
clinic.Appointments.Book(2, 2, DateTime.Now.AddDays(2).AddHours(4), 45);
clinic.Appointments.Book(3, 3, DateTime.Now.AddDays(3).AddHours(1), 60);

clinic.MedicalRecords.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", isChronic: true));
clinic.MedicalRecords.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-5), "J06.9", "Гострий ринофарингіт", isChronic: false));

clinic.MedicalRecords.Add(new LabResult(1, 1, DateTime.Today.AddDays(-10), "Гемоглобін", 145.0, "г/л", "120-160", isNormal: true));
clinic.MedicalRecords.Add(new LabResult(1, 1, DateTime.Today.AddDays(-10), "Холестерин", 6.2, "ммоль/л", "< 5.2", isNormal: false));

clinic.MedicalRecords.Add(new Prescription(1, 1, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "вранці"));

clinic.MedicalRecords.Add(new Prescription(2, 1, DateTime.Today.AddDays(-40), "Ібупрофен", "400 мг", 10, "після їжі"));
clinic.MedicalRecords.Add(new Diagnosis(2, 2, DateTime.Today.AddDays(-15), "E11", "Цукровий діабет 2 типу", isChronic: true));
clinic.MedicalRecords.Add(new LabResult(2, 2, DateTime.Today.AddDays(-15), "Глюкоза", 120.0, "мг/л", "70-126", isNormal: true));

clinic.MedicalRecords.Add(new LabResult(3, 2, DateTime.Today.AddDays(-3), "Гемоглобін", 150.0, "г/л", "120-160", isNormal: true));
clinic.MedicalRecords.Add(new LabResult(3, 2, DateTime.Today.AddDays(-2), "Загальний білок", 70.0, "г/л", "64-83", isNormal: true));

ShowMainMenu(clinic);


static void ShowMainMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ КЛІНІКИ ===");
        Console.WriteLine("1. Управління пацієнтами");
        Console.WriteLine("2. Управління лікарями");
        Console.WriteLine("3. Управління записами");
        Console.WriteLine("4. Медична картка");
        Console.WriteLine("5. Рапорт клініки");
        Console.WriteLine("6. Всі записи на сьогодні");
        Console.WriteLine("0. Вихід з програми");
        Console.Write("Оберіть розділ: ");

        string choice = Console.ReadLine()!;  

        switch (choice)
        {
            case "1":
                ShowPatientMenu(clinic);
                break;

            case "2":
                ShowDoctorMenu(clinic);
                break;

            case "3":
                ShowAppointmentMenu(clinic);
                break;

            case "4":
                ShowMedicalRecordsMenu(clinic);
                break;

            case "5":
                clinic.GenerateReport();
                break;

            case "6":
                clinic.DisplaySchedule(DateTime.Today);
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

static void ShowMedicalRecordsMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n--- Медична картка ---");
        Console.WriteLine("1. Картка пацієнта");
        Console.WriteLine("2. Усі записа пацієнта");
        Console.WriteLine("3. Додати діагноз");
        Console.WriteLine("4. Додати аналіз");
        Console.WriteLine("5. Додати рецепцт");
        Console.WriteLine("6. Усі записи лікаря");
        Console.WriteLine("0. Повернутися в головне меню");
        Console.Write("Оберіть дію: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                Console.Write("Введіть id пацієнта: ");
                string input = Console.ReadLine()!;

                if (int.TryParse(input, out int id))
                {
                    var patient = clinic.Patients.FindById(id);
                    if (patient == null)
                    {
                        Console.WriteLine("Помилка: Пацієнта з таким ID не знайдено!");
                        break; 
                    }
                    clinic.MedicalRecords.DisplayPatientSummary(id);
                }
                else
                {   
                    Console.WriteLine("Помилка: введено некоректне число!");
                }
                break;

            case "2":
                clinic.Patients.DisplayAll();
                Console.Write("Введіть id пацієнта: ");
                if (int.TryParse(Console.ReadLine(), out int patientId))
                {
                    var patient = clinic.Patients.FindById(patientId);
                    if (patient == null)
                    {
                        Console.WriteLine("Помилка: Пацієнта з таким ID не знайдено!");
                        break;
                    }
                    var records = clinic.MedicalRecords.GetByPatientId(patientId);

                    if (records.Length == 0)
                    {
                        Console.WriteLine("У цього пацієнта немає записів.");
                    }
                    else
                    {
                        foreach (var record in records)
                        {
                            Console.WriteLine(record); 
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Помилка: введено некоректне число!");
                }
                break;

            case "3":
                try
                {
                    clinic.Patients.DisplayAll();
                    Console.Write("ID пацієнта: ");
                    int pId = int.Parse(Console.ReadLine()!);
                    if (clinic.Patients.FindById(pId) == null)
                    {
                        Console.WriteLine("Помилка: Пацієнта з таким ID не знайдено!");
                        break;
                    }

                    clinic.Doctors.DisplayAll();
                    Console.Write("ID доктора: ");
                    int dId = int.Parse(Console.ReadLine()!);
                    if (clinic.Doctors.FindById(dId) == null)
                    {
                        Console.WriteLine("Помилка: Лікаря з таким ID не знайдено!");
                        break;
                    }

                    Console.Write("Код діагнозу(напр. J06.9): ");
                    string code = Console.ReadLine()!;

                    Console.Write("Опис: ");
                    string description = Console.ReadLine()!;

                    Console.Write("Хронічне? (1=так, 0=ні): ");
                    int chronicInput = int.Parse(Console.ReadLine()!);
                    bool isChronic = (chronicInput == 1);

                    Diagnosis diagnosis = new Diagnosis(pId, dId, DateTime.Today, code, description, isChronic);
                    clinic.MedicalRecords.Add(diagnosis);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка: введено некоректне число!");
                }
                break;

            case "4":
                try
                {
                    clinic.Patients.DisplayAll();
                    Console.Write("ID пацієнта: ");
                    int pId = int.Parse(Console.ReadLine()!);
                    if (clinic.Patients.FindById(pId) == null)
                    {
                        Console.WriteLine("Помилка: Пацієнта з таким ID не знайдено!");
                        break;
                    }

                    clinic.Doctors.DisplayAll();
                    Console.Write("ID доктора: ");
                    int dId = int.Parse(Console.ReadLine()!);
                    if (clinic.Doctors.FindById(dId) == null)
                    {
                        Console.WriteLine("Помилка: Лікаря з таким ID не знайдено!");
                        break;
                    }

                    Console.Write("Назва аналізу: ");
                    string testName = Console.ReadLine()!;

                    Console.Write("Значення (число): ");
                    double value = double.Parse(Console.ReadLine()!, NumberStyles.Float, CultureInfo.InvariantCulture);

                    Console.Write("Одиниці виміру: ");
                    string unit = Console.ReadLine()!;

                    Console.Write("Норма (напр. 4.0-9.0): ");
                    string rRange = Console.ReadLine()!;

                    Console.Write("В нормі? (1=так, 0=ні): ");
                    int normalInput = int.Parse(Console.ReadLine()!);
                    bool isNormal = (normalInput == 1);

                    LabResult labResult = new LabResult(pId, dId, DateTime.Today, testName, value, unit, rRange, isNormal);
                    clinic.MedicalRecords.Add(labResult);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка: введено некоректне число або формат даних!");
                }
                break;

            case "5":
                try
                {
                    clinic.Patients.DisplayAll();
                    Console.Write("ID пацієнта: ");
                    int pId = int.Parse(Console.ReadLine()!);
                    if (clinic.Patients.FindById(pId) == null)
                    {
                        Console.WriteLine("Помилка: Пацієнта з таким ID не знайдено!");
                        break;
                    }

                    clinic.Doctors.DisplayAll();
                    Console.Write("ID доктора: ");
                    int dId = int.Parse(Console.ReadLine()!);
                    if (clinic.Doctors.FindById(dId) == null)
                    {
                        Console.WriteLine("Помилка: Лікаря з таким ID не знайдено!");
                        break;
                    }

                    Console.Write("Препарат: ");
                    string medication = Console.ReadLine()!;

                    Console.Write("Дозування (напр. 10 мг): ");
                    string dosage = Console.ReadLine()!;

                    Console.Write("Кількість днів (ціле): ");
                    int durationDays = int.Parse(Console.ReadLine()!);

                    Console.Write("Інструкція (Enter — пропустити): ");
                    string instructions = Console.ReadLine()!;

                    Prescription prescription = new Prescription(pId, dId, DateTime.Today, medication, dosage, durationDays, instructions);
                    clinic.MedicalRecords.Add(prescription);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Помилка: введено некоректне число!");
                }
                break;

            case "6":
                clinic.Doctors.DisplayAll();
                Console.Write("Введіть id доктора: ");
                if (int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    var doctor = clinic.Doctors.FindById(doctorId);
                    if (doctor == null)
                    {
                        Console.WriteLine("Помилка: Доктора з таким ID не знайдено!");
                        break;
                    }
                    var records = clinic.MedicalRecords.GetByDoctorId(doctorId);

                    if (records.Length == 0)
                    {
                        Console.WriteLine("У цього доктора немає записів.");
                    }
                    else
                    {
                        foreach (var record in records)
                        {
                            Console.WriteLine(record);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Помилка: введено некоректне число!");
                }
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowPatientMenu(Clinic clinic)
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

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                break;

            case "2":
                try
                {
                    Console.Write("Введіть ім'я: ");
                    string firstName = Console.ReadLine()!;

                    Console.Write("Введіть прізвище: ");
                    string lastName = Console.ReadLine()!;

                    Console.Write("Введіть дату народження (РРРР-ММ-ДД): ");
                    DateTime birthDate = DateTime.Parse(Console.ReadLine()!);

                    Console.WriteLine("Оберіть групу крові:");
                    Console.WriteLine("0 - Unknown, 1 - APositive, 2 - ANegative, 3 - BPositive, 4 - BNegative, 5 - ABPositive, 6 - ABNegative, 7 - OPositive, 8 - ONegative");
                    Console.Write("Введіть номер групи крові (0-8): ");
                    int bloodChoice = int.Parse(Console.ReadLine()!);

                    BloodType bloodType = (BloodType)bloodChoice;

                    Console.Write("Введіть номер телефону: ");
                    string phone = Console.ReadLine()!;

                    Console.Write("Введіть email: ");
                    string email = Console.ReadLine()!;

                    Patient newPatient = new Patient(firstName, lastName, birthDate, bloodType, phone, email);
                    clinic.Patients.Add(newPatient);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"[Помилка діапазону]: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка валідації]: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("[Помилка формату]: Введено некоректне число або дату!");
                }
                break;

            case "3":
                Console.Write("Введіть ID пацієнта: ");
                int id = int.Parse(Console.ReadLine()!);
                Patient? p = clinic.Patients.FindById(id);
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
                string name = Console.ReadLine()!;
                Patient[] found = clinic.Patients.FindByName(name);
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
                int removeId = int.Parse(Console.ReadLine()!);
                if (clinic.Patients.RemoveById(removeId))
                {
                    Console.WriteLine($"Пацієнта з ID {removeId} успішно видалено.");
                }
                else
                {
                    Console.WriteLine($"Не вдалося видалити: пацієнта з ID {removeId} не знайдено.");
                }
                break;

            case "6":
                clinic.Patients.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowDoctorMenu(Clinic clinic)
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

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Doctors.DisplayAll();
                break;

            case "2":
                try
                {
                    Console.Write("Введіть ім'я: ");
                    string firstName = Console.ReadLine()!;

                    Console.Write("Введіть прізвище: ");
                    string lastName = Console.ReadLine()!;

                    Console.WriteLine("Оберіть спеціальність:");
                    Console.WriteLine("0 - General, 1 - Cardiology, 2 - Neurology, 3 - Pediatrics, 4 - Surgery, 5 - Orthopedics, 6 - Dermatology, 7 - Emergency");
                    Console.Write("Введіть номер спеціальності (0-7): ");
                    int specChoice = int.Parse(Console.ReadLine()!);

                    Speciality speciality = (Speciality)specChoice;

                    Console.Write("Введіть номер ліцензії: ");
                    string licenseNumber = Console.ReadLine()!;

                    Console.Write("Введіть номер телефону: ");
                    string phone = Console.ReadLine()!;

                    Console.Write("Початок робочого дня (година, за замовчуванням 8): ");
                    string startInput = Console.ReadLine()!;
                    int startHour = string.IsNullOrWhiteSpace(startInput) ? 8 : int.Parse(startInput);

                    Console.Write("Кінець робочого дня (година, за замовчуванням 17): ");
                    string endInput = Console.ReadLine()!;
                    int endHour = string.IsNullOrWhiteSpace(endInput) ? 17 : int.Parse(endInput);

                    Doctor newDoctor = new Doctor(firstName, lastName, speciality, licenseNumber, phone, new WorkSchedule(startHour, endHour));
                    clinic.Doctors.Add(newDoctor);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"[Помилка діапазону]: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка валідації]: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("[Помилка формату]: Введено некоректне число або дату!");
                }
                break;

            case "3":
                Console.Write("Введіть ID лікаря: ");
                int id = int.Parse(Console.ReadLine()!);
                Doctor? doc = clinic.Doctors.FindById(id);
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
                Console.Write("Введіть спеціальність (General, Cardiology, Surgery...): ");
                string inputSpec = Console.ReadLine()!;

                if (Enum.TryParse<Speciality>(inputSpec, true, out Speciality searchSpec))
                {
                    Doctor[] found = clinic.Doctors.FindBySpeciality(searchSpec);
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
                }
                else
                {
                    Console.WriteLine("Такої спеціальності не існує.");
                }
                break;

            case "5":
                Console.Write("Введіть ID лікаря для видалення: ");
                int removeId = int.Parse(Console.ReadLine()!);
                if (clinic.Doctors.Remove(removeId))
                {
                    Console.WriteLine($"Лікаря з ID {removeId} успішно видалено.");
                }
                else
                {
                    Console.WriteLine($"Не вдалося видалити: лікаря з ID {removeId} не знайдено.");
                }
                break;

            case "6":
                clinic.Doctors.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowAppointmentMenu(Clinic clinic)
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

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                try
                {
                    Console.WriteLine("\n=== Список пацієнтів ===");
                    clinic.Patients.DisplayAll();

                    Console.WriteLine("\n=== Список лікарів ===");
                    clinic.Doctors.DisplayAll();

                    Console.Write("\nВведіть ID пацієнта: ");
                    int pId = int.Parse(Console.ReadLine()!);

                    Console.Write("Введіть ID лікаря: ");
                    int dId = int.Parse(Console.ReadLine()!);

                    Console.Write("Введіть дату та час (РРРР-ММ-ДД ГГ:ХХ): ");
                    DateTime dt = DateTime.Parse(Console.ReadLine()!);

                    Console.Write("Введіть тривалість у хвилинах (за замовчуванням 30): ");
                    string durInput = Console.ReadLine()!;
                    int duration = string.IsNullOrWhiteSpace(durInput) ? 30 : int.Parse(durInput);

                    clinic.Appointments.Book(pId, dId, dt, duration);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"[Помилка діапазону]: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка валідації]: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("[Помилка формату]: Введено некоректне число або дату!");
                }
                break;

            case "2":
                Console.Write("Введіть ID запису для скасування: ");
                int cancelId = int.Parse(Console.ReadLine()!);

                Console.Write("Введіть причину скасування (опціонально): ");
                string reason = Console.ReadLine()!;

                clinic.Appointments.Cancel(cancelId, reason);
                break;

            case "3":
                Console.Write("Введіть ID запису для завершення: ");
                int completeId = int.Parse(Console.ReadLine()!);

                clinic.Appointments.Complete(completeId);
                break;

            case "4":
                Console.WriteLine("\n=== Майбутні записи ===");
                clinic.Appointments.DisplayList(clinic.Appointments.GetUpcoming());
                break;

            case "5":
                Console.Write("Введіть дату (РРРР-ММ-ДД): ");
                DateTime searchDate = DateTime.Parse(Console.ReadLine()!);

                Console.WriteLine($"\n=== Записи на {searchDate:dd.MM.yyyy} ===");
                clinic.Appointments.DisplayList(clinic.Appointments.GetByDate(searchDate));
                break;

            case "6":
                Console.Write("Введіть ID пацієнта: ");
                int searchPId = int.Parse(Console.ReadLine()!);

                Console.WriteLine($"\n=== Записи пацієнта #{searchPId} ===");
                clinic.Appointments.DisplayList(clinic.Appointments.GetByPatient(searchPId));
                break;

            case "7":
                Console.Write("Введіть ID лікаря: ");
                int searchDId = int.Parse(Console.ReadLine()!);

                Console.WriteLine($"\n=== Записи лікаря #{searchDId} ===");
                clinic.Appointments.DisplayList(clinic.Appointments.GetByDoctor(searchDId));
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

