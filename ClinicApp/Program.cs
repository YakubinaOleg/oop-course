using ClinicApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Тест GrowablePatientManager ===");
Console.WriteLine("Додаємо пацієнтів одного за одним...");

GrowablePatientManager growableManager = new GrowablePatientManager();

for (int i = 1; i <= 20; i++)
{
    Patient p = new Patient($"Тест{i}", $"Пацієнт{i}", DateTime.Now.AddYears(-20), "O+", "+380000000000", "test@mail.com");
    growableManager.Add(p);
    Console.WriteLine($"Додано [{p.Id}]. Розмір: {growableManager.Count} / {growableManager.Capacity}");
}

Console.WriteLine("\nТест пошуку:");
Patient p10 = growableManager.FindById(10);
Console.WriteLine($"FindById(10) -> {(p10 != null ? p10.FullName : "не знайдено")}");

Patient p99 = growableManager.FindById(99);
Console.WriteLine($"FindById(99) -> {(p99 != null ? p99.FullName : "не знайдено")}");

Console.WriteLine("\nПорівняння:");
Console.WriteLine($"PatientManager:          100 місць (фіксовано)");
Console.WriteLine($"GrowablePatientManager:  {growableManager.Capacity} місця (зросте при потребі)");