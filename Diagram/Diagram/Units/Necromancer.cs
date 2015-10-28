using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Necromancer : Unit
    {
        public Necromancer( Island island )
        {
            if( island == null ) throw new ArgumentNullException( "island is null." );
            _island = island;
            _unitType = UnitType.magic;
            
            _unitStat = new UnitStat( 75, 30, 15, 10, 10, 5 );
        }
    }
}
