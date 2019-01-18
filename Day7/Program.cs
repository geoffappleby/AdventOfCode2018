using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventUtilities;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            //load the instructions
            var stepList = Helper.LoadAllFromFile<StepInstruction>(@"..\..\..\inputs.txt");
            
            //load a dictionary of all the steps, populating all the next pointers
            var steps = new Dictionary<string, Step>();
            foreach (var instruction in stepList)
            {
                if (!steps.ContainsKey(instruction.Name))
                {
                    steps.Add(instruction.Name, new Step { Name = instruction.Name });
                }
                if (!steps.ContainsKey(instruction.NextName))
                {
                    steps.Add(instruction.NextName, new Step { Name = instruction.NextName });
                }
                steps[instruction.Name].Next.Add(steps[instruction.NextName]);
                steps[instruction.NextName].Previous.Add(steps[instruction.Name]);
            }
            Console.WriteLine($"Built tree with {steps.Keys.Count} steps");
            Helper.Pause();

            //sort everything
            foreach (var stepsKey in steps.Keys)
            {
                steps[stepsKey].Next.Sort();
                steps[stepsKey].Previous.Sort();
            }

            //now find the steps which have no previous
            var firstSteps = steps.Values.Where(x => !x.Previous.Any());

            var firstStep = new Step {Name = "."};

            foreach (var step in firstSteps)
            {
                firstStep.Next.Add(step);
            }
            firstStep.Next.Sort();

            Console.WriteLine($"First items located. There's are {firstStep.Next.Count}.");
            foreach (var step in firstStep.Next)
            {
                Console.Write($"[{step.Name}]   ");
            }
            Helper.Pause();

            bool runStepTwo = true;

            if (runStepTwo)
            {
                DoStepTwo(firstStep, steps.Keys.Count);
            }
            else
            {

                var stepSequence = new List<Step>();

                var nextStep = FindNext(firstStep);
                while (nextStep != null)
                {
                    nextStep.Executed = true;
                    stepSequence.Add(nextStep);
                    nextStep = FindNext(firstStep);
                }

                foreach (var step in stepSequence)
                {
                    Console.Write($"{step.Name}");
                }

                Console.WriteLine("");
            }
            Helper.Pause();
        }

        private static void DoStepTwo(Step firstStep, int totalSteps)
        {
            int numWorkers = 5;
            var currentWork = new Step[numWorkers];
            //clear each worker slot
            for (var i = 0; i < currentWork.Length; i++)
            {
                currentWork[i] = null;
            }

            //don't run the placeholder parent node
            firstStep.Executed = true;

            //keep running until all steps have executed
            var completedSteps = new List<Step>();
            int currentTime = 0;
            while(completedSteps.Count < totalSteps)
            {
                //start of a new second. Update anyone currently working.
                for (var i = 0; i < currentWork.Length; i++)
                {
                    if (currentWork[i] != null)
                    {
                        //add a second to the run time
                        currentWork[i].ExecutionTime++;
                        //this worker has finished this step
                        if (currentWork[i].ExecutionTime == currentWork[i].TotalExecutionTime)
                        {
                            currentWork[i].Executed = true;
                            currentWork[i].Executing = false;
                            completedSteps.Add(currentWork[i]);
                            currentWork[i] = null;
                        }
                    }
                }

                //find anyone to work that can
                for (var i = 0; i < currentWork.Length; i++)
                {
                    if (currentWork[i] == null)
                    {
                        currentWork[i] = FindNext2(firstStep);
                        //was work found?
                        if (currentWork[i] != null)
                        {
                            //mark them now running
                            currentWork[i].Executing = true;
                        }
                        else
                        {
                            //No one could be found. stop looking
                            break;
                        }
                    }
                }

                //print where we're at
                Console.WriteLine($"Second {currentTime}.");
                for (var i = 0; i < currentWork.Length; i++)
                {
                    if (currentWork[i] != null)
                    {
                        Console.WriteLine($" - [{currentWork[i].Name}]({currentWork[i].ExecutionTime})");
                    }
                    else
                    {
                        Console.WriteLine($" - [idle]");
                    }
                }

                if (completedSteps.Any())
                {
                    Console.Write($"Complete: ");
                    completedSteps.ForEach(x => Console.Write(x.Name));
                    Console.WriteLine(string.Empty);
                }

                //increment the current time if we're not finished yet
                if (completedSteps.Count < totalSteps)
                {
                    currentTime++;
                }
            }

            Console.WriteLine($"Finished {completedSteps.Count} in {currentTime} seconds");
            Helper.Pause();
        }

        private static Step FindNext2(Step startStep)
        {
            //if any of startStep's previouses haven't executed, then it's not allowed to execute.
            if (!startStep.CanExecute)
            {
                return null;
            }
            //if it's currently executing, then none of the next are allowed to run
            if (startStep.Executing)
            {
                return null;
            }

            //now, if "this" hasn't been executed it, return it
            if (!startStep.Executed)
            {
                return startStep;
            }

            var candidates = new List<Step>();
            foreach (var step in startStep.Next)
            {
                if (!step.Executed && step.CanExecute && !step.Executing)
                {
                    candidates.Add(step);
                }
                else if (!step.Executing)
                {
                    var candidate = FindNext2(step);
                    if (candidate != null)
                    {
                        candidates.Add(candidate);
                    }
                }
            }

            if (candidates.Any())
            {
                candidates.Sort();
                return candidates[0];
            }

            return null;
        }


        private static Step FindNext(Step startStep)
        {
            //if any of startStep's previouses haven't executed, then it's not allowed to execute.
            if (!startStep.CanExecute)
            {
                return null;
            }
            
            //now, if "this" hasn't been executed it, return it
            if (!startStep.Executed)
            {
                return startStep;
            }

            var candidates = new List<Step>();
            foreach (var step in startStep.Next)
            {
                if (!step.Executed && step.CanExecute)
                {
                    candidates.Add(step);
                }
                else
                {
                    var candidate = FindNext(step);
                    if (candidate != null)
                    {
                        candidates.Add(candidate);
                    }
                }
            }

            if (candidates.Any())
            {
                candidates.Sort();
                return candidates[0];
            }

            return null;
        }

        private static void DrawSteps(Step step, int level = 0)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(step.Name);
            foreach (var next in step.Next)
            {
                DrawSteps(next, level+1);
            }
        }
    }
}
