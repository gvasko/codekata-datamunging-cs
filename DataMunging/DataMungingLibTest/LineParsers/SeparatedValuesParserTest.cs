using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataMungingLib.LineParsers;

namespace DataMungingLibTest.LineParsers
{
    [TestClass]
    public class SeparatedValuesParserTest
    {
        [TestMethod]
        public void TestWithSingleSeparator()
        {
            var lineParser = new SeparatedValuesParser(" ");
            AssertArraysAreEqual(new string[] { "a", "b", "c" }, lineParser.Parse("a b c"));
            AssertArraysAreEqual(new string[] { "a", "b", "c" }, lineParser.Parse(" a b c "));
            AssertArraysAreEqual(new string[] { "a", "b", "c" }, lineParser.Parse("  a  b  c  "));
            AssertArraysAreEqual(new string[] { "aa", "bbb", "cccc" }, lineParser.Parse("aa     bbb     cccc"));
        }

        [TestMethod]
        public void TestWithMultipleSeparator()
        {
            var lineParser = new SeparatedValuesParser(" ,");
            AssertArraysAreEqual(new string[] { "a", "b", "c" }, lineParser.Parse("a, b, c"));
            AssertArraysAreEqual(new string[] { "a", "b", "c" }, lineParser.Parse(" a, b, c "));
            AssertArraysAreEqual(new string[] { "a", "b", "c" }, lineParser.Parse("  a , b , c , "));
            AssertArraysAreEqual(new string[] { "aa", "bbb", "cccc" }, lineParser.Parse("aa  ,,,,,   bbb   ,,,,,  cccc"));
        }

        [TestMethod]
        public void TestLimitedParsing()
        {
            var lineParser = new SeparatedValuesParser(" ");
            lineParser.FieldLimit = 2;
            AssertArraysAreEqual(new string[] { "a", "b" }, lineParser.Parse("a b c"));
            AssertArraysAreEqual(new string[] { "a", "b" }, lineParser.Parse("a b"));
            AssertArraysAreEqual(new string[] { "a" }, lineParser.Parse("a"));

        }

        private static void AssertArraysAreEqual(object[] expectedArray, object[] actualArray)
        {
            Assert.AreEqual(expectedArray.Length, actualArray.Length);
            for (int i = 0; i < expectedArray.Length; i++)
            {
                Assert.AreEqual(expectedArray[i], actualArray[i]);
            }
        }
    }
}
