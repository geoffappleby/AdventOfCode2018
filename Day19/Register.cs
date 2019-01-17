using System;
using System.Collections.Generic;
using System.Text;

namespace Day19
{
    public class Register
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Register(string name)
        {
            Name = name;
        }

        public Register(string name, int value) : this(name)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Register;
            if (other == null)
            {
                return base.Equals(obj);
            }
            return other.Value == Value;
        }
    }
}
