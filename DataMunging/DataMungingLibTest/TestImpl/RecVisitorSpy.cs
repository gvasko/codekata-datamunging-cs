using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLibTest.TestImpl
{
    internal class RecVisitorSpy : IStringRecordVisitor
    {
        private StringBuilder sb = new StringBuilder();

        public void Visit(IStringRecord rec)
        {
            for (int i = 0; i < rec.FieldCount; i++)
            {
                sb.Append(rec.GetField(i));
            }
        }

        public string JoinedValues { get { return sb.ToString();  } }
    }
}
