using System;
using System.Collections.Generic;
using System.Text;

namespace Day19
{
    public class Registers
    {
        public Register Register0 { get; set; }
        public Register Register1 { get; set; }
        public Register Register2 { get; set; }
        public Register Register3 { get; set; }
        public Register Register4 { get; set; }
        public Register Register5 { get; set; }

        public Register InstructionPointer { get; set; }

        public Registers()
        {
            Register0 = new Register("0", 0);
            Register1 = new Register("1", 0);
            Register2 = new Register("2", 0);
            Register3 = new Register("3", 0);
            Register4 = new Register("4", 0);
            Register5 = new Register("5", 0);

            InstructionPointer = Register0;
        }

        public int GetValueFromRegister(int register)
        {
            switch (register)
            {
                case 0:
                    return Register0.Value;
                case 1:
                    return Register1.Value;
                case 2:
                    return Register2.Value;
                case 3:
                    return Register3.Value;
                case 4:
                    return Register4.Value;
                case 5:
                    return Register5.Value;
            }

            return -1;
        }

        public void SetValueInRegister(int register, int value)
        {
            switch (register)
            {
                case 0:
                    Register0.Value = value;
                    break;
                case 1:
                    Register1.Value = value;
                    break;
                case 2:
                    Register2.Value = value;
                    break;
                case 3:
                    Register3.Value = value;
                    break;
                case 4:
                    Register4.Value = value;
                    break;
                case 5:
                    Register5.Value = value;
                    break;

            }
        }

        public void BindInstructionPointer(int register)
        {
            switch (register)
            {
                case 0:
                    InstructionPointer = Register0;
                    break;
                case 1:
                    InstructionPointer = Register1;
                    break;
                case 2:
                    InstructionPointer = Register2;
                    break;
                case 3:
                    InstructionPointer = Register3;
                    break;
                case 4:
                    InstructionPointer = Register4;
                    break;
                case 5:
                    InstructionPointer = Register5;
                    break;
            }
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
                other.Register3.Equals(Register3) &&
                other.Register4.Equals(Register4) &&
                other.Register5.Equals(Register5))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"[{Register0}, {Register1}, {Register2}, {Register3}, {Register4}, {Register5}]";
        }

    }
}
