using System;
using System.IO;

namespace Advent2018_Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int id = 0;
            int left = 0;
            int top = 0;
            int height = 0;
            int width = 0;
            int[,] fabric = new int[1001,1001];
            string textfile = "C:\\Users\\denus\\source\\repos\\Advent2018_Day3\\input.txt";
            using (StreamReader file = new StreamReader(textfile))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    id = Convert.ToInt32(ln.Substring(ln.IndexOf('#') + 1, ln.IndexOf('@') - 2));
                    left = Convert.ToInt32(ln.Substring(ln.IndexOf('@') + 2, ln.IndexOf(',') - ln.IndexOf('@') - 2));
                    top = Convert.ToInt32(ln.Substring(ln.IndexOf(',') + 1, ln.IndexOf(':') - ln.IndexOf(',') - 1));
                    width = Convert.ToInt32(ln.Substring(ln.IndexOf(':') + 2, ln.IndexOf('x') - ln.IndexOf(':') - 2));
                    height = Convert.ToInt32(ln.Substring(ln.IndexOf('x') + 1));
                    //Console.WriteLine($"{id} {left} {top} {width} {height}");
                    for (int x = left; x < left + width; x++)
                    {
                        for (int y = top; y < top + height; y++)
                        {
                            fabric[x, y] = fabric[x, y] + 1;
                        }
                    }
                }
                file.Close();
            }
            for (int x = 0; x < fabric.GetLength(0); x++)
            {
                for (int y = 0; y < fabric.GetLength(1); y++)
                {
                    if (fabric[x, y] > 1) count++;
                }
            }
            
            using (StreamReader file = new StreamReader(textfile))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    int nrclaims = 0;
                    id = Convert.ToInt32(ln.Substring(ln.IndexOf('#') + 1, ln.IndexOf('@') - 2));
                    left = Convert.ToInt32(ln.Substring(ln.IndexOf('@') + 2, ln.IndexOf(',') - ln.IndexOf('@') - 2));
                    top = Convert.ToInt32(ln.Substring(ln.IndexOf(',') + 1, ln.IndexOf(':') - ln.IndexOf(',') - 1));
                    width = Convert.ToInt32(ln.Substring(ln.IndexOf(':') + 2, ln.IndexOf('x') - ln.IndexOf(':') - 2));
                    height = Convert.ToInt32(ln.Substring(ln.IndexOf('x') + 1));
                    //Console.WriteLine($"{id} {left} {top} {width} {height}");
                    for (int x = left; x < left + width; x++)
                    {
                        for (int y = top; y < top + height; y++)
                        {
                            nrclaims = Math.Max(fabric[x, y], nrclaims);
                        }
                    }
                    if (nrclaims == 1) Console.WriteLine(id);
                }
                file.Close();
            }
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
