using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public interface IRegiment
    {
        string Name
        {
            get;
        }
        int Id
        {
            get;
        }
        UnitType UnitType
        {
            get;
        }
        UnitStat UnitStat
        {
            get;
        }
        Island Island
        {
            get;
            set;
        }


    }
}
