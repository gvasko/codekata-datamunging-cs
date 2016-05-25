using DataMungingLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            Assert.AreEqual(dummyRec[0], rec.GetField(0));
            Assert.AreEqual(dummyRec[1], rec.GetField(1));
            Assert.AreEqual(dummyRec[2], rec.GetField(2));
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

        [TestMethod]
        public void EqualsAndHashCodeImplemented()
        {
            // This is a value object
            string[] dummyRec1 = { "a", "b", "c" };
            DefaultStringRecord rec1 = new DefaultStringRecord(dummyRec1);
            DefaultStringRecord rec2 = new DefaultStringRecord(dummyRec1);

            Assert.AreEqual(rec1, rec2);
            Assert.AreEqual(rec1.GetHashCode(), rec2.GetHashCode());

            string[] dummyRec2 = { "aa", "bb", "cc" };
            DefaultStringRecord rec3 = new DefaultStringRecord(dummyRec2);
            Assert.AreNotEqual(rec2, rec3);
            Assert.AreNotEqual(rec1, rec3);
        }
    }
}
