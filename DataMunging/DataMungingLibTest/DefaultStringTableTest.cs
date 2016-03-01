using DataMungingLib;
using IDataMunging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute.Core;

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

            IDataMungingFactory fakeFactory = CreateRecordCreatorFakeFactory();
            var tableSUT = new DefaultStringTable(fakeFactory, dummyList);

            var recordVisitorSpy = new TestImpl.RecVisitorSpy();
            tableSUT.VisitAllRecords(recordVisitorSpy);

            Assert.AreEqual("a1c3b2", recordVisitorSpy.JoinedValues);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NullRecordThrowsException()
        {
            List<string[]> dummyList = new List<string[]>();
            dummyList.Add(new string[] { "a", "1" });
            dummyList.Add(null);
            dummyList.Add(new string[] { "b", "2" });

            var dummyFactory = Substitute.For<IDataMungingFactory>();
            var tableSUT = new DefaultStringTable(dummyFactory, dummyList);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void RecordsWithDifferentLengthsThrowsException1()
        {
            List<string[]> dummyList = new List<string[]>();
            dummyList.Add(new string[] { "a", "1" });
            dummyList.Add(new string[] { "b" });

            AssertThatRecordsWithDifferentLenghtsThrowsException(dummyList);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void RecordsWithDifferentLengthsThrowsException2()
        {
            List<string[]> dummyList = new List<string[]>();
            dummyList.Add(new string[] { "a", "1" });
            dummyList.Add(new string[] { "b", "2", "bb" });

            AssertThatRecordsWithDifferentLenghtsThrowsException(dummyList);
        }

        private static void AssertThatRecordsWithDifferentLenghtsThrowsException(List<string[]> dummyList)
        {
            var dummyRecord = Substitute.For<IStringRecord>();
            IDataMungingFactory fakeFactory = CreateRecordCreatorFakeFactory();
            var tableSUT = new DefaultStringTable(fakeFactory, dummyList);
        }

        private static IDataMungingFactory CreateRecordCreatorFakeFactory()
        {
            var fakeFactory = Substitute.For<IDataMungingFactory>();
            fakeFactory.CreateStringRecord(Arg.Any<string[]>()).Returns(
                values => CreateTestStringRecord((string[])values[0])
            );
            return fakeFactory;
        }

        private static IStringRecord CreateTestStringRecord(string[] values)
        {
            IStringRecord rec = Substitute.For<IStringRecord>();
            rec.FieldCount.Returns(values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                rec.GetField(Arg.Is<int>(i)).Returns(values[i]);
            }
            return rec;
        }


    }
}
