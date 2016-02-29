using DataMungingLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingLibTest
{
    [TestClass]
    public class DefaultStringRecordTest
    {
        [TestMethod]
        public void ValueOrderIsKept()
        {
            string[] dummyRec = { "aa", "bb", "cc" };
            DefaultStringRecord rec = new DefaultStringRecord(dummyRec);
            Assert.AreEqual("aa", rec.GetField(0));
            Assert.AreEqual("bb", rec.GetField(1));
            Assert.AreEqual("cc", rec.GetField(2));
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UnderIndexThrowsException()
        {
            string[] dummyRec = { "aa", "bb", "cc" };
            DefaultStringRecord rec = new DefaultStringRecord(dummyRec);
            rec.GetField(-1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OverIndexThrowsException()
        {
            string[] dummyRec = { "aa", "bb", "cc" };
            DefaultStringRecord rec = new DefaultStringRecord(dummyRec);
            rec.GetField(3);
        }
    }
}
