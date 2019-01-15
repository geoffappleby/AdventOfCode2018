using System.Collections.Generic;
using AdventUtilities;

namespace Day3
{
    /// <summary>
    /// Representation of a claim
    /// </summary>
    public class Claim : InputModel
    {
        public int Id { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// a set of claims that have been determined to overlap this one
        /// </summary>
        public List<Claim> OverlappedClaims { get; set; } = new List<Claim>();

        /// <summary>
        /// Parse the input string
        /// </summary>
        /// <param name="input"></param>
        private void ParseInput(string input)
        {
            int startIndex = 1;
            int endIndex = input.IndexOf(' ', startIndex);

            string temp = input.Substring(startIndex, endIndex - startIndex);
            Id = int.Parse(temp);

            startIndex = endIndex + 3;
            endIndex = input.IndexOf(',', startIndex);

            temp = input.Substring(startIndex, endIndex - startIndex);
            Left = int.Parse(temp);

            startIndex = endIndex + 1;
            endIndex = input.IndexOf(':', startIndex);

            temp = input.Substring(startIndex, endIndex - startIndex);
            Top = int.Parse(temp);

            startIndex = endIndex + 2;
            endIndex = input.IndexOf('x', startIndex);

            temp = input.Substring(startIndex, endIndex - startIndex);
            Width = int.Parse(temp);

            startIndex = endIndex + 1;

            temp = input.Substring(startIndex);
            Height = int.Parse(temp);
        }

        /// <summary>
        /// Pretty stringified output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, Left: {Left}, Top: {Top}, Width: {Width}, Height: {Height}";
        }

        /// <summary>
        /// Mark our coordinates in the grid
        /// </summary>
        /// <param name="commonSquares">The grid</param>
        public void DrawThySelf(int[,] commonSquares)
        {
            for (int i = Left; i < Left + Width; i++)
            {
                for (int j = Top; j < Top + Height; j++)
                {
                    commonSquares[i, j]++;
                }
            }

        }

        /// <summary>
        /// Search the grid for ourself, checking to see we don't overlap with anyone
        /// </summary>
        /// <param name="commonSquares">the grid</param>
        /// <returns>true if there's no overlap</returns>
        public bool FindThySelf(int[,] commonSquares)
        {
            for (int i = Left; i < Left + Width; i++)
            {
                for (int j = Top; j < Top + Height; j++)
                {
                    if (commonSquares[i, j] != 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Load our input
        /// </summary>
        public override void LoadInput()
        {
            ParseInput(Input);
        }
    }
}
