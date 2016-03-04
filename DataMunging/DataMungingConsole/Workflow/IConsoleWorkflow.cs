using IDataMunging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsole.Workflow
{
    internal interface IProcessingPhase
    {
        IProcessingPhase Execute();
        string Output { get; }
    }

    internal interface IConfigurationPhase
    {
        IConfigurationPhase SetProcessor(IStringRecordProcessor recProc);
        IConfigurationPhase UseFirstRowAsHeader();
        IProcessingPhase Ready();
    }

    internal interface IParsingPhase : IDisposable
    {
        IParsingPhase ExcludeLines(LineFilterDelegate filter);
        IConfigurationPhase LoadAndParseFile();
    }

}
