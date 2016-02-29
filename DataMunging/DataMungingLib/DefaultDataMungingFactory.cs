using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataMungingLib
{
    public class DefaultDataMungingFactory : IDataMungingFactory
    {
        public IStringRecord CreateStringRecord(string[] values)
        {
            return new DefaultStringRecord(values);
        }

        public IStringTable CreateStringTable(int columnCount)
        {
            throw new NotImplementedException();
        }

        public IStringTableParser CreateStringTableParser(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
