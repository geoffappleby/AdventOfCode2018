using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
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
    }
}
