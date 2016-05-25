using IDataMunging;
using System.Text;

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
