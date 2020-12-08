using System;
using System.IO;
using System.Collections.Generic;

namespace Advent2020_Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] map = new string[128, 8];
            List<int> sid = new List<int>();
            //string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day05\\Sample.txt";
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day05\\Input.txt";
            using (StreamReader file = new StreamReader(textFile))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    int row = 0, col = 0;
                    for (int i = 0; i < ln.Length; i++)
                    {
                        switch (ln[i])
                        {
                            case 'B':
                                row += Convert.ToInt32(Math.Pow(2, 6 - i));
                                break;
                            case 'R':
                                col += Convert.ToInt32(Math.Pow(2 ,9 - i));
                                break;
                            default:
                                break;
                        }
                    }
                    sid.Add(row * 8 + col);
                    map[row, col] = "#";
                }
                int heighest = -1;
                int mysid = -1;
                foreach (int id in sid) if (id > heighest) heighest = id;
                for(int x = 0; x < 128; x++) for (int y = 0; y < 8; y++) if (map[x, y] == null) if (x > 10 & x < 100) mysid = x * 8 + y;
                Console.WriteLine($"Part 1: {heighest}");
                Console.WriteLine($"Part 2: {mysid}");
            }
            Console.ReadKey();
        }
    }
}
