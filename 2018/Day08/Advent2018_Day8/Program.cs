using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018_Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> input = new List<int>();
            using (StreamReader file = new StreamReader("C:\\Users\\denus\\source\\repos\\Advent2018_Day8\\input.txt"))
            {
                string line;
                string letters = "";
                while ((line = file.ReadLine()) != null)
                {
                    foreach (char letter in line)
                    {

                        if (letter == ' ')
                        {
                            input.Add(Convert.ToInt32(letters));
                            letters = "";
                        }
                        letters = String.Concat(letters, letter);
                    }
                }
                file.Close();
            }
            List<int> indexes = new List<int>();
            int[] output = Child(input,0);
            Console.WriteLine($"{output[0]} ^ {output[1]}");
            Console.ReadKey();
        }

        static int[] Child(List<int> numbers, int depth)
        {
            depth++;
            Console.WriteLine($"Depth = {depth}");
            int[] score = new int[3];
            int[] inter = new int[3];
            bool has_childs = false;
            List<int> Childvalues = new List<int>();
            for (int i=0 ; i < numbers[0]; i++)
            {
                has_childs = true;
                inter = Child(numbers.GetRange((2+score[1]),numbers.Count()-(2+score[1])),depth);
                score[0] = score[0] + inter[0];
                score[1] = score[1] + inter[1];
                Childvalues.Add(inter[0]);
                Console.WriteLine($"{score[0]} | {score[1]} | {score[2]} | Child: {i}");
            }
            for (int i = 0; i < numbers[1]; i++)
            {
                if (has_childs)
                {
                    if (numbers[2 + score[1]] <= Childvalues.Count())
                    {
                        score[0] = score[0] + Childvalues[numbers[2 + score[1]] - 1];
                        Console.WriteLine($"Index {numbers[2 + score[1]]} with value {Childvalues[numbers[2 + score[1]] - 1]} added.");
                    }
                }
                else
                {
                    score[0] = score[0] + numbers[2 + score[1]];
                    Console.WriteLine($"Score added: {numbers[2 + score[1]]}");
                    
                }
                score[1]++;
            }
            score[1] = score[1] + 2;
            if (has_childs)
            {
                for (int i = 0; i < Childvalues.Count(); i++)
                {
                    score[0] = score[0] - Childvalues[i];
                }
            }
            return score;
            
        }
    }
}
