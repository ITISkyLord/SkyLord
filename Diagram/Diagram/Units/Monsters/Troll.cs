using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Troll : Unit
    {
        public Troll()
        {
            _unitDamageType = UnitDamageType.physical;
            _unitType = UnitType.monster;
            _unitStatistics = new UnitStatistics( 75, 15, 30, 10, 40, 10 );
        }
    }
}
