using DataMungingConsole.Processing;
using DataMungingConsole.Workflow;
using IDataMunging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.IO;

namespace DataMungingConsoleTest
{
    [TestClass]
    public class WorkflowTest
    {

        [TestMethod]
        public void BasicWorkflowTest()
        {
            string dummyFileName = "example.dat";
            var factoryStub = Substitute.For<IWorkflowFactory>();
            var parserStub = Substitute.For<IStringTableParser>();
            var spyTable = Substitute.For<IStringTable>();
            parserStub.Parse().Returns(spyTable);
            factoryStub.CreaterStringTableParser(Arg.Any<StreamReader>()).Returns(parserStub);

            var recProcStub = Substitute.For<IStringRecordProcessor>();
            string dummyProcResult = "example result of record processor";
            recProcStub.Result.Returns(dummyProcResult);

            DefaultConsoleWorkflow wf = new DefaultConsoleWorkflow(factoryStub);
            string output = wf.EntryPoint(dummyFileName)
                .LoadAndParseFile()
                .SetProcessor(recProcStub)
                .Ready()
                .Execute()
                .Output;

            factoryStub.Received().CreateStreamReader(Arg.Is(dummyFileName));
            spyTable.Received().VisitAllRecords(Arg.Is(recProcStub));
            Assert.AreEqual(dummyProcResult, output);
        }
    }
}
