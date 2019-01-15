using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day7
{
    public class Step : IComparable
    {
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string Name { get; set; }
        public List<Step> Previous { get; set; } = new List<Step>();
        public List<Step> Next { get; set; } = new List<Step>();
        public bool Executed { get; set; }

        public bool Executing { get; set; }

        public int TotalExecutionTime => 60 + alphabet.IndexOf(Name) + 1;

        public int ExecutionTime { get; set; } = 0;

        public bool CanExecute
        {
            get => Previous.All(x => x.Executed);
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Step);
        }

        public int CompareTo(Step other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Name}");
            sb.Append($" Prev({Previous.Count})");
            if (Previous.Any())
            {
                sb.Append($"[ ");
                Previous.ForEach(x => sb.Append($"{x.Name} "));
                sb.Append("]");
            }

            sb.Append($" Next({Next.Count})");
            if (Next.Any())
            {
                sb.Append($"[ ");
                Next.ForEach(x => sb.Append($"{x.Name} "));
                sb.Append("]");
            }

            return sb.ToString();
        }
    }
}
