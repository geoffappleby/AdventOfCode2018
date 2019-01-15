using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day9
{
    /// <summary>
    /// A model to represent a player
    /// </summary>
    public class Player
    {
        public long Number { get; set; }
        public List<Marble> OwnedMarbles { get; set; } = new List<Marble>();

        public long MarbleScore => OwnedMarbles.Sum(x => x.Score);

        public Player(long number)
        {
            Number = number;
        }
    }
}
