namespace Lab01;

public static class Task6
{
    public static void Run()
    {
        int medCard = int.Parse(Console.ReadLine()!);
        int rest = medCard % 10;

        string value = rest switch
        {
            0 or 1 => "загальна терапія",
            2 or 3 => "хірургія",
            4 or 5 => "кардіологія",
            6 or 7 => "неврологія",
            8 or 9 => "офтальмологія",
            _ => "невідоме відділення"
        };

        string privilege = (medCard % 2 == 0) ? "так" : "ні";
        string checkup = (medCard % 3 == 0) ? "так" : "ні";

        Console.WriteLine($"Відділення: {value}\n" +
            $"Пільгова:   {privilege}\n" +
            $"Огляд:      {checkup}");
    }
}