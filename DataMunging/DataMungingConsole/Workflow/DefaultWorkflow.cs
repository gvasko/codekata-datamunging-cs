using DataMungingConsole.WorkflowBuilder;
using IDataMunging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole
{
    public class DefaultWorkflow : ITableLoader, ITableHolder, IOperationExecutor
    {
        private IDataMungingFactory factory;
        private IStringTable table;
        private IStringRecordProcessor recProc;

        public DefaultWorkflow(IDataMungingFactory factory)
        {
            this.factory = factory;
        }

        public ITableLoader EntryPoint
        {
            get { return this; }
        }

        public ITableHolder LoadFile(string path)
        {
            using (StreamReader reader = factory.CreateStreamReader(path))
            {
                table = factory.CreaterStringTableParser().Parse(reader);
            }
            return this;
        }

        public IOperationExecutor Ready()
        {
            return this;
        }

        public ITableHolder SetProcessor(IStringRecordProcessor recProc)
        {
            this.recProc = recProc;
            return this;
        }

        public string Output
        {
            get
            {
                return recProc.Result;
            }
        }

        public IOperationExecutor Execute()
        {
            table.VisitAllRecords(recProc);
            return this;
        }

    }
}
