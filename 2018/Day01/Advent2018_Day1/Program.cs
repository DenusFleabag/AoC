using System;
using System.IO;

namespace Advent2018_Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxarray = 100000;
            double[] frequency_hist = new double[maxarray];
            int counter = 0;
            double frequency = 0;
            bool found = false;
            double foundvalue = 0;
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day1\\Input.txt";
            while ((found == false) && counter < maxarray)
            {
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;
                    while ((ln = file.ReadLine()) != null)
                    {
                        frequency = frequency + Convert.ToDouble(ln);
                        frequency_hist[counter] = frequency;
                        for (int i = 0; i < counter; i++)
                        {
                            if (frequency == frequency_hist[i] && found == false)
                            {
                                Console.WriteLine("found it");
                                found = true;
                                foundvalue = frequency;
                                break;
                            }
                        }
                        counter++;
                    }
                    file.Close();
                    Console.WriteLine($"{frequency} | {counter}");
                }
            }
            if (found) Console.WriteLine(foundvalue);
            Console.ReadLine();
        }
    }
}
