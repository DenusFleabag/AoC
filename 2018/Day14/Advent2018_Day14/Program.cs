using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018_Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            //puzzle input
            int request = 633601;
            int needed = request + 10;
            List<int> recipes = new List<int>();
            recipes.Add(3);
            recipes.Add(7);
            int elf_1_rec = recipes[0]; // current recipe
            int elf_1_pos = 0; // current position
            int elf_2_rec = recipes[1]; // current recipe
            int elf_2_pos = 1; // current position
            bool found = false;
            int position = -1;
            //while (recipes.Count() < needed) // part 1
            while (found == false) // part 2
            {
                //string output2 = "";
                //for (int i = 0; i < recipes.Count(); i++) output2 = string.Concat(output2, recipes[i]);
                //Console.WriteLine($"{output2} () {elf_1_pos},{elf_1_rec} [] {elf_2_pos},{elf_2_rec}");
                // Make new recipe
                int combined = elf_1_rec + elf_2_rec;
                // insert new recipe
                if (combined > 9)
                {
                    recipes.Add(combined / 10);
                    recipes.Add(combined % 10);
                }
                else
                {
                    recipes.Add(combined);
                }
                // Pick new recipe
                elf_1_pos = (elf_1_pos + elf_1_rec + 1) % recipes.Count();
                elf_1_rec = recipes[elf_1_pos];
                elf_2_pos = (elf_2_pos + elf_2_rec + 1) % recipes.Count();
                elf_2_rec = recipes[elf_2_pos];

                // Search Sequence
                if (request.ToString().Length < recipes.Count() - 2)
                {
                    for (int i = recipes.Count() - request.ToString().Length - 2; i < recipes.Count() - request.ToString().Length; i++)
                    {
                        string output1 = "";
                        for (int j = 0; j < request.ToString().Length; j++)
                        {
                            output1 = string.Concat(output1, recipes[i + j]);
                        }
                        //Console.WriteLine(output1);
                        if (request.ToString() == output1)
                        {
                            found = true;
                            position = i;
                        }
                    }
                }
            }

            //string output = "";
            //for (int i = request; i < needed; i++) output = string.Concat(output, recipes[i]);
            Console.WriteLine(position);
            Console.ReadKey();
        }
    }
}
