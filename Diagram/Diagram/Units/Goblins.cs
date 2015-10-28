using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Goblins : Unit
    {
        public Goblins( Island island )
        {
            if( island == null ) throw new ArgumentNullException( "island is null." );
            _island = island;
            _unitType = UnitType.monster;
            _unitStat = new UnitStat( 30, 15, 15, 15, 15, 5 );
        }

    }
}
