using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using ITI.SkyLord.Services;

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

        /// <summary>
        /// Gets the availability of the next level of a building on an island.
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <param name="currentIslandId"></param>
        /// <returns></returns>
        public RequirementAvailability GetNextLevelAvailablility( Building building, long currentIslandId )
        {
            Level currentLevel = building.Level;

            if ( currentLevel == null )
            {
                currentLevel = CurrentContext.BuildingLevels.Single( b => b.LevelId == building.Level.LevelId );
            }
            Level nextLevel = FindNextLevel( currentLevel );

            if( nextLevel == null )
            {
                return new RequirementAvailability { Availabilities = new List<Availability> { new Availability { Available = false, Requirement = null } } };
            }
            return CreateAvailability( (List<Requirement>)nextLevel.Requirements, currentIslandId );
        }

        /// <summary>
        /// Gets the availability of the next level of a technology.
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <param name="currentIslandId"></param>
        /// <returns>A RequirementAvailability object</returns>
        public RequirementAvailability GetNextLevelAvailablility( Technology technology, long currentIslandId )
        {
            Level currentLevel = technology.Level;

            if ( currentLevel == null )
            {
                currentLevel = CurrentContext.TechnologyLevels.Single( b => b.LevelId == technology.Level.LevelId );
            }
            Level nextLevel = FindNextLevel( currentLevel );

            if ( nextLevel == null )
            {
                return new RequirementAvailability { Availabilities = new List<Availability> { new Availability { Available = false, Requirement = null } } };
            }
            return CreateAvailability( (List<Requirement>)nextLevel.Requirements, currentIslandId );
        }

        /// <summary>
        /// Gets the availability of a building on an island.
        /// Need to include Requirements of the building level.
        /// </summary>
        /// <param name="building"></param>
        /// <param name="currentIslandId"></param>
        /// <returns>A RequirementAvailability object</returns>
        public RequirementAvailability GetAvailablility( Building building, long currentIslandId )
        {
            List<Requirement> requirements = (List<Requirement>)building.Level.Requirements;

            if ( building.Level == null || building.Level.Requirements == null )
            {
                requirements = (List<Requirement>)CurrentContext.BuildingLevels.Single( bl => bl.LevelId == building.Level.LevelId ).Requirements;
            }
            return CreateAvailability( requirements, currentIslandId );
        }

        /// <summary>
        /// Gets the availability of a technology.
        /// Need to include Requirements of the technology.
        /// </summary>
        /// <param name="technology"></param>
        /// <param name="currentIslandId"></param>
        /// <returns>A RequirementAvailability object</returns>
        public RequirementAvailability GetAvailablility( TechnologyLevel technologyLevel, long currentIslandId )
        {
            List<Requirement> requirements = (List<Requirement>)technologyLevel.Requirements;

            if ( technologyLevel == null || technologyLevel.Requirements == null )
            {
                requirements = (List<Requirement>)CurrentContext.TechnologyLevels.Single( tl => tl.LevelId == technologyLevel.LevelId ).Requirements;
            }

            return CreateAvailability( (List<Requirement>)technologyLevel.Requirements, currentIslandId );
        }

        /// <summary>
        /// Gets the availability of a unit on an island.
        /// Need to include Requirements of the unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="currentIslandId"></param>
        /// <returns>A RequirementAvailability object</returns>
        public RequirementAvailability GetAvailablility( Unit unit, long currentIslandId )
        {
            List<Requirement> requirements = (List<Requirement>)unit.Requirements;

            if( unit.Requirements == null )
            {
                requirements = (List<Requirement>)CurrentContext.Units.Single( u => u.UnitId == unit.UnitId ).Requirements;
            }
            return CreateAvailability( requirements, currentIslandId );
        }

        public Level FindNextLevel( Level currentLevel )
        {
            Level levelFound = null;
            // Recherche si Building
            if ( currentLevel is BuildingLevel )
            {
                BuildingLevel buildingLevel = (BuildingLevel)currentLevel;
                levelFound = CurrentContext.BuildingLevels.Include( bl => bl.Requirements ).Include( bl => bl.Cost ).Include( bl => bl.Bonuses)
                    .SingleOrDefault( bl => bl.BuildingName == buildingLevel.BuildingName && bl.Number == buildingLevel.Number + 1 );
            }
            // Recherche si Technology
            else if ( currentLevel is TechnologyLevel )
            {
                TechnologyLevel technoLevel = (TechnologyLevel)currentLevel;
                levelFound = CurrentContext.TechnologyLevels.Include( tl => tl.Requirements ).Include( tl => tl.Cost ).Include( tl => tl.Bonuses )
                    .SingleOrDefault( bl => bl.TechnologyName == technoLevel.TechnologyName && bl.Number == technoLevel.Number + 1 );
            }
            return levelFound;
        }

        /// <summary>
        /// Creates a RequirementAvailability from a RequirementList
        /// </summary>
        /// <param name="requirements">The requirements to check</param>
        /// <param name="currentIslandId">The current island</param>
        /// <returns></returns>
        private RequirementAvailability CreateAvailability( List<Requirement> requirements, long currentIslandId )
        {
            RequirementAvailability availability = new RequirementAvailability();
            if( requirements == null || requirements.Count() < 1 )
            {
                return availability;
            }

            List<Building> buildingsOnIsland = GetBuildingsOnCurrentIsland( currentIslandId );
            IList<Technology> playersTechnologies = CurrentContext.Islands.SingleOrDefault( i => i.IslandId == currentIslandId ).Owner.Technologies;
            if( playersTechnologies == null )
            {
                playersTechnologies = new List<Technology>();
            }
            //// If requirements contains at least a technology but the player doesn't have any yet, return null
            //if ( playersTechnologies == null && requirements.Any( r => r.TechnologyName != TechnologyName.none ) )
            //    return null;

            //if ( playersTechnologies != null )
            //{
                foreach ( Requirement requirement in requirements.Where( r => r.TechnologyName != TechnologyName.none ) )
                {
                    availability.Availabilities.Add( new Availability
                    {
                        Available = IsTechnologyRequirementMet( requirement, playersTechnologies ),
                        Requirement = requirement
                    } );
                }
            //}

            foreach ( Requirement requirement in requirements.Where( r => r.BuildingName != BuildingName.none ) )
            {
                availability.Availabilities.Add( new Availability
                {
                    Available = IsBuildingRequirementMet( requirement, buildingsOnIsland ),
                    Requirement = requirement
                } );
            }
            return availability;
        }

        private bool IsBuildingRequirementMet( Requirement buildingRequirement, IList<Building> buildingsOnIsland )
        {
            bool met = false;
            if ( buildingsOnIsland.Any( b => b.BuildingName == buildingRequirement.BuildingName && b.Level.Number >= buildingRequirement.Number ) )
                met = true;
            return met;
        }

        private bool IsTechnologyRequirementMet( Requirement technologyRequirement, IList<Technology> playersTechnologies )
        {
            bool met = false;
            if ( playersTechnologies.Any( b => b.TechnologyName == technologyRequirement.TechnologyName && b.Level.Number >= technologyRequirement.Number ) )
                met = true;
            return met;
        }

        public List<Building> GetBuildingsOnCurrentIsland( long currentIslandId )
        {
            return CurrentContext.Islands
                    .Include( i => i.Buildings ).ThenInclude( b => b.Level )
                    .Where( i => i.IslandId == currentIslandId ).SingleOrDefault().Buildings.ToList();
        }

        public static string StaticRequirementToString( Requirement requirement )
        {
            string requirementType = requirement.BuildingName == BuildingName.none ? "Technologie " : "Bâtiment ";
            string requirementName = requirement.BuildingName == BuildingName.none ? 
                TechnologyManager.StaticTechnologyNameToName( requirement.TechnologyName)  : BuildingManager.StaticBuildingNameToName( requirement.BuildingName );

            return requirementType + requirementName + " de niveau " + requirement.Number.ToString();
        }
    }
}

