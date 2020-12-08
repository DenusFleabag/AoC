using System;
using System.IO;
using System.Collections.Generic;

namespace Advent2018_Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] field = new int[400, 400];
            int[,] point = new int[50, 3];
            int regsize = 0;
            using (StreamReader file = new StreamReader("C:\\Users\\denus\\source\\repos\\Advent2018_Day6\\input.txt"))
            {
                int points = 0;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    point[points, 0] = Convert.ToInt32(line.Substring(0, line.IndexOf(',')));
                    point[points, 1] = Convert.ToInt32(line.Substring(line.IndexOf(',') + 1, line.Length - line.IndexOf(',') - 1));
                    field[point[points,0],point[points,1]] = 1;
                    points++;
                }
                file.Close();
            }
            
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    int closest = -1;
                    int bestdist = -1;
                    int totdist = 0;
                    bool valid = true;
                    for (int z = 0; z < 50; z++)
                    {
                        int distance = Math.Abs(point[z, 0] - x) + Math.Abs(point[z, 1] - y);
                        totdist = totdist + distance;
                        if (distance < bestdist || closest == -1)
                        {
                            bestdist = distance;
                            closest = z;
                            valid = true;
                        }
                        else if (distance == bestdist)
                        {
                            valid = false;
                        }
                    }
                    if (totdist < 10000) regsize++;
                    if (valid)
                    {
                        field[x, y] = closest;
                        point[closest, 2]++;
                    }
                    else
                    {
                        field[x, y] = -1;
                    }
                    if ((x == 0 || x == (field.GetLength(0) - 1) || y == 0 || y == (field.GetLength(1) - 1)) && closest != -1) 
                    {
                        point[closest, 2] = -1;
                    }
                }
            }

            /*for (int x = 0; x < field.GetLength(0); x++)
            {
                string output = "";
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    output = string.Concat(output, $"{field[x, y]}");
                }
                Console.WriteLine(output);
            }*/
            int bestarea = -1;
            int bestsize = -1;
            for (int x = 0; x < point.GetLength(0); x++)
            {
                string output = "";
                for (int y = 0; y < point.GetLength(1); y++)
                {
                    output = string.Concat(output, $"{point[x, y]} ");
                }
                if (point[x, 2] > bestsize || bestsize == -1) 
                {
                    bestarea = x;
                    bestsize = point[x, 2];
                }
                Console.WriteLine(output);
            }
            Console.WriteLine(bestsize);
            Console.WriteLine(regsize);
            Console.ReadKey();
        }
    }
}
