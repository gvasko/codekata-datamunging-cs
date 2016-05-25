
namespace IDataMunging
{
    public interface IStringRecord
    {
        int FieldCount { get; }
        string GetField(int i);
    }
}
