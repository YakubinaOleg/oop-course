namespace Lab02
{
    public static class Task4
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int m = int.Parse(Console.ReadLine()!);

            int[,] matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                string[] values = Console.ReadLine()!.Split(' ');
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }

            int k = 0;
            int l = 0;
            int sum = 0;
            string by_days = "";

            for (int i = 0; i < n; i++)
            {
                sum = 0;
                for (int j = 0; j < m; j++)
                {
                    sum += matrix[i, j];

                    if (matrix[i, j] > matrix[k, l])
                    {
                        k = i;
                        l = j;
                    }
                }
                Console.WriteLine($"Лікар {i + 1}: {sum} прийомів");
            }

            int[] sum_1 = new int[m];
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    sum_1[j] += matrix[i, j];
                }
            }

            Console.WriteLine($"По днях: {string.Join(", ", sum_1)}");
            Console.WriteLine($"Максимум: {matrix[k, l]} (Лікар {k + 1}, День {l + 1})");
        }
    }
}