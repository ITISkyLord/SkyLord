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
        public Island CurrentIsland { get; set; }

        /// <summary>
        /// Creates a new LevelManager instance
        /// </summary>
        /// <param name="currentContext">An opened BuildingContext</param>
        /// <param name="currentIsland">The island examined</param>
        public BuildingManager( BuildingContext currentContext, Island currentIsland )
        {
            CurrentContext = currentContext;
            CurrentIsland = currentIsland;
        }

        public bool isNextLevelAvailable( Level currentLevel )
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
            bool met = false;
            if( currentLevel is BuildingLevel )
            {
                BuildingLevel buildingLevel = (BuildingLevel)currentLevel;

                List<Building> buildingsOnIsland = CurrentContext.Islands
                    .Include( i => i.Buildings).ThenInclude( b => b.Level).ThenInclude( bl => bl.Requirements )
                    .Where( i => i.IslandId == CurrentIsland.IslandId ).SingleOrDefault().Buildings.ToList();
                met = buildingsOnIsland.Any( b => buildingLevel.Requirements
                .Any( bl => bl.BuildingName == b.Level.BuildingName && bl.Number == b.Level.Number ) );
            }
            else if ( currentLevel is TechnologyLevel )
            {
                throw new NotImplementedException();
            }
            return met;
        }
    }
}

