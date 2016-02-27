using DataMungingConsole;
using DataMungingConsole.WorkflowBuilder;
using IDataMunging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMungingConsoleTest
{
    [TestClass]
    class WorkflowTest
    {

        [TestMethod]
        public void BasicWorkflowTest()
        {
            // DataMungingConsoleFactory
            // IStringTableParser parser = factory.CreateStringTableParser
            // IStringTable table = parser.LoadAndParse(file)
            // IStringRecordProcessor : IStringRecordVisitor
            // IStringRecordProcessor recProcessor
            // table.VisitEachRecordWith(recProcessor)
            //      for each IStringRecord rec in table.Records
            //          recProcessor.ProcessRecord(record)
            //
            // return recProcessor.TextResult
            //


            string fakeFileName = "example.dat";
            var factory = Substitute.For<IDataMungingFactory>();

            var recProc = Substitute.For<IStringRecordProcessor>();
            string fakeProcResult = "example result of record processor";
            recProc.Result.Returns(fakeProcResult);

            DefaultWorkflow wf = new DefaultWorkflow(factory);
            string output = wf.EntryPoint
                .LoadFile(fakeFileName)
                .SetProcessor(recProc)
                .Ready()
                .Execute()
                .Output;

            factory.Received().CreateStreamReader(Arg.Is(fakeFileName));
            // assert: record processor visited all records
            Assert.AreEqual(fakeProcResult, output);
        }
    }
}
