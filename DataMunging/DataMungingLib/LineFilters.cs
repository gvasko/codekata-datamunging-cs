using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLib
{
    public sealed class LineFilters
    {
        private LineFilters()
        {
            throw new InvalidOperationException("No instances");
        }

        public static bool EmptyLines(string line)
        {
            return string.IsNullOrWhiteSpace(line);
        }

        public static bool SeparatorLines(string line)
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
    }
}
