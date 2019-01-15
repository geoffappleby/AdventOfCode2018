using System;
using System.Collections.Generic;
using AdventUtilities;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.LoadAllFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day16\inputs.txt");
            var samples = new List<Sample>();

            for (var i = 0; i < data.Count; i += 4)
            {
                samples.Add(new Sample(data[i], data[i + 1], data[i + 2]));
            }

            foreach (var sample in samples)
            {
                var numMatches = RunAll(sample);
            }

        }

        private static int RunAll(Sample sample)
        {

        }

}
}
