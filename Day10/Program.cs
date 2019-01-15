using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using AdventUtilities;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var points = Helper.LoadAllFromFile<Vector>(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day10\inputs.txt");

            Console.WriteLine($"Read {points.Count} points from the file.");
            Helper.Pause();

            //as a theory, lets move till there's no negatives
            var minX = points.Min(x => x.X);
            var minY = points.Min(x => x.Y);
            long counter = 0;
            while (minX < 0 || minY < 0)
            {
                points.ForEach(x => x.Move());
                counter++;
                minX = points.Min(x => x.X);
                minY = points.Min(x => x.Y);
                if (counter % 100 == 0)
                {
                    Console.WriteLine($"X: {minX}, Y: {minY}");
                }
            }

            //now let's do some arbitrary amount
            for (int i = 0; i < 40; i++)
            {
                points.ForEach(x => x.Move());
                //found it at 10304
                counter++;
                PrintMessage(points, counter);
            }
            Helper.Pause();
        }

        private static void PrintMessage(List<Vector> points, long timeElapsed)
        {
            var path = @"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day10\outputs\";
            var file = File.CreateText($"{path}{timeElapsed}.txt");

            var gridX = points.Max(x => x.X) + 5;
            var gridY = points.Max(x => x.Y) + 5;

            var x0 = points.Min(x => x.X);
            var y0 = points.Min(x => x.Y);

            var grid = new bool[gridX-x0, gridY-y0];

            points.ForEach(x => grid[x.X-x0, x.Y-y0] = true);

            for (int j = 0; j < gridY-y0; j++)
            {
                for (int i = 0; i < gridX-x0; i++)
                {
                    file.Write(grid[i, j] ? "#" : " ");
                }
                file.WriteLine(string.Empty);
            }
            file.Flush();
            file.Close();

        }
    }
}
