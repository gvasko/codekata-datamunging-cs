using DataMungingConsole.Processing;
using DataMungingConsole.Workflow;
using IDataMunging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsoleTest.Processing
{
    [TestClass]
    public class IntOperationLookupTest
    {
        private const int resultColumn = 0;
        private const int column1 = 1;
        private const int column2 = 2;

        private static IntOperationLookup createSUT()
        {
            return new IntOperationLookup(
                (a, b) => Math.Abs(a - b),
                (v, curr) => v < curr,
                resultColumn, 
                column1, 
                column2);
        }

        [TestMethod]
        public void FirstMatchWins()
        {
            IStringRecord rec1 = RecordStubA23();
            IStringRecord rec2 = RecordStubB21();

            IntOperationLookup lmd = createSUT();
            lmd.Visit(rec1);
            lmd.Visit(rec2);

            Assert.AreEqual("a", lmd.Result);
        }

        [TestMethod]
        public void ZeroVisitResultsEmptyString()
        {
            IntOperationLookup lmd = createSUT();

            Assert.AreEqual(string.Empty, lmd.Result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void NotANumberThrowsException()
        {
            IStringRecord rec = RecordStubA12X();

            IntOperationLookup lmd = createSUT();
            lmd.Visit(rec);
        }

        [TestMethod]
        public void MinorParsingProblemsCanBeFixed()
        {
            IStringRecord rec = RecordStubA12X();
            IntOperationLookup lmd = createSUT();
            lmd.AddFixer( (column, original) =>
            {
                return original.Replace("x", "");
            });
            lmd.Visit(rec);

            Assert.AreEqual("a", lmd.Result);
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
