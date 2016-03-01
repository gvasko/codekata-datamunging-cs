using IDataMunging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLib
{
    internal class DefaultStringTableParser : IStringTableParser
    {
        private TextReader reader;
        private ILineParser lineParser;
        private IDataMungingFactory factory;

        public DefaultStringTableParser(IDataMungingFactory factory, TextReader reader, ILineParser lineParser)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (lineParser == null)
            {
                throw new ArgumentNullException("lineParser");
            }

            this.factory = factory;
            this.reader = reader;
            this.lineParser = lineParser;
        }

        public IStringTable Parse()
        {
            List<string[]> recordList = new List<string[]>();
            while (reader.Peek() >= 0)
            {
                recordList.Add(lineParser.Parse(reader.ReadLine()));
            }
            return factory.CreateStringTable(recordList);
        }
    }
}
