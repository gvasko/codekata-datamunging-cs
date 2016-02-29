using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataMunging;

namespace DataMungingConsole.Workflow
{
    internal class LookupMinDiff : IStringRecordProcessor
    {
        private int resultIndex;
        private int columnIndex1;
        private int columnIndex2;

        private string result;
        private int minDiff;

        public LookupMinDiff(int resultIndex, int columnIndex1, int columnIndex2)
        {
            this.resultIndex = resultIndex;
            this.columnIndex1 = columnIndex1;
            this.columnIndex2 = columnIndex2;
            this.result = string.Empty;
            this.minDiff = Int32.MaxValue;
        }

        public string Result
        {
            get
            {
                return result;
            }
        }

        public void Visit(IStringRecord rec)
        {
            int col1 = ParseField(rec, columnIndex1);
            int col2 = ParseField(rec, columnIndex2);

            int actualMinDiff = Math.Abs(col1 - col2);

            if (actualMinDiff < minDiff)
            {
                minDiff = actualMinDiff;
                result = rec.GetField(resultIndex);
            }
        }

        private int ParseField(IStringRecord rec, int columnIndex)
        {
            int col1 = 0;
            if (!Int32.TryParse(rec.GetField(columnIndex), out col1))
            {
                throw new ArgumentException("Cannot parse parameter", "column1");
            }

            return col1;
        }
    }
}
