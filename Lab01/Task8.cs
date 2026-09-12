namespace Lab01;

public static class Task8
{
    public static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height);
    }

    public static string GetBMICategory(double BMI)
    {
        if (BMI < 18.5)
        {
            return "недостатня вага";
        }
        else if (BMI < 25.0)
        {
            return "норма";
        }
        else if (BMI < 30.0)
        {
            return "надмірна вага";
        }
        else
        {
            return "ожиріння";
        }
    }

    public static double CalculateCost(double price, int count, int discount)
    {
        return price * count * (1 - discount / 100.0);
    }

    public static string GetAgeCategory(int age)
    {
        return age switch
        {
            < 18 => "дитина",
            < 60 => "дорослий",
            _ => "пенсіонер"
        };
    }

    public static string GetPressureStatus(int systolicBP, int diastolicBP)
    {
        if (systolicBP >= 140 || diastolicBP >= 90)
        {
            return "гіпертонія 2 ступеня";
        }
        else if (systolicBP >= 130 || diastolicBP >= 80)
        {
            return "гіпертонія 1 ступеня";
        }
        else if (systolicBP >= 120 && diastolicBP < 80)
        {
            return "підвищений";
        }
        else
        {
            return "норма";
        }
    }

    public static void Run()
    {
        double weight = double.Parse(Console.ReadLine()!);
        double height = double.Parse(Console.ReadLine()!);

        double visitPrice = double.Parse(Console.ReadLine()!);
        int totalVisits = int.Parse(Console.ReadLine()!);
        int discount = int.Parse(Console.ReadLine()!);

        int birthdayYear = int.Parse(Console.ReadLine()!);

        int systolicBP = int.Parse(Console.ReadLine()!);
        int diastolicBP = int.Parse(Console.ReadLine()!);

        double BMI = CalculateBMI(weight, height);
        string BMICategory = GetBMICategory(BMI);

        double totalCost = CalculateCost(visitPrice, totalVisits, discount);


        int age = 2026 - birthdayYear;
        string ageCategory = GetAgeCategory(age);

        string pressureStatus = GetPressureStatus(systolicBP, diastolicBP);

        Console.WriteLine($"ІМТ: {BMI:F2} -> {BMICategory}\n" +
                          $"Сума: {totalCost:F2} грн\n" +
                          $"Вік: {2026 - birthdayYear} р., категорія: {ageCategory}\n" +
                          $"Тиск: {systolicBP}/{diastolicBP} — {pressureStatus}");
    }
}