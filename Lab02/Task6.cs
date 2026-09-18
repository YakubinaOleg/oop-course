using System.Reflection;

namespace Lab02
{
    public static class Task6
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int[][] price = new int[n][];

            for (int i = 0; i < n; ++i)
            {
                int k = int.Parse(Console.ReadLine()!);
                price[i] = new int[k];
                for (int j = 0; j < k; ++j)
                {
                    price[i][j] = int.Parse(Console.ReadLine()!);
                }
            }
            int maxIdx = 0;
            int maxSum = -1;

            for (int i = 0; i < n; ++i)
            {
                int sum = 0;
                for (int j = 0; j < price[i].Length; ++j)
                {
                    sum += price[i][j];
                }
                double avg = (double)sum / price[i].Length;

                if (sum > maxSum)
                {
                    maxSum = sum;
                    maxIdx = i;
                }

                Console.WriteLine($"Лікар {i + 1}: {price[i].Length} прийоми, сума={sum} грн, середня={avg:F2} грн");
            }

            Console.WriteLine($"Найбільший дохід: Лікар {maxIdx + 1} ({maxSum} грн)");
        }
    }
}
