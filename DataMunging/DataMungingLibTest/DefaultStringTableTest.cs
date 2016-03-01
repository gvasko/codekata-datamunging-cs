using DataMungingLib;
using IDataMunging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLibTest
{
    [TestClass]
    public class DefaultStringTableTest
    {
        [TestMethod]
        public void EmptyTableCanBeCreated()
        {
            var dummyFactory = Substitute.For<IDataMungingFactory>();
            List<string[]> emptyList = new List<string[]>();
            var tableSUT = new DefaultStringTable(dummyFactory, emptyList);
            var recordVisitorSpy = Substitute.For<IStringRecordVisitor>();
            tableSUT.VisitAllRecords(recordVisitorSpy);
            recordVisitorSpy.DidNotReceive().Visit(Arg.Any<IStringRecord>());
        }

        [TestMethod]
        public void TableKeepsOrderOfRecords()
        {
            List<string[]> dummyList = new List<string[]>();
            dummyList.Add(new string[] { "a", "1" });
            dummyList.Add(new string[] { "c", "3" });
            dummyList.Add(new string[] { "b", "2" });

            var dummyFactory = Substitute.For<IDataMungingFactory>();
            var tableSUT = new DefaultStringTable(dummyFactory, dummyList);

            var recordVisitorMock = Substitute.For<IStringRecordVisitor>();
            tableSUT.VisitAllRecords(recordVisitorMock);
            //            Received.InOrder(() =>
            //            {
            //                recordVisitorMock.Visit()
            //            });
            throw new NotImplementedException();
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void RecordsWithDifferentLengthsThrowsException1()
        {
            List<string[]> dummyList = new List<string[]>();
            dummyList.Add(new string[] { "a", "1" });
            dummyList.Add(new string[] { "b" });

            var dummyFactory = Substitute.For<IDataMungingFactory>();
            var tableSUT = new DefaultStringTable(dummyFactory, dummyList);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void RecordsWithDifferentLengthsThrowsException2()
        {
            List<string[]> dummyList = new List<string[]>();
            dummyList.Add(new string[] { "a", "1" });
            dummyList.Add(new string[] { "b", "2", "bb" });

            var dummyFactory = Substitute.For<IDataMungingFactory>();
            var tableSUT = new DefaultStringTable(dummyFactory, dummyList);
        }
    }
}
