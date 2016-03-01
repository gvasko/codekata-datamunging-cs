using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLib
{
    internal class DefaultStringTable : IStringTable
    {

        private List<IStringRecord> records;

        public DefaultStringTable(IDataMungingFactory factory, List<string[]> recordList)
        {
            this.records = new List<IStringRecord>(recordList.Count);
            foreach (string[] rec in recordList)
            {
                records.Add(factory.CreateStringRecord(rec));
            }
        }

        public void VisitAllRecords(IStringRecordVisitor visitor)
        {
            foreach (IStringRecord rec in records)
            {
                visitor.Visit(rec);
            }
        }
    }
}
