﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;
using CommandLine.Text;

namespace DataMungingConsole
{
    public class LookupOptions
    {
        public const string InputFileArg = "InputFile";
        public const string LookupColumnArg = "LookupColumn";
        public const string Column1Arg = "Column1";
        public const string Column2Arg = "Column2";

        [Option(InputFileArg, HelpText = "Input file that contains the data.")]
        public string InputFile { get; set; }

        [Option(LookupColumnArg, HelpText = "The column the result of the operation is returned from.")]
        public string LookupColumn { get; set; }

        [Option(Column1Arg, HelpText = "The first column used in calculations.")]
        public string Column1 { get; set; }

        [Option(Column2Arg, HelpText = "The second column used in calculations.")]
        public string Column2 { get; set; }

    }

    public class Options
    {
        public const string LookupMinDiffOp = "LookUpMinimalDifference";
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
