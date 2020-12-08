using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018_Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] grid = System.IO.File.ReadAllLines(@"C:\Users\denus\source\repos\Advent2018_Day13\input.txt");
            List<int[]> carts = new List<int[]>();
            List<int> skips = new List<int>();
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    int[] cartdata = new int[6];
                    switch (grid[y][x])
                    {
                        case '^':
                            cartdata[0] = y;
                            cartdata[1] = x;
                            cartdata[2] = 1;
                            cartdata[3] = 0;
                            cartdata[4] = 2;
                            cartdata[5] = 0;
                            carts.Add(cartdata);
                            break;
                        case '>':
                            cartdata[0] = y;
                            cartdata[1] = x;
                            cartdata[2] = 2;
                            cartdata[3] = 0;
                            cartdata[4] = 1;
                            cartdata[5] = 0;
                            carts.Add(cartdata);
                            break;
                        case 'v':
                            cartdata[0] = y;
                            cartdata[1] = x;
                            cartdata[2] = 3;
                            cartdata[3] = 0;
                            cartdata[4] = 2;
                            cartdata[5] = 0;
                            carts.Add(cartdata);
                            break;
                        case '<':
                            cartdata[0] = y;
                            cartdata[1] = x;
                            cartdata[2] = 4;
                            cartdata[3] = 0;
                            cartdata[4] = 1;
                            cartdata[5] = 0;
                            carts.Add(cartdata);
                            break;
                    }
                }
            }
            int alivecount = carts.Count();
            while (alivecount > 1)
            {
                skips.Clear();
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = 0; x < grid[y].Length; x++)
                    {
                        if ((grid[y][x] == '^' || grid[y][x] == '>' || grid[y][x] == 'v' || grid[y][x] == '<') && (skips.Contains(y * 10000 + x) == false))
                        {
                            for (int cart = 0; cart < carts.Count(); cart++)
                            {
                                if (carts[cart][0] == y && carts[cart][1] == x && carts[cart][5] == 0)
                                {
                                    // 0 = y coordinate 
                                    // 1 = x coordinate
                                    // 2 = direction (1 = up, 2 = right, 3 = down, 4 = left)
                                    // 3 = memory (0 = turn left, 1 = forward, 2 = turn right)
                                    // 4 = underneath (1 = horz, 2 = vert, 3 = bslash, 4 = fslash, 5 = intersection)
                                    // 5 = 1 = death 
                                    switch (carts[cart][2])
                                    {
                                        // Move Up ( y - 1 )
                                        case 1:
                                            // Check if car is there
                                            if (grid[y - 1][x] == '^' || grid[y - 1][x] == '>' || grid[y - 1][x] == 'v' || grid[y - 1][x] == '<')
                                            {
                                                carts[cart][5] = 1;
                                                //put road back
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                for (int cart2 = 0; cart2 < carts.Count(); cart2++)
                                                {
                                                    if (carts[cart2][0] == y - 1 && carts[cart2][1] == x)
                                                    {
                                                        carts[cart2][5] = 1;
                                                        //put road back
                                                        tempstring = grid[y - 1];
                                                        tempstring = tempstring.Remove(x, 1);
                                                        tempstring = tempstring.Insert(x, Placeback(carts[cart2][4]));
                                                        grid[y - 1] = tempstring;
                                                    }
                                                }
                                                Console.WriteLine($"{x},{y-1}");
                                                alivecount = alivecount - 2;
                                                break;
                                            }
                                            // move to new position
                                            else if (grid[y - 1][x] == '|')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 2;
                                                tempstring = grid[y - 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, "^");
                                                grid[y - 1] = tempstring;
                                                carts[cart][2] = 1;
                                            }
                                            else if (grid[y - 1][x] == '\\')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 3;
                                                tempstring = grid[y - 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, "<");
                                                grid[y - 1] = tempstring;
                                                carts[cart][2] = 4;
                                            }
                                            else if (grid[y - 1][x] == '/')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 4;
                                                tempstring = grid[y - 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, ">");
                                                grid[y - 1] = tempstring;
                                                carts[cart][2] = 2;
                                            }
                                            else if (grid[y - 1][x] == '+')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 5;
                                                tempstring = grid[y - 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                // 3 = memory (0 = turn left, 1 = forward, 2 = turn right)
                                                tempstring = tempstring.Insert(x, Memory(carts[cart][2], carts[cart][3]));
                                                grid[y - 1] = tempstring;
                                                carts[cart][2] = MakeTurn(carts[cart][2], carts[cart][3]);
                                                carts[cart][3] = (carts[cart][3] + 1) % 3;
                                            }
                                            carts[cart][0] = carts[cart][0] - 1;
                                            skips.Add((y - 1) * 10000 + x);
                                            break;
                                        // Move Right ( x + 1 )
                                        case 2:
                                            // Check if car is there
                                            if (grid[y][x + 1] == '^' || grid[y][x + 1] == '>' || grid[y][x + 1] == 'v' || grid[y][x + 1] == '<')
                                            {
                                                carts[cart][5] = 1;
                                                //put road back
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                for (int cart2 = 0; cart2 < carts.Count(); cart2++)
                                                {
                                                    if (carts[cart2][0] == y && carts[cart2][1] == x + 1)
                                                    {
                                                        carts[cart2][5] = 1;
                                                        //put road back
                                                        tempstring = grid[y];
                                                        tempstring = tempstring.Remove(x + 1, 1);
                                                        tempstring = tempstring.Insert(x + 1, Placeback(carts[cart2][4]));
                                                        grid[y] = tempstring;
                                                    }
                                                }
                                                Console.WriteLine($"{x + 1},{y}");
                                                alivecount = alivecount - 2;
                                                break;
                                            }
                                            // move to new position
                                            else if (grid[y][x + 1] == '-')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 1;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x + 1, 1);
                                                tempstring = tempstring.Insert(x + 1, ">");
                                                grid[y] = tempstring;
                                                carts[cart][2] = 2;
                                            }
                                            else if (grid[y][x + 1] == '\\')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 3;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x + 1, 1);
                                                tempstring = tempstring.Insert(x + 1, "v");
                                                grid[y] = tempstring;
                                                carts[cart][2] = 3;
                                            }
                                            else if (grid[y][x + 1] == '/')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 4;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x + 1, 1);
                                                tempstring = tempstring.Insert(x + 1, "^");
                                                grid[y] = tempstring;
                                                carts[cart][2] = 1;
                                            }
                                            else if (grid[y][x + 1] == '+')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 5;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x + 1, 1);
                                                // 3 = memory (0 = turn left, 1 = forward, 2 = turn right)
                                                tempstring = tempstring.Insert(x + 1, Memory(carts[cart][2], carts[cart][3]));
                                                grid[y] = tempstring;
                                                carts[cart][2] = MakeTurn(carts[cart][2], carts[cart][3]);
                                                carts[cart][3] = (carts[cart][3] + 1) % 3;
                                            }
                                            carts[cart][1] = carts[cart][1] + 1;
                                            skips.Add(y * 10000 + (x + 1));
                                            break;
                                        // Move Down ( y + 1 )
                                        case 3:
                                            // Check if car is there
                                            if (grid[y + 1][x] == '^' || grid[y + 1][x] == '>' || grid[y + 1][x] == 'v' || grid[y + 1][x] == '<')
                                            {
                                                carts[cart][5] = 1;
                                                //put road back
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                for (int cart2 = 0; cart2 < carts.Count(); cart2++)
                                                {
                                                    if (carts[cart2][0] == y + 1 && carts[cart2][1] == x)
                                                    {
                                                        carts[cart2][5] = 1;
                                                        //put road back
                                                        tempstring = grid[y + 1];
                                                        tempstring = tempstring.Remove(x, 1);
                                                        tempstring = tempstring.Insert(x, Placeback(carts[cart2][4]));
                                                        grid[y + 1] = tempstring;
                                                    }
                                                }
                                                Console.WriteLine($"{x},{y + 1}");
                                                alivecount = alivecount - 2;
                                                break;
                                            }
                                            // move to new position
                                            else if (grid[y + 1][x] == '|')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 2;
                                                tempstring = grid[y + 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, "v");
                                                grid[y + 1] = tempstring;
                                                carts[cart][2] = 3;
                                            }
                                            else if (grid[y + 1][x] == '\\')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 3;
                                                tempstring = grid[y + 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, ">");
                                                grid[y + 1] = tempstring;
                                                carts[cart][2] = 2;
                                            }
                                            else if (grid[y + 1][x] == '/')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 4;
                                                tempstring = grid[y + 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, "<");
                                                grid[y + 1] = tempstring;
                                                carts[cart][2] = 4;
                                            }
                                            else if (grid[y + 1][x] == '+')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 5;
                                                tempstring = grid[y + 1];
                                                tempstring = tempstring.Remove(x, 1);
                                                // 3 = memory (0 = turn left, 1 = forward, 2 = turn right)
                                                tempstring = tempstring.Insert(x, Memory(carts[cart][2], carts[cart][3]));
                                                grid[y + 1] = tempstring;
                                                carts[cart][2] = MakeTurn(carts[cart][2], carts[cart][3]);
                                                carts[cart][3] = (carts[cart][3] + 1) % 3;
                                            }
                                            carts[cart][0] = carts[cart][0] + 1;
                                            skips.Add((y + 1) * 10000 + x);
                                            break;
                                        // Move Left ( x - 1 )
                                        case 4:
                                            // Check if car is there
                                            if (grid[y][x - 1] == '^' || grid[y][x - 1] == '>' || grid[y][x - 1] == 'v' || grid[y][x - 1] == '<')
                                            {
                                                carts[cart][5] = 1;
                                                //put road back
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                for (int cart2 = 0; cart2 < carts.Count(); cart2++)
                                                {
                                                    if (carts[cart2][0] == y && carts[cart2][1] == x - 1)
                                                    {
                                                        carts[cart2][5] = 1;
                                                        //put road back
                                                        tempstring = grid[y];
                                                        tempstring = tempstring.Remove(x - 1, 1);
                                                        tempstring = tempstring.Insert(x - 1, Placeback(carts[cart2][4]));
                                                        grid[y] = tempstring;
                                                    }
                                                }
                                                Console.WriteLine($"{x - 1},{y}");
                                                alivecount = alivecount - 2;
                                                break;
                                            }
                                            // move to new position
                                            else if (grid[y][x - 1] == '-')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 1;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x - 1, 1);
                                                tempstring = tempstring.Insert(x - 1, "<");
                                                grid[y] = tempstring;
                                                carts[cart][2] = 4;
                                            }
                                            else if (grid[y][x - 1] == '\\')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 3;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x - 1, 1);
                                                tempstring = tempstring.Insert(x - 1, "^");
                                                grid[y] = tempstring;
                                                carts[cart][2] = 1;
                                            }
                                            else if (grid[y][x - 1] == '/')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 4;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x - 1, 1);
                                                tempstring = tempstring.Insert(x - 1, "v");
                                                grid[y] = tempstring;
                                                carts[cart][2] = 3;
                                            }
                                            else if (grid[y][x - 1] == '+')
                                            {
                                                //Replace old value
                                                string tempstring = grid[y];
                                                tempstring = tempstring.Remove(x, 1);
                                                tempstring = tempstring.Insert(x, Placeback(carts[cart][4]));
                                                grid[y] = tempstring;
                                                //store old value and place cart.
                                                carts[cart][4] = 5;
                                                tempstring = grid[y];
                                                tempstring = tempstring.Remove(x - 1, 1);
                                                // 3 = memory (0 = turn left, 1 = forward, 2 = turn right)
                                                tempstring = tempstring.Insert(x - 1, Memory(carts[cart][2], carts[cart][3]));
                                                grid[y] = tempstring;
                                                carts[cart][2] = MakeTurn(carts[cart][2], carts[cart][3]);
                                                carts[cart][3] = (carts[cart][3] + 1) % 3;
                                            }
                                            carts[cart][1] = carts[cart][1] - 1;
                                            skips.Add(y * 10000 + (x - 1));
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                //foreach(string line in grid) Console.WriteLine(line);
                //string output = "";
                //foreach (int[] henk in carts) output = string.Concat(output, Convert.ToString(henk[1]), " ", Convert.ToString(henk[0]), " = ");
                //Console.WriteLine(output);
                //Console.ReadKey();
            }
            foreach (int[] cart in carts)
            {
                if (cart[5] == 0) Console.WriteLine($"Last cart position: {cart[1]},{cart[0]}");
            }
            foreach (string line in grid) Console.WriteLine(line);
            Console.ReadKey();
        }
        private static string Placeback(int place)
        {
            // 4 = underneath (1 = horz, 2 = vert, 3 = bslash, 4 = fslash, 5 = intersection)
            return place switch
            {
                1 => "-",
                2 => "|",
                3 => "\\",
                4 => "/",
                5 => "+",
                _ => "*",
            };
        }
        private static int GetPosVal(char place)
        {
            // 4 = underneath (1 = horz, 2 = vert, 3 = bslash, 4 = fslash, 5 = intersection)
            return place switch
            {
                '-' => 1,
                '|' => 2,
                '\\' => 3,
                '/' => 4,
                '+' => 5,
                _ => 0,
            };
        }

        private static string Memory(int direction, int memory)
        {
            // 2 = direction (1 = up, 2 = right, 3 = down, 4 = left)
            // 3 = memory (0 = turn left, 1 = forward, 2 = turn right)
            switch (direction)
            {
                case 1:
                    return memory switch
                    {
                        0 => "<",
                        1 => "^",
                        2 => ">",
                        _ => "#",
                    };
                case 2:
                    return memory switch
                    {
                        0 => "^",
                        1 => ">",
                        2 => "v",
                        _ => "#",
                    };
                case 3:
                    return memory switch
                    {
                        0 => ">",
                        1 => "v",
                        2 => "<",
                        _ => "#",
                    };
                case 4:
                    return memory switch
                    {
                        0 => "v",
                        1 => "<",
                        2 => "^",
                        _ => "#",
                    };
                default:
                    return "#";
            }
        }
        private static int MakeTurn(int direction,int memory)
        {
            // 2 = direction (1 = up, 2 = right, 3 = down, 4 = left)
            // 3 = memory (0 = turn left, 1 = forward, 2 = turn right)
            switch (direction)
            {
                case 1:
                    return memory switch
                    {
                        0 => 4,
                        1 => 1,
                        2 => 2,
                        _ => 0,
                    };
                case 2:
                    return memory switch
                    {
                        0 => 1,
                        1 => 2,
                        2 => 3,
                        _ => 0,
                    };
                case 3:
                    return memory switch
                    {
                        0 => 2,
                        1 => 3,
                        2 => 4,
                        _ => 0,
                    };
                case 4:
                    return memory switch
                    {
                        0 => 3,
                        1 => 4,
                        2 => 1,
                        _ => 0,
                    };
                default:
                    return 0;
            }
        }
    }
}
