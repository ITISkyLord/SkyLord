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
            double physicResist = 1.0;
            double magicResist = 1.0;
            double ratioPhysicAttack;
            double ratioMagicAttack;
            double attackTotal;
            double attackPointsPhysic = 0;
            double attackPointsMagic = 0;

            foreach( KeyValuePair<Unit, int> kvp in attackingArmy.Regiments )
            {
                if( kvp.Key.UnitDamageType == UnitDamageType.physical )
                    attackPointsPhysic += (kvp.Value * kvp.Key.UnitStatistics.Attack);
                else
                    attackPointsMagic += (kvp.Value * kvp.Key.UnitStatistics.Attack);
            }
            attackTotal = attackPointsPhysic + attackPointsMagic;
            ratioPhysicAttack = attackPointsPhysic / attackTotal;
            ratioMagicAttack = attackPointsMagic / attackTotal;

            physicResist = GetPhysicResist( defendingArmy );
            magicResist = GetMagicResist( defendingArmy );

            if( attackPointsMagic == 0 || attackPointsPhysic == 0 )
            {
                SimpleAttack( attackingArmy, defendingArmy, attackPointsPhysic, physicResist, attackPointsMagic, magicResist );
            } else
            {
                ComplexeAttack( attackingArmy, defendingArmy, attackPointsPhysic, attackPointsMagic, physicResist, magicResist, ratioPhysicAttack, ratioMagicAttack);
            }
            return new CombatResult( _winningArmy, _loosingArmy );
        }

        private Army SimpleAttack( Army attackingArmy, Army defendingArmy, double attackPointsPhysic, double physicResist, double attackPointsMagic, double magicResist )
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
                return SimpleLossResult( ratioLooserPhysic, attackPointsPhysic, true );
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
                return SimpleLossResult( ratioLooserMagic, attackPointsMagic, false );
            }
            return null;
        }

        private Army SimpleLossResult( double ratio, double attackPoints, bool isPhysical )
        {
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
                foreach( int i in _winningArmy.Regiments.Values )
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
            _loosingArmy.Regiments.Clear();
            return _winningArmy;
        }

        private void ComplexeAttack( Army attackingArmy, Army defendingArmy, double attackPointsPhysic, double attackPointsMagic, double physicResist, double magicResist, double ratioPhysicAttack, double ratioMagicAttack )
        {
            bool physicWin = false;
            bool magicWin = false;
            Army attackingPhysicArmy = attackingArmy.Copy();
            attackingPhysicArmy.Regiments = attackingPhysicArmy.GetRegimentsByDamagetype( UnitDamageType.physical );
            Army attackingMagicArmy = attackingArmy.Copy();
            attackingMagicArmy.Regiments = attackingMagicArmy.GetRegimentsByDamagetype( UnitDamageType.magical );

            Army defendingAgainstPhysicArmy = defendingArmy.GetArmyByRatio( ratioPhysicAttack );
            Army defendingAgainstMagicArmy = defendingArmy.GetArmyByRatio( ratioMagicAttack );

            Army tmpArmyPhysic = SimpleAttack( attackingPhysicArmy, defendingAgainstPhysicArmy, attackPointsPhysic, GetPhysicResist( defendingAgainstPhysicArmy ), 0, 0 );
            if( tmpArmyPhysic == attackingPhysicArmy)
            {
                attackingPhysicArmy = _winningArmy; // Voir si ça marche ou s'il faut pas mettre tmpArmyPhysic
                defendingAgainstPhysicArmy = _loosingArmy;
                physicWin = true;

            } else if( tmpArmyPhysic == defendingAgainstPhysicArmy)
            {
                attackingPhysicArmy = _loosingArmy;
                defendingAgainstPhysicArmy = _winningArmy;  // Voir si ça marche ou s'il faut pas mettre tmpArmyPhysic
                physicWin = false;
            }
            Army tmpArmyMagic = SimpleAttack(attackingMagicArmy, defendingAgainstMagicArmy, 0,0,attackPointsMagic, GetMagicResist(defendingAgainstMagicArmy));
            if( tmpArmyMagic == attackingMagicArmy )
            {
                attackingMagicArmy = _winningArmy; // Voir si ça marche ou s'il faut pas mettre tmpArmyPhysic
                defendingAgainstMagicArmy = _loosingArmy;
                magicWin = true;
            }
            else if( tmpArmyMagic == defendingAgainstMagicArmy )
            {
                attackingMagicArmy = _loosingArmy; 
                defendingAgainstMagicArmy = _winningArmy; // Voir si ça marche ou s'il faut pas mettre tmpArmyMagic
                magicWin = false;
            }

            if( physicWin && magicWin )
            {
                // _winningArmy = attackingPhysicArmy.Join( attackingMagicArmy )();
                Console.WriteLine( "Victoire de l'attaquant !" );
            } else if( !physicWin && !magicWin )
            {
                //  _winningArmy = defendingAgainstPhysicArmy.Join( defendingAgainstMagicArmy )();
                Console.WriteLine( "Victoire du défenseur !" );

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private double GetPhysicResist( Army army )
        {
            double physicResist = 0;
            foreach( KeyValuePair<Unit,int> kvp in army.Regiments)
            {
                physicResist += (kvp.Value * kvp.Key.UnitStatistics.PhysicResist);
            }

            return physicResist;
        }
        private double GetMagicResist( Army army )
        {
            double magicResist = 0;
            foreach( KeyValuePair<Unit, int> kvp in army.Regiments)
            {
                magicResist += (kvp.Value * kvp.Key.UnitStatistics.MagicResist);
            }

            return magicResist;
        }
    }
}

