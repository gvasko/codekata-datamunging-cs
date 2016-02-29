using IDataMunging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Workflow
{
    public interface IWorkflowFactory
    {
        StreamReader CreateStreamReader(string path);
        IStringTableParser CreaterStringTableParser(StreamReader reader);
    }
}
