﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataMunging;

namespace DataMungingConsole.Workflow
{
    internal class DefaultWorkflowFactory : IWorkflowFactory
    {
        private IDataMungingFactory dmFactory;
        private ILineParser lineParser;

        public DefaultWorkflowFactory(IDataMungingFactory dmFactory, ILineParser lineParser)
        {
            this.dmFactory = dmFactory;
            this.lineParser = lineParser;
        }

        public IStringTableParser CreaterStringTableParser(StreamReader reader)
        {
            return dmFactory.CreateStringTableParser(reader, lineParser);
        }

        public StreamReader CreateStreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
