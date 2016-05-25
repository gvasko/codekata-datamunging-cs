using DataMungingConsole.Processing;
using IDataMunging;
using System;

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
        IProcessingPhase Ready();
    }

    internal interface IParsingPhase : IDisposable
    {
        IParsingPhase UseFirstRowAsHeader(bool toggle);
        IParsingPhase ExcludeLines(LineFilterDelegate filter);
        IConfigurationPhase LoadAndParseFile();
    }

}
