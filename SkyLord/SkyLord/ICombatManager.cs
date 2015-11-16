using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.Model
{
    public interface ICombatManager
    {
        CombatResult Resolve( Army attackingArmy, Army defendingArmy );

    }
}
