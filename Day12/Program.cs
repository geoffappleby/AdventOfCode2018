using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AdventUtilities;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.LoadFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day12\inputs.txt");

            //construct our first set of pots
            var pots = new List<Pot>();
            var originalPots = pots;

            Pot newPot = null;
            //negative padding
            for (int i = -3; i < 0; i++)
            {
                newPot = new Pot {Index = i, Plant = false};
                if (pots.Any())
                {
                    newPot.Previous = pots.Last();
                    newPot.Previous.Next = newPot;
                }

                pots.Add(newPot);
            }

            //read the plants
            for (int i = 0; i < data.Length; i++)
            {
                newPot = new Pot {Index = i, Plant = data[i] == '#'};
                if (pots.Any())
                {
                    newPot.Previous = pots.Last();
                    newPot.Previous.Next = newPot;
                }
                pots.Add(newPot);
            }

            //positive padding
            for (long i = data.Length; i < data.Length + 10; i++)
            {
                newPot = new Pot {Index = i, Plant = false};
                if (pots.Any())
                {
                    newPot.Previous = pots.Last();
                    newPot.Previous.Next = newPot;
                }
                pots.Add(newPot);
            }

            var rules = Helper.LoadAllFromFile<Rule>(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day12\rules.txt");

            PrintPots(pots, 0);
            long currentGeneration = 0;
            while (currentGeneration < 20)
            {
                currentGeneration++;
                pots.ForEach(x => x.Reset());
                ExecuteGeneration(pots, rules);
                PrintPots(pots, currentGeneration);
            }

            Helper.Pause();
            Console.WriteLine("Now running part 2...");

            //construct our first set of pots
            pots = new List<Pot>();

            //negative padding
            for (int i = -3; i < 0; i++)
            {
                newPot = new Pot { Index = i, Plant = false };
                if (pots.Any())
                {
                    newPot.Previous = pots.Last();
                    newPot.Previous.Next = newPot;
                }

                pots.Add(newPot);
            }

            //read the plants
            for (int i = 0; i < data.Length; i++)
            {
                newPot = new Pot { Index = i, Plant = data[i] == '#' };
                if (pots.Any())
                {
                    newPot.Previous = pots.Last();
                    newPot.Previous.Next = newPot;
                }
                pots.Add(newPot);
            }

            //positive padding
            for (long i = data.Length; i < data.Length + 3; i++)
            {
                newPot = new Pot { Index = i, Plant = false };
                if (pots.Any())
                {
                    newPot.Previous = pots.Last();
                    newPot.Previous.Next = newPot;
                }
                pots.Add(newPot);
            }


            currentGeneration = 10000;
            long currentScore = 530466;
            var file = System.IO.File.CreateText(
                @"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day12\Output\counting.txt");

            while (currentGeneration < 50000000000)
            {
                currentGeneration += 10000;
                currentScore += 530000;
                if (currentGeneration % 500000 == 0)
                //if (currentGeneration == 500000 || currentGeneration == 1000000)
                Console.WriteLine($"Gen {currentGeneration}, Score {currentScore}");
            }

            Helper.Pause();

            //var scores = new List<(long, long)>();

            //while (currentGeneration < 50000000000)
            //{
            //    currentGeneration++;
            //    pots.ForEach(x => x.Reset());
            //    ExecuteGeneration(pots, rules);
            //    if (currentGeneration % 1000 == 0)
            //    {
            //        scores.Add((currentGeneration, pots.Sum(x => x.GenerationResult ? x.Index : 0)));
            //        //Console.WriteLine($"[{DateTime.Now}] Processed {currentGeneration}");
            //        PrintPots(pots, currentGeneration, true);
            //    }
            //    PrintPotsToFile(pots, currentGeneration, file);
            //    if (scores.Count > 30)
            //    {
            //        DumpScores(scores);
            //        break;
            //    }
            //}
            ////Console.WriteLine(string.Empty);
            ////PrintPots(pots, currentGeneration-1);


            //Helper.Pause();
        }

        private static void DumpScores(List<(long generation, long score)> scores)
        {
            var file = File.CreateText(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day12\Output\scores.txt");
            foreach (var valueTuple in scores)
            {
                file.WriteLine($"{valueTuple.generation} - {valueTuple.score}");
            }
            file.Flush();
            file.Close();
        }

        private static void PrintIndexes(List<Pot> pots)
        {
            pots.ForEach(x => Console.Write($"[{x.Index}]"));
            Console.WriteLine(string.Empty);
        }

        private static void ExecuteGeneration(List<Pot> pots, List<Rule> rules)
        {
            //var nextGeneration = new List<Pot>();
            Pot newPot = null;

            Parallel.ForEach(pots, (pot) =>
                    //foreach (var pot in pots)
                {
                    if (pot.CanGenerate)
                    {
                        var matched = false;
                        foreach (var rule in rules)
                        {
                            if (rule.Match(pot))
                            {
                                pot.GenerationResult = rule.Result;
                                matched = true;
                                break;
                            }
                        }

                        if (!matched)
                        {
                            pot.GenerationResult = false;
                        }
                    }
                    else
                    {
                        pot.GenerationResult = pot.Plant;
                    }
                }
            );

            newPot = new Pot { Index = pots[pots.Count - 1].Index + 1, Plant = false };
            newPot.Previous = pots.Last();
            newPot.Previous.Next = newPot;
            pots.Add(newPot);

        }

        private static void PrintPotsToFile(List<Pot> pots, long generation, StreamWriter file)
        {
            file.Write($"{generation:00}: ");
            pots.ForEach(x => file.Write(x.GenerationResult ? "#" : "."));
            var sum = pots.Sum(x => x.GenerationResult ? x.Index : 0);
            file.Write($" (score: {sum})");
            file.WriteLine(string.Empty);
        }

        private static void PrintPots(List<Pot> pots, long generation, bool justScore = false)
        {
            Console.Write($"{generation:00}: ");
            if (!justScore)
            {
                pots.ForEach(x => Console.Write(x.GenerationResult ? "#" : "."));
            }
            var sum = pots.Sum(x => x.GenerationResult ? x.Index : 0);
            Console.Write($" (score: {sum})");
            Console.WriteLine(string.Empty);
        }
    }
}
