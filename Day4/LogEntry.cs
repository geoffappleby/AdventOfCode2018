using System;
using System.Collections.Generic;
using System.Text;
using AdventUtilities;

namespace Day4
{
    public class LogEntry : InputModel, IComparable
    {
        public DateTime EntryDate { get; set; }
        public LogType Entry { get; set; }
        public int GuardId { get; set; }

        public string Line { get; set; }

        /// <summary>
        /// Make a pretty string output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"[{EntryDate}] ");
            switch (Entry)
            {
                case LogType.Guard:
                    sb.Append($"Guard {GuardId} starts");
                    break;
                case LogType.Sleep:
                    sb.Append("sleeps");
                    break;
                case LogType.Wake:
                    sb.Append("wakes");
                    break;
            }

            return sb.ToString();

        }

        /// <summary>
        /// Read the log line input
        /// </summary>
        /// <param name="line"></param>
        private void ParseLine(string line)
        {
            Line = line;
            int startIndex = 1;
            int endIndex = line.IndexOf(']', startIndex);

            EntryDate = DateTime.Parse(line.Substring(startIndex, endIndex - startIndex));
            startIndex = endIndex + 2;
            string remainder = line.Substring(startIndex);
            if (remainder == "falls asleep")
            {
                Entry = LogType.Sleep;
            }
            else if (remainder == "wakes up")
            {
                Entry = LogType.Wake;
            }
            else
            {
                startIndex += 7;
                endIndex = line.IndexOf(' ', startIndex);
                GuardId = int.Parse(line.Substring(startIndex, endIndex - startIndex));
            }
        }

        /// <summary>
        /// Compare this entry with another
        /// </summary>
        /// <param name="obj">The other entry</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return CompareTo(obj as LogEntry);
        }

        /// <summary>
        /// Compare this entry with another
        /// </summary>
        /// <param name="otherLog">The other entry</param>
        /// <returns></returns>
        public int CompareTo(LogEntry otherLog)
        {
            if (otherLog == null)
            {
                return 1;
            }

            return this.EntryDate.CompareTo(otherLog.EntryDate);
        }

        public override void LoadInput()
        {
            ParseLine(Input);
        }
    }
}
