using System;
using System.IO;

namespace Advent2018_Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 50;
            double[,] land = new double[size, size];
            int x;
            int y = 0;
            double answer = 0;
            //string textFile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day18\\Sample.txt";
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day18\\Input.txt";
            string output = "";
            using (StreamReader file = new StreamReader(textFile))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    x = 0;
                    Console.WriteLine($"{ln}");
                    foreach (char j in ln)
                    {
                        if (j.Equals('.'))
                        {
                            land[x, y] = 1;
                        }
                        else if (j.Equals('|'))
                        {
                            land[x, y] = 2;
                        }
                        else
                        {
                            land[x, y] = 3;
                        }
                        //Console.WriteLine(land[x, y]);
                        x++;

                    }
                    y++;
                }
                file.Close();
                Console.WriteLine();
            }

            for (int minutes = 1; minutes < 100000; minutes++)
            {
                double[,] newland = new double[size, size];
                // Change the Field
                for (int x1 = 0; x1 < size; x1++)
                {
                    for (int y1 = 0; y1 < size; y1++)
                    {
                        // get surroundings
                        double[] counter = new double[3];
                        for (int x2 = x1 - 1; x2 < x1 + 2; x2++)
                        {
                            for (int y2 = y1 - 1; y2 < y1 + 2; y2++)
                            {
                                if (x2 > -1 & y2 > -1 & ((x2 != x1) | (y2 != y1)) & x2 < size & y2 < size)
                                {
                                    if (land[x2, y2] == 1) counter[0]++; // Open Arces
                                    if (land[x2, y2] == 2) counter[1]++; // Tree Arces
                                    if (land[x2, y2] == 3) counter[2]++; // Lumber Arces
                                }
                            }
                        }

                        // Open Arce
                        if (land[x1, y1] == 1)
                        {
                            // Needs 3 Tree Arces to become a Tree Arce
                            if (counter[1] > 2)
                            {
                                newland[x1, y1] = 2; // Convert to Tree Arce
                            }
                            else
                            {
                                newland[x1, y1] = 1; // Remain Open Arce
                            }
                        }


                        // Tree Arce
                        if (land[x1, y1] == 2)
                        {
                            // Needs 3 Lumber Arces to become a Lumber Arce
                            if (counter[2] > 2)
                            {
                                newland[x1, y1] = 3; // Convert to Lumber Arce
                            }
                            else
                            {
                                newland[x1, y1] = 2; // Remain Tree Arce
                            }
                        }

                        // Lumber Arce
                        if (land[x1, y1] == 3)
                        {
                            // Needs 1 Lumber Arce and 1 Tree Arce to remain a Lumber Arce, else Open arce
                            if (counter[1] > 0 & counter[2] > 0)
                            {
                                newland[x1, y1] = 3; // Remain Lumber Arce
                            }
                            else
                            {
                                newland[x1, y1] = 1; // Change to Open Arce
                            }
                        }
                    }
                }
                // Console.WriteLine("");
                double[] counter1 = new double[3];
                for (int y1 = 0; y1 < size; y1++)
                {
                    output = "";
                    for (int x1 = 0; x1 < size; x1++)
                    {
                        if (newland[x1, y1] == 1)
                        {
                            output = output + ".";
                            counter1[0]++;
                        }
                        if (newland[x1, y1] == 2)
                        {
                            output = output + "|";
                            counter1[1]++;
                        }
                        if (newland[x1, y1] == 3)
                        {
                            output = output + "#";
                            counter1[2]++;
                        }
                    }
                    Console.WriteLine($"{output}");
                }
                land = newland;
                answer = answer + counter1[2];
                Console.WriteLine($"{minutes} {counter1[0]} {counter1[1]} {counter1[2]} {answer}");
                Console.ReadLine();
            }

            Console.WriteLine($"Done {answer}");
            Console.ReadLine();
        }
    }
}