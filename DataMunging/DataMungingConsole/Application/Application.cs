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
            string fileName = string.Empty;
            IStringRecordProcessor recordProcessor = null;
            bool skipEmptyLines = false;
            int parsedColumnLimit = 0;
            bool useFirstRowAsHeader = false;

            if (invokedVerb == Options.LookupMinDiffOp)
            {
                LookupOptions lookupOptions = (LookupOptions)invokedVerbOptions;
                fileName = lookupOptions.InputFile;
                recordProcessor = new LookupMinDiff(
                    lookupOptions.LookupColumnAsInt,
                    lookupOptions.Column1,
                    lookupOptions.Column2);

                if (lookupOptions.UseIntegerFixer)
                {
                    recordProcessor.AddFixer(Fixers.KeepDigitsOnly);
                }
                skipEmptyLines = lookupOptions.SkipEmptyLines;
                parsedColumnLimit = lookupOptions.ParsedColumnLimit;
                useFirstRowAsHeader = lookupOptions.UseFirstRowAsHeader;
            }
            else
            {
                throw new InvalidOperationException("Internal error: unknown verb.");
            }

            if (recordProcessor == null)
            {
                throw new InvalidOperationException("Internal error: no record operation");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new InvalidOperationException("Internal error: no input file provided");
            }

            SeparatedValuesParser svParser = new SeparatedValuesParser(" ");
            if (parsedColumnLimit > 0)
            {
                svParser.FieldLimit = parsedColumnLimit;
            }
            ILineParser lineParser = svParser;

            var wfFactory = new DefaultWorkflowFactory(new DefaultDataMungingFactory(), lineParser);
            DefaultConsoleWorkflow wf = new DefaultConsoleWorkflow(wfFactory);
            var parsingPhase = wf.EntryPoint(fileName);
            if (skipEmptyLines)     // TODO: setSkip(bool)
            {
                parsingPhase.ExcludeLines(LineFilters.EmptyLines);
            }
            parsingPhase.UseFirstRowAsHeader(useFirstRowAsHeader);
            var configPhase = parsingPhase.LoadAndParseFile();
            configPhase.SetProcessor(recordProcessor);
            var processingPhase = configPhase.Ready();
            string output = processingPhase.Execute().Output;

            return output;
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
