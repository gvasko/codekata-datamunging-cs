using IDataMunging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole
{
    public interface IDataMungingFactory
    {
        StreamReader CreateStreamReader(string path);
        IStringTableParser CreaterStringTableParser();
    }
}
