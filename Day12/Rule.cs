using System;
using System.Collections.Generic;
using System.Text;
using AdventUtilities;

namespace Day12
{
    public class Rule: InputModel
    {
        public List<bool> Conditions { get; set; } = new List<bool> {false, false, false, false, false};
        public bool Result { get; set; }
        public override void LoadInput()
        {
            for (var i = 0; i < 5; i++)
            {
                Conditions[i] = Input[i] == '#';
            }

            Result = Input[9] == '#';
        }

        public bool Match(Pot p)
        {
            if ((p.Plant == Conditions[2]) &&
                (p.Previous.Plant == Conditions[1]) &&
                (p.Previous.Previous.Plant == Conditions[0]) &&
                (p.Next.Plant == Conditions[3]) &&
                (p.Next.Next.Plant == Conditions[4]))
            {
                return true;
            }

            return false;
        }
    }
}
