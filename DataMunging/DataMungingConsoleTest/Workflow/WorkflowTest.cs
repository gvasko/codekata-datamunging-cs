using DataMungingConsole;
using DataMungingConsole.WorkflowBuilder;
using IDataMunging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsoleTest
{
    [TestClass]
    public class WorkflowTest
    {

        [TestMethod]
        public void BasicWorkflowTest()
        {
            string dummyFileName = "example.dat";
            var factoryStub = Substitute.For<IDataMungingFactory>();
            var parserStub = Substitute.For<IStringTableParser>();
            var spyTable = Substitute.For<IStringTable>();
            parserStub.Parse(Arg.Any<StreamReader>()).Returns(spyTable);
            factoryStub.CreaterStringTableParser().Returns(parserStub);

            var recProcStub = Substitute.For<IStringRecordProcessor>();
            string dummyProcResult = "example result of record processor";
            recProcStub.Result.Returns(dummyProcResult);

            DefaultWorkflow wf = new DefaultWorkflow(factoryStub);
            string output = wf.EntryPoint
                .LoadFile(dummyFileName)
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
