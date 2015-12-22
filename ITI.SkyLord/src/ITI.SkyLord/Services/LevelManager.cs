using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public class LevelManager
    {
        public SetupContext CurrentContext { get; set; }
        public Island CurrentIsland { get; set; }

        public LevelManager( SetupContext currentContext, Island currentIsland )
        {
            CurrentContext = currentContext;
            CurrentIsland = currentIsland;
        }

        public bool isNextLevelAvailable( BuildingLevel currentLevel )
        {
            return IsRequirementMet( FindNextLevel( currentLevel ) );
        }

        public BuildingLevel FindNextLevel( BuildingLevel currentLevel )
        {
            return currentLevel.BuildingDefinition
                .PossibleLevels.SingleOrDefault( pl => pl.Number == currentLevel.Number + 1 );
        }

        public bool IsRequirementMet( BuildingLevel nextLevel )
        {
            return CurrentIsland.Buildings.Where( b => b.Name == nextLevel.BuildingDefinition.Name )
                .OrderByDescending( b => b.Level.Number ).FirstOrDefault()
                .Level.Number >= nextLevel.Number;
        }
    }
}

