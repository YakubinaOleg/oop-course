namespace Lab02
{
    public static class Task8
    {
        public static void Run()
        {
            int d = int.Parse(Console.ReadLine()!);
            int w = int.Parse(Console.ReadLine()!);

            int[,,] data = new int[d, w, 2];

            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    data[i, j, 0] = int.Parse(Console.ReadLine()!);
                    data[i, j, 1] = int.Parse(Console.ReadLine()!);
                }
            }

            int[] totals = new int[d];

            for (int i = 0; i < d; i++)
            {
                Console.WriteLine($"Відділення {i + 1}:");

                for (int j = 0; j < w; j++)
                {
                    int morning = data[i, j, 0];
                    int evening = data[i, j, 1];
                    int weekTotal = morning + evening;
                    totals[i] += weekTotal;

                    Console.WriteLine($"  Тиждень {j + 1}: ранок {morning}, вечір {evening} -> разом {weekTotal}");
                }

                Console.WriteLine($"Разом: {totals[i]} пацієнтів");
            }

            int maxDept = 0;
            for (int i = 1; i < d; i++)
            {
                if (totals[i] > totals[maxDept])
                {
                    maxDept = i;
                }
            }

            Console.WriteLine($"Найзавантаженіше: Відділення {maxDept + 1} ({totals[maxDept]} пацієнтів)");
        }
    }
}