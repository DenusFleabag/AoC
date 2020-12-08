using System;
using System.IO;

namespace Advent2015_Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            int total = 0, count = 0;
            //string textFile = "C:\\Users\\denus\\source\\repos\\Advent2015_Day01\\Sample.txt";
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2015_Day01\\Input.txt";
            using StreamReader file = new StreamReader(textFile);
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                foreach (char c in ln)
                {
                    if (c == '(')
                    {
                        total += 1;
                        count++;
                    }
                    else
                    {
                        total += -1;
                        count++;
                    }
                    if (total == -1) Console.WriteLine($"Part 2: {count}");
                }
            }
            Console.WriteLine($"Part 1:{total}");
            Console.ReadKey();
        }
    }
}
