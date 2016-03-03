using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Workflow
{
    public delegate string ParserFixerDelegate(int column, string original);

    public interface IStringRecordProcessor : IStringRecordVisitor
    {
        void AddFixer(ParserFixerDelegate fixer);
        string Result { get; }
    }
}
