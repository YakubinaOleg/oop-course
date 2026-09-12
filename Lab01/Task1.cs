namespace Lab01;

public static class Task1
{
    public static void Run()
    {
        Console.Write("Введіть вагу: ");
        double weight = double.Parse(Console.ReadLine()!);

        Console.Write("Введіть зріст: ");
        double height = double.Parse(Console.ReadLine()!);

        double body_mass_index = weight / (height * height);

        Console.WriteLine($"{body_mass_index:F2}");
    }
}