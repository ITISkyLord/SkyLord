//using ITI.SkyLord.Units;
using ITI.SkyLord;
using ITI.SkyLord.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.Model
{
    public class CombatManager : ICombatManager
    {
        private Army _winningArmy;
        private Army _loosingArmy;

        /// <summary>
        /// Resolve the fight beetween 2 armies.
        /// </summary>
        /// <param name="attackingArmy"></param>
        /// <param name="defendingArmy"></param>
        /// <returns></returns>
        public CombatResult Resolve( Army attackingArmy, Army defendingArmy )
        {
            if( attackingArmy == null ) throw new ArgumentNullException( "attackingArmy is null" );
            if( defendingArmy == null ) throw new ArgumentNullException( "defendingArmy is null" );
            double physicResist = 1.0;
            double magicResist = 1.0;
            double ratioPhysicAttack;
            double ratioMagicAttack;
            double attackTotal;
            double attackPointsPhysic = 0;
            double attackPointsMagic = 0;

            foreach( Regiment r in attackingArmy.Regiments )
            {
                if( r.Unit.UnitDamageType == UnitDamageType.physical )
                    attackPointsPhysic += (r.Number * r.Unit.UnitStatistics.Attack);
                else
                    attackPointsMagic += (r.Number * r.Unit.UnitStatistics.Attack);
            }
            attackTotal = attackPointsPhysic + attackPointsMagic;
            ratioPhysicAttack = attackPointsPhysic / attackTotal;
            ratioMagicAttack = attackPointsMagic / attackTotal;

            physicResist = GetPhysicResist( defendingArmy );
            magicResist = GetMagicResist( defendingArmy );
            Fight( attackingArmy, defendingArmy, attackPointsPhysic, physicResist, attackPointsMagic, magicResist, ratioPhysicAttack, ratioMagicAttack );

            return new CombatResult( _winningArmy, _loosingArmy );
        }

        /// <summary>
        /// Simple attack in case of round combat, Obsolete at the moment.
        /// </summary>
        /// <param name="attackingArmy"></param>
        /// <param name="defendingArmy"></param>
        /// <param name="attackPointsPhysic"></param>
        /// <param name="physicResist"></param>
        /// <param name="attackPointsMagic"></param>
        /// <param name="magicResist"></param>
        /// <returns></returns>
        [Obsolete]
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

        /// <summary>
        /// Calculate the losses of the simple attack and complexe attack in case of round combat. Obsolete at the moment.
        /// </summary>
        /// <param name="ratio"></param>
        /// <param name="attackPoints"></param>
        /// <param name="isPhysical"></param>
        /// <returns></returns>
        [Obsolete]
        private Army SimpleLossResult( double ratio, double attackPoints, bool isPhysical )
        {
            int totalNumberInRegiments = 0;
            double result = 0;
            List<Regiment> typeRegiments = new List<Regiment>();
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
                foreach( Regiment r in typeRegiments )
                {
                    totalNumberInRegiments += r.Number;
                    unitsWithDamage.Add( r.Unit, r.Number * r.Unit.UnitStatistics.Attack );
                }

                foreach( KeyValuePair<Unit, int> kvp in unitsWithDamage )
                {
                    double res = (double)kvp.Value / attackPoints;
                    unitsWithratio.Add( kvp.Key, res );
                }

                foreach( Regiment r in typeRegiments )
                {
                    result = r.Number * Math.Pow( ratio, 1.5 );

                    foreach( KeyValuePair<Unit, double> kvp2 in unitsWithratio )
                    {
                        if( kvp2.Key == r.Unit )
                        {

                            int loss = (int)(result * kvp2.Value);
                            Console.Write( "Unit = " + r.Name );
                            Console.WriteLine( " : loss = " + loss );
                            Console.WriteLine( "Avant le substract, winningArmy première unité dans le dico = " + _winningArmy.Regiments.First() );
                            Console.WriteLine( "winningArmyState = " + _winningArmy.ArmyState );

                            _winningArmy.SubstractFromRegiment( r.Unit, loss );
                            Console.WriteLine( "Après le substract, winningArmy première unité dans le dico = " + _winningArmy.Regiments.First() );
                        }
                    }
                }
            }
            else
            {
                int numberOfDefendingTroops = 0;
                int numberOfUnits = 0;
                foreach( Regiment r in _winningArmy.Regiments )
                {
                    numberOfDefendingTroops += r.Number;
                    numberOfUnits++;
                }
                result = numberOfDefendingTroops * Math.Pow( ratio, 1.5 );
                int resultByUnits = (int)result / numberOfUnits;
                Army tmpArmy = _winningArmy.Copy();
                foreach( Regiment r in tmpArmy.Regiments )
                {
                    Console.Write( "Unit = " + r.Unit );
                    Console.WriteLine( " : loss = " + resultByUnits );
                    Console.WriteLine( "winningArmyState = " + _winningArmy.ArmyState );
                    Console.WriteLine( "Avant le substract, winningArmy warrior = " + _winningArmy.Regiments.First() );
                    _winningArmy.SubstractFromRegiment( r.Unit, resultByUnits );
                    Console.WriteLine( "Après le substract, winningArmy warrior = " + _winningArmy.Regiments.First() );

                }
            }
            _loosingArmy.ClearRegiments();
            return _winningArmy;
        }

        /// <summary>
        /// Complexe attack in case of round combat. Obsolete at the moment.
        /// </summary>
        /// <param name="attackingArmy"></param>
        /// <param name="defendingArmy"></param>
        /// <param name="attackPointsPhysic"></param>
        /// <param name="attackPointsMagic"></param>
        /// <param name="physicResist"></param>
        /// <param name="magicResist"></param>
        /// <param name="ratioPhysicAttack"></param>
        /// <param name="ratioMagicAttack"></param>
        [Obsolete]
        private void ComplexeAttack( Army attackingArmy, Army defendingArmy, double attackPointsPhysic, double attackPointsMagic, double physicResist, double magicResist, double ratioPhysicAttack, double ratioMagicAttack )
        {
            bool physicWin = false;
            bool magicWin = false;
            Army attackingPhysicArmy = attackingArmy.Copy();
            //
            // A CHANGER SI LA METHODE EST EXPLITÉE UN JOUR
            //
            // attackingPhysicArmy.Regiments = attackingPhysicArmy.GetRegimentsByDamagetype( UnitDamageType.physical );
            Army attackingMagicArmy = attackingArmy.Copy();
            // attackingMagicArmy.Regiments = attackingMagicArmy.GetRegimentsByDamagetype( UnitDamageType.magical );

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
                attackingPhysicArmy.JoinArmies( attackingMagicArmy );
                _winningArmy = attackingPhysicArmy;
                Console.WriteLine( "Victoire de l'attaquant !" );
            } else if( !physicWin && !magicWin )
            {
                 defendingAgainstPhysicArmy.JoinArmies( defendingAgainstMagicArmy );
                _winningArmy = defendingAgainstPhysicArmy;
                Console.WriteLine( "Victoire du défenseur !" );

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Return the physic resist of an army.
        /// </summary>
        /// <param name="army"></param>
        /// <returns></returns>
        private double GetPhysicResist( Army army )
        {
            double physicResist = 0;
            foreach( Regiment r in army.Regiments)
            {
                physicResist += r.Number * r.Unit.UnitStatistics.PhysicResist;
            }

            return physicResist;
        }
        /// <summary>
        /// Return the magic resist of an army.
        /// </summary>
        /// <param name="army"></param>
        /// <returns></returns>
        private double GetMagicResist( Army army )
        {
            double magicResist = 0;
            foreach( Regiment r in army.Regiments)
            {
                magicResist += (r.Number * r.Unit.UnitStatistics.MagicResist);
            }

            return magicResist;
        }

        /// <summary>
        /// General fight. Modify winning army and loosing army. LossingArmy is cleared. Winning army get losses.
        /// </summary>
        /// <param name="attackingArmy"></param>
        /// <param name="defendingArmy"></param>
        /// <param name="attackPointsPhysic"></param>
        /// <param name="physicResist"></param>
        /// <param name="attackPointsMagic"></param>
        /// <param name="magicResist"></param>
        /// <param name="ratioPhysicAttack"></param>
        /// <param name="ratioMagicAttack"></param>
        private void Fight( Army attackingArmy, Army defendingArmy, double attackPointsPhysic, double physicResist, double attackPointsMagic, double magicResist, double ratioPhysicAttack, double ratioMagicAttack )
        {
            if( attackingArmy == null ) throw new ArgumentNullException( "attackingArmy is null" );
            if( defendingArmy == null ) throw new ArgumentNullException( "defendingArmy is null" );
            if( attackPointsPhysic < 0 ) throw new ArgumentException( "attackPointsPhysic under 0 is anormal." );
            if( physicResist < 0 ) throw new ArgumentException( "physicResist under 0 is anormal." );
            if( magicResist < 0 ) throw new ArgumentException( "magicResist under 0 is anormal." );
            if( ratioPhysicAttack < 0 ) throw new ArgumentException( "ratioPhysicAttack under 0 is anormal." );
            if( ratioMagicAttack < 0 ) throw new ArgumentException( "ratioMagicAttack under 0 is anormal." );

            double totalAttack = attackPointsMagic + attackPointsPhysic;
            double totalDefense = physicResist * (ratioPhysicAttack+0.05) + magicResist * (ratioMagicAttack+0.05);
            double totalWinner;
            double totalLooser;


            if( totalAttack > totalDefense )
            {
                _winningArmy = attackingArmy;
                _loosingArmy = defendingArmy;
                totalWinner = totalAttack;
                totalLooser = totalDefense;
            } else
            {
                _winningArmy = defendingArmy;
                _loosingArmy = attackingArmy;
                totalWinner = totalDefense;
                totalLooser = totalAttack;
            }
            double numberInWinnerArmy = _winningArmy.Count();
            double numberInLooserArmy = _loosingArmy.Count();
            double result = 100 * Math.Pow((totalLooser/totalWinner), (1.5 - 0.08 * Math.Log10(( numberInWinnerArmy + numberInLooserArmy ) / 1000 )));
            Console.WriteLine( "result est : " + result );

            _winningArmy.SubstractFromArmy( result / 100 );
            _loosingArmy.ClearRegiments();

            //100·(pp/pg)^X 
      //      X = 1,5 - 0,08·log10( N / 1000 ) où N correspond
      //au nombre de troupes( au nombres de troupes et non a la consomation de
      //céréales!)

        }

    }
}

