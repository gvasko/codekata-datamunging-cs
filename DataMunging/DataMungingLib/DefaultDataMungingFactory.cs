using IDataMunging;
using System.Collections.Generic;
using System.IO;

namespace DataMungingLib
{
    public class DefaultDataMungingFactory : IDataMungingFactory
    {
        public IStringRecord CreateStringRecord(string[] values)
        {
            return new DefaultStringRecord(values);
        }

        public IStringTable CreateStringTable(List<string[]> recordList)
        {
            return new DefaultStringTable(this, recordList);
        }

        public IStringTable CreateStringTable(string[] header, List<string[]> recordList)
        {
            return new DefaultStringTable(this, header, recordList);
        }

        public IStringTableParser CreateStringTableParser(TextReader reader, ILineParser lineParser)
        {
            return new DefaultStringTableParser(this, reader, lineParser);
        }
    }
}
