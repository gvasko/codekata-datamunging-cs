using DataMungingConsole.Workflow;
using DataMungingLib;
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
                throw new ArgumentException();
            }
        }

        public void Dispose()
        {
            // nothing to do
        }

        public string Run()
        {
            IStringRecordProcessor recordProcessor = null;
            string fileName = string.Empty;

            if (invokedVerb == Options.LookupMinDiffOp)
            {
                LookupOptions lookupOptions = (LookupOptions)invokedVerbOptions;
            }
            else
            {
                throw new ArgumentException("Internal error: unknown verb.");
            }

            if (recordProcessor == null)
            {
                throw new InvalidOperationException("Internal error: no record operation");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Internal error: no input file provided");
            }

            var wfFactory = new DefaultWorkflowFactory(new DefaultDataMungingFactory());
            DefaultWorkflow wf = new DefaultWorkflow(wfFactory);
            string output = wf.EntryPoint
                .LoadFile(fileName)
                .SetProcessor(recordProcessor)
                .Ready()
                .Execute()
                .Output;

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
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }
    }

}
