using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class CombatManager : ICombatManager
    {

        public CombatResult Resolve( Army attackingArmy, Army defendingArmy )
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
                    armor += (kvp.Value * kvp.Key.UnitStatistics.PhysicResist);
                else
                    magicResist += (kvp.Value * kvp.Key.UnitStatistics.MagicResist);
            }
            double ratioWinnerPhysic = 1.0;
            double ratioWinnerMagic = 1.0;
            double ratioLooserPhysic = 1.0;
            double ratioLooserMagic = 1.0;
            bool isAttackingArmyWinPhysic;
            bool isAttackingArmyWinMagic;
            Army _winningArmy = null;
            Army _loosingArmy = null;

            if( attackPointsPhysic > armor )
            {
                ratioWinnerPhysic = attackPointsPhysic / armor;
                ratioLooserPhysic = armor / attackPointsPhysic;
                isAttackingArmyWinPhysic = true;
            }
            else
            {
                ratioWinnerPhysic = armor / attackPointsPhysic;
                ratioLooserPhysic = attackPointsPhysic / armor;
                isAttackingArmyWinPhysic = false;
            }


            if( attackPointsMagic > magicResist )
            {
                ratioWinnerMagic = attackPointsMagic / magicResist;
                ratioLooserMagic = magicResist / attackPointsMagic;
                isAttackingArmyWinMagic = true;
            }
            else
            {
                isAttackingArmyWinMagic = false;
                ratioWinnerMagic = magicResist / attackPointsMagic;
                ratioLooserMagic = attackPointsMagic / magicResist;
            }





            if( isAttackingArmyWinPhysic && isAttackingArmyWinMagic)
            {
                _winningArmy = attackingArmy;
                _loosingArmy = defendingArmy;
            } else if (!isAttackingArmyWinPhysic && !isAttackingArmyWinMagic)
            {
                _winningArmy = defendingArmy;
                _loosingArmy = attackingArmy;
            } else if (isAttackingArmyWinPhysic && !isAttackingArmyWinMagic)
            {
                if( ratioWinnerPhysic > ratioWinnerMagic)
                {
                    _winningArmy = attackingArmy;
                    _loosingArmy = defendingArmy;
                }
                else
                {
                    _winningArmy = defendingArmy;
                    _loosingArmy = attackingArmy;
                }

            } else if (!isAttackingArmyWinPhysic && isAttackingArmyWinMagic)
            {
                if( ratioWinnerMagic > ratioWinnerPhysic )
                {
                    _winningArmy = attackingArmy;
                    _loosingArmy = defendingArmy;
                }
                else
                {
                    _winningArmy = defendingArmy;
                    _loosingArmy = attackingArmy;
                }
            }

            return new CombatResult( _winningArmy, _loosingArmy, ratioLooserPhysic, ratioLooserMagic );
        }
    }
}
