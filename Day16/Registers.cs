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


        public override bool Equals(object obj)
        {
            var other = obj as Registers;
            if (other == null)
            {
                return base.Equals(obj);
            }

            if (other.Register0.Equals(Register0) &&
                other.Register1.Equals(Register1) &&
                other.Register2.Equals(Register2) &&
                other.Register3.Equals(Register3))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"[{Register0}, {Register1}, {Register2}, {Register3}]";
        }

    }
}
