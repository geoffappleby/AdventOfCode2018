using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    public class OldsPot
    {
        public bool Plant { get; set; }
        public long Index { get; set; }
        public Pot Next { get; set; }
        public Pot Previous { get; set; }
        public bool CanGenerate => (Next != null && Previous != null && Next.Next != null && Previous.Previous != null);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{Index}](");
            sb.Append(Plant ? "#" : ".");
            sb.Append($")");
            return sb.ToString();
        }

        public Pot Copy()
        {
            return new Pot {Index = Index, Plant = Plant};
        }

    }
}
