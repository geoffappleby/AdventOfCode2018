using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    public class Registers
    {
        public Register Register0 { get; set; }
        public Register Register1 { get; set; }
        public Register Register2 { get; set; }
        public Register Register3 { get; set; }

        public Registers()
        {
            Register0 = new Register("0", 0);
            Register1 = new Register("1", 0);
            Register2 = new Register("2", 0);
            Register3 = new Register("3", 0);
        }
    }
}
