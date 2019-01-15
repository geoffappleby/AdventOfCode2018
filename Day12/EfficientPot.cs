using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    public class Pot
    {
        private bool plant = false;
        public bool Plant
        {
            get => plant;
            set
            {
                plant = value;
                GenerationResult = plant;
            }
        }

        public long Index { get; set; }
        public Pot Next { get; set; }
        public Pot Previous { get; set; }
        public bool CanGenerate => (Next != null && Previous != null && Next.Next != null && Previous.Previous != null);
        public bool GenerationResult { get; set; }


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

        public void Reset()
        {
            Plant = GenerationResult;
        }

    }
}
