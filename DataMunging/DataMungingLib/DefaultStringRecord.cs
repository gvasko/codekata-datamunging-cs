using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLib
{
    internal class DefaultStringRecord : IStringRecord
    {
        private string[] values;

        public DefaultStringRecord(string[] values)
        {
            this.values = values;
        }

        public string GetField(int i)
        {
            if (i < 0 || i >= values.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return values[i];
        }
    }
}
