using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Guard : Unit
    {
        public Guard(Island island)
        {
            if( island == null ) throw new ArgumentNullException( "island is null." );
            _island = island;
            _unitType = UnitType.soldier;
            _unitStat = new UnitStat( 50, 70, 30, 15, 15, 5 );
        }

    }
}