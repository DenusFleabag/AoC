using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent2018_Day15
{
    public class Program : IAdventProblemSet
    {
        private List<Combatant> _combatants;

        public DayFifteen()
        {
            _combatants = new List<Combatant>();
        }

        public string Description()
        {
            return "Beverage Bandits (HARD)";
        }

        public int SortOrder()
        {
            return 15;
        }

        public string PartA()
        {
            string filePath = @"Fifteen\DayFifteenInput.txt";
            int outcome = FindCombatOutcome(filePath, 3, false).Value;
            return outcome.ToString();
        }

        public string PartB()
        {
            string filePath = @"Fifteen\DayFifteenInput.txt";
            int elfPower = 3;
            do
            {
                elfPower++;

                int? outcome = FindCombatOutcome(filePath, elfPower, true);
                if (outcome != null)
                    return outcome.ToString();

                _combatants = new List<Combatant>();
            } while (true);
        }

        public int? FindCombatOutcome(string filePath, int elfPower, bool failOnElfDeath)
        {
            char[,] board = ParseBoard(filePath, elfPower);
            int round = 0;

            do
            {
                for (int i = 0; i < _combatants.Count; i++)
                {
                    Combatant current = _combatants[i];

                    List<Combatant> potenentialTargets = _combatants.Where(t => t.Type != current.Type).ToList();

                    // Check for end state
                    if (!potenentialTargets.Any())
                        return round * _combatants.Sum(c => c.HitPoints);

                    // If none adjacent, then move
                    if (!potenentialTargets.Any(t => current.IsAdjacent(t)))
                        current.TryMove(potenentialTargets, board, _combatants);

                    // Check if they can attack
                    Combatant attackTarget = current.FindAttackTarget(potenentialTargets);
                    if (attackTarget != null)
                    {
                        current.Attack(attackTarget);

                        if (!attackTarget.IsAlive)
                        {
                            // Exit early if Part 2 and the dead one is an elf
                            if (failOnElfDeath && attackTarget.Type == 'E')
                                return null;

                            // Otherwise, continue
                            int index = _combatants.IndexOf(attackTarget);
                            _combatants.RemoveAt(index);

                            // Handle i if the dead combatant went already
                            if (index < i)
                                i--;
                        }
                    }
                }

                PrintBoard(board);

                _combatants = _combatants.OrderBy(c => c.Coordinate.Y).ThenBy(c => c.Coordinate.X).ToList();
                round++;
            } while (true);
        }

        private void PrintBoard(char[,] board)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                StringBuilder builder = new StringBuilder();
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    Combatant match = _combatants.FirstOrDefault(c => c.Coordinate.X == x && c.Coordinate.Y == y);
                    if (match != null)
                        builder.Append(match.Type);
                    else
                        builder.Append(board[x, y]);
                }
                Debug.WriteLine(builder.ToString());
            }
        }

        private char[,] ParseBoard(string filePath, int elfPower)
        {
            string line;
            StreamReader file = new StreamReader(filePath);

            List<char[]> temp = new List<char[]>();
            // Iterate over each line in the input
            while ((line = file.ReadLine()) != null)
            {
                temp.Add(line.ToCharArray());
            }
            file.Close();

            char[,] board = new char[temp.Count, temp[0].Length];
            for (int y = 0; y < temp.Count; y++)
            {
                for (int x = 0; x < temp[0].Length; x++)
                {
                    char currentChar = temp[y][x];
                    if (currentChar == 'E' || currentChar == 'G')
                    {
                        Combatant combatant = new Combatant(x, y, currentChar, elfPower);
                        _combatants.Add(combatant);
                        board[x, y] = '.';
                    }
                    else
                    {
                        board[x, y] = temp[y][x];
                    }
                }
            }

            return board;
        }
    }

    public class Combatant
    {
        public Coordinate Coordinate { get; set; }

        public char Type { get; set; }

        public int HitPoints { get; set; }

        public int AttackPower { get; set; }

        public bool IsAlive { get; set; }

        public Combatant(int x, int y, char type, int elfPower)
        {
            Coordinate = new Coordinate(x, y);
            Type = type;
            HitPoints = 200;
            AttackPower = type == 'E' ? elfPower : 3;
            IsAlive = true;
        }

        public bool IsAdjacent(Combatant other)
        {
            return (Math.Abs(Coordinate.X - other.Coordinate.X) + Math.Abs(Coordinate.Y - other.Coordinate.Y)) == 1;
        }

        public Combatant FindAttackTarget(List<Combatant> targets)
        {
            // Find best adjacent target, which is what can be attacked
            // can be null if none
            Combatant bestAdjacent = targets
                    .Where(t => IsAdjacent(t))
                    .OrderBy(t => t.HitPoints)
                    .ThenBy(t => t.Coordinate.Y)
                    .ThenBy(t => t.Coordinate.X)
                    .FirstOrDefault();

            return bestAdjacent;
        }

        public void Attack(Combatant enemy)
        {
            enemy.HitPoints -= AttackPower;
            if (enemy.HitPoints <= 0)
                enemy.IsAlive = false;
        }

        // Important: ordered in reading order
        private static readonly Coordinate[] moveDeltas = { new Coordinate(0, -1), new Coordinate(-1, 0), new Coordinate(1, 0), new Coordinate(0, 1) };

        private bool IsOpen(Coordinate toEvaluate, char[,] board, List<Combatant> combatants)
        {
            bool openOnBoard = board[toEvaluate.X, toEvaluate.Y] == '.';
            bool occupied = combatants.Any(c => c.Coordinate.Equals(toEvaluate));
            return openOnBoard && !occupied;
        }

        public void TryMove(List<Combatant> targets, char[,] board, List<Combatant> allCombatants)
        {
            HashSet<Coordinate> inRange = new HashSet<Coordinate>();
            foreach (Combatant target in targets)
            {
                foreach (Coordinate delta in moveDeltas)
                {
                    Coordinate coord = new Coordinate(target.Coordinate.X + delta.X, target.Coordinate.Y + delta.Y);
                    if (IsOpen(coord, board, allCombatants))
                        inRange.Add(coord);
                }
            }

            Queue<Coordinate> queue = new Queue<Coordinate>();
            Dictionary<Coordinate, Coordinate> prevs = new Dictionary<Coordinate, Coordinate>();
            queue.Enqueue(Coordinate);
            prevs.Add(Coordinate, new Coordinate(-1, -1));
            while (queue.Count > 0)
            {
                Coordinate currentCoord = queue.Dequeue();
                foreach (Coordinate c in moveDeltas)
                {
                    Coordinate newCoord = new Coordinate(currentCoord.X + c.X, currentCoord.Y + c.Y);
                    if (prevs.ContainsKey(newCoord) || !IsOpen(newCoord, board, allCombatants))
                        continue;

                    queue.Enqueue(newCoord);
                    prevs.Add(newCoord, currentCoord);
                }
            }

            List<Path> paths = inRange.Select(t => new Path() { Coordinate = t, PathToThere = GetPath(t, prevs) })
                .Where(t => t.PathToThere != null)
                .OrderBy(t => t.PathToThere.Count)
                .ThenBy(t => t.Coordinate.Y)
                .ThenBy(t => t.Coordinate.X)
                .ToList();

            // Finally move there
            List<Coordinate> bestPath = paths.FirstOrDefault()?.PathToThere;
            if (bestPath != null)
                Coordinate = bestPath[0];
        }

        private List<Coordinate> GetPath(Coordinate destination, Dictionary<Coordinate, Coordinate> prevs)
        {
            if (!prevs.ContainsKey(destination))
                return null;

            List<Coordinate> path = new List<Coordinate>();
            //(int x, int y) = (destX, destY);
            while (destination != Coordinate)
            {
                path.Add(destination);
                destination = prevs[destination];
            }

            path.Reverse();
            return path;
        }

        public override string ToString()
        {
            return $"X: {Coordinate.X}, Y: {Coordinate.Y}, HP: {HitPoints}";
        }
    }

    public class Path
    {
        public Coordinate Coordinate { get; set; }

        public List<Coordinate> PathToThere { get; set; }
    }

    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            CoordinateComparer comparer = new CoordinateComparer();
            return comparer.Equals(this, (Coordinate)obj);
        }

        public override int GetHashCode()
        {
            CoordinateComparer comparer = new CoordinateComparer();
            return comparer.GetHashCode(this);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }

    public class CoordinateComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate c1, Coordinate c2)
        {
            if (c2 == null && c1 == null)
                return true;
            else if (c1 == null | c2 == null)
                return false;
            else if (c1.X == c2.X && c1.Y == c2.Y)
                return true;
            else
                return false;
        }

        public int GetHashCode(Coordinate c)
        {
            return $"{c.X}{c.Y}".GetHashCode();
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018_Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"C:\Users\denus\source\repos\Advent2018_Day15\test.txt");
            char[,,] field = new char[input.Length, input.Length, 5];

            // List the players
            List<Player> players = new List<Player>();

            // Get the field
            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    field[x, y, 0] = line[x];
                    if (field[x, y, 0] == 'E' || field[x, y, 0] == 'G')
                    {
                        Player speler = new Player();
                        speler.team = field[x, y, 0];
                        speler.position = new int[]{ x, y};
                    }
                }
            }

            for (int y = 0; y < field.GetLength(1); y++)
            {
                Console.Write("\n");
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    Console.Write(field[x, y, 0]);
                }
            }
            Console.ReadKey();
        }
    }

    class Player
    {
        public int team;
        public int[] position = new int[2];
        public int HP = 200;
    }
}*/
