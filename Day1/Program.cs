using System;
using System.Collections.Generic;
using AdventUtilities;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {

            var lines = Helper.LoadAllFromFile(@"..\..\..\inputs.txt");
            
            int newValue;
            int total = 0;
            var seenFrequencies = new List<int> {total};

            Console.WriteLine("Initialising frequency to 0");

            int duplicate = -1;
            foreach(var line in lines)
            {
                if (int.TryParse(line, out newValue))
                {
                    total += newValue;
                    if (seenFrequencies.Contains(total))
                    {
                        duplicate = total;
                        Console.WriteLine($"Found a duplicate! - {total}");
                    }
                    seenFrequencies.Add(total);
                    Console.WriteLine($"Inputting new frequency of {newValue}. Result frequency is {total}");
                }
            }
            Console.WriteLine($"Final frequency is {total}.");
            Console.WriteLine($"Duplicate is {duplicate}");

            Helper.Pause();

            //lets keep going till we find a duplicate
            int totalIterations = 1;
            while (duplicate == -1)
            {
                foreach(var line in lines)
                {
                    if (int.TryParse(line, out newValue))
                    {
                        total += newValue;
                        if (seenFrequencies.Contains(total))
                        {
                            duplicate = total;
                            Console.WriteLine($"Found a duplicate! - {duplicate}");
                            break;
                        }
                        seenFrequencies.Add(total);
                    }
                }

                totalIterations++;
                Console.WriteLine($"Total iterations so far: {totalIterations}");
            }
            Helper.Pause();
        }


        
    }
}
