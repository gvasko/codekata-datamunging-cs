using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;
using CommandLine.Text;

namespace DataMungingConsole.Application
{
    internal class LookupOptions
    {
        public const string InputFileArg = "data";
        public const string LookupColumnArg = "resultCol";
        public const string Column1Arg = "col1";
        public const string Column2Arg = "col2";

        [Option(InputFileArg, HelpText = "Input file that contains the data.")]
        public string InputFile { get; set; }

        [Option(LookupColumnArg, HelpText = "The column the result of the operation is returned from.")]
        public string LookupColumn { get; set; }
        public int LookupColumnAsInt { get { return Int32.Parse(LookupColumn); } }

        [Option(Column1Arg, HelpText = "The first column used in calculations.")]
        public string Column1 { get; set; }
        public int Column1AsInt { get { return Int32.Parse(Column1); } }

        [Option(Column2Arg, HelpText = "The second column used in calculations.")]
        public string Column2 { get; set; }
        public int Column2AsInt { get { return Int32.Parse(Column2); } }

    }

    internal class Options
    {
        public const string LookupMinDiffOp = "LookupMinDiff";
        [VerbOption(LookupMinDiffOp, HelpText = "Lookup a value from a column, based on the difference of values in other columns.")]
        public LookupOptions LookupMinDiffVerb { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
