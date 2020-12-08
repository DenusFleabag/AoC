using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018_Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] input = new char[101, 2];
            string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string textfile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day7\\input.txt";
            using (StreamReader file = new StreamReader(textfile))
            {
                string line;
                int pointer = 0;
                while ((line = file.ReadLine()) != null)
                {
                    input[pointer, 0] = Convert.ToChar(line.Substring(5, 1));
                    input[pointer, 1] = Convert.ToChar(line.Substring(36, 1));
                    pointer++;
                }
                file.Close();
            }


            List<char> finalorder = new List<char>();
            List<char> abclist = new List<char>();
            foreach (char letter in abc) abclist.Add(letter);
            while (finalorder.Count() < abc.Length)
            {
                List<char> stepletters = new List<char>();
                for (int i = 0; i < input.GetLength(0); i++) stepletters.Add(input[i, 1]);
                for (int i = 0; i < abclist.Count(); i++)
                {
                    if (stepletters.Contains(abclist[i]) == false)
                    {
                        finalorder.Add(abclist[i]);
                        abclist.Remove(abclist[i]);
                        break;
                    }
                }
                for (int i = 0; i < input.GetLength(0); i++)
                {
                    if (input[i, 0] == finalorder.Last())
                    {
                        input[i, 0] = '0';
                        input[i, 1] = '0';
                    }
                }
            }
            string final = "";
            foreach (char letter in finalorder) final = string.Concat(final, "",letter);
            Console.WriteLine($"{final}");

            using (StreamReader file = new StreamReader(textfile))
            {
                string line;
                int pointer = 0;
                while ((line = file.ReadLine()) != null)
                {
                    input[pointer, 0] = Convert.ToChar(line.Substring(5, 1));
                    input[pointer, 1] = Convert.ToChar(line.Substring(36, 1));
                    pointer++;
                }
                file.Close();
            }

            bool available = false;
            int[,] worker = new int[5, 2];
            int time = -1;
            char avl_letter = '#';
            abc = "";
            foreach (char letter in finalorder) abc = string.Concat(abc, "", letter);
            finalorder.Clear();
            abclist.Clear();
            foreach (char letter in abc) abclist.Add(letter);
            while (finalorder.Count() < abc.Length )
            {
                // search if worker is done working
                for (int i = 0; i < worker.GetLength(0); i++)
                {
                    if (worker[i, 0] == time)
                    {
                        // Set worker available
                        worker[i, 0] = 0;
                        // Remove letter if work is finished
                        for (int j = 0; j < input.GetLength(0); j++)
                        {
                            if (Convert.ToInt32(input[j, 0]) == worker[i,1])
                            {
                                input[j, 0] = '0';
                                input[j, 1] = '0';
                            }
                        }
                        // prevent rechoose same letter
                        abclist.Remove(Convert.ToChar(worker[i, 1]));
                        // Renew available letter due to finished work
                        available = false;
                    }
                }

                List<char> stepletters = new List<char>();
                for (int i = 0; i < input.GetLength(0); i++) stepletters.Add(input[i, 1]);

                anotherletter:
                // Search for available letter
                for (int i = 0; i < abclist.Count(); i++)
                {
                    if (stepletters.Contains(abclist[i]) == false && available == false && abclist[i] != worker[0, 1] && abclist[i] != worker[1, 1] && abclist[i] != worker[2, 1] && abclist[i] != worker[3, 1] && abclist[i] != worker[4, 1])
                    {
                        // make letter available
                        avl_letter = abclist[i];
                        Console.WriteLine($"Made: {avl_letter} available");
                        // Make letter available for assignment
                        available = true;
                    }
                }

                // Assign letter to Worker
                if (available)
                {
                    for (int i = 0; i < worker.GetLength(0); i++)
                    {
                        if (worker[i,0] == 0)
                        {
                            // make letter used
                            finalorder.Add(avl_letter);
                            // Store letter 
                            worker[i, 1] = Convert.ToInt32(finalorder.Last());
                            // Set time when finished
                            worker[i, 0] = time + worker[i, 1] - 4;
                            Console.WriteLine($"Worker {i}: Letter:{Convert.ToChar(worker[i, 1])} Finished on:{worker[i, 0]}");
                            // Maybe assign another worker
                            available = false;
                            goto anotherletter;
                        }
                    }
                } 

                // Add a cycletime
                time++;
                Console.WriteLine($"Time: {time}");
            }
            final = "";
            foreach (char letter in finalorder) final = string.Concat(final, "", letter);
            Console.WriteLine($"{final}");

            Console.ReadKey();
        }
    }
}
