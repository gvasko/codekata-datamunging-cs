using System;
using System.Collections.Generic;

namespace DataMungingLib
{
    public sealed class LineFilters
    {
        private LineFilters()
        {
            throw new InvalidOperationException("No instances");
        }

        public static bool EmptyLines(int index, string line)
        {
            return string.IsNullOrWhiteSpace(line);
        }

        public static bool SeparatorLines(int index, string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return true;
            }
            string trimmedLine = line.Trim();
            if (Char.IsLetterOrDigit(trimmedLine[0]))
            {
                return false;
            }
            return string.IsNullOrWhiteSpace(trimmedLine.Replace(trimmedLine.Substring(0, 1), ""));
        }

        public class LinesAtIndex
        {
            private List<int> lines;

            public LinesAtIndex(int line)
                : this(new int[] { line })
            {
            }

            public LinesAtIndex(int[] lines)
            {
                this.lines = new List<int>();
                this.lines.AddRange(lines);
            }

            public bool IsSelected(int index, string line)
            {
                return lines.Contains(index);
            }
        }

    }
}
