using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    public class OpCode
    {
        public int Code { get; set; }
        public Command Command { get; set; } = Command.noop;

        public override string ToString()
        {
            return $"0x{Code}";
        }
    }
}
