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
            double physicResist = 1.0;
            double attackPointsMagic = 1.0;
            double magicResist = 1.0;

            foreach( KeyValuePair<Unit, int> kvp in attackingArmy.Regiments )
            {
                if( kvp.Key.UnitDamageType == UnitDamageType.physical )
                    attackPointsPhysic += (kvp.Value * kvp.Key.UnitStatistics.Attack);
                else
                    attackPointsMagic += (kvp.Value * kvp.Key.UnitStatistics.Attack);

            }

            foreach( KeyValuePair<Unit, int> kvp in defendingArmy.Regiments )
            {
                physicResist += (kvp.Value * kvp.Key.UnitStatistics.PhysicResist);
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

            if( attackPointsMagic <5 || attackPointsPhysic < 5 )
            {
                if( attackPointsMagic < 5 )
                {
                    if( attackPointsPhysic > physicResist )
                    {
                        ratioWinnerPhysic = attackPointsPhysic / physicResist;
                        ratioLooserPhysic = physicResist / attackPointsPhysic;
                        _winningArmy = attackingArmy;
                        _loosingArmy = defendingArmy;
                    }
                    else
                    {
                        ratioWinnerPhysic = physicResist / attackPointsPhysic;
                        ratioLooserPhysic = attackPointsPhysic / physicResist;
                        isAttackingArmyWinPhysic = false;
                        _winningArmy = defendingArmy;
                        _loosingArmy = attackingArmy;
                    }

                }
                else if( attackPointsPhysic < 5 )
                {
                    if( attackPointsMagic > magicResist )
                    {
                        ratioWinnerMagic = attackPointsMagic / magicResist;
                        ratioLooserMagic = magicResist / attackPointsMagic;
                        isAttackingArmyWinMagic = true;
                        _winningArmy = attackingArmy;
                        _loosingArmy = defendingArmy;
                    }
                    else
                    {
                        isAttackingArmyWinMagic = false;
                        ratioWinnerMagic = magicResist / attackPointsMagic;
                        ratioLooserMagic = attackPointsMagic / magicResist;
                        _winningArmy = defendingArmy;
                        _loosingArmy = attackingArmy;
                    }
                }
                return new CombatResult( _winningArmy, _loosingArmy, ratioLooserPhysic, ratioLooserMagic );
            }

            return null;
          

            //else if( isAttackingArmyWinPhysic && !isAttackingArmyWinMagic )
            //{
            //    if( ratioWinnerPhysic > ratioWinnerMagic )
            //    {
            //        _winningArmy = attackingArmy;
            //        _loosingArmy = defendingArmy;
            //    }
            //    else
            //    {
            //        _winningArmy = defendingArmy;
            //        _loosingArmy = attackingArmy;
            //    }

            //}
            //else if( !isAttackingArmyWinPhysic && isAttackingArmyWinMagic )
            //{
            //    if( ratioWinnerMagic > ratioWinnerPhysic )
            //    {
            //        _winningArmy = attackingArmy;
            //        _loosingArmy = defendingArmy;
            //    }
            //    else
            //    {
            //        _winningArmy = defendingArmy;
            //        _loosingArmy = attackingArmy;
            //    }
            //}


        }
    }
}

