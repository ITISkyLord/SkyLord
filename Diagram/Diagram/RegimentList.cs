using ITI.SkyLord.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class RegimentList : List<Regiment>
    {
        public void Add( Unit unit, int number )
        {
            Add( new Regiment( unit, number ) );
        }
    }
}
