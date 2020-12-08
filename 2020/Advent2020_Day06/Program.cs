using System;
using System.IO;
using System.Collections.Generic;

namespace Advent2020_Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> q0 = new List<char>(), q1 = new List<char>(), q2 = new List<char>();
            int t1 = 0, t2 = 0;
            bool first = true;
            //string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day06\\Sample.txt";
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day06\\Input.txt";
            using StreamReader file = new StreamReader(textFile);
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                if (ln != "")
                {
                    foreach (char c in ln) if (q0.Contains(c) == false) q0.Add(c);
                    if (first)
                    {
                        first = false;
                        foreach (char c in ln) if (q2.Contains(c) == false) q2.Add(c);
                    }
                    else
                    {
                        foreach (char c in ln) if (q1.Contains(c) == true) q2.Add(c);                       
                    }
                    q1 = q2;
                    q2 = new List<char>();
                }
                else // end of group
                {
                    t1 += q0.Count;
                    q0.Clear();
                    t2 += q1.Count;
                    q1.Clear();
                    first = true;
                }
            }
            file.Close();
            Console.WriteLine($"Part 1: {t1}");
            Console.WriteLine($"Part 2: {t2}");
            Console.ReadKey();
        }
    }
}
