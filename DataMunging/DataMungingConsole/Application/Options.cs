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
        public const string SkipEmptyLinesArg = "skipEmptyLines";
        public const string ParsedColumnLimitArg = "parsedColumnLimit";

        [Option(InputFileArg, HelpText = "Input file that contains the data.")]
        public string InputFile { get; set; }

        [Option(LookupColumnArg, HelpText = "The column the result of the operation is returned from.")]
        public string LookupColumn { get; set; }
        public int LookupColumnAsInt { get { return Int32.Parse(LookupColumn); } }

        [Option(Column1Arg, HelpText = "The first column used in calculations.")]
        public int Column1 { get; set; }

        [Option(Column2Arg, HelpText = "The second column used in calculations.")]
        public int Column2 { get; set; }

        [Option(SkipEmptyLinesArg, HelpText = "Exclude empty lines during loading the file.")]
        public bool SkipEmptyLines { get; set; }
        
        [Option(ParsedColumnLimitArg, HelpText = "Limiting the number of column parsed.")]
        public int ParsedColumnLimit { get; set; }


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
