using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventUtilities;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
             * Some valid values
             * Fuel cell at  3,5, grid serial number 8: power level 4.
             * Fuel cell at  122,79, grid serial number 57: power level -5.
             * Fuel cell at 217,196, grid serial number 39: power level  0.
             * Fuel cell at 101,153, grid serial number 71: power level  4.
             * Goal to run: serial 3031
             *
             */
            var examples = new List<(int x, int y, int s, int p)>
            {
                (3, 5, 8, 4),
                (122, 79, 57, -5),
                (217, 196, 39, 0),
                (101, 153, 71, 4),
            };

            foreach (var valueTuple in examples)
            {
                Console.WriteLine($"Testing power algorithm for cell ({valueTuple.x},{valueTuple.y}), using serial {valueTuple.s}. Expecting a power level of {valueTuple.p}");
                var power = GetPowerLevel(valueTuple.x, valueTuple.y, valueTuple.s);
                Console.WriteLine($"The power level calculated was {power}.");
                if (power != valueTuple.p)
                {
                    var oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR. THIS DOES NOT MATCH.");
                    Console.ForegroundColor = oldColor;
                }
            }

            //now we've run our tests, we need to do a full set of calculations for a 300x300 grid
            var cells = new int[300, 300];
            //walk each x and y
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 300; j++)
                {
                    cells[i, j] = GetPowerLevel(i + 1, j + 1, 3031);
                }
            }

            //sum up each set of 9
            var cellSets = new int[297, 297];
            for (int i = 0; i < 297; i++)
            {
                for (int j = 0; j < 297; j++)
                {
                    cellSets[i, j] = SumPowerLevels(cells, i, j, 3);
                }
            }

            //find the max
            int? max = null;
            (int x, int y) maxLocation = (0,0);
            for (int i = 0; i < 297; i++)
            {
                for (int j = 0; j < 297; j++)
                {
                    if (max == null || max < cellSets[i, j])
                    {
                        max = cellSets[i, j];
                        maxLocation.x = i;
                        maxLocation.y = j;
                    }
                }
            }
            Console.WriteLine($"The biggest one appears to be {max}, at {maxLocation.x + 1},{maxLocation.y + 1}");
            Helper.Pause();

            //ok, part 2 time. genericise the 3x3 grid to any size, and calculate for all of them


            max = null;
            maxLocation = (0, 0);
            var theGridSize = 0;
            for (int gridSize = 1; gridSize <= 300; gridSize++)
            {
                Console.WriteLine($"Processing grid size of {gridSize}x{gridSize}");
                cellSets = new int[300 - gridSize, 300 - gridSize];
                for (int i = 0; i < 300-gridSize; i++)
                {
                    for (int j = 0; j < 300-gridSize; j++)
                    {
                        cellSets[i, j] = SumPowerLevels(cells, i, j, gridSize);
                    }
                }
                Console.WriteLine($"   Scanning for a big one");
                for (int i = 0; i < 300 - gridSize; i++)
                {
                    for (int j = 0; j < 300 - gridSize; j++)
                    {
                        if (max == null || max < cellSets[i, j])
                        {
                            max = cellSets[i, j];
                            maxLocation.x = i;
                            maxLocation.y = j;
                            theGridSize = gridSize;
                        }
                    }
                }
            }

            Console.WriteLine($"The biggest one appears to be {max}, at {maxLocation.x + 1},{maxLocation.y + 1} with a grid size of {theGridSize}");

            Helper.Pause();
        }

        private static int SumPowerLevels(int[,] cells, int i, int j, int gridSize)
        {
            var total = 0;
            for (int x = i; x < i + gridSize; x++)
            {
                for (int y = j; y < j + gridSize; y++)
                {
                    total += cells[x, y];
                }
            }

            return total;
        }

        private static int GetPowerLevel(int x, int y, int serial)
        {
            /*
                Find the fuel cell's rack ID, which is its X coordinate plus 10.
                Begin with a power level of the rack ID times the Y coordinate.
                Increase the power level by the value of the grid serial number (your puzzle input).
                Set the power level to itself multiplied by the rack ID.
                Keep only the hundreds digit of the power level (so 12345 becomes 3; numbers with no hundreds digit become 0).
                Subtract 5 from the power level.
            */
            var rackId = x + 10;
            var power = rackId * y;
            power += serial;
            power = power * rackId;
            power = GetHundredsColumn(power);
            power -= 5;
            return power;
        }

        private static int GetHundredsColumn(int power)
        {
            if (power < 100)
            {
                return 0;
            }

            if (power < 1000)
            {
                return power / 100;
            }

            var str = power.ToString();
            var ch = str.Substring(str.Length - 3, 1);
            return int.Parse(ch);
        }
    }
}
