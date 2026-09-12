namespace Lab01;

public static class Task4
{
    public static void Run()
    {
        int systolicBP = int.Parse(Console.ReadLine()!);
        int diastolicBP = int.Parse(Console.ReadLine()!);

        if (systolicBP >= 140 || diastolicBP >= 90)
        {
            Console.WriteLine($"Тиск: {systolicBP}/{diastolicBP} — гіпертонія 2 ступеня");
        }
        else if (systolicBP >= 130 || diastolicBP >= 80)
        {
            Console.WriteLine($"Тиск: {systolicBP}/{diastolicBP} — гіпертонія 1 ступеня");
        }
        else if (systolicBP >= 120 && diastolicBP < 80)
        {
            Console.WriteLine($"Тиск: {systolicBP}/{diastolicBP} — підвищений");
        }
        else
        {
            Console.WriteLine($"Тиск: {systolicBP}/{diastolicBP} — норма");
        }
    }
}