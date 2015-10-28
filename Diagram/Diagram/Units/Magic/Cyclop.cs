using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Cyclop : Unit
    {
        public Cyclop()
        {
            _unitDamageType = UnitDamageType.magical;
            _unitType = UnitType.magic;
            _unitStatistics = new UnitStatistics( 50, 40, 20, 5, 10, 10 );
        }
    }
}
