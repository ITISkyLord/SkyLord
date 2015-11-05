using ITI.SkyLord.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class CombatManager : ICombatManager
    {
        private Army _winningArmy;
        private Army _loosingArmy;
        private Army _attackingArmy;
        private Army _defendingArmy;
        double _attackPointsPhysic = 0;
        double _attackPointsMagic = 0;

        public CombatResult Resolve( Army attackingArmy, Army defendingArmy )
        {
            double physicResist = 1.0;
            double magicResist = 1.0;
            this._attackingArmy = attackingArmy;
            this._defendingArmy = defendingArmy;

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
            return new CombatResult(_winningArmy, _loosingArmy);
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
                SimpleLossResult( ratioLooserPhysic, attackPointsPhysic, true );
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
                SimpleLossResult( ratioLooserMagic, attackPointsMagic, false );
            }

        }

        private void SimpleLossResult( double ratio, double attackPoints, bool isPhysical )
        {

            //double result = _winningArmy.Regiments.Values.First() * Math.Pow( _ratioPhysic , 1.5);
            //int regiment = _winningArmy.Regiments.Values.First();
            int totalNumberInRegiments = 0;
            double result = 0;
            Dictionary<Unit,int> typeRegiments = new Dictionary<Unit, int>();
            Dictionary<Unit,int> unitsWithDamage = new Dictionary<Unit, int>();
            Dictionary<Unit,double> unitsWithratio = new Dictionary<Unit, double>();


            if( isPhysical )
            {
                typeRegiments = _winningArmy.GetRegimentsByDamagetype( UnitDamageType.physical );
            }
            else
            {
                typeRegiments = _winningArmy.GetRegimentsByDamagetype( UnitDamageType.magical );
            }

            if( _winningArmy.ArmyState == ArmyState.movement )
            {
                //foreach( KeyValuePair<Unit, int> kvp in typeRegiments )
                //{
                //    attackPoints += kvp.Value * kvp.Key.UnitStatistics.Attack;
                //}
                foreach( KeyValuePair<Unit, int> kvp in typeRegiments )
                {
                    totalNumberInRegiments += kvp.Value;
                    unitsWithDamage.Add( kvp.Key, kvp.Value * kvp.Key.UnitStatistics.Attack );
                }

                foreach( KeyValuePair<Unit, int> kvp in unitsWithDamage )
                {
                    double res = (double)kvp.Value / attackPoints;
                    unitsWithratio.Add( kvp.Key, res );
                }

                foreach( KeyValuePair<Unit, int> kvp in typeRegiments )
                {
                    result = kvp.Value * Math.Pow( ratio, 1.5 );

                    foreach( KeyValuePair<Unit, double> kvp2 in unitsWithratio )
                    {
                        if( kvp2.Key == kvp.Key )
                        {

                            int loss = (int)(result * kvp2.Value);
                            Console.Write( "Unit = " + kvp.Key );
                            Console.WriteLine( " : loss = " + loss );
                            Console.WriteLine( "Avant le substract, winningArmy première unité dans le dico = " + _winningArmy.Regiments.Values.First() );
                            Console.WriteLine( "winningArmyState = " + _winningArmy.ArmyState );

                            _winningArmy.SubstractFromRegiment( kvp.Key, loss );
                            Console.WriteLine( "Après le substract, winningArmy première unité dans le dico = " + _winningArmy.Regiments.Values.First() );
                        }
                    }
                }
            }
            else
            {
                int numberOfDefendingTroops = 0;
                int numberOfUnits = 0;
                foreach( int i in _defendingArmy.Regiments.Values )
                {
                    numberOfDefendingTroops += i;
                    numberOfUnits++;
                }
                result = numberOfDefendingTroops * Math.Pow( ratio, 1.5 );
                int resultByUnits = (int)result / numberOfUnits;
                Army tmpArmy = _winningArmy.Copy();
                foreach( Unit u in tmpArmy.Regiments.Keys )
                {
                    Console.Write( "Unit = " + u );
                    Console.WriteLine( " : loss = " + resultByUnits );
                    Console.WriteLine( "winningArmyState = " + _winningArmy.ArmyState );
                    Console.WriteLine( "Avant le substract, winningArmy warrior = " + _winningArmy.Regiments.Values.First() );
                    _winningArmy.SubstractFromRegiment( u, resultByUnits );
                    Console.WriteLine( "Après le substract, winningArmy warrior = " + _winningArmy.Regiments.Values.First() );

                }
            }
        }
    }
}

