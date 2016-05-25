using System;
using System.Collections.Generic;
using IDataMunging;

namespace DataMungingConsole.Processing
{
    internal class IntOperationLookup : IStringRecordProcessor
    {
        public delegate int IntOperation(int param1, int param2);
        public delegate bool IsPreferred(int value, int currentPreferredValue);

        private int resultIndex;
        private int columnIndex1;
        private int columnIndex2;

        private string result;
        private int currentPreferredValue;
        private bool preferredInitialized;

        private List<ParserFixerDelegate> fixers;

        private IntOperation intOperation;
        private IsPreferred isPreferred;

        public IntOperationLookup(IntOperation op, IsPreferred pred, int resultIndex, int columnIndex1, int columnIndex2)
        {
            this.resultIndex = resultIndex;
            this.columnIndex1 = columnIndex1;
            this.columnIndex2 = columnIndex2;
            this.intOperation = op;
            this.isPreferred = pred;
            this.result = string.Empty;
            this.currentPreferredValue = 0;
            this.preferredInitialized = false;
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

            int value = intOperation(param1, param2);

            if (!preferredInitialized || isPreferred(value, currentPreferredValue))
            {
                preferredInitialized = true;
                currentPreferredValue = value;
                result = rec.GetField(resultIndex);
            }
        }
/*
        private static int intOperation(int param1, int param2)
        {
            return Math.Abs(param1 - param2);
        }

        private static bool isPreferred(int value, int currentPreferredValue)
        {
            return value < currentPreferredValue;
        }
*/
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
