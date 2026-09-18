namespace Lab02;

public static class Task3
{
    public static void Run()
    {
        string[] days = {"Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя"};
        int[] patients = new int[7];

        int total = 0;
        int maxIdx = 0;
        int minIdx = 0;

        for (int i = 0; i < 7; i++)
        {
            patients[i] = int.Parse(Console.ReadLine()!);
            total += patients[i];

            if (patients[i] > patients[maxIdx]) maxIdx = i;
            if (patients[i] < patients[minIdx]) minIdx = i;
        }

        Console.WriteLine(
        $"{days[0] + "  :"} {patients[0]} пацієнтів\n" +
        $"{days[1] + "   :"} {patients[1]} пацієнтів\n" +
        $"{days[2] + "     :"} {patients[2]} пацієнтів\n" +
        $"{days[3] + "     :"} {patients[3]} пацієнтів\n" +
        $"{days[4] + "   :"} {patients[4]} пацієнтів\n" +
        $"{days[5] + "     :"} {patients[5]} пацієнтів\n" +
        $"{days[6] + "     :"} {patients[6]} пацієнтів\n" +
        $"Разом:      {total}\n" +
        $"Найбільше:  {days[maxIdx]} ({patients[maxIdx]})\n" +
        $"Найменше:   {days[minIdx]} ({patients[minIdx]})"
        );
    }
}