using IDataMunging;
using System;
using System.Collections.Generic;

namespace DataMungingLib
{
    internal class DefaultStringTable : IStringTable
    {
        private string[] header;
        private List<IStringRecord> records;
        private int fieldCount;

        public DefaultStringTable(IDataMungingFactory factory, string[] header, List<string[]> recordList)
        {
            this.header = header;
            this.fieldCount = -1;
            this.records = new List<IStringRecord>(recordList.Count);
            for (int i = 0; i < recordList.Count; i++)
            {
                string[] rec = recordList[i];

                if (rec == null)
                {
                    throw new ArgumentNullException(string.Format("Index={0}", i));
                }

                if (this.fieldCount == -1)
                {
                    this.fieldCount = rec.Length;
                }

                if (this.fieldCount != rec.Length)
                {
                    throw new ArgumentException(string.Format("Unexpected lenght of array. Index={0}", i));
                }

                records.Add(factory.CreateStringRecord(rec));
            }
        }

        public DefaultStringTable(IDataMungingFactory factory, List<string[]> recordList)
            :this(factory, null, recordList)
        {
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
