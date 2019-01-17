using System;
using System.Collections.Generic;
using System.Text;

namespace Day19
{
    public class Instruction
    {
        public OpCode OpCode { get; set; } = new OpCode();
        public int Index { get; set; }
        public int InputA { get; set; }

        public int InputB { get; set; }

        public int OutputC { get; set; }

        public override string ToString()
        {
            return $"[{OpCode} ({InputA}, {InputB}, {OutputC})]";
        }
    }
}
