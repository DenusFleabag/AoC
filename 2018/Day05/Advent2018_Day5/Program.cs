using System;
using System.IO;

namespace Advent2018_Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            //char[,] abc = new char[2, 26];
            string abc = "abcdefghijklmnopqrstuvwxyz";
            string textfile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day5\\input.txt";
            using (StreamReader file = new StreamReader(textfile))
            {
                string original, modified;
                while ((original = file.ReadLine()) != null)
                {
                    int size;
                    int best= -1;
                    modified = original;
                    for (int i = 0; i < abc.Length; i++)
                    {
                        modified = Remove(original, abc[i]);
                        size = React(modified).Length;
                        if (best == -1 || size < best) best = size;
                    }
                    Console.WriteLine(best);
                }
                file.Close();
            }
            Console.ReadKey();
        }

        static string React(string input)
        {
            string upper = input.ToUpper();
            int currsize;
            int prevsize = 0;
            currsize = input.Length;
            while (currsize != prevsize)
            {
                prevsize = currsize;
                for (int i = 0; i < input.Length - 1; i++)
                {
                    if (upper[i] == upper[i + 1])
                    {
                        if (input[i] != input[i + 1])
                        {
                            input = input.Remove(i, 2);
                            upper = upper.Remove(i, 2);
                            currsize = input.Length;
                        }
                    }
                }
            }
            return input;
        }

        static string Remove(string input, char letter)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == letter || input[i] == char.ToUpper(letter))
                {
                    input = input.Remove(i, 1);
                    i--;
                }
            }
            return input;
        }
    }
}
