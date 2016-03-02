using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLib.LineParsers
{
    public class SeparatedValuesParser : ILineParser
    {
        private char[] separators;
        public SeparatedValuesParser(string separators)
        {
            this.separators = separators.ToCharArray();
        }

        public string[] Parse(string line)
        {
            string[] rawSeparation = line.Split(separators);
            List<string> cleanSeparation = new List<string>(rawSeparation.Length);

            foreach (string item in rawSeparation)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    cleanSeparation.Add(item);
                }
            }

            return cleanSeparation.ToArray();
        }
    }
}
