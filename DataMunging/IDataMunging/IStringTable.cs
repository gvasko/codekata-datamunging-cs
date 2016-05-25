
namespace IDataMunging
{
    public interface IStringTable
    {
        void VisitAllRecords(IStringRecordVisitor visitor);
    }
}
