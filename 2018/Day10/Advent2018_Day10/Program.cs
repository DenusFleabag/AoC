using System;

namespace Advent2018_Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] point = new int[4, 400];
            int velocity = 10076;
            string key = "0";
            while (key == "0")
            {
                Console.WriteLine($"{velocity}");
                int number = 0;
                string[] input = System.IO.File.ReadAllLines(@"C:\Users\denus\source\repos\Advent2018_Day10\input.txt");
                foreach (string line in input)
                {
                    point[2, number] = Convert.ToInt32(line.Substring(36, 2));
                    point[3, number] = Convert.ToInt32(line.Substring(40, 2));
                    point[0, number] = Convert.ToInt32(line.Substring(10, 6)) + point[2, number] * velocity;
                    point[1, number] = Convert.ToInt32(line.Substring(18, 6)) + point[3, number] * velocity;
                    number++;
                }
                char[,] map = new char[101, 101];
                int offset_x = 130;
                int offset_y = 120;
                for (int x = 0; x < map.GetLength(0); x++) for (int y = 0; y < map.GetLength(1); y++) map[x, y] = '.';
                for (int i = 0; i < number - 1; i++)
                {
                    if ((point[0, i] - offset_x) > 0 && (point[0, i] - offset_x) < 100 && (point[1, i] - offset_y) > 0 && (point[1, i] - offset_y) < 100)
                    {
                        map[point[0, i] - offset_x, point[1, i] - offset_y] = '#';
                    }
                }
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    string output = "";
                    for (int x = 0; x < map.GetLength(0); x++) 
                    {
                        output = string.Concat(output, map[x, y]);
                    }
                    Console.WriteLine(output);
                }
                velocity++;
                key = Console.ReadLine();
            }
        }
    }
}
