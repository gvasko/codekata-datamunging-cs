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
            return string.IsNullOrEmpty(line.Trim());
        }
    }
}
