using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using AdventUtilities;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var claims = Helper.LoadAllFromFile<Claim>(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day3\inputs.txt");

            Console.WriteLine($"{claims.Count} claims read.");
            Helper.Pause();

            //render
            var commonSquares = new int[1001,1001];

            //mark all claims on the grid
            foreach (var claimSet in claims)
            {
                claimSet.DrawThySelf(commonSquares);
            }

            //count how many grid squares are populated
            int totalShaded = 0;
            for (int i = 0; i <= 1000; i++)
            {
                for (int j = 0; j <= 1000; j++)
                {
                    if (commonSquares[i, j] >= 2)
                    {
                        totalShaded++;
                    }
                }
            }

            Console.WriteLine($"Total Squares shaded = {totalShaded}");

            Helper.Pause();

            //find any claim that doesn't overlap
            bool isLonely = false;
            foreach (var claimSet in claims)
            {
                isLonely = claimSet.FindThySelf(commonSquares);
                if (isLonely)
                {
                    //winner winner chicken dinner
                    Console.WriteLine($"Found a lonely guy: {claimSet}");
                    break;
                }
            }

            if (!isLonely)
            {
                Console.WriteLine("Didn't find him");
            }

            Helper.Pause();

        }
    }

}
