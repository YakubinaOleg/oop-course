using ClinicApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

PatientManager manager = new PatientManager();

Console.WriteLine("=== 1. ПЕРЕВІРКА ПОРОЖНЬОГО МЕНЕДЖЕРА ===");
manager.DisplayAll();
manager.DisplayStats();
Console.WriteLine();

Console.WriteLine("=== 2. ДОДАВАННЯ ПАЦІЄНТІВ ===");
Patient p1 = new Patient("Іван", "Петренко", new DateTime(1983, 5, 14), "A+", "0501234567", "ivan@gmail.com");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1991, 11, 30), "B-", "0672345678", "");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 3, 15), "0+", "0933456789", "");
Patient p4 = new Patient();
Patient p5 = new Patient("Марія", "Ткач");

manager.Add(p1);
manager.Add(p2);
manager.Add(p3);
manager.Add(p4);
manager.Add(p5);
Console.WriteLine();

Console.WriteLine("=== 3. ПОШУК ЗА ID ===");
int searchId = 2;
Patient? foundById = manager.FindById(searchId);
if (foundById != null)
{
    Console.WriteLine($"Знайдено пацієнта з ID {searchId}: {foundById}");
}
else
{
    Console.WriteLine($"Пацієнта з ID {searchId} не знайдено.");
}
Console.WriteLine();

Console.WriteLine("=== 4. ПОШУК ЗА ІМ'ЯМ / ПРІЗВИЩЕМ ('ан') ===");
Patient[] foundByName = manager.FindByName("ан");
Console.WriteLine($"Знайдено елементів: {foundByName.Length}");
foreach (var p in foundByName)
{
    Console.WriteLine($" -> {p}");
}
Console.WriteLine();

Console.WriteLine("=== 5. ВИВІД СПИСКУ ТА СТАТИСТИКИ ===");
manager.DisplayAll();
manager.DisplayStats();
Console.WriteLine();

Console.WriteLine("=== 6. ВИДАЛЕННЯ ПАЦІЄНТА (ID = 3) ===");
bool removed = manager.Remove(3);
Console.WriteLine($"Результат видалення ID 3: {removed}");
Console.WriteLine();

Console.WriteLine("=== 7. СПИСОК ПІСЛЯ ВИДАЛЕННЯ ===");
manager.DisplayAll();
manager.DisplayStats();