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
            string fakeFileName = "example.dat";
            var factory = Substitute.For<IDataMungingFactory>();
            var fakeParser = Substitute.For<IStringTableParser>();
            var spyTable = Substitute.For<IStringTable>();
            fakeParser.Parse(Arg.Any<StreamReader>()).Returns(spyTable);
            factory.CreaterStringTableParser().Returns(fakeParser);

            var fakeRecProc = Substitute.For<IStringRecordProcessor>();
            string fakeProcResult = "example result of record processor";
            fakeRecProc.Result.Returns(fakeProcResult);

            DefaultWorkflow wf = new DefaultWorkflow(factory);
            string output = wf.EntryPoint
                .LoadFile(fakeFileName)
                .SetProcessor(fakeRecProc)
                .Ready()
                .Execute()
                .Output;

            factory.Received().CreateStreamReader(Arg.Is(fakeFileName));
            spyTable.Received().VisitAllRecords(Arg.Is(fakeRecProc));
            Assert.AreEqual(fakeProcResult, output);
        }
    }
}
