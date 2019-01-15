using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AdventUtilities;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            //read in the data
            var line = Helper.LoadFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day5\inputs.txt");

            Console.WriteLine($"Polymer read. It's {line.Length} units long..");
            Helper.Pause();

            bool didReact = true;
            string polymer = line;
            string oldPolymer = string.Empty;
            int i = 0;
            //Keep on reacting the polymer until it's stable
            while (didReact)
            {
                (polymer, didReact) = React(polymer);
                //print a dot every 100 reactions
                i++;
                if (i % 100 == 0)
                {
                    Console.Write($".");
                }
            }
            Console.WriteLine($"{Environment.NewLine}Final chain: {polymer}{Environment.NewLine}Length is {polymer.Length}");
            Helper.Pause();

            //strip each letter of the alphabet adn see how it reacts
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            int[] lengths = new int[26];
            for (int j = 0; j < alphabet.Length; j++)
            {
                string newPolymer = Strip(line, alphabet.Substring(j, 1));
                newPolymer = ChainReact(newPolymer);
                lengths[j] = newPolymer.Length;

                Console.WriteLine($"Unit {alphabet.Substring(j, 1)} removed, ultimate length is {lengths[j]}");
            }

            Helper.Pause();

        }

        /// <summary>
        /// Strip a unit from a polymer
        /// </summary>
        /// <param name="input">the polymer</param>
        /// <param name="charToRemove">the unit</param>
        /// <returns></returns>
        private static string Strip(string input, string charToRemove)
        {
            //keep removing until there's no more found
            while (input.Contains(charToRemove, StringComparison.InvariantCultureIgnoreCase))
            {
                input = input.Replace(charToRemove, string.Empty, StringComparison.InvariantCultureIgnoreCase);
            }

            return input;
        }

        /// <summary>
        /// Run the reaction through a polymer
        /// </summary>
        /// <param name="polymer">the polymer</param>
        /// <returns>the stable polymer</returns>
        private static string ChainReact(string polymer)
        {
            bool didReact = true;
            string oldPolymer = string.Empty;
            int i = 0;
            while (didReact)
            {
                (polymer, didReact) = React(polymer);
            }

            return polymer;
        }

        /// <summary>
        /// Runs one reaction on a polymer
        /// </summary>
        /// <param name="polymer"></param>
        /// <returns></returns>
        private static (string newPolymer, bool didReact) React(string polymer)
        {
            for (int i = 0; i < polymer.Length-1; i++)
            {
                if (char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1]))
                {
                    if ((Char.IsUpper(polymer[i]) && char.IsLower(polymer[i + 1])) ||
                        ((Char.IsLower(polymer[i]) && char.IsUpper(polymer[i + 1]))))
                    {
                        StringBuilder sb = new StringBuilder(polymer.Substring(0, i));
                        sb.Append(polymer.Substring(i + 2));
                        return (sb.ToString(), true);
                    }
                }
            }
            return (polymer, false);
        }

    }
}
