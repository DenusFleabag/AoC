using System;
using System.IO;

namespace Advent2020_Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            //string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day03\\Sample.txt";
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day03\\Input.txt";
            using (StreamReader file = new StreamReader(textFile))
            {
                int[,] x = new int[5, 2] { { 0, 1 }, { 0, 3 }, { 0, 5 }, { 0, 7 }, { 0, 1 } };
                long[] t = new long[5] { 0, 0, 0, 0, 0 };
                string ln;
                bool skip = true;
                while ((ln = file.ReadLine()) != null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i != 4 | skip) // Skip uneven lines for 2 down
                        {
                            char c = ln[x[i,0] % ln.Length];
                            if (c == '#') t[i]++;
                        }
                        if (i != 4 | skip) x[i, 0] += x[i, 1];
                    }
                    if (skip) { skip = false; } else skip = true;
                }
                Console.WriteLine($"Part 1: {t[1]}");
                Console.WriteLine($"Part 2: {t[0] * t[1] * t[2] * t[3] * t[4]}");
                Console.ReadLine();
            }
        }
    }
}
