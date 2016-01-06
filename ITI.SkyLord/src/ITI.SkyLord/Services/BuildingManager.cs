using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.Services
{
    public class BuildingManager
    {
        LevelContext _currentContext;
        long _currentIslandId;
        long _playerId;

        public BuildingManager( LevelContext currentContext, long currentIslandId, long playerId )
        {
            _currentContext = currentContext;
            _currentIslandId = currentIslandId;
            _playerId = playerId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingName"></param>
        /// <returns>True : building was added. False : building not allowed.</returns>
        public bool AddBuildingToIsland( BuildingName buildingName )
        {
            Building buildingToAdd;
            buildingToAdd = new Building
            {
                Name = BuildingNameToName( buildingName ),
                BuildingName = buildingName,
                Level = _currentContext.BuildingLevels.Where( bl => bl.BuildingName == buildingName && bl.Number == 1 ).Single()
            };
            _currentContext.Add( buildingToAdd );

            Island currentIsland = _currentContext.GetIsland( _currentIslandId, _playerId);

            if( currentIsland.Buildings == null )
            {
                currentIsland.Buildings = new List<Building>();
            }
            currentIsland.Buildings.Add( buildingToAdd );

            return true;
        }

        public List<Building> GetAvailableBuildings()
        {
            List<Building> availableBuildings = new List<Building>();
            foreach( BuildingName buildingName in Enum.GetValues( typeof( BuildingName ) ) )
            {
                if( buildingName != BuildingName.none )
                {
                    availableBuildings.Add( new Building { BuildingName = buildingName, Name = BuildingNameToName( buildingName ) } );
                }
            }
            return availableBuildings;
        }
        private string BuildingNameToName( BuildingName buildingName )
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 13);
            return "test" + month ;
        }
    }
}
