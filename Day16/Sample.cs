using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Day16
{
    public class Sample
    {
        public Registers Before { get; set; } = new Registers();
        public Instruction Instruction { get; set; } = new Instruction();
        public Registers After { get; set; } = new Registers();
        //public int Matches { get; set; } = 0;
        public List<Command> Matches { get; set; } = new List<Command>();

        public Sample(string before, string instruction, string after)
        {
            //Before: [0, 3, 0, 2]
            var sub = before.Substring(9, before.Length - 9 - 1);
            var bits = sub.Split(',');
            Before.Register0.Value = Convert.ToInt32(bits[0].Trim());
            Before.Register1.Value = Convert.ToInt32(bits[1].Trim());
            Before.Register2.Value = Convert.ToInt32(bits[2].Trim());
            Before.Register3.Value = Convert.ToInt32(bits[3].Trim());

            //13 0 0 3
            bits = instruction.Split(' ');
            Instruction.OpCode.Code = Convert.ToInt32((bits[0].Trim()));
            Instruction.InputA = Convert.ToInt32((bits[1].Trim()));
            Instruction.InputB = Convert.ToInt32((bits[2].Trim()));
            Instruction.OutputC = Convert.ToInt32((bits[3].Trim()));

            //After:  [0, 3, 0, 0]
            sub = after.Substring(9, after.Length - 9 - 1);
            bits = sub.Split(',');
            After.Register0.Value = Convert.ToInt32(bits[0].Trim());
            After.Register1.Value = Convert.ToInt32(bits[1].Trim());
            After.Register2.Value = Convert.ToInt32(bits[2].Trim());
            After.Register3.Value = Convert.ToInt32(bits[3].Trim());
        }
    }
}
