﻿using IDataMunging;
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
        private List<LineFilterDelegate> lineFilters;

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
            this.lineFilters = new List<LineFilterDelegate>();
        }

        public IStringTable Parse()
        {
            List<string[]> recordList = new List<string[]>();
            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();
                if (!IsExcluded(line))
                {
                    recordList.Add(lineParser.Parse(line));
                }
            }
            return factory.CreateStringTable(recordList);
        }

        private bool IsExcluded(string line)
        {
            foreach (LineFilterDelegate lineFilter in lineFilters)
            {
                if (lineFilter(line))
                {
                    return true;
                }
            }
            return false;
        }

        public IStringTableParser Exclude(LineFilterDelegate filter)
        {
            lineFilters.Add(filter);
            return this;
        }
    }
}
