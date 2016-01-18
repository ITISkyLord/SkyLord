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

        public int GetModifiedDuration( Unit unit, long playerId, long islandId )
        {
            return ResolveAllUnitBonuses( unit, playerId, islandId ).Duration;
        }

        /// <summary>
        /// Creates a Unit wich has resolved all its bonuses (does not add it to the DB !)
        /// </summary>
        /// <param name="unit">The Unit to resolve</param>
        /// <param name="playerId">The Id of the player</param>
        /// <param name="islandId">The Id of the island</param>
        /// <returns>A cloned Unit with all its bonuses applied</returns>
        public Unit ResolveAllUnitBonuses( Unit unit, long playerId, long islandId )
        {
            Unit resolvedUnit = CloneUnit( unit );
            foreach ( BonusTechnologyOnUnit bonus in GetBonusesTechnologyOnUnit( unit, playerId ) )
            {
                ResolveUnitBonus( resolvedUnit, bonus );
            }

            foreach ( BonusBuildingOnUnit bonus in GetBonusesBuildingOnUnit( unit, islandId ) )
            {
                ResolveUnitBonus( resolvedUnit, bonus );
            }

            return resolvedUnit;
        }

        public List<BonusBuildingOnUnit> GetBonusesBuildingOnUnit( Unit unit, long islandId )
        {
            List<Building> builingsOnIsland = CurrentContext.Islands
                .Include( i => i.Buildings ).ThenInclude( b => b.Level ).ThenInclude( bl => bl.BuildingBonuses )
                .Single( i => i.IslandId == islandId ).Buildings.ToList();

            List<BonusBuildingOnUnit> allBuildingBonusesOnUnit = new List<BonusBuildingOnUnit>();
            foreach ( List<BonusBuilding> buildingBonusList in builingsOnIsland.Select( b => b.Level.BuildingBonuses ) )
            {
                foreach ( BonusBuilding bonus in buildingBonusList )
                {
                    if ( bonus is BonusBuildingOnUnit )
                    {
                        BonusBuildingOnUnit bonusBuildingOnUnit = (BonusBuildingOnUnit)bonus;
                        if ( bonusBuildingOnUnit.TargetUnit == UnitType.all || bonusBuildingOnUnit.TargetUnit == unit.UnitType )
                        {
                            allBuildingBonusesOnUnit.Add( bonusBuildingOnUnit );
                        }
                    }
                }
            }
            return allBuildingBonusesOnUnit;
        }

        public List<BonusTechnologyOnUnit> GetBonusesTechnologyOnUnit( Unit unit, long playerId )
        {
            List<Technology> playerTechnologies = CurrentContext.Players.Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.TechnologyBonuses )
                .Single( p => p.PlayerId == playerId ).Technologies.ToList();

            List<BonusTechnologyOnUnit> allTechnologyBonusesOnUnit = new List<BonusTechnologyOnUnit>();
            foreach ( List<BonusTechnology> technologyList in playerTechnologies.Select( t => t.Level.TechnologyBonuses ) )
            {
                foreach ( BonusTechnology bonus in technologyList )
                {
                    if ( bonus is BonusTechnologyOnUnit )
                    {
                        BonusTechnologyOnUnit bonusTechnologyOnUnit = (BonusTechnologyOnUnit)bonus;
                        if ( bonusTechnologyOnUnit.TargetUnit == UnitType.all || bonusTechnologyOnUnit.TargetUnit == unit.UnitType )
                        {
                            allTechnologyBonusesOnUnit.Add( bonusTechnologyOnUnit );
                        }
                    }
                }
            }
            return allTechnologyBonusesOnUnit;
        }

        public Unit ResolveUnitBonus( Unit unit, BonusTechnologyOnUnit bonus )
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

        public Unit ResolveUnitBonus( Unit unit, BonusBuildingOnUnit bonus )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.army_attack:
                    unit.UnitStatistics.Attack = unit.UnitStatistics.Attack * ( bonus.Modifier / 100 );
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
                Consumption = unit.UnitStatistics.Capacity,
                TimeToBuild = unit.UnitStatistics.TimeToBuild
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

        //public int GetModifiedDuration( Building building, long playerId, long islandId )
        //{
        //    return ResolveAllUnitBonuses( building, playerId, islandId ).Duration;
        //}

        ///// <summary>
        ///// Creates a BuildingLevel wich has resolved all its bonuses (does not add it to the DB !)
        ///// </summary>
        ///// <param name="unit">The BuildingLevel to resolve</param>
        ///// <param name="playerId">The Id of the player</param>
        ///// <param name="islandId">The Id of the island</param>
        ///// <returns>A cloned BuildingLevel with all its bonuses applied</returns>
        //public BuildingLevel ResolveAllBuildingLevelBonuses( BuildingLevel buildingLevel, long playerId, long islandId )
        //{
        //    Building resolvedBuildingLevel = CloneBuildingLevel( buildingLevel );
        //    foreach ( BonusTechnologyOnBuilding bonus in GetBonusesTechnologyOnBuilding( buildingLevel, playerId ) )
        //    {
        //        ResolveUnitBonus( resolvedBuildingLevel, bonus );
        //    }

        //    foreach ( BonusBuildingOnBuilding bonus in GetBonusesBuildingOnBuilding( buildingLevel, islandId ) )
        //    {
        //        ResolveUnitBonus( resolvedBuildingLevel, bonus );
        //    }

        //    return resolvedBuildingLevel;
        //}

        //public List<BonusBuildingOnBuilding> GetBonusesBuildingOnBuilding( BuildingLevel buildingLevel, long islandId )
        //{
        //    List<BuildingLevel> builingsLevelsOnIsland = CurrentContext.Islands
        //        .Include( i => i.Buildings ).ThenInclude( b => b.Level ).ThenInclude( bl => bl.BuildingBonuses )
        //        .Single( i => i.IslandId == islandId ).Buildings.ToList().Select( b => b.Level).ToList();

        //    List<BonusBuildingOnBuilding> allBuildingBonusesOnBuilding = new List<BonusBuildingOnBuilding>();
        //    foreach ( List<BonusBuilding> buildingBonusList in builingsLevelsOnIsland.Select( bl => bl.BuildingBonuses ) )
        //    {
        //        foreach ( BonusBuilding bonus in buildingBonusList )
        //        {
        //            if ( bonus is BonusBuildingOnBuilding )
        //            {
        //                BonusBuildingOnBuilding bonusBuildingOnBuilding = (BonusBuildingOnBuilding)bonus;
        //                if ( bonusBuildingOnBuilding.TargetBuilding == BuildingName.none || bonusBuildingOnBuilding.TargetBuilding == buildingLevel.BuildingName )
        //                {
        //                    allBuildingBonusesOnBuilding.Add( bonusBuildingOnBuilding );
        //                }
        //            }
        //        }
        //    }
        //    return allBuildingBonusesOnBuilding;
        //}

        //public List<BonusTechnologyOnBuilding> GetBonusesTechnologyOnBuilding( BuildingLevel buildingLevel, long playerId )
        //{
        //    List<Technology> playerTechnologies = CurrentContext.Players.Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.TechnologyBonuses )
        //        .Single( p => p.PlayerId == playerId ).Technologies.ToList();

        //    List<BonusTechnologyOnBuilding> allTechnologyBonusesOnBuildng = new List<BonusTechnologyOnBuilding>();
        //    foreach ( List<BonusTechnology> technologyList in playerTechnologies.Select( t => t.Level.TechnologyBonuses ) )
        //    {
        //        foreach ( BonusTechnology bonus in technologyList )
        //        {
        //            if ( bonus is BonusTechnologyOnBuilding )
        //            {
        //                BonusTechnologyOnBuilding bonusTechnologyOnBuilding = (BonusTechnologyOnBuilding)bonus;
        //                if ( bonusTechnologyOnBuilding.TargetBuilding == BuildingName.none || bonusTechnologyOnBuilding.TargetBuilding == building.BuildingName )
        //                {
        //                    allTechnologyBonusesOnBuildng.Add( bonusTechnologyOnBuilding );
        //                }
        //            }
        //        }
        //    }
        //    return allTechnologyBonusesOnBuildng;
        //}

        //public Unit ResolveBuildingBonus( Building building, BonusTechnologyOnBuilding bonus )
        //{
        //    switch ( bonus.BonusType )
        //    {
        //        case BonusType.duration:
        //            building. += unit.UnitStatistics.Attack * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_magicalDefense:
        //            unit.UnitStatistics.MagicResist += unit.UnitStatistics.MagicResist * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_physicalDefense:
        //            unit.UnitStatistics.PhysicResist += unit.UnitStatistics.PhysicResist * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_speed:
        //            unit.UnitStatistics.Speed += unit.UnitStatistics.Speed * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_capacity:
        //            unit.UnitStatistics.Capacity += unit.UnitStatistics.Capacity * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.duration:
        //            unit.Duration -= unit.Duration * ( bonus.Modifier / 100 );
        //            break;
        //        default:
        //            throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
        //    }

        //    return unit;
        //}

        //public Unit ResolveBuildingBonus( Unit unit, BonusBuildingOnUnit bonus )
        //{
        //    switch ( bonus.BonusType )
        //    {
        //        case BonusType.army_attack:
        //            unit.UnitStatistics.Attack = unit.UnitStatistics.Attack * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_magicalDefense:
        //            unit.UnitStatistics.MagicResist += unit.UnitStatistics.MagicResist * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_physicalDefense:
        //            unit.UnitStatistics.PhysicResist += unit.UnitStatistics.PhysicResist * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_speed:
        //            unit.UnitStatistics.Speed += unit.UnitStatistics.Speed * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.army_capacity:
        //            unit.UnitStatistics.Capacity += unit.UnitStatistics.Capacity * ( bonus.Modifier / 100 );
        //            break;
        //        case BonusType.duration:
        //            unit.Duration -= unit.Duration * ( bonus.Modifier / 100 );
        //            break;
        //        default:
        //            throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
        //    }

        //    return unit;
        //}

        ///// <summary>
        ///// Creates a clone of a Unit with the same reference to Requirements and Cost, but a new local UnitStatistics
        ///// </summary>
        ///// <param name="unit">The Unit to clone</param>
        ///// <returns>The cloned Unit</returns>
        //Unit CloneUnit( Unit unit )
        //{
        //    UnitStatistics unitStatitics = new UnitStatistics
        //    {
        //        Attack = unit.UnitStatistics.Attack,
        //        PhysicResist = unit.UnitStatistics.PhysicResist,
        //        MagicResist = unit.UnitStatistics.MagicResist,
        //        Speed = unit.UnitStatistics.Speed,
        //        Capacity = unit.UnitStatistics.Capacity,
        //        Consumption = unit.UnitStatistics.Capacity,
        //        TimeToBuild = unit.UnitStatistics.TimeToBuild
        //    };

        //    return new Unit
        //    {
        //        Name = unit.Name,
        //        UnitName = unit.UnitName,
        //        UnitType = unit.UnitType,
        //        UnitDamageType = unit.UnitDamageType,
        //        UnitCost = unit.UnitCost,
        //        UnitStatistics = unitStatitics,
        //        Requirements = unit.Requirements,
        //        Duration = unit.Duration,
        //        IsModel = false
        //    };

        //}
        
        #endregion

        // IF TechnologyName = TechnologyName.none ==> touche toutes les technos !!

        #region TechnologyBonuses

        #endregion
        List<Island> GetAllIslandsArmiesFromPlayer( long playerId )
        {
            return CurrentContext.Players
                .Include( p => p.Islands ).ThenInclude( i => i.Armies ).ThenInclude( a => a.Regiments )
                .ThenInclude( r => r.Unit ).ThenInclude( u => u.UnitStatistics )
                .Single( p => p.PlayerId == playerId ).Islands.ToList();
        }
    }
}
