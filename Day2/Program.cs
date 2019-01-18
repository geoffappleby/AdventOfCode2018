using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using AdventUtilities;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            //load in the stuff
            var codes = Helper.LoadAllFromFile<Code>(@"..\..\..\inputs.txt");

            //magic counting code
            var totalTwos = codes.Count(x => x.HasTwoCount);
            var totalThrees = codes.Count(x => x.HasThreeCount);

            //and the answer is...
            Console.WriteLine($"The checksum of the twos ({totalTwos}) * the threes ({totalThrees}) = {totalTwos * totalThrees} ");

            Helper.Pause();

            //make room to list similar codes
            var matchies = new List<Code>();

            //for every code
            for (var i = 0; i < codes.Count; i++)
            {
                var thisCode = codes[i];
                var added = false;

                //check it against every other code
                for (var j = i+1; j < codes.Count; j++)
                {
                    thisCode.CheckSimilar(codes[j]);
                }

                //save it if there were any similarities
                if (thisCode.SimilarCodes.Any())
                {
                    matchies.Add(thisCode);
                }
            }

            //print out the similar items
            foreach (var match in matchies)
            {
                Console.WriteLine($"Match: {match.ID}");
                foreach (var subMatch in match.SimilarCodes)
                {
                    Console.WriteLine($"          {subMatch.ID}");
                }
            }

            Helper.Pause();

        }

    }
}
