using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IDataMunging;
using NSubstitute;
using DataMungingLib;
using System.Collections.Generic;

namespace DataMungingLibTest
{
    [TestClass]
    public class DefaultStringTableParserTest
    {

        // Test if the parser is done and deny any further use

        private string line1;
        private string line2;
        private string line3;
        private string testDataTable;
        private StringReader dataTableReader;
        private ILineParser spyLineParser;
        private IDataMungingFactory spyFactory;

        [TestInitialize]
        public void SetUp()
        {
            line1 = "line1";
            line2 = "";
            line3 = "line3";
            testDataTable =
                line1 + Environment.NewLine +
                line2 + Environment.NewLine +
                line3 + Environment.NewLine;
            dataTableReader = new StringReader(testDataTable);
            spyLineParser = Substitute.For<ILineParser>();
            spyFactory = Substitute.For<IDataMungingFactory>();
        }

        [TestMethod]
        public void TableParserProcessesEachLine()
        {
            IStringTableParser tableParser = new DefaultStringTableParser(spyFactory, dataTableReader, spyLineParser);

            tableParser.Parse();

            Received.InOrder(() =>
            {
                spyLineParser.Parse(Arg.Is<string>(line1));
                spyLineParser.Parse(Arg.Is<string>(line2));
                spyLineParser.Parse(Arg.Is<string>(line3));
            });

            spyFactory.Received().CreateStringTable(Arg.Any<List<string[]>>());
        }

        [TestMethod]
        public void LinesCanBeExcludedFromParsing()
        {
            IStringTableParser tableParser = new DefaultStringTableParser(spyFactory, dataTableReader, spyLineParser);

            LineFilterDelegate emptyLines = (index, line) => string.IsNullOrWhiteSpace(line);
            tableParser.Exclude(emptyLines);
            tableParser.Parse();

            Received.InOrder(() =>
            {
                spyLineParser.Parse(Arg.Is<string>(line1));
                spyLineParser.Parse(Arg.Is<string>(line3));
            });
            spyLineParser.DidNotReceive().Parse(Arg.Is<string>(line2));

            spyFactory.Received().CreateStringTable(Arg.Any<List<string[]>>());
        }

        [TestMethod]
        public void FirstRowCanBeHeader()
        {
            IStringTableParser tableParser = new DefaultStringTableParser(spyFactory, dataTableReader, spyLineParser);
            tableParser.UseFirstRowAsHeader = true;

            LineFilterDelegate emptyLines = (index, line) => string.IsNullOrWhiteSpace(line);
            tableParser.Exclude(emptyLines);
            var table = tableParser.Parse();

            spyLineParser.Received().Parse(Arg.Is<string>(line1));
            spyLineParser.DidNotReceive().Parse(Arg.Is<string>(line2));
            spyLineParser.Received().Parse(line3);

            spyFactory.Received().CreateStringTable(Arg.Any<string[]>(), Arg.Any<List<string[]>>());
        }

        [TestMethod]
        public void LineFiltersGetLinesWithIndices()
        {
            IStringTableParser tableParser = new DefaultStringTableParser(spyFactory, dataTableReader, spyLineParser);

            var spyFilter = Substitute.For<LineFilterDelegate>();
            spyFilter(Arg.Any<int>(), Arg.Any<string>()).Returns(true);
            tableParser.Exclude(spyFilter);
            tableParser.Parse();

            Received.InOrder(() =>
            {
                spyFilter(Arg.Is<int>(0), Arg.Is<string>(line1));
                spyFilter(Arg.Is<int>(1), Arg.Is<string>(line2));
                spyFilter(Arg.Is<int>(2), Arg.Is<string>(line3));
            });
        }


    }
}
