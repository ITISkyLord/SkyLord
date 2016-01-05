using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public class LeveManager
    {
        public LevelContext CurrentContext { get; set; }
        public long CurrentIslandId { get; set; }

        /// <summary>
        /// Creates a new LevelManager instance
        /// </summary>
        /// <param name="currentContext">An opened LevelContext</param>
        /// <param name="currentIsland">The island examined</param>
        public LeveManager( LevelContext currentContext, long currentIslandId )
        {
            CurrentContext = currentContext;
            CurrentIslandId = currentIslandId;
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

        public bool IsNextLevelAvailable( Level currentLevel )
        {
            return AreAllRequirementsMet( FindNextLevel( currentLevel ) );
        }

        public Level FindNextLevel( Level currentLevel )
        {
            Level levelFound = null;
            if ( currentLevel is BuildingLevel )
            {
                BuildingLevel buildingLevel = (BuildingLevel)currentLevel;

                levelFound = CurrentContext.BuildingLevels.Include( bl => bl.Requirements ).Include( bl => bl.Cost )
                    .SingleOrDefault( bl => bl.BuildingName == buildingLevel.BuildingName && bl.Number == buildingLevel.Number + 1 );
            }
            else if ( currentLevel is TechnologyLevel )
            {
                TechnologyLevel technoLevel = (TechnologyLevel)currentLevel;

                levelFound = CurrentContext.TechnologyLevels.Include( bl => bl.Requirements ).Include( bl => bl.Cost )
                    .SingleOrDefault( bl => bl.TechnologyName == technoLevel.TechnologyName && bl.Number == technoLevel.Number + 1 );
            }
            return levelFound;
        }

        public bool AreAllRequirementsMet( Level level )
        {
            //If there is no requirement, return true
            if ( level.Requirements == null || level.Requirements.Count() == 0 )
                return true;

            List<Building> buildingsOnIsland = GetBuildingsOnCurrentIsland();
            IList<Technology> playersTechnologies = CurrentContext.Islands.SingleOrDefault( i => i.IslandId == CurrentIslandId ).Owner.Technologies;

            // If requirements contains at least a technology but the player doesn't have any yet, return false
            if ( playersTechnologies == null && level.Requirements.Any( r => r.TechnologyName != TechnologyName.none ) )
                return false;

            if ( playersTechnologies != null )
            {
                foreach ( Requirement requirement in level.Requirements.Where( r => r.TechnologyName != TechnologyName.none ) )
                {
                    if ( !IsTechnologyRequirementMet( requirement, playersTechnologies ) )
                        return false;
                }
            }

            foreach ( Requirement requirement in level.Requirements.Where( r => r.BuildingName != BuildingName.none ) )
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

        public List<Building> GetBuildingsOnCurrentIsland()
        {
            return CurrentContext.Islands
                    .Include( i => i.Buildings ).ThenInclude( b => b.Level )
                    .Where( i => i.IslandId == CurrentIslandId ).SingleOrDefault().Buildings.ToList();
        }
    }
}

