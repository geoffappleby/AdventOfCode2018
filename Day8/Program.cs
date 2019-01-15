using System;
using AdventUtilities;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.LoadFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode\Day8\inputs.txt");
            var licenseData = data.Split(' ');

            int currentIndex = 0;
            int nextIndex = 0;
            var parentNode = new Node(nextIndex);
            nextIndex++;

            //load the top level node.
            parentNode.Load(licenseData, ref currentIndex, ref nextIndex);

            //and we're done
            parentNode.Print(0);

            Helper.Pause();

            //now lets find all the metadata and sum it
            var checksum = parentNode.GetChecksum();
            Console.WriteLine($"Checksum = {checksum}");
            Helper.Pause();

            //now let's get the value
            var value = parentNode.GetValue();

            Console.WriteLine($"Value = {value}");
            Helper.Pause();
        }
    }
}
