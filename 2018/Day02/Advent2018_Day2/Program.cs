using System;
using System.IO;

namespace Advent2018_Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] counted = new int[27];
            int[] tellers = new int[27];
            string textfile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day2\\input.txt";
            using (StreamReader file = new StreamReader(textfile))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    for (int i = 0; i < counted.Length - 1; i++)
                    {
                        counted[i] = 0;
                    }
                    for (int i = 0; i < ln.Length; i++)
                    {
                        int count = 1;
                        for (int j = i+1; j < ln.Length;j++)
                        {
                            if (ln[i] == ln[j])
                            {
                                ln = string.Concat(ln.Substring(0, j), ln.Substring(j + 1));                              
                                count++;
                                j--;
                            }
                        }
                        if (counted[count] == 0)
                        {
                            tellers[count] = tellers[count] + 1;
                            counted[count] = 1;
                        }
                    }
                }
                file.Close();
            }
            foreach (int value in tellers) Console.WriteLine(value);
            Console.WriteLine($"Answer: {tellers[2] * tellers[3]}");
            Console.ReadKey();
        }
    }
}
