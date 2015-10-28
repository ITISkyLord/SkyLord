using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public interface ICombatManager
    {
        Island Resolve( Army attackingArmy, Army defendingArmy );

    }
}
