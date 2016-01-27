using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public class BonusManager
    {
        public ILevelContext CurrentContext { get; }

        public BonusManager( ILevelContext currentContext )
        {
            CurrentContext = currentContext;
        }

        #region Units bonuses

        /// <summary>
        /// Get the modified duration of a unit, DOES NOT CHANGE DB
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="playerId"></param>
        /// <param name="islandId"></param>
        /// <returns></returns>
        public int GetModifiedDuration( Unit unit, long playerId, long islandId )
        {
            return ResolveBonuses( unit, playerId, islandId ).Duration;
        }

        /// <summary>
        /// Resolves all the units contained in all the armies of a player (CHANGES THE UNIT IN DB !)
        /// </summary>
        /// <param name="playerId">The player</param>
        public void ResolvePlayersArmies( long playerId, long islandId )
        {
            List<Island> playersIslands = CurrentContext.Islands.Include( i => i.Owner ).Include( i => i.Armies )
                .ThenInclude( a => a.Regiments ).ThenInclude( r => r.Unit ).ThenInclude( u => u.UnitStatistics )
                .Where( i => i.Owner.PlayerId == playerId ).ToList();

            List<Regiment> allPlayersRegiments = new List<Regiment>();

            foreach ( Island island in playersIslands )
            {
                foreach ( Army army in island.Armies )
                {
                    allPlayersRegiments.AddRange( army.Regiments );
                }
            }

            // Update all the player's units in all his regiments with a new modified unit
            foreach ( Regiment regiment in allPlayersRegiments )
            {
                Unit resolvedUnit = ResolveBonuses( regiment.Unit, playerId, islandId );

                CurrentContext.Add( resolvedUnit.UnitStatistics );
                CurrentContext.Remove( regiment.Unit.UnitStatistics );

                regiment.Unit.Duration = resolvedUnit.Duration;
                regiment.Unit.IsModel = false;
                regiment.Unit.Name = resolvedUnit.Name;
                resolvedUnit.Requirements = resolvedUnit.Requirements;
                regiment.Unit.UnitCost = resolvedUnit.UnitCost;
                regiment.Unit.UnitDamageType = resolvedUnit.UnitDamageType;
                regiment.Unit.UnitName = resolvedUnit.UnitName;
                regiment.Unit.UnitStatistics = resolvedUnit.UnitStatistics;
                regiment.Unit.UnitType = resolvedUnit.UnitType;

            }
        }

        public List<Unit> GetResolvedUnits( List<Unit> units, long playerId, long islandId )
        {
            List<Bonus> bonusesOnCurrentIsland = GetAllBonusesOnCurrentIsland( playerId, islandId );

            List<Unit> resolvedUnits = new List<Unit>();
            foreach ( Unit unit in units )
            {
                resolvedUnits.Add( ResolveBonuses( unit, bonusesOnCurrentIsland, playerId, islandId ) );
            }
            return resolvedUnits;
        }

        /// <summary>
        /// Creates a Unit wich has resolved all its bonuses (does not add it to the DB !)
        /// </summary>
        /// <param name="unit">The Unit to resolve</param>
        /// <param name="playerId">The Id of the player</param>
        /// <param name="islandId">The Id of the island</param>
        /// <returns>A cloned Unit with all its bonuses applied</returns>
        public Unit ResolveBonuses( Unit unit, long playerId, long islandId )
        {
            Unit resolvedUnit = CurrentContext.Units.Include( u => u.UnitStatistics ).Include( u => u.Requirements ).Include( u => u.UnitCost )
                .Single( u => u.UnitName == unit.UnitName && u.IsModel );

            List<Bonus> bonusesOnCurrentIsland = GetAllBonusesOnCurrentIsland( playerId, islandId );

            resolvedUnit = CloneUnit( resolvedUnit );
            foreach ( BonusOnUnit bonus in GetBonusesOnUnit( unit, bonusesOnCurrentIsland ) )
            {
                resolvedUnit = ResolveUnitBonus( resolvedUnit, bonus );
            }

            return resolvedUnit;
        }

        /// <summary>
        /// Creates  a unit that have resolved their bonuses, with a bonusList already defined
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="bonusesOnCurrentIsland"></param>
        /// <param name="playerId"></param>
        /// <param name="islandId"></param>
        /// <returns></returns>
        public Unit ResolveBonuses( Unit unit, List<Bonus> bonusesOnCurrentIsland, long playerId, long islandId )
        {
            Unit resolvedUnit = CurrentContext.Units.Include( u => u.UnitStatistics ).Include( u => u.Requirements ).Include( u => u.UnitCost )
                .Single( u => u.UnitName == unit.UnitName && u.IsModel );

            resolvedUnit = CloneUnit( resolvedUnit );
            foreach ( BonusOnUnit bonus in GetBonusesOnUnit( unit, bonusesOnCurrentIsland ) )
            {
                resolvedUnit = ResolveUnitBonus( resolvedUnit, bonus );
            }

            return resolvedUnit;
        }

        private List<BonusOnUnit> GetBonusesOnUnit( Unit unit, List<Bonus> bonusesOnCurrentIsland )
        {
            List<BonusOnUnit> allBonusOnUnit = new List<BonusOnUnit>();

            foreach ( Bonus bonus in bonusesOnCurrentIsland )
            {
                if ( bonus is BonusOnUnit )
                {
                    BonusOnUnit bonusOnUnit = (BonusOnUnit)bonus;
                    if ( bonusOnUnit.TargetUnit == UnitType.all || bonusOnUnit.TargetUnit == unit.UnitType )
                    {
                        allBonusOnUnit.Add( bonusOnUnit );
                    }
                }
            }
            return allBonusOnUnit;
        }

        private Unit ResolveUnitBonus( Unit unit, BonusOnUnit bonus )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.army_attack:
                    unit.UnitStatistics.Attack += (int)( unit.UnitStatistics.Attack * ( (double)bonus.Modifier / 100 ) );
                    break;
                case BonusType.army_magicalDefense:
                    unit.UnitStatistics.MagicResist += (int)( unit.UnitStatistics.MagicResist * ( (double)bonus.Modifier / 100 ) );
                    break;
                case BonusType.army_physicalDefense:
                    unit.UnitStatistics.PhysicResist += (int)( unit.UnitStatistics.PhysicResist * ( (double)bonus.Modifier / 100 ) );
                    break;
                case BonusType.army_speed:
                    unit.UnitStatistics.Speed += (int)( unit.UnitStatistics.Speed * ( (double)bonus.Modifier / 100 ) );
                    break;
                case BonusType.army_capacity:
                    unit.UnitStatistics.Capacity += (int)( unit.UnitStatistics.Capacity * ( (double)bonus.Modifier / 100 ) );
                    break;
                case BonusType.duration:
                    unit.Duration -= (int)( unit.Duration * ( (double)bonus.Modifier / 100 ) );
                    break;
                default:
                    throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
            }

            return unit;
        }

        /// <summary>
        /// Creates a clone of a Unit with the same reference to Requirements and Cost, but a new local UnitStatistics
        /// </summary>
        /// <param name="unit">The Unit to clone</param>
        /// <returns>The cloned Unit</returns>
        private Unit CloneUnit( Unit unit )
        {
            UnitStatistics unitStatitics = new UnitStatistics
            {
                Attack = unit.UnitStatistics.Attack,
                PhysicResist = unit.UnitStatistics.PhysicResist,
                MagicResist = unit.UnitStatistics.MagicResist,
                Speed = unit.UnitStatistics.Speed,
                Capacity = unit.UnitStatistics.Capacity,
                Consumption = unit.UnitStatistics.Capacity
            };

            Ressource unitCost = new Ressource
            {
                Wood = unit.UnitCost.Wood,
                Metal = unit.UnitCost.Metal,
                Cristal = unit.UnitCost.Cristal,
                Magic = unit.UnitCost.Magic
            };

            List<Requirement> unitRequirements = new List<Requirement>();
            foreach ( Requirement requirement in unit.Requirements )
            {
                unitRequirements.Add( new Requirement
                {
                    BuildingName = requirement.BuildingName,
                    TechnologyName = requirement.TechnologyName,
                    Number = requirement.Number
                } );
            }

            return new Unit
            {
                Name = unit.Name,
                UnitName = unit.UnitName,
                UnitType = unit.UnitType,
                UnitDamageType = unit.UnitDamageType,
                UnitCost = unit.UnitCost,
                UnitStatistics = unitStatitics,
                Requirements = unitRequirements,
                Duration = unit.Duration,
                IsModel = false,
                Description = unit.Description
            };

        }

        private List<Unit> GetUnitsByUnitType( List<Island> playerIslands, UnitType unitType )
        {
            List<Army> armiesToResolve = new List<Army>();
            foreach ( List<Army> armyList in playerIslands.Select( i => i.Armies ) )
            {
                armiesToResolve.Intersect( armyList );
            }

            List<Regiment> regimentToResolve = new List<Regiment>();
            foreach ( List<Regiment> regimentList in armiesToResolve.Select( a => a.Regiments ) )
            {
                regimentToResolve.Intersect( regimentList );
            }

            return regimentToResolve.Where( r => r.Unit.UnitType == unitType ).Select( r => r.Unit ).ToList();
        }
        #endregion

        #region BuildingBonuses

        public int GetModifiedDuration( BuildingLevel buildingLevel, long playerId, long islandId )
        {
            return ResolveBonuses( buildingLevel, playerId, islandId ).Duration;
        }

        /// <summary>
        /// Resolves the bonuses of a list of buildings (DOES NOT CHANGE DB !)
        /// </summary>
        /// <param name="buildings">The buildings to resolve</param>
        /// <param name="playerId"></param>
        /// <param name="islandId"></param>
        /// <returns> A list of cloned buildings with their bonuses ressolved</returns>
        public List<Building> GetResolvedBuildings( List<Building> buildings, long playerId, long islandId )
        {
            List<Bonus> bonusesOnCurrentIsland = GetAllBonusesOnCurrentIsland( playerId, islandId );

            List<Building> resolvedBuildings = new List<Building>();
            foreach ( Building building in buildings )
            {
                BuildingLevel resolvedLevel = ResolveBonuses( building.Level, bonusesOnCurrentIsland, playerId, islandId );
                resolvedBuildings.Add( CloneBuilding( building, resolvedLevel ) );
            }
            return resolvedBuildings;
        }

        /// <summary>
        /// Creates a BuildingLevel wich has resolved all its bonuses (does not add it to the DB !)
        /// </summary>
        /// <param name="unit">The BuildingLevel to resolve</param>
        /// <param name="playerId">The Id of the player</param>
        /// <param name="islandId">The Id of the island</param>
        /// <returns>A cloned BuildingLevel with all its bonuses applied</returns>
        public BuildingLevel ResolveBonuses( BuildingLevel buildingLevel, long playerId, long islandId )
        {
            List<Bonus> bonusesOnCurrentIsland = GetAllBonusesOnCurrentIsland( playerId, islandId );
            BuildingLevel resolvedBuildingLevel = CloneBuildingLevel( buildingLevel );
            foreach ( BonusOnBuilding bonus in GetBonusesOnBuilding( buildingLevel, bonusesOnCurrentIsland ) )
            {
                ResolveBuildingBonus( resolvedBuildingLevel, bonus );
            }

            return resolvedBuildingLevel;
        }

        public BuildingLevel ResolveBonuses( BuildingLevel buildingLevel, List<Bonus> bonusesOnCurrentIsland, long playerId, long islandId )
        {
            BuildingLevel resolvedBuildingLevel = CloneBuildingLevel( buildingLevel );
            foreach ( BonusOnBuilding bonus in GetBonusesOnBuilding( buildingLevel, bonusesOnCurrentIsland ) )
            {
                ResolveBuildingBonus( resolvedBuildingLevel, bonus );
            }

            return resolvedBuildingLevel;
        }

        public List<BonusOnBuilding> GetBonusesOnBuilding( BuildingLevel buildingLevel, List<Bonus> bonusesOnCurrentIsland )
        {
            List<BonusOnBuilding> allBonusesOnBuilding = new List<BonusOnBuilding>();
            foreach ( Bonus bonus in bonusesOnCurrentIsland )
            {
                if ( bonus is BonusOnBuilding )
                {
                    BonusOnBuilding bonusOnBuilding = (BonusOnBuilding)bonus;
                    if ( bonusOnBuilding.TargetBuilding == BuildingName.none || bonusOnBuilding.TargetBuilding == buildingLevel.BuildingName )
                    {
                        allBonusesOnBuilding.Add( bonusOnBuilding );
                    }
                }
            }
            return allBonusesOnBuilding;
        }

        public BuildingLevel ResolveBuildingBonus( BuildingLevel buildingLevel, BonusOnBuilding bonus )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.duration:
                    buildingLevel.Duration -= (int)( buildingLevel.Duration * ( (double)bonus.Modifier / 100 ) );
                    break;
                case BonusType.queueRange:
                    throw new NotImplementedException( "Queue Range is not handled yet !" );
                    break;
                default:
                    throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
            }

            return buildingLevel;
        }

        /// <summary>
        /// Creates a clone of a BuildingLevel with the same reference to Requirements, Cost and Bonuses
        /// </summary>
        /// <param name="buildingLevel">The BuildingLevel to clone</param>
        /// <returns>The cloned BuildingLevel</returns>
        BuildingLevel CloneBuildingLevel( BuildingLevel buildingLevel )
        {
            if ( buildingLevel is ShieldLevel )
            {
                ShieldLevel shieldLevel = (ShieldLevel)buildingLevel;
                return new ShieldLevel
                {
                    BuildingName = shieldLevel.BuildingName,
                    Number = shieldLevel.Number,
                    Cost = shieldLevel.Cost,
                    Requirements = shieldLevel.Requirements,
                    Duration = shieldLevel.Duration,
                    Bonuses = shieldLevel.Bonuses,
                    Defense = shieldLevel.Defense
                };
            }
            else if ( buildingLevel is FieldLevel )
            {
                FieldLevel fieldLevel = (FieldLevel)buildingLevel;
                return new FieldLevel
                {
                    BuildingName = fieldLevel.BuildingName,
                    Number = fieldLevel.Number,
                    Cost = fieldLevel.Cost,
                    Requirements = fieldLevel.Requirements,
                    Duration = fieldLevel.Duration,
                    Bonuses = fieldLevel.Bonuses,
                    Production = fieldLevel.Production
                };
            }
            return new BuildingLevel
            {
                BuildingName = buildingLevel.BuildingName,
                Number = buildingLevel.Number,
                Cost = buildingLevel.Cost,
                Requirements = buildingLevel.Requirements,
                Duration = buildingLevel.Duration,
                Bonuses = buildingLevel.Bonuses
            };
        }

        Building CloneBuilding( Building building, BuildingLevel buildingLevel )
        {
            return new Building
            {
                Name = BuildingManager.StaticBuildingNameToName( building.BuildingName ),
                BuildingName = building.BuildingName,
                Level = buildingLevel,
                Position = building.Position
            };
        }

        #endregion

        #region TechnologyBonuses

        public int GetModifiedDuration( TechnologyLevel technologyLevel, long playerId, long islandId )
        {
            return ResolveBonuses( technologyLevel, playerId, islandId ).Duration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildings"></param>
        /// <param name="playerId"></param>
        /// <param name="islandId"></param>
        /// <returns></returns>
        public List<Technology> GetResolvedTechnologies( List<Technology> technologies, long playerId, long islandId )
        {
            List<Bonus> bonusesOnCurrentIsland = GetAllBonusesOnCurrentIsland( playerId, islandId );

            List<Technology> resolvedBuildings = new List<Technology>();
            foreach ( Technology technology in technologies )
            {
                TechnologyLevel resolvedLevel = ResolveBonuses( technology.Level, bonusesOnCurrentIsland, playerId, islandId );
                resolvedBuildings.Add( CloneTechnology( technology, resolvedLevel ) );
            }
            return resolvedBuildings;
        }

        /// <summary>
        /// Creates a TechnologyLevel wich has resolved all its bonuses (does not add it to the DB !)
        /// </summary>
        /// <param name="unit">The TechnologyLevel to resolve</param>
        /// <param name="playerId">The Id of the player</param>
        /// <param name="islandId">The Id of the island</param>
        /// <returns>A cloned TechnologyLevel with all its bonuses applied</returns>
        public TechnologyLevel ResolveBonuses( TechnologyLevel technologyLevel, long playerId, long islandId )
        {
            List<Bonus> bonusesOnCurrentIsland = GetAllBonusesOnCurrentIsland( playerId, islandId );
            TechnologyLevel resolvedTechnologyLevel = CloneTechnologyLevel( technologyLevel );
            foreach ( BonusOnTechnology bonus in GetBonusesOnTechnology( technologyLevel, bonusesOnCurrentIsland ) )
            {
                ResolveTechnologyBonus( resolvedTechnologyLevel, bonus );
            }

            return resolvedTechnologyLevel;
        }

        /// <summary>
        /// Creates a TechnologyLevel wich has resolved all its bonuses (does not add it to the DB !)
        /// </summary>
        /// <param name="unit">The TechnologyLevel to resolve</param>
        /// <param name="playerId">The Id of the player</param>
        /// <param name="islandId">The Id of the island</param>
        /// <returns>A cloned TechnologyLevel with all its bonuses applied</returns>
        public TechnologyLevel ResolveBonuses( TechnologyLevel technologyLevel, List<Bonus> bonusesOnCurrentIsland, long playerId, long islandId )
        {
            bonusesOnCurrentIsland = GetAllBonusesOnCurrentIsland( playerId, islandId );
            TechnologyLevel resolvedTechnologyLevel = CloneTechnologyLevel( technologyLevel );
            foreach ( BonusOnTechnology bonus in GetBonusesOnTechnology( technologyLevel, bonusesOnCurrentIsland ) )
            {
                ResolveTechnologyBonus( resolvedTechnologyLevel, bonus );
            }

            return resolvedTechnologyLevel;
        }

        public List<BonusOnTechnology> GetBonusesOnTechnology( TechnologyLevel technologyLevel, List<Bonus> bonusesOnCurrentIsland )
        {
            List<BonusOnTechnology> allTechnologyBonusesOnBuildng = new List<BonusOnTechnology>();

            foreach ( Bonus bonus in bonusesOnCurrentIsland )
            {
                if ( bonus is BonusOnTechnology )
                {
                    BonusOnTechnology bonusOnTechnology = (BonusOnTechnology)bonus;
                    if ( bonusOnTechnology.TargetTechnology == TechnologyName.none || bonusOnTechnology.TargetTechnology == technologyLevel.TechnologyName )
                    {
                        allTechnologyBonusesOnBuildng.Add( bonusOnTechnology );
                    }
                }
            }
            return allTechnologyBonusesOnBuildng;
        }

        public TechnologyLevel ResolveTechnologyBonus( TechnologyLevel technologyLevel, BonusOnTechnology bonus )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.duration:
                    technologyLevel.Duration -= (int)( technologyLevel.Duration * ( (double)bonus.Modifier / 100 ) );
                    break;
                default:
                    throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
            }

            return technologyLevel;
        }

        /// <summary>
        /// Creates a clone of a TechnologyLevel with the same reference to Requirements, Cost and Bonuses
        /// </summary>
        /// <param name="technologyLevel">The TechnologyLevel to clone</param>
        /// <returns>The cloned TechnologyLevel</returns>
        TechnologyLevel CloneTechnologyLevel( TechnologyLevel technologyLevel )
        {
            return new TechnologyLevel
            {
                TechnologyName = technologyLevel.TechnologyName,
                Number = technologyLevel.Number,
                Cost = technologyLevel.Cost,
                Requirements = technologyLevel.Requirements,
                Duration = technologyLevel.Duration,
                Bonuses = technologyLevel.Bonuses
            };
        }

        public Technology CloneTechnology( Technology technology, TechnologyLevel technologyLevel )
        {
            return new Technology
            {
                Name = TechnologyManager.StaticTechnologyNameToName( technology.TechnologyName ),
                TechnologyName = technology.TechnologyName,
                Level = technologyLevel
            };
        }

        #endregion

        public static string StaticBonusToString( Bonus bonus )
        {
            return "Bonus de " + bonus.Modifier.ToString() + "% sur" + BonusTypeToString( bonus.BonusType ) + "de" + BonusTargetToString( bonus );
        }

        private static string BonusTargetToString( Bonus bonus )
        {
            string result = "";
            if ( bonus is BonusOnBuilding )
            {
                BonusOnBuilding bonusOnBuilding = (BonusOnBuilding)bonus;
                switch ( bonusOnBuilding.TargetBuilding )
                {
                    case BuildingName.none:
                        result = " tous les bâtiments";
                        break;
                    default:
                        result = " tous les bâtiments";
                        break;
                }
            }
            if ( bonus is BonusOnTechnology )
            {
                BonusOnTechnology bonusOnTechnology = (BonusOnTechnology)bonus;
                switch ( bonusOnTechnology.TargetTechnology )
                {
                    case TechnologyName.none:
                        result = " toutes les technologies";
                        break;
                    default:
                        result = " toutes les technologies";
                        break;
                }
            }
            if ( bonus is BonusOnUnit )
            {
                BonusOnUnit bonusOnUnit = (BonusOnUnit)bonus;
                switch ( bonusOnUnit.TargetUnit )
                {
                    case UnitType.soldier:
                        result = " les unités de type soldat";
                        break;
                    case UnitType.monster:
                        result = " les unités de type monstre";
                        break;
                    case UnitType.mecanical:
                        result = " les unités de type mécanique";
                        break;
                    case UnitType.magic:
                        result = " les unités de type mage";
                        break;
                    case UnitType.utility:
                        result = " les unités de type utilitaire";
                        break;
                    case UnitType.all:
                        result = " toutes les unités";
                        break;
                    default:
                        result = " toutes les unités";
                        break;
                }
            }

            return result;
        }

        private static string BonusTypeToString( BonusType bonusType )
        {
            switch ( bonusType )
            {
                case BonusType.army_attack:
                    return " l'attaque ";
                case BonusType.army_magicalDefense:
                    return " la défense magique ";
                case BonusType.army_physicalDefense:
                    return " la défense physique ";
                case BonusType.army_speed:
                    return " la vitesse ";
                case BonusType.army_capacity:
                    return " la capacité de transport ";
                case BonusType.duration:
                    return " le temps de production ";
                case BonusType.cacheSize:
                    return " la taille de la cache ";
                default:
                    throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
            }
        }

        private List<Bonus> GetAllBonusesOnCurrentIsland( long playerId, long islandId )
        {
            // Get the bonus lists stored in technologies and buildings
            List<Technology> playersTechnology = CurrentContext.Players.Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.Bonuses )
                .Single( p => p.PlayerId == playerId ).Technologies.ToList();

            // Fill the levels with bonuses because thenInclude doesn't do its job
            List<TechnologyLevel> playersTechnologyLevels = new List<TechnologyLevel>();
            foreach ( TechnologyLevel level in playersTechnology.Select( t => t.Level ) )
            {
                level.Bonuses = CurrentContext.TechnologyLevels.Include( t => t.Bonuses ).Single( tl => tl.LevelId == level.LevelId ).Bonuses.ToList();
                playersTechnologyLevels.Add( level );
            }

            List<Building> buildingsOnIsland = CurrentContext.Islands.Include( i => i.Buildings ).ThenInclude( b => b.Level ).ThenInclude( bl => bl.Bonuses )
                .Single( i => i.IslandId == islandId ).Buildings.ToList();

            // Fill the levels with bonuses because thenInclude doesn't do its job
            List<BuildingLevel> buildingsLevelsOnIsland = new List<BuildingLevel>();
            foreach ( BuildingLevel level in buildingsOnIsland.Select( b => b.Level ) )
            {
                level.Bonuses = CurrentContext.BuildingLevels.Include( b => b.Bonuses ).Single( bl => bl.LevelId == level.LevelId ).Bonuses.ToList();
                buildingsLevelsOnIsland.Add( level );
            }

            // Agglomerate all those lists
            List<Bonus> allBonuses = new List<Bonus>();
            foreach ( List<Bonus> bonusList in playersTechnology.Select( t => t.Level.Bonuses ) )
                allBonuses.AddRange( bonusList );
            foreach ( List<Bonus> bonusList in buildingsOnIsland.Select( b => b.Level.Bonuses ) )
                allBonuses.AddRange( bonusList );


            // Add up the modifiers when 2 bonuses have the same bonus type
            List<Bonus> copy = new List<Bonus>();
            List<Bonus> filteredBonuses = ConcatBonusLists( allBonuses, copy );

            return allBonuses;
        }

        private List<Bonus> ConcatBonusLists( List<Bonus> list1, List<Bonus> list2 )
        {
            return list1.Concat( list2 )
                 .ToLookup( b => b.BonusType )
                 .Select( g => g.Aggregate( ( b1, b2 ) => new Bonus
                 {
                     BonusType = b1.BonusType,
                     Modifier = b1.Modifier + b2.Modifier
                 } ) ).ToList();
        }
    }
}
