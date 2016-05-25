
namespace IDataMunging
{
    public delegate bool LineFilterDelegate(int index, string line);

    public interface IStringTableParser
    {
        bool UseFirstRowAsHeader { get; set; }
        IStringTableParser Exclude(LineFilterDelegate filter);
        IStringTable Parse();
    }
}
