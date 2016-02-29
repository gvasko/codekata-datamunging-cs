using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using DataMungingConsole;
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
            string result = LaunchCLIWith(@"LookupMinDiff --data=Data\weather.dat --resultCol=0 --col1=1 --col2=2");

            string expectedDayInWeatherDatFile = "14";

            Assert.AreEqual(expectedDayInWeatherDatFile + Environment.NewLine, result);
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
