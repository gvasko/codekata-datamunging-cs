using IDataMunging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Workflow
{
    internal class DefaultConsoleWorkflow : IParsingPhase, IConfigurationPhase, IProcessingPhase
    {
        private IWorkflowFactory factory;

        #region FileLoadingPhase
        private StreamReader reader;
        private IStringTableParser parser;
        #endregion

        #region TableParsingPhase
        private IStringTable table;
        private IStringRecordProcessor recProc;
        #endregion

        public DefaultConsoleWorkflow(IWorkflowFactory factory)
        {
            this.factory = factory;
        }

        #region FileLoadingPhase

        public IParsingPhase EntryPoint(string path)
        {
            reader = factory.CreateStreamReader(path);
            parser = factory.CreaterStringTableParser(reader);
            return this;
        }

        public IParsingPhase ExcludeLines(LineFilterDelegate filter)
        {
            parser.Exclude(filter);
            return this;
        }

        public void Dispose()
        {
            reader.Dispose();
        }

        public IConfigurationPhase LoadAndParseFile()
        {
            table = parser.Parse();
            return this;
        }

        #endregion

        #region TableParsingPhase

        public IProcessingPhase Ready()
        {
            return this;
        }

        public IConfigurationPhase SetProcessor(IStringRecordProcessor recProc)
        {
            this.recProc = recProc;
            return this;
        }

        public IConfigurationPhase UseFirstRowAsHeader()
        {
            table.UseFirstRowAsHeader();
            return this;
        }

        #endregion

        #region ProcessingPhase

        public string Output
        {
            get
            {
                return recProc.Result;
            }
        }

        public IProcessingPhase Execute()
        {
            table.VisitAllRecords(recProc);
            return this;
        }

        #endregion
    }
}
