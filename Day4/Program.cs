using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AdventUtilities;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {

            var logs = Helper.LoadAllFromFile<LogEntry>(@"..\..\..\inputs.txt");

            Console.WriteLine($"{logs.Count} logs read.");
            Helper.Pause();

            //sort all the logs into chronological order
            logs.Sort();

            //now update all the entries so that they're all marked with the correct guard id
            int currentId = -1;
            foreach (var log in logs)
            {
                if (log.Entry == LogType.Guard)
                {
                    currentId = log.GuardId;
                }
                else
                {
                    log.GuardId = currentId;
                }
            }

            Helper.Pause();

            //group by guard id
            var groupedEntries = new Dictionary<int, LogSet>();
            foreach (var entry in logs)
            {
                if (entry.Entry == LogType.Guard && !groupedEntries.ContainsKey(entry.GuardId))
                {
                    groupedEntries.Add(entry.GuardId, new LogSet(entry.GuardId));
                }
                groupedEntries[entry.GuardId].Add(entry);
                
            }

            //now calculate how long they slept for
            foreach (var entry in groupedEntries.Keys)
            {
                var logSet = groupedEntries[entry];
                LogEntry prev = null;
                foreach (var subEntry in logSet)
                {
                    if (prev != null)
                    {
                        if ((prev.Entry == LogType.Sleep) && (subEntry.Entry == LogType.Wake))
                        {
                            //calculate minutes
                            var minutes = subEntry.EntryDate.Subtract(prev.EntryDate);
                            logSet.TotalSleepTime += minutes.Minutes;
                        }
                    }

                    prev = subEntry;
                }
            }

            //now find who slept the longest
            int id = -1;
            int maxSleep = -1;

            foreach (var entry in groupedEntries.Keys)
            {
                var logSet = groupedEntries[entry];
                if (logSet.TotalSleepTime > maxSleep)
                {
                    maxSleep = logSet.TotalSleepTime;
                    id = entry;
                }
            }

            Console.WriteLine($"Guard {id} slept for {maxSleep} minutes");

            var sleepiestGuard = groupedEntries[id];
            int maxMinuteIndex = sleepiestGuard.FindMaxMinute();

            Console.WriteLine($"Index of max minute was {maxMinuteIndex} - had a count of {sleepiestGuard.Minutes[maxMinuteIndex]}");

            Helper.Pause();

            foreach (var entry in groupedEntries.Keys)
            {
                groupedEntries[entry].SortMinutes();
            }

            int maxMinuteOfAll = -1;
            LogSet theSet = null;
            int bestIndex = -1;

            for (int i = 0; i < 61; i++)
            {
                int thisMaxMinute = -1;
                LogSet thisSet = null;
                foreach (var entry in groupedEntries.Keys)
                {
                    if (groupedEntries[entry].Minutes[i] > thisMaxMinute)
                    {
                        thisMaxMinute = groupedEntries[entry].Minutes[i];
                        thisSet = groupedEntries[entry];
                    }
                }
                if (thisMaxMinute > maxMinuteOfAll)
                {
                    maxMinuteOfAll = thisMaxMinute;
                    theSet = thisSet;
                    bestIndex = i;
                }
            }

            Console.WriteLine($"Selected guard {theSet.GuardId}, with minute {bestIndex}");
            Console.WriteLine($"Which is {theSet.GuardId * bestIndex}");

            Helper.Pause();
        }




    }
}
