using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Goblin : Unit
    {
        public Goblin()
        {
            _unitDamageType = UnitDamageType.physical;
            _unitType = UnitType.monster;
            _unitStatistics = new UnitStatistics( 30, 15, 15, 15, 15, 5 );
        }

    }
}
