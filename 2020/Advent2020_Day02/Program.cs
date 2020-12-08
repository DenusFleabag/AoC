using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Advent2020_Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            //string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day02\\Sample.txt";
            string textFile = "C:\\Users\\denus\\source\\repos\\Advent2020_Day02\\Input.txt";
            using (StreamReader file = new StreamReader(textFile))
            {
                int validcounter1 = 0;
                int validcounter2 = 0;
                Regex rex = new Regex(@"(.+)-(.+) (.): (.+)");
                foreach (Match match in rex.Matches(file.ReadToEnd())) {
                    int min = int.Parse(match.Groups[1].Value);
                    int max = int.Parse(match.Groups[2].Value);
                    char x = match.Groups[3].Value.ToCharArray()[0];
                    string pw = match.Groups[4].Value;
                    int count1 = 0;
                    foreach (char c in pw) if (c == x) count1++;
                    if ((min - 1) < count1 & count1 < (max + 1))validcounter1++;
                    if (pw[min - 1] != x ^ pw[max - 1] != x) validcounter2++;
                }
                file.Close();
                Console.WriteLine($"Part 1 {validcounter1} passwords are correct");
                Console.WriteLine($"Part 2 {validcounter2} passwords are correct");
            }
            Console.ReadLine();
        }
    }
}
