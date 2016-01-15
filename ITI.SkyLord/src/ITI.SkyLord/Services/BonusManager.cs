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
        public List<BonusBuildingOnUnit> GetBonusesBuildingOnUnit( Unit unit, long islandId )
        {
            List<Building> builingsOnIsland = CurrentContext.Islands
                .Include( i => i.Buildings ).ThenInclude( b => b.Level ).ThenInclude( bl => bl.BuildingBonuses )
                .Single( i => i.IslandId == islandId ).Buildings.ToList();

            List<BonusBuildingOnUnit> allBuildingBonusesOnUnit = new List<BonusBuildingOnUnit>();
            foreach ( List<BonusBuilding> buildingBonusList in builingsOnIsland.Select( b => b.Level.BuildingBonuses ) )
            {
                foreach ( BonusBuildingOnUnit bonus in buildingBonusList )
                {
                    if ( bonus is BonusBuildingOnUnit )
                        allBuildingBonusesOnUnit.Add( bonus );
                }
            }
            return allBuildingBonusesOnUnit;
        }

        public List<BonusTechnologyOnUnit> GetBonusesTechnologyOnUnit( Unit unit, long playerId )
        {
            List<Technology> playerTechnology = CurrentContext.Players.Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.TechnologyBonuses )
                .Single( p => p.PlayerId == playerId ).Technologies.ToList();

            List<BonusTechnologyOnUnit> allTechnologyBonusesOnUnit = new List<BonusTechnologyOnUnit>();
            foreach ( List<BonusTechnology> technologyList in playerTechnology.Select( t => t.Level.TechnologyBonuses ) )
            {
                foreach ( BonusTechnologyOnUnit bonus in technologyList )
                {
                    if ( bonus is BonusTechnologyOnUnit )
                        allTechnologyBonusesOnUnit.Add( bonus );
                }
            }
            return allTechnologyBonusesOnUnit;
        }

        public void ResolveAllUnitBonuses( Unit unit, long playerId, long islandId )
        {
            foreach ( BonusTechnologyOnUnit bonus in GetBonusesTechnologyOnUnit( unit, playerId ) )
            {
                ResolveUnitBonus( unit, bonus );
            }

            foreach ( BonusBuildingOnUnit bonus in GetBonusesBuildingOnUnit( unit, islandId ) )
            {
                ResolveUnitBonus( unit, bonus );
            }
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
                case BonusType.duration:
                    unit.Duration -= unit.Duration * ( bonus.Modifier / 100 );
                    break;
                default:
                    throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
            }

            return unit;
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

        // IF BuildingName = BuildingName.none ==> touche tous les buildings !!

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
