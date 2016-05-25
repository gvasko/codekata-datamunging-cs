using DataMungingConsole.Processing;
using IDataMunging;
using System.IO;

namespace DataMungingConsole.Workflow
{
    internal class DefaultConsoleWorkflow : IParsingPhase, IConfigurationPhase, IProcessingPhase
    {
        private IWorkflowFactory factory;

        #region ParsingPhase
        private StreamReader reader;
        private IStringTableParser parser;
        #endregion

        #region ConfigurationPhase
        private IStringTable table;
        private IStringRecordProcessor recProc;
        #endregion

        public DefaultConsoleWorkflow(IWorkflowFactory factory)
        {
            this.factory = factory;
        }

        #region ParsingPhase

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

        public IParsingPhase UseFirstRowAsHeader(bool toggle)
        {
            parser.UseFirstRowAsHeader = toggle;
            return this;
        }

        public IConfigurationPhase LoadAndParseFile()
        {
            table = parser.Parse();
            return this;
        }

        #endregion

        #region ConfigurationPhase

        public IProcessingPhase Ready()
        {
            return this;
        }

        public IConfigurationPhase SetProcessor(IStringRecordProcessor recProc)
        {
            this.recProc = recProc;
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
