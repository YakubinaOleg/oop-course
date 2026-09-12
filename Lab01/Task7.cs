namespace Lab01;

public static class Task7
{
    public static void Run()
    {
        int n = int.Parse(Console.ReadLine()!);
        decimal[] prices = new decimal[n];

        for (int i = 0; i < n; i++)
        {
            prices[i] = decimal.Parse(Console.ReadLine()!);
        }

        decimal totalSum = 0;
        decimal min = prices[0];
        decimal max = prices[0];

        foreach (decimal price in prices)
        {
            totalSum += price;
            if (price < min)
            {
                min = price;
            }
            if (price > max)
            {
                max = price;
            }
        }

        decimal average = totalSum / n;
        int aboveAverage = 0;

        for (int i = 0; i < n; i++)
        {
            if (prices[i] > average)
            { 
                aboveAverage++;
            }
        }

        int firstExpensiveIDX = -1;
        int index = 0;

        while (index < n)
        {
            if (prices[index] > 1000)
            {
                firstExpensiveIDX = index;
                break;
            }
            else
            {
                index++;
            }
        }

        string expensiveText = (firstExpensiveIDX != -1)
            ? $"#{firstExpensiveIDX + 1} — {prices[firstExpensiveIDX]:F2} грн"
            : "немає";

        Console.WriteLine("=== Звіт по прийомах ===");
        Console.WriteLine($"Кількість:        {n}");
        Console.WriteLine($"Загальна сума:    {totalSum:F2} грн");
        Console.WriteLine($"Середня:          {average:F2} грн");
        Console.WriteLine($"Мін / Макс:       {min:F2} / {max:F2} грн");
        Console.WriteLine($"Вище середнього:  {aboveAverage} з {n}");
        Console.WriteLine($"Перший > 1000:    {expensiveText}");
        Console.WriteLine("========================");
    }
}