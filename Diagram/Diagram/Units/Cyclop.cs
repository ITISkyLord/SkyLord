using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Cyclop : Unit
    {
        public Cyclop( Island island )
        {
            if( island == null ) throw new ArgumentNullException( "island is null." );
            _island = island;
            _unitType = UnitType.magic;

            _unitStat = new UnitStat( 50, 40, 20, 5, 10, 10 );
        }
    }
}
