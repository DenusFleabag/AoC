using System;

namespace Advent2018_Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            int SN = 7315;
            bool foundsize = false;
            int size_1 = 0;
            int bestsize = 0;
            int[] bestsizepoint = new int[2];
            for (int size = 1;size < 301; size++)
            {
                int[,] grid = new int[301, 301];
                for (int y = 1; y < grid.GetLength(1); y++)
                {
                    for (int x = 1; x < grid.GetLength(0); x++)
                    {
                        grid[x, y] = (((((x + 10) * y + SN) * (x + 10)) / 100) % 10) - 5;
                    }
                }
                bool foundbest = false;
                int[] bestpoint = new int[2];
                int best = 0;
                for (int y = 1; y < grid.GetLength(1) - (size - 1); y++)
                {
                    for (int x = 1; x < grid.GetLength(0) - (size - 1); x++)
                    {
                        int value = 0;
                        for (int y_1 = 0 + y; y_1 < size + y; y_1++)
                        {
                            for (int x_1 = 0 + x; x_1 < size + x; x_1++)
                            {
                                value = value + grid[x_1, y_1];
                            }
                        }
                        if ((foundbest == false) || (value > best))
                        {
                            foundbest = true;
                            bestpoint[0] = x;
                            bestpoint[1] = y;
                            best = value;
                        }
                    }
                }
                //if (foundbest) Console.WriteLine($"{bestpoint[0]},{bestpoint[1]},{best}");
                if ((foundsize == false) || (best > bestsize))
                {
                    foundsize = true;
                    bestsizepoint[0] = bestpoint[0];
                    bestsizepoint[1] = bestpoint[1];
                    bestsize = best;
                    size_1 = size;
                }
                if (foundsize) Console.WriteLine($"{bestsizepoint[0]},{bestsizepoint[1]},{size_1},{bestsize},{size}");
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
