using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02
{
    internal class Task8
    {
        public static void Run()
        {
            int D = int.Parse(Console.ReadLine()!);
            int W = int.Parse(Console.ReadLine()!);

            int[,,] data = new int[D, W, 2];

            for (int i = 0; i < D; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    data[i, j, 0] = int.Parse(Console.ReadLine()!); 
                    data[i, j, 1] = int.Parse(Console.ReadLine()!); 
                }
            }

            int maxTotal = 0;
            int maxPatients = -1;

            for (int i = 0; i < D; i++)
            {
                Console.WriteLine($"Відділення {i + 1}:");
                int totalP = 0;

                for (int j = 0; j < W; j++)
                {
                    int morning = data[i, j, 0];
                    int evening = data[i, j, 1];
                    int weekTotal = morning + evening;
                    totalP += weekTotal;

                    Console.WriteLine($"  Тиждень {j + 1}: ранок {morning}, вечір {evening} -> разом {weekTotal}");
                }

                Console.WriteLine($"  Разом: {totalP} пацієнтів");

                if (totalP > maxPatients)
                {
                    maxPatients = totalP;
                    maxTotal = i;
                }
            }

            Console.WriteLine($"Найзавантаженіше: Відділення {maxTotal + 1} ({maxPatients} пацієнтів)");
        }
    }
}
