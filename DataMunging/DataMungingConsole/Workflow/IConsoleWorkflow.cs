using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Workflow
{
    public interface IOperationExecutor
    {
        IOperationExecutor Execute();
        string Output { get; }
    }

    public interface ITableHolder
    {
        ITableHolder SetProcessor(IStringRecordProcessor recProc);
        IOperationExecutor Ready();
    }

    public interface ITableLoader
    {
        ITableHolder LoadFile(string path);
    }

}
