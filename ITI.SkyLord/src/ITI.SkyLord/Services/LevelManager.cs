using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;

namespace ITI.SkyLord
{
    public class LevelManager
    {
        public ILevelContext CurrentContext { get; set; }

        /// <summary>
        /// Creates a new LevelManager instance
        /// </summary>
        /// <param name="currentContext">An opened LevelContext</param>
        /// <param name="currentIsland">The island examined</param>
        public LevelManager( ILevelContext currentContext )
        {
            CurrentContext = currentContext;
        }

        /// <summary>
        /// Levels up a building
        /// </summary>
        /// <param name="building">The building to level up</param>
        /// <returns>A bool indicating if the level was incremented or not</returns>
        public bool LevelUp( Building building )
        {
            Level nextLevel = FindNextLevel( building.Level );
            if ( nextLevel != null )
            {
                building.Level = (BuildingLevel)nextLevel;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Levels up a technology
        /// </summary>
        /// <param name="technology">The technology to level up</param>
        /// <returns>A bool indicating if the level was incremented or not</returns>
        public bool LevelUp( Technology technology )
        {
            Level nextLevel = FindNextLevel( technology.Level );
            if ( nextLevel != null )
            {
                technology.Level = (TechnologyLevel)nextLevel;
                return true;
            }
            else
                return false;
        }

        public bool IsNextLevelAvailable( Level currentLevel, long currentIslandId )
        {
            Level nextLevel = FindNextLevel( currentLevel );
            long activePlayerId = CurrentContext.Islands.SingleOrDefault( i => i.IslandId == currentIslandId ).Owner.PlayerId;

            Island currentIsland = CurrentContext.GetIsland( currentIslandId, activePlayerId );
            if ( nextLevel != null )
            {
                return AreAllRequirementsMet( FindNextLevel( currentLevel ).Requirements.ToList(), currentIslandId );
            }
            return false;
        }

        public bool IsUnitAvailable( Unit unit, long currentIslandId )
        {
            return AreAllRequirementsMet( (List<Requirement>)unit.Requirements, currentIslandId );
        }

        public Level FindNextLevel( Level currentLevel )
        {
            Level levelFound = null;
            // Recherche si Building
            if ( currentLevel is BuildingLevel )
            {
                BuildingLevel buildingLevel = (BuildingLevel)currentLevel;
                levelFound = CurrentContext.BuildingLevels.Include( bl => bl.Requirements ).Include( bl => bl.Cost )
                    .SingleOrDefault( bl => bl.BuildingName == buildingLevel.BuildingName && bl.Number == buildingLevel.Number + 1 );
            }
            // Recherche si Technology
            else if ( currentLevel is TechnologyLevel )
            {
                TechnologyLevel technoLevel = (TechnologyLevel)currentLevel;
                levelFound = CurrentContext.TechnologyLevels.Include( bl => bl.Requirements ).Include( bl => bl.Cost )
                    .SingleOrDefault( bl => bl.TechnologyName == technoLevel.TechnologyName && bl.Number == technoLevel.Number + 1 );
            }
            return levelFound;
        }

        public bool AreAllRequirementsMet( List<Requirement> requirements, long currentIslandId )
        {
            //If there is no requirement, return true
            if ( requirements == null || requirements.Count() == 0 )
                return true;

            List<Building> buildingsOnIsland = GetBuildingsOnCurrentIsland( currentIslandId );
            IList<Technology> playersTechnologies = CurrentContext.Islands.SingleOrDefault( i => i.IslandId == currentIslandId ).Owner.Technologies;

            // If requirements contains at least a technology but the player doesn't have any yet, return false
            if ( playersTechnologies == null && requirements.Any( r => r.TechnologyName != TechnologyName.none ) )
                return false;

            if ( playersTechnologies != null )
            {
                foreach ( Requirement requirement in requirements.Where( r => r.TechnologyName != TechnologyName.none ) )
                {
                    if ( !IsTechnologyRequirementMet( requirement, playersTechnologies ) )
                        return false;
                }
            }

            foreach ( Requirement requirement in requirements.Where( r => r.BuildingName != BuildingName.none ) )
            {
                if ( !IsBuildingRequirementMet( requirement, buildingsOnIsland ) )
                    return false;
            }
            return true;
        }

        public bool IsBuildingRequirementMet( Requirement buildingRequirement, IList<Building> buildingsOnIsland )
        {
            bool met = false;
            if ( buildingsOnIsland.Any( b => b.BuildingName == buildingRequirement.BuildingName && b.Level.Number == buildingRequirement.Number ) )
                met = true;
            return met;
        }

        public bool IsTechnologyRequirementMet( Requirement technologyRequirement, IList<Technology> playersTechnologies )
        {
            bool met = false;
            if ( playersTechnologies.Any( b => b.TechnologyName == technologyRequirement.TechnologyName && b.Level.Number == technologyRequirement.Number ) )
                met = true;
            return met;
        }

        public List<Building> GetBuildingsOnCurrentIsland( long currentIslandId )
        {
            return CurrentContext.Islands
                    .Include( i => i.Buildings ).ThenInclude( b => b.Level )
                    .Where( i => i.IslandId == currentIslandId ).SingleOrDefault().Buildings.ToList();
        }
    }
}

