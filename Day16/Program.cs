using System;
using System.Collections.Generic;
using System.Linq;
using AdventUtilities;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Helper.LoadAllFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode2018\Day16\inputs.txt");
            var unknownSamples = new List<Sample>();
            var knownSamples = new List<Sample>();
            var knownOpCodes = new List<OpCode>();

            for (var i = 0; i < data.Count; i += 4)
            {
                unknownSamples.Add(new Sample(data[i], data[i + 1], data[i + 2]));
            }

            while (unknownSamples.Any())
            {
                Console.WriteLine($"{unknownSamples.Count} unknown, {knownSamples.Count} known, {knownOpCodes.Count} discovered opCodes");
                TestAllSamples(unknownSamples, knownSamples);
                IdentifySingles(unknownSamples, knownSamples, knownOpCodes);
                Console.WriteLine($"{unknownSamples.Count} unknown, {knownSamples.Count} known, {knownOpCodes.Count} discovered opCodes");
                Console.WriteLine(string.Empty);
            }

            Helper.Pause();

            data = Helper.LoadAllFromFile(@"C:\Users\geoff.appleby\source\repos\AdventOfCode2018\Day16\inputs2.txt");
            var execution = new List<Instruction>();
            foreach (var row in data)
            {
                var instruction = new Instruction();

                //13 0 0 3
                var bits = row.Split(' ');
                instruction.OpCode.Code = Convert.ToInt32((bits[0].Trim()));
                instruction.OpCode.Command = knownOpCodes.Find(x => x.Code == instruction.OpCode.Code).Command;
                instruction.InputA = Convert.ToInt32((bits[1].Trim()));
                instruction.InputB = Convert.ToInt32((bits[2].Trim()));
                instruction.OutputC = Convert.ToInt32((bits[3].Trim()));

                execution.Add(instruction);
            }

            var executionRegisters = new Registers();
            RunExecution(execution, executionRegisters);

            Console.WriteLine($"{executionRegisters}");
            Helper.Pause();
        }

        private static void RunExecution(List<Instruction> execution, Registers executionRegisters)
        {
            Executor ex = new Executor();
            foreach (var e in execution)
            {
                ex.ExecuteInstruction(e, executionRegisters);
            }
        }

        private static void IdentifySingles(List<Sample> unknownSamples, List<Sample> knownSamples, List<OpCode> knownOpCodes)
        {
            foreach (var s in unknownSamples)
            {
                if (s.Matches.Count == 1)
                {
                    s.Instruction.OpCode.Command = s.Matches[0];
                    if (!knownSamples.Contains(s))
                    {
                        knownSamples.Add(s);
                    }

                    if (knownOpCodes.All(x => x.Command != s.Instruction.OpCode.Command))
                    {
                        knownOpCodes.Add(s.Instruction.OpCode);
                    }
                }
            }

            foreach (var o in knownOpCodes)
            {
                if (unknownSamples.Any(x => x.Instruction.OpCode.Code == o.Code))
                {
                    foreach (var s in unknownSamples.Where(y => y.Instruction.OpCode.Code == o.Code))
                    {
                        s.Instruction.OpCode.Command = o.Command;
                        if (!knownSamples.Contains(s))
                        {
                            knownSamples.Add(s);
                        }
                    }
                }
            }

            foreach (var s in knownSamples)
            {
                if (unknownSamples.Contains(s))
                {
                    unknownSamples.Remove(s);
                }
            }
        }

        private static void TestAllSamples(List<Sample> unknownSamples, List<Sample> knownSamples)
        {
            var identifiedOpCodes = knownSamples.Select(x => x.Instruction.OpCode).Distinct();
            foreach (var sample in unknownSamples)
            {
                RunAll(sample, identifiedOpCodes);
            }
        }

        private static void RunAll(Sample sample, IEnumerable<OpCode> ignoredCodes)
        {
            Executor ex = new Executor();

            sample.Matches.Clear();
            foreach (var command in Helper.GetValues<Command>())
            {
                if (ignoredCodes.All(x => x.Command != command))
                {
                    var ret = ex.Execute(command, sample);
                    if (sample.After.Equals(ret))
                    {
                        sample.Matches.Add(command);
                    }
                }
            }
        }

        private static OpCode FindCode(Sample sample, List<OpCode> ignoredCodes)
        {
            Executor ex = new Executor();
            var code = new OpCode();

            foreach (var command in Helper.GetValues<Command>())
            {
                if (ignoredCodes.All(x => x.Command != command))
                {

                    var ret = ex.Execute(command, sample);
                    if (sample.After.Equals(ret))
                    {
                        code.Command = command;
                        code.Code = sample.Instruction.OpCode.Code;
                        return code;
                    }
                }
                else
                {
                    code.Command = command;
                    code.Code = sample.Instruction.OpCode.Code;
                    return code;
                }
            }
            return null;
        }

    }
}
