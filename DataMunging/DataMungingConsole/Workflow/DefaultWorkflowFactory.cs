using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataMunging;

namespace DataMungingConsole.Workflow
{
    public class DefaultWorkflowFactory : IWorkflowFactory
    {
        private IDataMungingFactory dmFactory;

        public DefaultWorkflowFactory(IDataMungingFactory dmFactory)
        {
            this.dmFactory = dmFactory;
        }

        public IStringTableParser CreaterStringTableParser(StreamReader reader)
        {
            return dmFactory.CreateStringTableParser(reader);
        }

        public StreamReader CreateStreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
