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
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine(options.LookupMinDiffVerb.LookupColumn);
            }
        }

        private static void ProcessCommandLineVerb(string name, object obj)
        {
            Console.WriteLine(name);
        }
    }

    
}
