using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Warrior : Unit
    {
        public Warrior( Island island )
        {
            if( island == null ) throw new ArgumentNullException( "island is null." );
            _island = island;
            _unitType = UnitType.soldier;
            _unitStat = new UnitStat( 70, 50, 25, 25, 15, 5 );
        }

    }
}
