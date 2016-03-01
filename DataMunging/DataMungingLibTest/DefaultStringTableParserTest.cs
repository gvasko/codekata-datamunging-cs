using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IDataMunging;
using NSubstitute;
using DataMungingLib;

namespace DataMungingLibTest
{
    [TestClass]
    public class DefaultStringTableParserTest
    {

        // Test if the parser is done and deny any further use

        [TestMethod]
        public void TableParserProcessesEachLine()
        {
            string line1 = "line1";
            string line2 = "line2";
            string line3 = "line3";
            string testDataTable = 
                line1 + Environment.NewLine + 
                line2 + Environment.NewLine + 
                line3 + Environment.NewLine;
            var dataTableReader = new StringReader(testDataTable);
            var spyLineParser = Substitute.For<ILineParser>();
            var dummyFactory = Substitute.For<IDataMungingFactory>();
            IStringTableParser tableParser = new DefaultStringTableParser(dummyFactory, dataTableReader, spyLineParser);

            tableParser.Parse();

            Received.InOrder(() =>
            {
                spyLineParser.Parse(line1);
                spyLineParser.Parse(line2);
                spyLineParser.Parse(line3);
            });

        }

        [TestMethod]
        public void StartingAndEndingWhiteSpacesAreRemoved()
        {
            throw new NotImplementedException();
        }

    }
}
