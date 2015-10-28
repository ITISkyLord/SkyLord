using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Guard : Unit
    {
        public Guard()
        {
            _unitDamageType = UnitDamageType.physical;
            _unitType = UnitType.soldier;
            _unitStatistics = new UnitStatistics( 50, 70, 30, 15, 15, 5 );
        }

    }
}