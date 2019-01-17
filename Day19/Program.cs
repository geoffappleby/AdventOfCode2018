using System;
using System.Collections.Generic;
using AdventUtilities;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.LoadAllFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode2018\Day19\inputs.txt");
            var executionRegisters = new Registers();

            //ip
            executionRegisters.BindInstructionPointer(int.Parse(data[0].Substring(4)));

            var execution = new List<Instruction>();
            var ipValue = 0;
            foreach (var row in data)
            {
                if (!row.StartsWith('#'))
                {
                    var bits = row.Split(' ');
                    var instruction = new Instruction
                    {
                        InputA = Convert.ToInt32((bits[1].Trim())),
                        InputB = Convert.ToInt32((bits[2].Trim())),
                        OutputC = Convert.ToInt32((bits[3].Trim())),
                        Index = ipValue
                    };
                    instruction.OpCode.Command = Enum.Parse<Command>(bits[0]);

                    execution.Add(instruction);
                    ipValue++;
                }
            }

            executionRegisters.Register0.Value = 1;
            RunExecution(execution, executionRegisters);

            Console.WriteLine($"{executionRegisters}");
            Helper.Pause();
        }

        private static void RunExecution(List<Instruction> execution, Registers executionRegisters)
        {
            var currentInstruction = 0;
            var executor = new Executor();
            var c = 0;

            Console.WriteLine($"Starting: [{DateTime.Now}]");
            //the program ends when the instruction pointer points at an instruction outside the bounds of the program
            while (currentInstruction >= 0 && currentInstruction < execution.Count && c < 300)
            {
                if (currentInstruction == 1)
                {
                    //we've now loaded a really big number into register 4, and the real work begins.
                    //so the rest of the app is just calculating the sum of the factors of that number.
                    //for part 2 that number is 10551347
                    //for part 1 it's 947
                    //part 1 executes fine. 
                    //for part 2 i stopped here and went to wolphram alpha
                }


                //each iteration is:
                //1: set the current instruction pointer value to the bound register
                executionRegisters.InstructionPointer.Value = currentInstruction;
                //2: run the instruction at that index
                //if (c % 1000000 == 0)
                //{
                    Console.WriteLine($"{executionRegisters}");
                //}
                executor.ExecuteInstruction(execution[currentInstruction], executionRegisters);
                //3: update the instruction pointer with the value in the bound register
                //if (c % 1000000 == 0)
                //{
                    Console.WriteLine($"[{execution[currentInstruction]}] - ci = {currentInstruction}");
                    Console.WriteLine($"{executionRegisters}");
                    Console.WriteLine($"----------");
                //}

                currentInstruction = executionRegisters.InstructionPointer.Value;
                //4: increment the instruction pointer.
                //if (c % 1000000 == 0 && c != 0)
                //{
                //    Console.Write(".");
                //}
                //if (c % 80000000 == 0 && c != 0)
                //{
                //    Console.WriteLine($" [{c}]");
                //}

                currentInstruction++;
                c+=1;
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine($"Execution complete after {c} instructions executed. [{DateTime.Now}]");

        }
    }
}
