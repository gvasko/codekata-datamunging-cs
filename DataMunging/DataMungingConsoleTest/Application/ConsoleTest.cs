using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using System.Diagnostics;
using DataMungingConsole.Application;

namespace DataMungingConsoleTest
{
    [TestClass]
    public class ConsoleTest
    {
        [TestMethod]
        public void Given_WeatherDatFile_Calculate_DayWithSmallestTemperatureSpread_TestStdOut()
        {
            string result = LaunchCLIWith(@"Lookup.MinDiff --data=Data\weather.dat --firstRowAsHeader --intFixer --parsedColumnLimit=3 --resultCol=0 --col1=1 --col2=2 --skipEmptyLines");

            string expectedDayInWeatherDatFile = "14";

            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
        }

        [TestMethod, Ignore()]
        public void Given_WeatherDatFile_Calculate_DayWithSmallestTemperatureSpread_UsingHeader_TestStdOut()
        {
            // Challenge: refer to columns with their name in the header
            string result = LaunchCLIWith(@"Lookup.MinDiff --data=Data\weather.dat --firstRowAsHeader --intFixer --resultCol=Dy --col1=MxT --col2=MnT --skipEmptyLines");

            string expectedDayInWeatherDatFile = "14";

            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
        }

        [TestMethod]
        public void Given_WeatherDatFile_Calculate_DayWithGreatestTemperatureSpread_TestStdOut()
        {
            // Challenge: exclude the last summary line somehow
            string result = LaunchCLIWith(@"Lookup.MaxDiff --data=Data\weather.dat --firstRowAsHeader --intFixer --parsedColumnLimit=3 --skipLines=32  --resultCol=0 --col1=1 --col2=2 --skipEmptyLines");

            string expectedDayInWeatherDatFile = "9";

            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
        }

        [TestMethod, Ignore()]
        public void Given_WeatherDatFile_Calculate_DayWithSmallestRSpread_TestStdOut()
        {
            // Challenge: handle partly empty columns that cannot be avoided
            string result = LaunchCLIWith(@"Lookup.MinDiff --data=Data\weather.dat --firstRowAsHeader --intFixer --resultCol=0 --col1=14 --col2=15 --skipEmptyLines");

            string expectedDayInWeatherDatFile = "14";

            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
        }

        [TestMethod, Ignore()]
        public void Given_WeatherDatFile_Calculate_DayWithSmallestRSpread_UsingHeader_TestStdOut()
        {
            // Challenge: refer to columns with their name in the header
            string result = LaunchCLIWith(@"Lookup.MinDiff --data=Data\weather.dat --firstRowAsHeader --intFixer --resultCol=Dy --col1=MxR --col2=MnR --skipEmptyLines");

            string expectedDayInWeatherDatFile = "14";

            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
        }

        [TestMethod]
        public void Given_FootballDatFile_Calculate_TeamOfSmallestGoalDifference_TestStdOut()
        {
            string result = LaunchCLIWith(@"Lookup.MinDiff --data=Data\football.dat --firstRowAsHeader --resultCol=1 --col1=6 --col2=8 --skipSeparatorLines");

            string expectedTeamInFootballDatFile = "Aston_Villa";

            Assert.AreEqual(expectedTeamInFootballDatFile + Environment.NewLine, result);
        }

        private static string LaunchCLIWith(string cmdLine)
        {
            string appName = typeof(Application).Assembly.Location;
            ProcessStartInfo dataMungingConsoleApp = new ProcessStartInfo(appName, cmdLine);
            dataMungingConsoleApp.UseShellExecute = false;
            dataMungingConsoleApp.RedirectStandardOutput = true;
            dataMungingConsoleApp.CreateNoWindow = true;
            dataMungingConsoleApp.WindowStyle = ProcessWindowStyle.Hidden;

            string output = string.Empty;
            using (Process dataMungingProc = Process.Start(dataMungingConsoleApp))
            {
                output = dataMungingProc.StandardOutput.ReadToEnd();
                dataMungingProc.WaitForExit();
            }

            return output;
        }

        private static string DebugCLIWith(string cmdLine)
        {
            StringWriter capturedStdOut = new StringWriter();
            System.Console.SetOut(capturedStdOut);

            string[] arguments = cmdLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Application.Main(arguments);

            return capturedStdOut.ToString();

        }

    }

}
