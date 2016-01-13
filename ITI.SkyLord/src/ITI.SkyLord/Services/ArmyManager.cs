using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class ArmyManager
    {
        public IArmyContext CurrentContext { get; set; }

        public ArmyManager( IArmyContext context)
        {
            CurrentContext = context;
            
        }

        public CombatResult ResolveCombat( Army attackingArmy, Army defendingArmy )
        {
            return new CombatManager(this).Resolve( attackingArmy, defendingArmy );
        }

        public Army GetArmy( long id )
        {
            return CurrentContext.Armies
                            .Include( a => a.Regiments ).ThenInclude( r => r.Unit )
                            .Include( a => a.Island ).ThenInclude( i => i.Armies )
                            .ThenInclude( a => a.Regiments ).ThenInclude( r => r.Unit )
                            .FirstOrDefault( a => a.ArmyId == id );
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
                // CurrentContext.SaveChanges();

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

                // CurrentContext.SaveChanges();
            }
            else
            {
                regimentFound.Number += number;
                // CurrentContext.SaveChanges();
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
            // CurrentContext.SaveChanges();

            if ( regimentFound.Number < 0 ) throw new ArgumentException( "A regiment cannot have a negative Number roperty." );
            if ( regimentFound.Number == 0 )
            {
                CurrentContext.Regiments.Remove( regimentFound );
                // CurrentContext.SaveChanges();
                if ( CurrentContext.Regiments.Where( r => r.ArmyId == armyFound.ArmyId ).Count() == 0 )
                {
                    CurrentContext.Armies.Remove( armyFound );
                    // CurrentContext.SaveChanges();
                }
            }
            return armyFound;
        }

        internal Army CreateArmy( Dictionary<string, int> unitsToSend, Island currentIsland )
        {
            Army army = new Army();

            List<Regiment> regiments = new List<Regiment>();

            army.ArmyState = ArmyState.movement;
            army.Island = currentIsland;
            army.Island.AllRessources = currentIsland.AllRessources; 
            CurrentContext.Armies.Add( army );

            foreach ( KeyValuePair<string, int> kvp in unitsToSend )
            {
                if( kvp.Value > 0 )
                {
                    UnitName uN = (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true );
                    Unit unit = CurrentContext.Units.Include( u => u.UnitStatistics ).Where( u => u.UnitName == uN ).FirstOrDefault();
                    regiments.Add( new Regiment { Unit = unit, Number = kvp.Value } );
                }
            }
            foreach( Regiment r in regiments ) 
            {
                CurrentContext.Add( r );
            }

            army.Regiments = regiments;

            Army defenseArmy = GetCurrentDefenseArmy( currentIsland.IslandId );

            SubstractFromArmy( defenseArmy, army );
            army.Island.AllRessources = currentIsland.AllRessources;
            return army;
        }

        public Army GetCurrentDefenseArmy( long IslandId )
        {
            return CurrentContext.Armies.Include( a => a.Island ).ThenInclude( a => a.AllRessources )
                .Include( a => a.Regiments ).ThenInclude( r => r.Unit)
                .SingleOrDefault( a => a.ArmyState == ArmyState.defense && a.Island.IslandId == IslandId );
        }

        /// <summary>
        /// Finds a specific regiment
        /// </summary>
        /// <param name="regiment"></param>
        /// <returns>The regiment found or null if it did not find any</returns>
        public Regiment FindRegiment( Army army, UnitName unitName )
        {
            return army.Regiments.Where( r => r.Unit.UnitName == unitName ).FirstOrDefault();
        }

        internal Army CopyArmy( Army originalArmy )
        {
            return new Army {
                Island = originalArmy.Island,
                Regiments = new List<Regiment>(originalArmy.Regiments),
                ArmyState = originalArmy.ArmyState
            };
        }

        public Army JoinArmies ( Army armyOnIsland, Army armyOnMovement )
        {
            Army joinedArmy = armyOnIsland;
            if( armyOnIsland == null || armyOnIsland.Regiments == null)
            {
                armyOnMovement.ArmyState = ArmyState.defense;
                // CurrentContext.SaveChanges();
                return armyOnMovement;
            }
            else
            {
                foreach( Regiment reg in armyOnMovement.Regiments )
                {
                    Regiment regimentFound = armyOnIsland.Regiments.Where( r => r.Unit.UnitName == reg.Unit.UnitName ).SingleOrDefault();
                    if( regimentFound == null )
                    {
                        armyOnIsland.Regiments.Add( reg );
                    }
                    else
                    {
                        regimentFound.Number += reg.Number;
                    }
                }
                armyOnMovement.ArmyState = ArmyState.obsolete;
                return armyOnMovement;
            }
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

            if ( finalUnitNumber > 0 )
                r.Number = finalUnitNumber;
            else if ( finalUnitNumber == 0 )
                army.Regiments.Remove( r );
            else
                throw new ArgumentException( " A regiment cannot have a negative value." );
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
          //  CurrentContext.SaveChanges(); À voir si ça été changé après l'impact sur l'attaqué, EventManager
        }

        internal void SubstractFromArmy( Army armyToRemoveFrom, Army armyToBeRemoved )
        {
            Army tmpArmy = this.CopyArmy( armyToBeRemoved );

            foreach ( Regiment r in armyToBeRemoved.Regiments )
            {
                this.RemoveUnit( r.Unit, r.Number, armyToRemoveFrom.Island, armyToRemoveFrom );
            }
        }

        /// <summary>
        /// Put the army on obsolete. 
        /// </summary>
        /// <param name="army"></param>
        internal void RemoveArmy( Army army )
        {
            if( army.Regiments == null )
                army.ArmyState = ArmyState.obsolete;
            // Remplace la suppression.
            else
            {
                foreach( Regiment r in army.Regiments )
                {
                    CurrentContext.Remove( r );
                }
                army.ArmyState = ArmyState.obsolete;
                // Remplace la suppression.
            }
        }

        /// <summary>
        /// Count the number of unit in a army.
        /// </summary>
        /// <returns></returns>
        internal double ArmyCount( Army army)
        {
            double number = 0;

            foreach ( Regiment r in army.Regiments )
            {
                number += r.Number;
            }

            return number;
        }
    }
}
