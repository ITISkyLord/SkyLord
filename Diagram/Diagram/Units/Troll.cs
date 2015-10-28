using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Troll : Unit
    {
        public Troll( Island island )
        {
            if( island == null ) throw new ArgumentNullException( "island is null." );
            _island = island;
            _unitType = UnitType.monster;
            _unitStat = new UnitStat( 75, 15, 30, 10, 40, 10 );
        }
    }
}
