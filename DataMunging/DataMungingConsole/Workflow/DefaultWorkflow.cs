using DataMungingConsole.WorkflowBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole
{
    public class DefaultWorkflow : ITableLoader, ITableHolder, IOperationExecutor
    {
        public DefaultWorkflow(IDataMungingFactory factory)
        {
            throw new NotImplementedException();
        }

        public ITableLoader EntryPoint
        {
            get { return this; }
        }

        public ITableHolder LoadFile(string path)
        {
            return this;
        }

        public IOperationExecutor Ready()
        {
            return this;
        }

        public ITableHolder SetProcessor(IStringRecordProcessor recProc)
        {
            return this;
        }

        public string Output
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IOperationExecutor Execute()
        {
            return this;
        }

    }
}
