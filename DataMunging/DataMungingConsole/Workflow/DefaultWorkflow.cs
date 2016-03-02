using IDataMunging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Workflow
{
    internal class DefaultWorkflow : ITableLoader, ITableHolder, IOperationExecutor
    {
        private IWorkflowFactory factory;

        #region TableLoader
        private StreamReader reader;
        private IStringTableParser parser;
        #endregion

        #region TableHolder
        private IStringTable table;
        private IStringRecordProcessor recProc;
        #endregion

        public DefaultWorkflow(IWorkflowFactory factory)
        {
            this.factory = factory;
        }

        #region TableLoader

        public ITableLoader EntryPoint(string path)
        {
            reader = factory.CreateStreamReader(path);
            parser = factory.CreaterStringTableParser(reader);
            return this;
        }

        public ITableLoader ExcludeLines(LineFilterDelegate filter)
        {
            parser.Exclude(filter);
            return this;
        }

        public void Dispose()
        {
            reader.Dispose();
        }

        #endregion

        #region TableHolder

        public ITableHolder LoadFile()
        {
            table = parser.Parse();
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

        #endregion

        #region OperationExecutor

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

        #endregion
    }
}
