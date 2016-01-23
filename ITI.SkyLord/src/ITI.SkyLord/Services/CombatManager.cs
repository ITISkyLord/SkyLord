//
using ITI.SkyLord;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;

namespace ITI.SkyLord
{
    public class CombatManager : ICombatManager
    {
        private Army _winningArmy;
        private Army _loosingArmy;
        private Army _tmpWin;


        internal Dictionary<string, int> Loss { get; set; }

        private readonly ArmyManager _armyManager;

        public CombatManager( ArmyManager armyManager )
        {
            _armyManager = armyManager;
        }

        /// <summary>
        /// Resolve the fight beetween 2 armies.
        /// </summary>
        /// <param name="attackingArmy"></param>
        /// <param name="defendingArmy"></param>
        /// <returns></returns>
        public CombatResult Resolve( Army attackingArmy, Army defendingArmy, ArmyEvent ae, SetupContext ctx )
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

            return new CombatResult( _winningArmy, _loosingArmy, this, ae, ctx, _tmpWin );
        }

        internal CombatResult ResolveSendingRessources( Army sendingArmy, ArmyEvent ae, SetupContext ctx )
        {
            if( sendingArmy == null ) throw new ArgumentNullException( "sendingArmy is null" );
            return new CombatResult( sendingArmy, ae, ctx );
        }

        /// <summary>
        /// Return the physic resist of an army.
        /// </summary>
        /// <param name="army"></param>
        /// <returns></returns>
        private double GetPhysicResist( Army army )
        {
            double physicResist = 0;
            foreach( Regiment r in army.Regiments )
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
            foreach( Regiment r in army.Regiments )
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
            double totalDefense = physicResist * ( ratioPhysicAttack + 0.05 ) + magicResist * ( ratioMagicAttack + 0.05 );
            double totalWinner;
            double totalLooser;


            if( totalAttack > totalDefense )
            {
                _tmpWin = _armyManager.CopyArmy( attackingArmy );
                _winningArmy = attackingArmy;
                _loosingArmy = defendingArmy;
                totalWinner = totalAttack;
                totalLooser = totalDefense;
            }
            else
            {
                _tmpWin = _armyManager.CopyArmy( defendingArmy );
                _winningArmy = defendingArmy;
                _loosingArmy = attackingArmy;
                totalWinner = totalDefense;
                totalLooser = totalAttack;
            }

            double numberInWinnerArmy = _armyManager.ArmyCount( _winningArmy );
            double numberInLooserArmy = _armyManager.ArmyCount( _loosingArmy );
            double result = 100 * Math.Pow( ( totalLooser / totalWinner ), ( 1.5 - 0.08 * Math.Log10( ( numberInWinnerArmy + numberInLooserArmy ) / 1000 ) ) );
            Console.WriteLine( "result est : " + result );

            Loss = _armyManager.SubstractFromArmy( _winningArmy, (result / 100) );
            if( _loosingArmy.Regiments.Count > 0 )
                _armyManager.RemoveArmy( _loosingArmy );

            //100·(pp/pg)^X 
            //      X = 1,5 - 0,08·log10( N / 1000 ) où N correspond
            //au nombre de troupes( au nombres de troupes et non a la consomation de
            //céréales!)
        }

    }
}

