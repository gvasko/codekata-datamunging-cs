using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataMunging;

namespace DataMungingConsole.Processing
{
    internal class LookupMinDiff : IStringRecordProcessor
    {
        private int resultIndex;
        private int columnIndex1;
        private int columnIndex2;

        private string result;
        private int minDiff;

        private List<ParserFixerDelegate> fixers;

        public LookupMinDiff(int resultIndex, int columnIndex1, int columnIndex2)
        {
            this.resultIndex = resultIndex;
            this.columnIndex1 = columnIndex1;
            this.columnIndex2 = columnIndex2;
            this.result = string.Empty;
            this.minDiff = Int32.MaxValue;
            this.fixers = new List<ParserFixerDelegate>();
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
            int param1 = ParseField(rec, columnIndex1);
            int param2 = ParseField(rec, columnIndex2);

            int actualMinDiff = Math.Abs(param1 - param2);

            if (actualMinDiff < minDiff)
            {
                minDiff = actualMinDiff;
                result = rec.GetField(resultIndex);
            }
        }

        private int ParseField(IStringRecord rec, int fieldIndex)
        {
            int param = 0;
            string value = rec.GetField(fieldIndex);
            bool parsed = Int32.TryParse(value, out param);

            foreach (ParserFixerDelegate fixer in fixers)
            {
                parsed = Int32.TryParse(fixer(fieldIndex, value), out param);
                if (parsed)
                {
                    break;
                }
            }

            if (!parsed)
            {
                throw new ArgumentException("Unable to parse data", value);
            }

            return param;
        }

        public void AddFixer(ParserFixerDelegate fixer)
        {
            fixers.Add(fixer);
        }
    }
}
