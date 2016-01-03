using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public class BuildingManager
    {
        public BuildingContext CurrentContext { get; set; }
        public long CurrentIslandId { get; set; }

        /// <summary>
        /// Creates a new LevelManager instance
        /// </summary>
        /// <param name="currentContext">An opened BuildingContext</param>
        /// <param name="currentIsland">The island examined</param>
        public BuildingManager( BuildingContext currentContext, long currentIslandId )
        {
            CurrentContext = currentContext;
            CurrentIslandId = currentIslandId;
        }

        public bool IsNextLevelAvailable( Level currentLevel )
        {
            return IsRequirementMet( FindNextLevel( currentLevel ) );
        }

        public Level FindNextLevel( Level currentLevel )
        {
            Level levelFound = null;
            if( currentLevel is BuildingLevel )
            {
                BuildingLevel buildingLevel = (BuildingLevel)currentLevel;

                levelFound = CurrentContext.BuildingLevels.Include( bl => bl.Requirements ).Include( bl => bl.Cost )
                    .SingleOrDefault( bl => bl.BuildingName == buildingLevel.BuildingName && bl.Number == buildingLevel.Number + 1 );
            }
            else if ( currentLevel is TechnologyLevel )
            {
                TechnologyLevel technoLevel = (TechnologyLevel)currentLevel;

                levelFound = CurrentContext.TechnologyLevels.Include( bl => bl.Requirements ).Include( bl => bl.Cost )
                    .SingleOrDefault( bl => bl.TechnologyName == technoLevel.TechnologyName && bl.Number == technoLevel.Number );
            }
            return levelFound;
        }

        public bool IsRequirementMet( Level currentLevel )
        {
            if( currentLevel is BuildingLevel )
            {
                BuildingLevel buildingLevel = (BuildingLevel)currentLevel;
                List<Building> buildingsOnIsland = GetBuildingsOnCurrentIsland();

                foreach ( Requirement r in currentLevel.Requirements )
                {
                    if( !buildingsOnIsland.Any( b => b.BuildingName == r.BuildingName && b.Level.Number == r.Number ) )
                    {
                        return false;
                    }
                }
            }
            else if ( currentLevel is TechnologyLevel )
            {
                throw new NotImplementedException();
            }
            return true;
        }

        public List<Building> GetBuildingsOnCurrentIsland()
        {
            return CurrentContext.Islands
                    .Include( i => i.Buildings ).ThenInclude( b => b.Level )
                    .Where( i => i.IslandId == CurrentIslandId ).SingleOrDefault().Buildings.ToList();
        }
    }
}

