using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
