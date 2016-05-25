using IDataMunging;
using System.IO;

namespace DataMungingConsole.Workflow
{
    public interface IWorkflowFactory
    {
        StreamReader CreateStreamReader(string path);
        IStringTableParser CreaterStringTableParser(StreamReader reader);
    }
}
