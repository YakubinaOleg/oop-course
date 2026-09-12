namespace Lab01;

public static class Task2
{
    public static void Run()
    {
        double visitPrice = double.Parse(Console.ReadLine()!);
        int totalVisits = int.Parse(Console.ReadLine()!);
        int discount = int.Parse(Console.ReadLine()!);

        double formula = visitPrice * totalVisits * (1 - discount / 100.0);

        Console.WriteLine($"{formula:F2}");
    }
}