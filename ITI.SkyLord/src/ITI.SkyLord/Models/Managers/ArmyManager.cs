using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Managers
{
    public class ArmyManager
    {
        ArmyContext CurrentContext { get; set; }
        public ArmyManager( ArmyContext context )
        {
            CurrentContext = context;
        }

        public CombatResult ResolveCombat( Army attackingArmy, Army defendingArmy )
        {
            return new CombatManager().Resolve( attackingArmy, defendingArmy );
        }

        /// <summary>
        /// Adds a unit to the current island's army. Creates or updates the army and regiment accordingly.
        /// </summary>
        /// <param name="unit">The type of unit to add</param>
        /// <param name="number">The number of units to add</param>
        /// <param name="island">The current island</param>
        public Army AddUnit( Unit unit, int number, Island island )
        {
            Island islandFound = CurrentContext.Islands.Include( i => i.Armies )
            .SingleOrDefault( i => i.IslandId == island.IslandId );
            if ( islandFound == null ) throw new ArgumentException( "The island does not exist in the Database" );

            Army armyFound = islandFound.Armies.Where( a => a.ArmyState == ArmyState.defense ).SingleOrDefault();

            if ( armyFound == null )
            {
                Army newArmy = new Army
                {
                    ArmyState = ArmyState.defense,
                    Island = islandFound
                };
                CurrentContext.Armies.Add( newArmy );
                CurrentContext.SaveChanges();

                armyFound = newArmy;
            }

            Regiment regimentFound = CurrentContext.Regiments.FirstOrDefault( r => r.ArmyId == armyFound.ArmyId && r.Unit.UnitId == unit.UnitId );
            if ( regimentFound == null )
            {
                Regiment newRegiment = new Regiment
                {
                    Unit = CurrentContext.Units.SingleOrDefault( u => u.UnitId == unit.UnitId ),
                    Number = number,
                    ArmyId = armyFound.ArmyId
                };
                CurrentContext.Regiments.Add( newRegiment );

                CurrentContext.SaveChanges();
            }
            else
            {
                regimentFound.Number += number;
                CurrentContext.SaveChanges();
            }

            return armyFound;
        }

        public Army RemoveUnit( Unit unit, int number, Island island, Army army )
        {
            // Checks if the island exists
            Island islandFound = CurrentContext.Islands.Include( i => i.Armies ).Single( i => i.IslandId == island.IslandId );
            // Checks if the army belongs to the island, if not exception
            Army armyFound = islandFound.Armies.Single( a => a.ArmyId == army.ArmyId );
            // Checks if there is a single regiment that belongs to the army, if not exception
            Regiment regimentFound = CurrentContext.Regiments.Single( r => r.ArmyId == armyFound.ArmyId && r.Unit.UnitId == unit.UnitId );

            regimentFound.Number -= number;
            CurrentContext.SaveChanges();

            if ( regimentFound.Number < 0 ) throw new ArgumentException( "A regiment cannot have a negative Number roperty." );
            if ( regimentFound.Number == 0 )
            {
                CurrentContext.Regiments.Remove( regimentFound );
                CurrentContext.SaveChanges();
                if ( CurrentContext.Regiments.Where( r => r.ArmyId == armyFound.ArmyId ).Count() == 0 )
                {
                    CurrentContext.Armies.Remove( armyFound );
                    CurrentContext.SaveChanges();
                }
            }
            return armyFound;
        }

        internal Army CreateArmy( Dictionary<string, int> unitsToSend )
        {
            Army army = new Army();

            List<Regiment> regiments = new List<Regiment>();
            foreach( KeyValuePair<string, int> kvp in unitsToSend )
            {
                UnitName uN = (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true );
                Unit unit = CurrentContext.Units.Where( u => u.UnitName == uN).FirstOrDefault();
                regiments.Add( new Regiment { Unit = unit, Number = kvp.Value } );
                
            }

            foreach( Regiment r in regiments ) 
            {
                CurrentContext.Add( r );
            }
            CurrentContext.SaveChanges();
            army.Regiments = regiments;
            army.ArmyState = ArmyState.movement;
            CurrentContext.Armies.Add( army );
            CurrentContext.SaveChanges();
            return army;
        }

        /// <summary>
        /// Finds a specific regiment
        /// </summary>
        /// <param name="regiment"></param>
        /// <returns>The regiment found or null if it did not find any</returns>
        public Regiment FindRegiment( Army army, UnitName unitName )
        {
            return CurrentContext.Regiments.Where( r => r.ArmyId == army.ArmyId && r.Unit.UnitName == unitName ).FirstOrDefault();
        }

        internal Army CopyArmy( Army originalArmy )
        {
            return new Army {
                Island = originalArmy.Island,
                Regiments = new List<Regiment>(originalArmy.Regiments),
                ArmyState = originalArmy.ArmyState
            };
        }

        /// <summary>
        /// Substracts a number of units from a regiment in a army, if the final number is under 0, it removes the regiment.
        /// DOES NOT AFFECT DATABASE
        /// </summary>
        /// <param name="unit">the unit type to remove.</param>
        /// <param name="numberToTemove">the number of units to substract.</param>
        internal void SubstractFromRegiment( Army army, Unit unit, int numberToTemove )
        {
            Regiment r = FindRegiment( army, unit.UnitName );

            if ( r == null ) throw new ArgumentException( "The unit you are trying to handle is not present in the army." );

            int initialUnitNumber = r.Number;
            int finalUnitNumber = initialUnitNumber - numberToTemove;
            army.Regiments.Remove( r );

            if ( finalUnitNumber > 0 )
                army.Regiments.Add( new Regiment { Army = army, Number = finalUnitNumber, Unit = unit } );
        }

        internal void SubstractFromArmy( Army army, double ratio )
        {
            Army tmpArmy = this.CopyArmy( army );

            foreach ( Regiment r in tmpArmy.Regiments )
            {
                int number = (int)( r.Number * ratio );
                Console.WriteLine( "number in the unit : " + r.Name + " = " + r.Number );
                Console.WriteLine( "loss = " + number );
                this.SubstractFromRegiment( army, r.Unit, number);
            }
        }
    }
}
