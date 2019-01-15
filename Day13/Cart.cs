using System;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
    public class Cart
    {
        public string Id { get; set; }
        public Direction CurrentDirection { get; set; }
        public Turn NextTurn { get; set; }
        public bool Collided { get; set; }
        public int Generation { get; set; } = 0;
        public Track track { get; set; }

        public Cart()
        {
            NextTurn = Turn.Left;
        }

        public void MakeTurn()
        {
            switch (NextTurn)
            {
                case Turn.Left:
                    NextTurn = Turn.Straight;
                    break;
                case Turn.Straight:
                    NextTurn = Turn.Right;
                    break;
                case Turn.Right:
                    NextTurn = Turn.Left;
                    break;
            }

        }
    }
}
