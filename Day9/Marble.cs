using System;
using System.Collections.Generic;
using System.Text;

namespace Day9
{
    /// <summary>
    /// A model to represent a marble
    /// </summary>
    public class Marble
    {
        //value of the marble
        public long Score { get; set; }

        //is this a marble that's a multiple of 23?
        public bool IsScoringMarble => Score % 23 == 0;

        //the next marble in the circle
        public Marble Next { get; set; }

        //the previous marble in the circle
        public Marble Previous { get; set; }

        public Marble(long score)
        {
            Score = score;
            //default next and previous to self. a circle of one.
            Next = Previous = this;
        }

        /// <summary>
        /// Inserts a marble longo the next position
        /// </summary>
        /// <param name="marble"></param>
        public void Insert(Marble marble)
        {
            Next.Previous = marble;
            marble.Next = Next;
            marble.Previous = this;
            Next = marble;
        }

        /// <summary>
        /// Remove the next marble from the circle
        /// </summary>
        /// <returns>The removed marble</returns>
        public Marble RemoveNext()
        {
            var marble = Next;
            marble.Next.Previous = this;
            Next = marble.Next;
            marble.Next = marble.Previous = marble;
            return marble;
        }

    }
}
