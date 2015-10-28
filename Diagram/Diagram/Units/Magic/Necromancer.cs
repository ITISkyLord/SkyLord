using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Necromancer : Unit
    {
        public Necromancer()
        {
            _unitDamageType = UnitDamageType.magical;
            _unitType = UnitType.magic;
            _unitStatistics = new UnitStatistics( 75, 30, 15, 10, 10, 5 );
        }
    }
}
