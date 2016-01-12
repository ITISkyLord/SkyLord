using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public class BonusManager
    {
        public LevelContext CurrentContext { get; }

        public BonusManager( LevelContext currentContext )
        {
            CurrentContext = currentContext;
        }

        //public List<Bonus> GetUnitBonuses( Unit unit, long playerId )
        //{
        //    List<Island> playerIslands = CurrentContext.Players
        //        .Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.Bonuses )
        //        .Include( p => p.Islands ).ThenInclude( i => i.Buildings ).ThenInclude( b => b.Level ).ThenInclude( bl => bl.Bonuses )
        //        .Single( p => p.PlayerId == playerId ).Islands.ToList();

        //    // Ne pas répéter 2 fois le même bonus (Equality conparer ??)
        //    List<Building> allBuildings = new List<Building>();
        //    foreach( List<Building> buildingList in playerIslands.Select( i => i.Buildings) )
        //    {
        //        allBuildings.Intersect( buildingList );
        //    }

        //    List<BuildingLevel> allBuildingLevels = new List<BuildingLevel>();
        //    foreach( BuildingLevel buildingLevel in allBuildingLevels )
        //    {
        //        allBuildingLevels.Intersect
        //    }
        //}

        public Unit ResolveUnitBonus( Unit unit, BonusTechnologyOnUnit bonus, long playerId )
        {
            switch ( bonus.BonusType )
            {
                case BonusType.army_attack:
                    unit.UnitStatistics.Attack += bonus.Modifier;
                    break;
                case BonusType.army_magicalDefense:
                    unit.UnitStatistics.MagicResist += bonus.Modifier;
                    break;
                case BonusType.army_physicalDefense:
                    unit.UnitStatistics.PhysicResist += bonus.Modifier;
                    break;
                default :
                    throw new NotImplementedException( "You are trying to handle a not existing type of bonus !" );
            }

            return unit;
        }

        public void ResolveAllUnitsBonus( BonusTechnologyOnUnit bonus, long playerId )
        {
            List<Island> playerIslands = GetAllIslandsArmiesFromPlayer( playerId );

            List<Unit> unitsToResolve = GetUnitsByUnitType( playerIslands, bonus.TargetUnit );

            foreach( Unit unit in unitsToResolve )
            {
                ResolveUnitBonus( unit, bonus, playerId );
            }
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

        List<Island> GetAllIslandsArmiesFromPlayer( long playerId )
        {
            return CurrentContext.Players
                .Include( p => p.Islands ).ThenInclude( i => i.Armies ).ThenInclude( a => a.Regiments )
                .ThenInclude( r => r.Unit ).ThenInclude( u => u.UnitStatistics )
                .Single( p => p.PlayerId == playerId ).Islands.ToList();
        }
    }
}
