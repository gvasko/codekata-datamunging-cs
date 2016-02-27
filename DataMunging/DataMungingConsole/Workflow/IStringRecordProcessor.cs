using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole
{
    public interface IStringRecordProcessor : IStringRecordVisitor
    {
        string Result { get; }
    }
}
