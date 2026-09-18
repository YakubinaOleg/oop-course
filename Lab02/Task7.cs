using System.Globalization;

namespace Lab02
{
    public static class Task7
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);

            string[] name = new string[n];
            double[] bmi = new double[n];

            for (int i = 0; i < n; i++)
            {
                name[i] = Console.ReadLine()!;
                bmi[i] = double.Parse(Console.ReadLine()!);
            }

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (bmi[j] < bmi[j + 1])
                    {
                        double tempB = bmi[j];
                        bmi[j] = bmi[j + 1];
                        bmi[j + 1] = tempB;

                        string tempN = name[j];
                        name[j] = name[j + 1];
                        name[j + 1] = tempN;
                    }
                }
            }

            Console.WriteLine("=== Рейтинг ІМТ ===");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"#{i + 1} {name[i]}: {bmi[i].ToString("F2", CultureInfo.InvariantCulture)}");
            }
        }
    }
}
