using System.Reflection;

namespace Lab02
{
    public static class Task5
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

            for (int i = 0; i < n; i++)
            {
                main[i] = matrix[i, i];
                side[i] = matrix[i, n - 1 - i];
                mSum += matrix[i, i];
                sSum += matrix[i, n - 1 - i];
            }

            Console.WriteLine($"Головна діагональ: {string.Join(", ", main)} (сума = {mSum})");
            Console.WriteLine($"Побічна діагональ: {string.Join(", ", side)} (сума = {sSum})");
        }
    }
}
