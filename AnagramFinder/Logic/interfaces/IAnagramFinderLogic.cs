using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.interfaces
{
    public interface IAnagramFinderLogic
    {
        List<string> GetAnagramsByWord(string searchTerm);
    }
}
