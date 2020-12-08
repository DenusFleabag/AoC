using System;
using System.IO;
using System.Collections.Generic;

namespace Advent2018_Day4
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            int[,] timeline = new int[63, 255];
            int[,] stats = new int[62, 23];
            int i_date = 0;
            int currtime = 0;
            int Error = 0;
            int currguard = 0;
            int nrofstats = 0;
            string textfile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day4\\input.txt";
            using (StreamReader file = new StreamReader(textfile))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    input.Add(ln);
                }
                file.Close();
            }
            input.Sort();
            foreach (string line in input)
            {
                int year = Convert.ToInt32(line.Substring(1, 4));
                int month = Convert.ToInt32(line.Substring(6, 2));
                int day = Convert.ToInt32(line.Substring(9, 2));
                int time = Convert.ToInt32(line.Substring(12, 4));

                switch (line.Substring(18, 5))
                {
                    case "Guard":
                        i_date++;
                        currguard = Convert.ToInt32(line.Substring(25, line.IndexOf('b', 25) - 25));
                        timeline[60, i_date] = currguard;
                        currtime = 0;
                        //Console.WriteLine(currguard);
                        break;
                    case "falls":
                        timeline[61, i_date] = Convert.ToInt32(String.Concat(year.ToString("D4"), month.ToString("D2"), day.ToString("D2")));
                        currtime = time;
                        break;
                    case "wakes":
                        for (int i = currtime; i < time; i++) timeline[i, i_date] = 1;
                        break;
                    default:
                        Error = 1;
                        break;
                }
                timeline[62, i_date] = currtime;
                string output = "";
                for (int i = 0; i < timeline.GetLength(0); i++)
                {
                    output = string.Concat(output, " ", timeline[i, i_date]);
                }
                Console.WriteLine($"{output}");
            }
            Console.WriteLine("Final:");
            for (int j = 0; j < timeline.GetLength(1); j++)
            {
                string output1 = "";
                for (int i = 0; i < timeline.GetLength(0); i++)
                {
                    output1 = string.Concat(output1, " ", timeline[i, j]);
                }
                Console.WriteLine($"{output1}");
            }
            //Console.WriteLine($"{output1}");
            Console.WriteLine($"Error: {Error}");
            for (int j = 1; j < timeline.GetLength(1); j++)
            {
                if (nrofstats == 0)
                {
                    for (int i = 0; i < stats.GetLength(0)-1; i++)
                    {
                        stats[i, 0] = timeline[i, j];
                    }
                    stats[61, 0]++;
                    nrofstats++;
                }
                else
                {
                    //find guard
                    bool found = false;
                    int position = 0;
                    for (int i = 0; i < nrofstats; i++)
                    {
                        if (stats[60, i] == timeline[60, j])
                        {
                            found = true;
                            position = i;
                        }
                    }
                    if (found==true)
                    {
                        for (int i = 0; i < stats.GetLength(0) - 1; i++)
                        {
                            stats[i, position] = stats[i, position] + timeline[i, j];
                            if (i == 60) stats[i, position] = timeline[i, j];
                        }
                        stats[61, position]++;
                    } 
                    else
                    {
                        for (int i = 0; i < stats.GetLength(0) - 1; i++)
                        {
                            stats[i, nrofstats] = stats[i, nrofstats] + timeline[i, j];
                            if (i == 60) stats[i, nrofstats] = timeline[i, j];
                        }
                        stats[61, nrofstats]++;
                        nrofstats++;
                    }
                }
            }

            for (int j = 0; j < stats.GetLength(1); j++)
            {
                string output2 = "";
                for (int i = 0; i < stats.GetLength(0); i++)
                {
                    output2 = string.Concat(output2, " ", stats[i, j]);
                }
                Console.WriteLine($"{output2}");
            }
            Console.ReadKey();
        }
    }
}
