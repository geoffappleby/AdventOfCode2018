using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AdventUtilities;

namespace Day2
{
    /// <summary>
    /// Model class to represent a code
    /// </summary>
    public class Code : InputModel
    {
        //the code
        public string ID { get; set; }
        //flag to show if it has at least one pair of chars the same
        public bool HasTwoCount { get; set; }
        //flag to show if it has at least one triple of charas the same
        public bool HasThreeCount { get; set; }

        //A set of codes similar to this one
        public List<Code> SimilarCodes { get; set; } = new List<Code>();

        /// <summary>
        /// Scan the ID looking for pairs and triples
        /// </summary>
        private void Analyse()
        {
            for (int i = 0; i < ID.Length; i++)
            {
                switch (ID.Count(x => x == ID[i]))
                {
                    case 2:
                        HasTwoCount = true;
                        break;
                    case 3:
                        HasThreeCount = true;
                        break;
                }
            }
        }

        /// <summary>
        /// compare this code with another 
        /// </summary>
        /// <param name="other">the other code</param>
        /// <returns>true if similar</returns>
        public bool CheckSimilar(Code other)
        {
            //for each character in the code
            for (int i = 0; i < ID.Length; i++)
            {
                //construct and test a regex with this current char as a wildcard
                string p = CreatePattern(i);
                Regex r = new Regex(p);
                if (r.IsMatch(other.ID))
                {
                    //save this similar code
                    SimilarCodes.Add(other);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// create a wildcard regex pattern from the current code
        /// </summary>
        /// <param name="index">the character index to make a wildcard</param>
        /// <returns>the regex pattern</returns>
        private string CreatePattern(int index)
        {
            var chars = ID.ToCharArray();
            chars[index] = '.';
            return new string(chars);
        }

        /// <summary>
        /// save and parse our input string
        /// </summary>
        public override void LoadInput()
        {
            SimilarCodes = new List<Code>();
            ID = Input;
            Analyse();
        }
    }

}
