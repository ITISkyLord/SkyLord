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
            _island = island;
            _unitType = UnitType.soldier;
            _unitStatistics = new UnitStatisitcs( 70, 50, 20, 15, 15, 5 );
        }

    }
}