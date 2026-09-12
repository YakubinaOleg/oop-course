namespace Lab01;

public static class Task3
{
    public static void Run()
    {
        int birthdayYear = int.Parse(Console.ReadLine()!);

        int age = 2026 - birthdayYear;

        Console.WriteLine($"Вік: {age} р.");

        if (age < 18)
        {
            Console.WriteLine("Категорія: дитина");
        }
        else if (age < 60)
        {
            Console.WriteLine("Категорія: дорослий");
        }
        else
        {
            Console.WriteLine("Категорія: пенсіонер");
        }

    }
}