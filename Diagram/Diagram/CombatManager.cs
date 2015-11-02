using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class CombatManager : ICombatManager
    {
        private Army _winningArmy;
        private Army _loosingArmy;

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
            if( attackPointsMagic < 5 || attackPointsPhysic < 5 )
            {
                SimpleAttack( attackingArmy, defendingArmy, attackPointsPhysic, physicResist, attackPointsMagic, magicResist );
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

        private void SimpleAttack( Army attackingArmy, Army defendingArmy, double attackPointsPhysic, double physicResist, double attackPointsMagic, double magicResist )
        {
            double ratioWinnerPhysic = 1.0;
            double ratioWinnerMagic = 1.0;
            double ratioLooserPhysic = 1.0;
            double ratioLooserMagic = 1.0;

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
                    _winningArmy = defendingArmy;
                    _loosingArmy = attackingArmy;
                }
                SimpleLossResult( ratioLooserPhysic );
            }
            else if( attackPointsPhysic < 5 )
            {
                if( attackPointsMagic > magicResist )
                {
                    ratioWinnerMagic = attackPointsMagic / magicResist;
                    ratioLooserMagic = magicResist / attackPointsMagic;
                    _winningArmy = attackingArmy;
                    _loosingArmy = defendingArmy;
                }
                else
                {
                    ratioWinnerMagic = magicResist / attackPointsMagic;
                    ratioLooserMagic = attackPointsMagic / magicResist;
                    _winningArmy = defendingArmy;
                    _loosingArmy = attackingArmy;
                }
                SimpleLossResult( ratioLooserMagic );

            }
            
        }

        private Army SimpleLossResult( double ratio )
        {
            double result = _winningArmy.Regiments.Values.First() * Math.Pow( ratio , 1.5);
            int regiment = _winningArmy.Regiments.Values.First();
            regiment -= (int)result;
            _winningArmy.Regiments.Remove( _winningArmy.Regiments.Keys.First() );
            _winningArmy.Regiments.Add( new Warrior(), regiment );
            Console.WriteLine( "Regiment = " + regiment );
            Console.WriteLine( " result = " + result );
            return _winningArmy;
        }
    }
}

