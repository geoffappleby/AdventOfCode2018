using System;
using System.Collections.Generic;
using System.Text;

namespace Day4
{
    /// <summary>
    /// A list of log entries, with extra features
    /// </summary>
    public class LogSet : List<LogEntry>
    {
        //The total time this guard was asleep
        public int TotalSleepTime { get; set; }

        //a count of how many times the guard was asleep, for each minute in the hour
        public int[] Minutes { get; set; } = new int[61];

        public int GuardId { get; set; }

        public LogSet(int guardId)
        {
            GuardId = guardId;
        }

        //update the sleep tracker for each sleeping entry
        public void SortMinutes()
        {
            Minutes = new int[61];
            LogEntry prev = null;
            foreach (var subEntry in this)
            {
                if (prev != null)
                {
                    if ((prev.Entry == LogType.Sleep) && (subEntry.Entry == LogType.Wake))
                    {
                        //calculate minutes
                        for (int i = prev.EntryDate.Minute; i < subEntry.EntryDate.Minute; i++)
                        {
                            Minutes[i]++;
                        }
                    }
                }
                prev = subEntry;
            }
        }

        //Find the index into the minutes array with the highest sleep count
        public int FindMaxMinute()
        {
            this.SortMinutes();
            int maxValue = -1;
            int maxIndex = -1;
            for (int i = 0; i <= 60; i++)
            {
                if (Minutes[i] > maxValue)
                {
                    maxValue = Minutes[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

    }
}
