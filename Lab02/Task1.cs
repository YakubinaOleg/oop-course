using System.Runtime.InteropServices;

namespace Lab02;

public static class Task1
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        double[] patients = new double[n];
        for (int i = 0; i < n; ++i)
        {
            patients[i] = double.Parse(Console.ReadLine()!);
        }
        double min = patients[0];
        double max = patients[0];
        double sum = 0;
        foreach (double i in patients)
        {
            if (i < min)
            {
                min = i;
            }
            if (i > max) 
            {
                max = i;
            }
            sum += i;
        }


        int total = 0;
        double average = sum / n;
        foreach (double i in patients)
        {
            if (i > average)
            {
                total += 1;
            }
        }
        Console.WriteLine($"Кількість: {n} / Середня вага: {average:F1} кг / Мін / Макс: {min:F1} / {max:F1} кг / Вище середнього: {total} з {n}");

    }
}