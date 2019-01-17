using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Day19
{

    public enum Command
    {
        //blank
        noop,
        //addr (add register) stores into register C the result of adding register A and register B.
        addr,
        //addi (add immediate) stores into register C the result of adding register A and value B.
        addi,

        //mulr (multiply register) stores into register C the result of multiplying register A and register B.
        mulr,
        //muli (multiply immediate) stores into register C the result of multiplying register A and value B.
        muli,

        //banr (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
        banr,
        //bani (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
        bani,

        //borr (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
        borr,
        //bori (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
        bori,

        //setr (set register) copies the contents of register A into register C. (Input B is ignored.)
        setr,
        //seti (set immediate) stores value A into register C. (Input B is ignored.)
        seti,

        //gtir (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
        gtir,
        //gtri (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
        gtri,
        //gtrr (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
        gtrr,

        //eqir (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
        eqir,
        //eqri (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
        eqri,
        //eqrr (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.    
        eqrr,

    }

    public class Executor
    {
        private int GetValueFromRegister(int register, Registers registers)
        {
            return registers.GetValueFromRegister(register);
        }

        private void SetValueInRegister(int register, Registers registers, int value)
        {
            registers.SetValueInRegister(register, value);
        }

        public void ExecuteInstruction(Instruction instruction, Registers executionRegisters, Command commandOverride = Command.noop)
        {
            int a, b, c;
            if (commandOverride == Command.noop)
            {
                commandOverride = instruction.OpCode.Command;
            }

            switch (commandOverride)
            {
                //addr (add register) stores into register C the result of adding register A and register B.
                case Command.addr:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    c = a + b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //addi (add immediate) stores into register C the result of adding register A and value B.
                case Command.addi:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = instruction.InputB;
                    c = a + b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //mulr (multiply register) stores into register C the result of multiplying register A and register B.
                case Command.mulr:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    c = a * b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //muli (multiply immediate) stores into register C the result of multiplying register A and value B.
                case Command.muli:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = instruction.InputB;
                    c = a * b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //banr (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
                case Command.banr:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    c = a & b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //bani (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
                case Command.bani:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = instruction.InputB;
                    c = a & b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //borr (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
                case Command.borr:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    c = a | b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //bori (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
                case Command.bori:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = instruction.InputB;
                    c = a | b;
                    SetValueInRegister(instruction.OutputC, executionRegisters, c);
                    break;

                //setr (set register) copies the contents of register A into register C. (Input B is ignored.)
                case Command.setr:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    SetValueInRegister(instruction.OutputC, executionRegisters, a);
                    break;

                //seti (set immediate) stores value A into register C. (Input B is ignored.)
                case Command.seti:
                    a = instruction.InputA;
                    SetValueInRegister(instruction.OutputC, executionRegisters, a);
                    break;

                //gtir (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
                case Command.gtir:
                    a = instruction.InputA;
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    SetValueInRegister(instruction.OutputC, executionRegisters, a > b ? 1 : 0);
                    break;

                //gtri (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
                case Command.gtri:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = instruction.InputB;
                    SetValueInRegister(instruction.OutputC, executionRegisters, a > b ? 1 : 0);
                    break;

                //gtrr (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
                case Command.gtrr:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    SetValueInRegister(instruction.OutputC, executionRegisters, a > b ? 1 : 0);
                    break;

                //eqir (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
                case Command.eqir:
                    a = instruction.InputA;
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    SetValueInRegister(instruction.OutputC, executionRegisters, a == b ? 1 : 0);
                    break;

                //eqri (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
                case Command.eqri:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = instruction.InputB;
                    SetValueInRegister(instruction.OutputC, executionRegisters, a == b ? 1 : 0);
                    break;

                //eqrr (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.    
                case Command.eqrr:
                    a = GetValueFromRegister(instruction.InputA, executionRegisters);
                    b = GetValueFromRegister(instruction.InputB, executionRegisters);
                    SetValueInRegister(instruction.OutputC, executionRegisters, a == b ? 1 : 0);
                    break;

            }
        }
    }
}
