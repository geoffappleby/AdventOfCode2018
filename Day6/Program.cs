using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using AdventUtilities;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            //read the coordinates
            var coords = Helper.LoadAllFromFile<CoOrdinate>(@"..\..\..\inputs.txt");

            //set them all with arbitrary ids
            for (int i = 0; i < coords.Count; i++)
            {
                coords[i].Id = i;
            }


            Console.WriteLine($"{coords.Count} coords read.");
            Helper.Pause();

            //what are our bounds? find the max on each axis
            int maxX = 0;
            int maxY = 0;
            foreach (var coord in coords)
            {
                if (coord.X > maxX)
                {
                    maxX = coord.X;
                }

                if (coord.Y > maxY)
                {
                    maxY = coord.Y;
                }
            }


            Console.WriteLine($"X: {maxX}, Y: {maxY}");
            Helper.Pause();

            int bounds = 360;
            int[,] ourSystem = new int[bounds, bounds];

            //walk the grid, calculating stuff
            for (int i = 0; i < bounds; i++)
            {
                for (int j = 0; j < bounds; j++)
                {
                    int closestId = -1;
                    int closestDistance = -1;
                    CoOrdinate c = new CoOrdinate(i, j);
                    //compare every coordinate loaded to here, finding which one is closest
                    foreach (var coord in coords)
                    {
                        int distance = c.ManhattanDistance(coord);
                        if (distance < closestDistance || closestDistance == -1)
                        {
                            closestDistance = distance;
                            closestId = coord.Id;
                        }
                    }

                    //we need to compensate for equidistance. let's run it a second time and see if we can find a second id with the same distance
                    bool collision = false;
                    foreach (var coord in coords)
                    {
                        int distance = c.ManhattanDistance(coord);
                        if (distance == closestDistance && coord.Id != closestId)
                        {
                            collision = true;
                        }
                    }

                    //write the id of the closes coord to this location, unless there was a collision
                    ourSystem[i, j] = collision ? -1 : closestId;
                }
            }

            Helper.Pause();


            //to ignore any infinites, throw away all ids that are on the edge of the system
            var idsToIgnore = new List<int> {-1}; //ignore any collisions right from the start

            //walk the two edges where j = 0  and max
            for (int i = 0; i < bounds; i++)
            {
                if (!idsToIgnore.Contains(ourSystem[i, 0]))
                {
                    idsToIgnore.Add(ourSystem[i, 0]);
                }
                if (!idsToIgnore.Contains(ourSystem[i, bounds - 1]))
                {
                    idsToIgnore.Add(ourSystem[i, bounds - 1]);
                }
            }
            //walk the two edges where i = 0 and max
            for (int j = 0; j < bounds; j++)
            {
                if (!idsToIgnore.Contains(ourSystem[0, j]))
                {
                    idsToIgnore.Add(ourSystem[0, j]);
                }
                if (!idsToIgnore.Contains(ourSystem[bounds - 1, j]))
                {
                    idsToIgnore.Add(ourSystem[bounds - 1, j]);
                }
            }

            //display who we're ignoring
            foreach (var i in idsToIgnore)
            {
                Console.WriteLine($"Ignoring id: {i}");
            }

            Helper.Pause();

            //now walk the grid, counting each id (if it's not in our ignore list)
            var counts = new Dictionary<int, int>();
            for (int i = 1; i < bounds-1; i++)
            {
                for (int j = 1; j < bounds-1; j++)
                {
                    //countable
                    if (!idsToIgnore.Contains(ourSystem[i, j]))
                    {
                        if (!counts.ContainsKey(ourSystem[i, j]))
                        {
                            counts.Add(ourSystem[i,j], 0);
                        }

                        counts[ourSystem[i, j]]++;
                    }
                }
            }

            foreach (var key in counts.Keys)
            {
                Console.WriteLine($"Id: {key}, Count: {counts[key]}");

            }
            Helper.Pause();

            int[,] ourSafeSystem = new int[bounds, bounds];
            int safeDistance = 10000;
            int totalSafeSquares = 0;
            //walk the grid, calculating stuff
            for (int i = 0; i < bounds; i++)
            {
                for (int j = 0; j < bounds; j++)
                {
                    CoOrdinate c = new CoOrdinate(i, j);
                    //compare every coordinate loaded to here, finding which one is closest
                    foreach (var coord in coords)
                    {
                        ourSafeSystem[i,j] += c.ManhattanDistance(coord);
                    }
                    if (ourSafeSystem[i, j] < safeDistance)
                    {
                        totalSafeSquares++;
                        //    Console.WriteLine($"Safe: [{i:000}, {j:000}] = {ourSafeSystem[i,j]}");
                    }
                }
            }
            Console.WriteLine($"There were {totalSafeSquares} squares with a distance under {safeDistance}");
            Helper.Pause();


        }
    }

}
