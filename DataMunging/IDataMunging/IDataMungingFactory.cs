using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataMunging
{
    public interface IDataMungingFactory
    {
        IStringRecord CreateStringRecord(string[] values);
        IStringTable CreateStringTable(List<string[]> recordList);
        IStringTableParser CreateStringTableParser(TextReader reader, ILineParser lineParser);
    }
}
