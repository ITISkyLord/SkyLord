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
            return ResolveAllUnitBonuses( unit, playerId, islandId ).Duration;
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

            foreach ( Unit unit in allPlayersRegiments.Select( r => r.Unit ) )
            {
                ResolveBonuses( unit, playerId, islandId );
            }
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
            Unit resolvedUnit = CurrentContext.Units.Include( u => u.UnitStatistics )
                .Single( u => u.UnitName == unit.UnitName && u.IsModel );
            resolvedUnit = CloneUnit( resolvedUnit );
            foreach ( BonusOnUnit bonus in GetBonusesOnUnit( unit, islandId, playerId ) )
            {
                ResolveUnitBonus( resolvedUnit, bonus );
            }

            return resolvedUnit;
        }

        public List<BonusOnUnit> GetBonusesOnUnit( Unit unit, long islandId, long playerId )
        {
            List<BonusOnUnit> allBonusOnUnit = new List<BonusOnUnit>();

            foreach ( Bonus bonus in GetAllBonusesOnCurrentIsland( playerId, islandId ) )
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

        public Unit ResolveUnitBonus( Unit unit, BonusOnUnit bonus )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.army_attack:
                    unit.UnitStatistics.Attack += unit.UnitStatistics.Attack * ( bonus.Modifier / 100 );
                    break;
                case BonusType.army_magicalDefense:
                    unit.UnitStatistics.MagicResist += unit.UnitStatistics.MagicResist * ( bonus.Modifier / 100 );
                    break;
                case BonusType.army_physicalDefense:
                    unit.UnitStatistics.PhysicResist += unit.UnitStatistics.PhysicResist * ( bonus.Modifier / 100 );
                    break;
                case BonusType.army_speed:
                    unit.UnitStatistics.Speed += unit.UnitStatistics.Speed * ( bonus.Modifier / 100 );
                    break;
                case BonusType.army_capacity:
                    unit.UnitStatistics.Capacity += unit.UnitStatistics.Capacity * ( bonus.Modifier / 100 );
                    break;
                case BonusType.duration:
                    unit.Duration -= unit.Duration * ( bonus.Modifier / 100 );
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
        Unit CloneUnit( Unit unit )
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

            return new Unit
            {
                Name = unit.Name,
                UnitName = unit.UnitName,
                UnitType = unit.UnitType,
                UnitDamageType = unit.UnitDamageType,
                UnitCost = unit.UnitCost,
                UnitStatistics = unitStatitics,
                Requirements = unit.Requirements,
                Duration = unit.Duration,
                IsModel = false
            };

        }

        List<Unit> GetUnitsByUnitType( List<Island> playerIslands, UnitType unitType )
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
            return ResolveAllBuildingLevelBonuses( buildingLevel, playerId, islandId ).Duration;
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
            BuildingLevel resolvedBuildingLevel = CloneBuildingLevel( buildingLevel );
            foreach ( BonusOnBuilding bonus in GetBonusesOnBuilding( buildingLevel, islandId, playerId ) )
            {
                ResolveBuildingBonus( resolvedBuildingLevel, bonus );
            }

            return resolvedBuildingLevel;
        }

        public List<BonusOnBuilding> GetBonusesOnBuilding( BuildingLevel buildingLevel, long islandId, long playerId )
        {
            List<BonusOnBuilding> allBonusesOnBuilding = new List<BonusOnBuilding>();
            foreach ( Bonus bonus in GetAllBonusesOnCurrentIsland( playerId, islandId ) )
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
                    buildingLevel.Duration -= buildingLevel.Duration * ( bonus.Modifier / 100 );
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

        #endregion

        #region TechnologyBonuses

        public int GetModifiedDuration( TechnologyLevel technologyLevel, long playerId, long islandId )
        {
            return ResolveAllTechnologyLevelBonuses( technologyLevel, playerId, islandId ).Duration;
        }

        /// <summary>
        /// Creates a TechnologyLevel wich has resolved all its bonuses (does not add it to the DB !)
        /// </summary>
        /// <param name="unit">The TechnologyLevel to resolve</param>
        /// <param name="playerId">The Id of the player</param>
        /// <param name="islandId">The Id of the island</param>
        /// <returns>A cloned TechnologyLevel with all its bonuses applied</returns>
        public TechnologyLevel ResolveAllTechnologyLevelBonuses( TechnologyLevel technologyLevel, long playerId, long islandId )
        {
            TechnologyLevel resolvedTechnologyLevel = CloneTechnologyLevel( technologyLevel );
            foreach ( BonusBuildingOnTechnology bonus in GetBonusesBuildingOnTechnology( technologyLevel, playerId ) )
            {
                ResolveTechnologyBonus( resolvedTechnologyLevel, bonus );
            }

            foreach ( BonusTechnologyOnTechnology bonus in GetBonusesTechnologyOnTechnology( technologyLevel, islandId ) )
            {
                ResolveTechnologyBonus( resolvedTechnologyLevel, bonus );
            }

            return resolvedTechnologyLevel;
        }

        public List<BonusTechnologyOnTechnology> GetBonusesTechnologyOnTechnology( TechnologyLevel technologyLevel, long playerId )
        {
            List<TechnologyLevel> playerTechnologieLevels = CurrentContext.Players.Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.TechnologyBonuses )
                .Single( p => p.PlayerId == playerId ).Technologies.ToList().Select( t => t.Level ).ToList();

            List<BonusTechnologyOnTechnology> allTechnologyBonusesOnBuilding = new List<BonusTechnologyOnTechnology>();
            foreach ( List<BonusTechnology> buildingBonusList in playerTechnologieLevels.Select( tl => tl.TechnologyBonuses ) )
            {
                foreach ( BonusTechnology bonus in buildingBonusList )
                {
                    if ( bonus is BonusTechnologyOnBuilding )
                    {
                        BonusTechnologyOnTechnology bonusTechnologyOnTechnology = (BonusTechnologyOnTechnology)bonus;
                        if ( bonusTechnologyOnTechnology.TargetTechnology == TechnologyName.none || bonusTechnologyOnTechnology.TargetTechnology == technologyLevel.TechnologyName )
                        {
                            allTechnologyBonusesOnBuilding.Add( bonusTechnologyOnTechnology );
                        }
                    }
                }
            }
            return allTechnologyBonusesOnBuilding;
        }

        public List<BonusBuildingOnTechnology> GetBonusesBuildingOnTechnology( TechnologyLevel technologyLevel, long islandId )
        {
            List<BuildingLevel> builingsLevelsOnIsland = CurrentContext.Islands
                .Include( i => i.Buildings ).ThenInclude( b => b.Level ).ThenInclude( bl => bl.BuildingBonuses )
                .Single( i => i.IslandId == islandId ).Buildings.ToList().Select( b => b.Level ).ToList();

            List<BonusBuildingOnTechnology> allTechnologyBonusesOnBuildng = new List<BonusBuildingOnTechnology>();

            foreach ( List<BonusBuilding> technologyList in builingsLevelsOnIsland.Select( bl => bl.BuildingBonuses ) )
            {
                foreach ( BonusBuilding bonus in technologyList )
                {
                    if ( bonus is BonusBuildingOnTechnology )
                    {
                        BonusBuildingOnTechnology bonusBuildingOnTechnology = (BonusBuildingOnTechnology)bonus;
                        if ( bonusBuildingOnTechnology.TargetTechnology == TechnologyName.none || bonusBuildingOnTechnology.TargetTechnology == technologyLevel.TechnologyName )
                        {
                            allTechnologyBonusesOnBuildng.Add( bonusBuildingOnTechnology );
                        }
                    }
                }
            }
            return allTechnologyBonusesOnBuildng;
        }

        public TechnologyLevel ResolveTechnologyBonus( TechnologyLevel technologyLevel, BonusTechnologyOnTechnology bonus )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.duration:
                    technologyLevel.Duration -= technologyLevel.Duration * ( bonus.Modifier / 100 );
                    break;
                default:
                    throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
            }

            return technologyLevel;
        }

        public TechnologyLevel ResolveTechnologyBonus( TechnologyLevel technologyLevel, BonusBuildingOnTechnology bonus )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.duration:
                    technologyLevel.Duration -= technologyLevel.Duration * ( bonus.Modifier / 100 );
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
                TechnologyBonuses = technologyLevel.TechnologyBonuses
            };
        }

        #endregion

        private List<Bonus> GetAllBonusesOnCurrentIsland( long playerId, long islandId )
        {
            List<Technology> playersTechnology = CurrentContext.Players.Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.TechnologyBonuses )
                .Single( p => p.PlayerId == playerId ).Technologies.ToList();
            List<Building> buildingsOnIsland = CurrentContext.Islands.Include( i => i.Buildings ).ThenInclude( b => b.Level ).ThenInclude( bl => bl.BuildingBonuses )
                .Single( i => i.IslandId == islandId ).Buildings.ToList();

            List<Bonus> allBonuses = new List<Bonus>();
            foreach ( List<Bonus> bonusList in playersTechnology.Select( t => t.Level.Bonuses ) )
            {
                allBonuses.AddRange( bonusList );
            }
            foreach ( List<Bonus> bonusList in buildingsOnIsland.Select( b => b.Level.Bonuses ) )
            {
                allBonuses.AddRange( bonusList );
            }

            return allBonuses;
        }
    }
}
