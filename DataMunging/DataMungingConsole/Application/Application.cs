using DataMungingConsole.Processing;
using DataMungingConsole.Workflow;
using DataMungingLib;
using DataMungingLib.LineParsers;
using IDataMunging;
using System;


namespace DataMungingConsole.Application
{
    public class Application : IDisposable
    {
        private LookupOptions invokedVerbOptions;
        private IStringRecordProcessor recordProcessor;

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

        internal Application(string[] args)
        {
            var options = new Options();
            string invokedVerb = string.Empty;
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbOptions = (LookupOptions)subOptions;
                }))
            {
                System.Console.WriteLine(options.GetUsage());
                throw new ArgumentException("Invalid arguments.");
            }

            InitOperation(invokedVerb);
        }

        public void Dispose()
        {
            // nothing to do
        }

        internal string Run()
        {
            DefaultConsoleWorkflow workflow = CreateWorkflow();

            var parsingPhase = workflow.EntryPoint(invokedVerbOptions.InputFile);

            setupParsingPhase(parsingPhase);

            var configPhase = parsingPhase.LoadAndParseFile();

            setupConfigPhase(configPhase);

            var processingPhase = configPhase.Ready();

            string output = processingPhase.Execute().Output;

            return output;
        }

        private void InitOperation(string invokedVerb)
        {
            recordProcessor = CreateOperationForVerb(invokedVerb, invokedVerbOptions);
            CheckErrors(invokedVerbOptions, recordProcessor);

            if (invokedVerbOptions.UseIntegerFixer)
            {
                recordProcessor.AddFixer(Fixers.KeepDigitsOnly);
            }
        }

        private DefaultConsoleWorkflow CreateWorkflow()
        {
            ILineParser lineParser = GetDefaultLineParser(invokedVerbOptions);
            IWorkflowFactory workflowFactory = new DefaultWorkflowFactory(new DefaultDataMungingFactory(), lineParser);
            DefaultConsoleWorkflow workflow = new DefaultConsoleWorkflow(workflowFactory);
            return workflow;
        }

        private static ILineParser GetDefaultLineParser(LookupOptions lookupOptions)
        {
            SeparatedValuesParser svParser = new SeparatedValuesParser(" ");
            if (lookupOptions.ParsedColumnLimit > 0)
            {
                svParser.FieldLimit = lookupOptions.ParsedColumnLimit;
            }
            return svParser;
        }

        private void setupParsingPhase(IParsingPhase parsingPhase)
        {
            if (invokedVerbOptions.SkipEmptyLines)
            {
                parsingPhase.ExcludeLines(LineFilters.EmptyLines);
            }

            if (invokedVerbOptions.SkipSeparatorLines)
            {
                parsingPhase.ExcludeLines(LineFilters.SeparatorLines);
            }

            if (invokedVerbOptions.SkippedLines.Length > 0)
            {
                // TODO: What about garbage collection here?
                LineFilters.LinesAtIndex linesAtIndex = new LineFilters.LinesAtIndex(invokedVerbOptions.SkippedLines);
                parsingPhase.ExcludeLines(linesAtIndex.IsSelected);
            }

            parsingPhase.UseFirstRowAsHeader(invokedVerbOptions.UseFirstRowAsHeader);
        }

        private void setupConfigPhase(IConfigurationPhase configPhase)
        {
            configPhase.SetProcessor(recordProcessor);
        }

        private static IStringRecordProcessor CreateOperationForVerb(string invokedVerb, LookupOptions invokedVerbOptions)
        {
            IStringRecordProcessor recordProcessor;
            if (invokedVerb == Options.LookupMinDiffOp)
            {
                recordProcessor = new IntOperationLookup(
                    (a, b) => Math.Abs(a - b),
                    (v, curr) => v < curr,
                    invokedVerbOptions.LookupColumnAsInt,
                    invokedVerbOptions.Column1,
                    invokedVerbOptions.Column2);
            }
            else if (invokedVerb == Options.LookupMaxDiffOp)
            {
                recordProcessor = new IntOperationLookup(
                    (a, b) => Math.Abs(a - b),
                    (v, curr) => v > curr,
                    invokedVerbOptions.LookupColumnAsInt,
                    invokedVerbOptions.Column1,
                    invokedVerbOptions.Column2);
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

    }

}
