using System.Collections.Generic;
using System.IO;

namespace IDataMunging
{
    public interface IDataMungingFactory
    {
        IStringRecord CreateStringRecord(string[] values);
        IStringTable CreateStringTable(List<string[]> recordList);
        IStringTable CreateStringTable(string[] header, List<string[]> recordList);
        IStringTableParser CreateStringTableParser(TextReader reader, ILineParser lineParser);
    }
}
