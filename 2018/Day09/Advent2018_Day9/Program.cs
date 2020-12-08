using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018_Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"C:\Users\denus\source\repos\Advent2018_Day9\input.txt");
            string[] words = input.Split(' ');
            int playercount = Convert.ToInt32(words[0]);
            int maxMarble = Convert.ToInt32(words[6]) * 100;
            int curMarble = 0;
            int curPlayer = 0;
            double[] playerscore = new double[playercount];
            List<double> Circle = new List<double>();
            Circle.Add(0);
            Console.WriteLine($"{playercount} {maxMarble}");
            for (int marble = 1; marble < maxMarble; marble ++)
            {
                if(marble % 23 == 0)
                {
                    playerscore[curPlayer] += marble;
                    int newCurMarble = curMarble - 7;
                    if (newCurMarble < 0) newCurMarble += Circle.Count();
                    playerscore[curPlayer] += Circle[newCurMarble];
                    Circle.RemoveAt(newCurMarble);
                    curMarble = newCurMarble;
                } 
                else
                {
                    int newCurMarble = (curMarble + 2) % Circle.Count();
                    if (newCurMarble == 0) newCurMarble = Circle.Count();
                    Circle.Insert(newCurMarble, marble);
                    curMarble = newCurMarble;
                }
                curPlayer = (curPlayer + 1) % playerscore.Length;
                //string output1 = "";
                //foreach (int item in Circle) output1 = string.Concat(output1, item, ',');
                //Console.WriteLine(marble);
            }
            string output2 = "";
            foreach (double player in playerscore) output2 = string.Concat(output2, player, ',');
            Console.WriteLine(output2);
            double output3 = 0;
            foreach (double player in playerscore) output3 = Math.Max(output3,player);
            Console.WriteLine(output3);
            Console.ReadKey();
        }
    }
}
