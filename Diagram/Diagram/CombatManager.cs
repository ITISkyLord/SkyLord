using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class CombatManager : ICombatManager
    {
        public Island Resolve( Army attackingArmy, Army defendingArmy )
        {
            double attackPointsPhysic = 1.0;
            double armor = 1.0;
            double attackPointsMagic = 1.0;
            double magicResist = 1.0;

            foreach (KeyValuePair<Unit, int> kvp in attackingArmy.Regiments)
            {
                if( kvp.Key.UnitDamageType == UnitDamageType.physical)
                    attackPointsPhysic += (kvp.Value * kvp.Key.UnitStatistics.Attack);
                else
                    attackPointsMagic += (kvp.Value * kvp.Key.UnitStatistics.Attack);

            }

            foreach( KeyValuePair<Unit, int> kvp in defendingArmy.Regiments )
            {
                if( kvp.Key.UnitDamageType == UnitDamageType.physical )
                    armor += (kvp.Value * kvp.Key.UnitStatistics.Armor);
                else
                    magicResist += (kvp.Value * kvp.Key.UnitStatistics.MagicResist);
            }
            double result1 = 1.0;
            double result2 = 1.0;
            bool a;
            bool b;

            if( attackPointsPhysic > armor )
            {
                result1 = attackPointsPhysic / armor;
                a = true;
            }

            else
            {
                result1 = armor / attackPointsPhysic;
                a = false;
            }

            if( attackPointsMagic > magicResist )
            {
                result2 = attackPointsMagic / magicResist;
                b = true;
            }
            else
            {
                b = false;
                result2 = magicResist / attackPointsMagic;
            }

            if( a && b)
            {
                return attackingArmy.Island;
            } else if (!a && !b)
            {
                return defendingArmy.Island;
            } else if (a && !b)
            {
                if( result1 > result2)
                    return attackingArmy.Island;
                else
                    return defendingArmy.Island;
            } else if (!a && b)
                if( result2 > result1 )
                    return attackingArmy.Island;
                else
                    return defendingArmy.Island;
            else
            {
                return null; //Egalité.
            }
        }
    }
}
