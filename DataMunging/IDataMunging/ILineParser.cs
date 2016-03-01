using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataMunging
{
    public interface ILineParser
    {
        string[] Parse(string line);
    }
}
