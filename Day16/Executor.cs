using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Day16
{

    public enum Command
    {
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
        public Registers Execute(Command command, Sample sample)
        {
            Registers ret = new Registers();
            ret.Register0.Value = sample.Before.Register0.Value;
            ret.Register1.Value = sample.Before.Register1.Value;
            ret.Register2.Value = sample.Before.Register2.Value;
            ret.Register3.Value = sample.Before.Register3.Value;

            switch (command)
            {
                //addr (add register) stores into register C the result of adding register A and register B.
                case Command.addr:
                    var a = 0;
                    //find register a
                    switch (sample.Instruction.InputA)
                    {
                        case 0:
                            a = ret.Register0.Value;
                            break;
                        case 1:
                            a = ret.Register1.Value;
                            break;

                    }
                    break;

                //addi (add immediate) stores into register C the result of adding register A and value B.
                case Command.addi:
                    break;

                //mulr (multiply register) stores into register C the result of multiplying register A and register B.
                case Command.mulr:
                    break;

                //muli (multiply immediate) stores into register C the result of multiplying register A and value B.
                case Command.muli:
                    break;

                //banr (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
                case Command.banr:
                    break;

                //bani (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
                case Command.bani:
                    break;

                //borr (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
                case Command.borr:
                    break;

                //bori (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
                case Command.bori:
                    break;

                //setr (set register) copies the contents of register A into register C. (Input B is ignored.)
                case Command.setr:
                    break;

                //seti (set immediate) stores value A into register C. (Input B is ignored.)
                case Command.seti:
                    break;

                //gtir (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
                case Command.gtir:
                    break;

                //gtri (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
                case Command.gtri:
                    break;

                //gtrr (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
                case Command.gtrr:
                    break;

                //eqir (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
                case Command.eqir:
                    break;

                //eqri (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
                case Command.eqri:
                    break;

                //eqrr (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.    
                case Command.eqrr:
                    break;

            }

            return ret;
        }

        private int GetValueFromRegister(int register, Registers registers)
        {
            switch (register)
            {
                case 0:
                    return = registers.Register0.Value;
                    break;
                case 0:
                    return = registers.Register0.Value;
                    break;
                case 0:
                    return = registers.Register0.Value;
                    break;

            }

        }

    }
}
