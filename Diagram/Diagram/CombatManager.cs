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
        double _attackPointsPhysic = 1.0;
        double _attackPointsMagic = 1.0;

        public CombatResult Resolve( Army attackingArmy, Army defendingArmy )
        {
            double physicResist = 1.0;
            double magicResist = 1.0;

            foreach( KeyValuePair<Unit, int> kvp in attackingArmy.Regiments )
            {
                if( kvp.Key.UnitDamageType == UnitDamageType.physical )
                    _attackPointsPhysic += (kvp.Value * kvp.Key.UnitStatistics.Attack);
                else
                    _attackPointsMagic += (kvp.Value * kvp.Key.UnitStatistics.Attack);
            }

            foreach( KeyValuePair<Unit, int> kvp in defendingArmy.Regiments )
            {
                physicResist += (kvp.Value * kvp.Key.UnitStatistics.PhysicResist);
                magicResist += (kvp.Value * kvp.Key.UnitStatistics.MagicResist);
            }
            if( _attackPointsMagic < 5 || _attackPointsPhysic < 5 )
            {
                SimpleAttack( attackingArmy, defendingArmy, _attackPointsPhysic, physicResist, _attackPointsMagic, magicResist );
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
                SimpleLossResult( ratioLooserPhysic, true );
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
                SimpleLossResult( ratioLooserMagic, false );

            }
            
        }

        private Army SimpleLossResult( double ratio, bool isPhysical )
        {

            //double result = _winningArmy.Regiments.Values.First() * Math.Pow( _ratioPhysic , 1.5);
            //int regiment = _winningArmy.Regiments.Values.First();
            int totalNumberInRegiments = 0;
            double result = 0;
            Dictionary<Unit,int> unitsWithDamage = new Dictionary<Unit, int>();
            Dictionary<Unit, double> unitsWithratio = new Dictionary<Unit, double>();
            if( isPhysical )
            {
                int attackPoints = 0;
                
                Dictionary<Unit,int> physicRegiments = _winningArmy.GetRegimentsByDamagetype( UnitDamageType.physical );

                foreach( KeyValuePair<Unit, int> kvp in physicRegiments )
                {
                    attackPoints += kvp.Value * kvp.Key.UnitStatistics.Attack;
                }
                foreach( KeyValuePair<Unit, int> kvp in physicRegiments )
                {
                    totalNumberInRegiments += kvp.Value;
                    unitsWithDamage.Add( kvp.Key, kvp.Value * kvp.Key.UnitStatistics.Attack );
                }
                result = totalNumberInRegiments * Math.Pow( ratio, 1.5 );

                foreach( KeyValuePair<Unit,int> kvp in unitsWithDamage )
                {
                    unitsWithratio.Add( kvp.Key, kvp.Value / attackPoints );
                }

                foreach( KeyValuePair<Unit, int> kvp in physicRegiments )
                {
                    foreach( KeyValuePair<Unit, double> kvp2 in unitsWithratio )
                    {
                        if( kvp.Key == kvp2.Key ) 
                        {

                        }
                    }
                }

            }
            int regiment = _winningArmy.Regiments.Count;
            
            

            regiment -= (int)result;
            _winningArmy.Regiments.Remove( _winningArmy.Regiments.Keys.First() );
            _winningArmy.Regiments.Add( new Warrior(), regiment );
            Console.WriteLine( "Regiment = " + regiment );
            Console.WriteLine( " result = " + result );
            return _winningArmy;
        }
    }
}

