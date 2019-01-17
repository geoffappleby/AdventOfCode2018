using System;
using System.Collections.Generic;
using System.Text;

namespace Day19
{
    public class OpCode
    {
        public Command Command { get; set; } = Command.noop;

        public override string ToString()
        {
            return $"{Command}";
        }

    }
}
