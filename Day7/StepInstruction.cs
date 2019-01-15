using System;
using System.Collections.Generic;
using System.Text;
using AdventUtilities;

namespace Day7
{
    /// <summary>
    /// A model representing one step instruction in a chain of instructions
    /// </summary>
    public class StepInstruction : InputModel
    {
        //The name of this step instruction
        public string Name { get; set; }
        //The name of the next step instruction
        public string NextName { get; set; }

        public override void LoadInput()
        {
            var nameIndex = 5;
            Name = Input.Substring(nameIndex, 1);

            var nextNameIndex = 36;
            NextName = Input.Substring(nextNameIndex, 1);
        }
    }
}
