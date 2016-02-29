using DataMungingConsole.Workflow;
using IDataMunging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsoleTest.Workflow
{
    [TestClass]
    public class LookupMinDiffTest
    {
        [TestMethod]
        public void MinimumIsCommutative()
        {
            IStringRecord rec1 = RecordStubA13();
            IStringRecord rec2 = RecordStubB21();

            int resultColumn = 0;
            int column1 = 1;
            int column2 = 2;

            LookupMinDiff lmd1 = new LookupMinDiff(resultColumn, column1, column2);
            lmd1.Visit(rec1);
            lmd1.Visit(rec2);

            Assert.AreEqual("b", lmd1.Result);

            LookupMinDiff lmd2 = new LookupMinDiff(resultColumn, column1, column2);
            lmd2.Visit(rec2);
            lmd2.Visit(rec1);

            Assert.AreEqual("b", lmd2.Result);
        }

        [TestMethod]
        public void FirstMatchWins()
        {
            IStringRecord rec1 = RecordStubA23();
            IStringRecord rec2 = RecordStubB21();

            int resultColumn = 0;
            int column1 = 1;
            int column2 = 2;

            LookupMinDiff lmd = new LookupMinDiff(resultColumn, column1, column2);
            lmd.Visit(rec1);
            lmd.Visit(rec2);

            Assert.AreEqual("a", lmd.Result);
        }

        [TestMethod]
        public void ZeroVisitResultsEmptyString()
        {
            int resultColumn = 0;
            int column1 = 1;
            int column2 = 2;

            LookupMinDiff lmd = new LookupMinDiff(resultColumn, column1, column2);

            Assert.AreEqual(string.Empty, lmd.Result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void NotANumberThrowsException()
        {
            IStringRecord rec1 = RecordStubA12X();

            int resultColumn = 0;
            int column1 = 1;
            int column2 = 2;

            LookupMinDiff lmd = new LookupMinDiff(resultColumn, column1, column2);
            lmd.Visit(rec1);
        }

        private static IStringRecord RecordStubA12X()
        {
            IStringRecord rec2 = Substitute.For<IStringRecord>();
            rec2.GetField(Arg.Is<int>(0)).Returns("a");
            rec2.GetField(Arg.Is<int>(1)).Returns("1");
            rec2.GetField(Arg.Is<int>(2)).Returns("2x");
            return rec2;
        }


        private static IStringRecord RecordStubB21()
        {
            IStringRecord rec2 = Substitute.For<IStringRecord>();
            rec2.GetField(Arg.Is<int>(0)).Returns("b");
            rec2.GetField(Arg.Is<int>(1)).Returns("2");
            rec2.GetField(Arg.Is<int>(2)).Returns("1");
            return rec2;
        }

        private static IStringRecord RecordStubA23()
        {
            IStringRecord rec2 = Substitute.For<IStringRecord>();
            rec2.GetField(Arg.Is<int>(0)).Returns("a");
            rec2.GetField(Arg.Is<int>(1)).Returns("2");
            rec2.GetField(Arg.Is<int>(2)).Returns("3");
            return rec2;
        }

        private static IStringRecord RecordStubA13()
        {
            IStringRecord rec1 = Substitute.For<IStringRecord>();
            rec1.GetField(Arg.Is<int>(0)).Returns("a");
            rec1.GetField(Arg.Is<int>(1)).Returns("1");
            rec1.GetField(Arg.Is<int>(2)).Returns("3");
            return rec1;
        }

    }
}
