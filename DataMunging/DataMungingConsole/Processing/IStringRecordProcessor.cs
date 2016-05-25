using IDataMunging;

namespace DataMungingConsole.Processing
{
    public delegate string ParserFixerDelegate(int column, string original);

    public interface IStringRecordProcessor : IStringRecordVisitor
    {
        void AddFixer(ParserFixerDelegate fixer);
        string Result { get; }
    }
}
