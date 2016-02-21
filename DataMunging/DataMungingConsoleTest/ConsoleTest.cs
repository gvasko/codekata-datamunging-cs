using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace DataMungingConsoleTest
{
    [TestClass]
    public class ConsoleTest
    {
        [TestMethod]
        public void Given_WeatherDatFile_Calculate_DayWithSmallestTemperatureSpread_TestStdOut()
        {
            string inputFile = "weather.dat";
            string operation = "LookUpMinimalDifference";
            string lookupColumn = "/LookupColumn=Dy";
            string column1 = "/Column1=MxT";
            string column2 = "/Column2=MnT";
            StringWriter capturedStdOut = new StringWriter();
            System.Console.SetOut(capturedStdOut);
            DataMungingConsole.Application.Main(new string[] { inputFile, operation, lookupColumn, column1, column2 });
            string result = capturedStdOut.ToString();
            string expectedDayInWeatherDatFile = "14";
            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
        }
    }
}
