using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using DataMungingConsole;

namespace DataMungingConsoleTest
{
    [TestClass]
    public class ConsoleTest
    {
        [TestMethod]
        public void Given_WeatherDatFile_Calculate_DayWithSmallestTemperatureSpread_TestStdOut()
        {
            string operation = Options.LookupMinDiffOp;
            string inputFile = OptionWithValue(LookupOptions.InputFileArg, "weather.dat");
            string lookupColumn = OptionWithValue(LookupOptions.LookupColumnArg, "Dy");
            string column1 = OptionWithValue(LookupOptions.Column1Arg, "MxT");
            string column2 = OptionWithValue(LookupOptions.Column2Arg, "MnT");

            StringWriter capturedStdOut = CaptureStdOut();

            Application.Main(new string[] { operation, inputFile, lookupColumn, column1, column2 });

            string result = capturedStdOut.ToString();
            string expectedDayInWeatherDatFile = "14";
            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
        }

        private static StringWriter CaptureStdOut()
        {
            StringWriter capturedStdOut = new StringWriter();
            System.Console.SetOut(capturedStdOut);
            return capturedStdOut;
        }

        private static string OptionWithValue(string option, string value)
        {
            return "--" + option + "=" + value;
        }
    }

}
