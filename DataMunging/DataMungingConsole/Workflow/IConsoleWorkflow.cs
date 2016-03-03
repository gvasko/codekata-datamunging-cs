using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Workflow
{
    internal interface IOperationExecutor
    {
        IOperationExecutor Execute();
        string Output { get; }
    }

    internal interface ITableHolder
    {
        ITableHolder SetProcessor(IStringRecordProcessor recProc);
        ITableHolder UseFirstRowAsHeader();
        IOperationExecutor Ready();
    }

    internal interface ITableLoader : IDisposable
    {
        ITableLoader ExcludeLines(LineFilterDelegate filter);
        ITableHolder LoadFile();
    }

}
