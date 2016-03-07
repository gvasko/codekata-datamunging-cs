using DataMungingConsole.Processing;
using DataMungingConsole.Workflow;
using DataMungingLib;
using DataMungingLib.LineParsers;
using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Application
{
    public class Application : IDisposable
    {
        private string invokedVerb = string.Empty;
        private object invokedVerbOptions = null;

        public Application(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbOptions = subOptions;
                }))
            {
                System.Console.WriteLine(options.GetUsage());
                throw new ArgumentException("Invalid arguments.");
            }
        }

        public void Dispose()
        {
            // nothing to do
        }

        internal string Run()
        {
            IStringRecordProcessor recordProcessor = ProcessCLIVerb();

            LookupOptions lookupOptions = (LookupOptions)invokedVerbOptions;

            CheckErrors(lookupOptions, recordProcessor);

            if (lookupOptions.UseIntegerFixer)
            {
                recordProcessor.AddFixer(Fixers.KeepDigitsOnly);
            }

            ILineParser lineParser = GetDefaultLineParser(lookupOptions);

            var workflowFactory = new DefaultWorkflowFactory(new DefaultDataMungingFactory(), lineParser);
            DefaultConsoleWorkflow workflow = new DefaultConsoleWorkflow(workflowFactory);
            var parsingPhase = workflow.EntryPoint(lookupOptions.InputFile);

            if (lookupOptions.SkipEmptyLines)
            {
                parsingPhase.ExcludeLines(LineFilters.EmptyLines);
            }

            if (lookupOptions.SkipSeparatorLines)
            {
                parsingPhase.ExcludeLines(LineFilters.SeparatorLines);
            }

            parsingPhase.UseFirstRowAsHeader(lookupOptions.UseFirstRowAsHeader);
            var configPhase = parsingPhase.LoadAndParseFile();
            configPhase.SetProcessor(recordProcessor);
            var processingPhase = configPhase.Ready();
            string output = processingPhase.Execute().Output;

            return output;
        }

        private static ILineParser GetDefaultLineParser(LookupOptions lookupOptions)
        {
            SeparatedValuesParser svParser = new SeparatedValuesParser(" ");
            if (lookupOptions.ParsedColumnLimit > 0)
            {
                svParser.FieldLimit = lookupOptions.ParsedColumnLimit;
            }
            ILineParser lineParser = svParser;
            return lineParser;
        }

        private IStringRecordProcessor ProcessCLIVerb()
        {
            IStringRecordProcessor recordProcessor;
            if (invokedVerb == Options.LookupMinDiffOp)
            {
                LookupOptions lookupOptions = (LookupOptions)invokedVerbOptions;
                recordProcessor = new IntOperationLookup(
                    (a, b) => Math.Abs(a - b),
                    (v, curr) => v < curr,
                    lookupOptions.LookupColumnAsInt,
                    lookupOptions.Column1,
                    lookupOptions.Column2);
            }
            else if (invokedVerb == Options.LookupMaxDiffOp)
            {
                LookupOptions lookupOptions = (LookupOptions)invokedVerbOptions;
                recordProcessor = new IntOperationLookup(
                    (a, b) => Math.Abs(a - b),
                    (v, curr) => v > curr,
                    lookupOptions.LookupColumnAsInt,
                    lookupOptions.Column1,
                    lookupOptions.Column2);
            }
            else
            {
                throw new InvalidOperationException("Internal error: unknown verb.");
            }

            return recordProcessor;
        }

        private static void CheckErrors(LookupOptions lookupOptions, IStringRecordProcessor recordProcessor)
        {
            if (recordProcessor == null)
            {
                throw new InvalidOperationException("Internal error: no record operation");
            }

            if (string.IsNullOrEmpty(lookupOptions.InputFile))
            {
                throw new InvalidOperationException("Internal error: no input file provided");
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                using (Application app = new Application(args))
                {
                    System.Console.WriteLine(app.Run());
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                //Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }
    }

}
