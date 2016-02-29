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
        // commutative
        [TestMethod]
        public void MinimumIsCommutative()
        {
            IStringRecord rec1 = Substitute.For<IStringRecord>();
            rec1.GetField(Arg.Is<int>(0)).Returns("a");
            rec1.GetField(Arg.Is<int>(1)).Returns("1");
            rec1.GetField(Arg.Is<int>(2)).Returns("3");
            IStringRecord rec2 = Substitute.For<IStringRecord>();
            rec2.GetField(Arg.Is<int>(0)).Returns("b");
            rec2.GetField(Arg.Is<int>(1)).Returns("2");
            rec2.GetField(Arg.Is<int>(2)).Returns("1");

            LookupMinDiff lmd1 = new LookupMinDiff(0, 1, 2);
            lmd1.Visit(rec1);
            lmd1.Visit(rec2);

            LookupMinDiff lmd2 = new LookupMinDiff(0, 1, 2);
            lmd2.Visit(rec2);
            lmd2.Visit(rec1);

            Assert.AreEqual("b", lmd1.Result);
            Assert.AreEqual("b", lmd2.Result);
        }

        // first min wins
        // zero visit
        // parse exception
    }
}
