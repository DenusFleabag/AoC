using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018_Day12
{
    class Program
    {
        static void Main(string[] args)
        { 
            List<string> input = new List<string>();
            int potindex = 5;
            double generations = 50000000000;
            string plants = ".....#...#...##..####..##.####.#...#...#.#.#.#......##....#....######.####.##..#..#..##.##..##....#######......................";
            //string plants = "....#..#.#..##......###...###............";
            string[] file = System.IO.File.ReadAllLines(@"C:\Users\denus\source\repos\Advent2018_Day12\input.txt");
            foreach (string line in file) input.Add(line);

            for (double gen = 0; gen <= generations; gen++)
            {
                bool found = true;
                for (int i = 0; i < 10; i++)
                {
                    if (plants[i] == '#') found = false;
                }
                if (found)
                {
                    plants = plants.Remove(0, 5);
                    potindex = potindex - 5;
                }
                found = true;
                for (int i = 0; i < 10; i++)
                {
                    if (plants[(plants.Count() - 1) - i] == '#') found = false;
                }
                if (found == false)
                {
                    plants = plants.Insert(plants.Count(),"..");
                }
                double pots = 0;
                for (int pot = 0; pot < plants.Length; pot++)
                {
                    if (plants[pot] == '#') pots = pots + (pot - potindex);
                }
                string str_gen = gen.ToString("00");
                Console.WriteLine($"{str_gen}: {plants} ^ {pots}");
                string emptyplants = "";
                for (double i = 0; i < plants.Length; i++) emptyplants = string.Concat(emptyplants, ".");

                string nextgenplants = emptyplants;

                for (int i = 0; i < plants.Length - 4; i++)
                {
                    for (int j = 0; j < input.Count(); j++)
                    {
                        bool match = true;
                        for (int k = 0; k < 5; k++)
                        {
                            if (plants[i + k] != Convert.ToChar(input[j][k]))
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            //Console.WriteLine($"Match for plant; {i + 2}");
                            nextgenplants = nextgenplants.Remove(i + 2, 1);
                            nextgenplants = nextgenplants.Insert(i + 2, Convert.ToString(input[j][9]));
                            break;
                        }
                    }
                }
                plants = nextgenplants;
            }
            Console.ReadLine();
        }
    }
}
