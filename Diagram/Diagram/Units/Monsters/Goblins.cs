using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Goblins : Unit
    {
        public Goblins()
        {
            _unitDamageType = UnitDamageType.physical;
            _unitType = UnitType.monster;
            _unitStatistics = new UnitStatistics( 30, 15, 15, 15, 15, 5 );
        }

    }
}
