using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole
{
    public class Application
    {
        public static void Main(string[] args)
        {
            string invokedVerb = string.Empty;
            object invokedVerbOptions = null;

            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options, 
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbOptions = subOptions;
                }))
            {
                System.Console.WriteLine(options.GetUsage());
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            if (invokedVerb == Options.LookupMinDiffOp)
            {
                LookupOptions lookupOptions = (LookupOptions)invokedVerbOptions;
            }
            else
            {
                System.Console.WriteLine("Internal error.");
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

        }

    }

    
}
