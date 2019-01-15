using System;
using System.Collections.Generic;
using System.Text;
using AdventUtilities;

namespace Day6
{
    /// <summary>
    /// a model to represent a coordinate
    /// </summary>
    public class CoOrdinate: InputModel
    {
        //the x coord
        public int X { get; set; }

        //the y coord
        public int Y { get; set; }

        //an id for this coordinate
        public int Id { get; set; }

        public CoOrdinate() : this(0, 0)
        {

        }

        public CoOrdinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Calculate the manhattan distance between this and another coordinate
        /// </summary>
        /// <param name="other">the other coord</param>
        /// <returns>the distance</returns>
        public int ManhattanDistance(CoOrdinate other)
        {
            return (Math.Abs(X - other.X) + Math.Abs(Y - other.Y));
        }

        /// <summary>
        /// parse the coordinates line
        /// </summary>
        public override void LoadInput()
        {
            var startIndex = 0;
            var endIndex = Input.IndexOf(',');
            X = int.Parse(Input.Substring(startIndex, endIndex - startIndex));
            startIndex = endIndex + 2;
            Y = int.Parse(Input.Substring(startIndex));
        }
    }

}
