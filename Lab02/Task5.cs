using System.Reflection;

namespace Lab02
{
    internal class Task5
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] row = Console.ReadLine()!.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(row[j]);
                }
            }

            int[] main = new int[n];
            int[] side = new int[n];
            int mSum = 0;
            int sSum = 0;

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == j)
                    {
                        main[i] = matrix[i, j];
                        mSum += matrix[i, j];
                    }
                    if (i + j == n - 1)
                    {
                        side[i] = matrix[i, j];
                        sSum += matrix[i, j];
                    }
                }
            }
            Console.WriteLine($"Головна діагональ: {string.Join(", ", main)} (сума = {mSum})");
            Console.WriteLine($"Побічна діагональ: {string.Join(", ", side)} (сума = {sSum})");
        }
    }
}
