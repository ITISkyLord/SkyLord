using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Services
{
    public class BuildingManager
    {
        LevelContext _currentContext;
        long _currentIslandId;
        long _playerId;

        public BuildingManager( LevelContext currentContext, long currentIslandId, long playerId )
        long _lastCurrentIsland;
        List<Building> _buildingsOnIlsand;
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
        public bool AddBuildingToIsland( BuildingName buildingName, long currentIslandId, int position )
        {
            Building buildingToAdd;
            buildingToAdd = new Building
            {
                Name = BuildingNameToName( buildingName ),
                BuildingName = buildingName,
                Level = _currentContext.BuildingLevels.Where( bl => bl.BuildingName == buildingName && bl.Number == 1 ).Single()
            };

            // If the building already exists AND is supposed to be unique on the island, or if the position is not free, don't add it and return false
            if ( GetBuildingsOnCurrentIsland( currentIslandId ).Any( b => buildingToAdd.BuildingName == b.BuildingName ) && IsBuildingUnique( buildingName )
                || !IsPositionAvailable( position, currentIslandId ) )
            {
                return false;
            }

            long playerId = CurrentContext.Islands.Include( i => i.Owner ).SingleOrDefault( i => i.IslandId == currentIslandId ).Owner.PlayerId;
            if ( currentIsland.Buildings == null )
            {
                currentIsland.Buildings = new List<Building>();
            }
            currentIsland.Buildings.Add( buildingToAdd );

            return true;
        }

        //private Building GetBuildingAtPosition( int position, long currentIslandId )
        //{
        //    GetBuildingsOnCurrentIsland( currentIslandId )
        //}

        public bool IsPositionAvailable( int position, long currentIslandId )
        {
            return !GetBuildingsOnCurrentIsland( currentIslandId ).Any( b => b.Position == position );
        }

        public List<Building> GetAvailableBuildings()
        {
            List<Building> availableBuildings = new List<Building>();
            foreach ( BuildingName buildingName in Enum.GetValues( typeof( BuildingName ) ) )
            {
                if ( buildingName != BuildingName.none )
                {
                    availableBuildings.Add( new Building { BuildingName = buildingName, Name = BuildingNameToName( buildingName ) } );
                }
            }
            return availableBuildings;
        }
        private string BuildingNameToName( BuildingName buildingName )
        {
            string name;
            switch ( buildingName )
            {
                case BuildingName.academy:
                    name = "Académie";
                    break;
                case BuildingName.barrack:
                    name = "Baraquement";
                    break;
                case BuildingName.cache:
                    name = "Cachette";
                    break;
                case BuildingName.cristalField:
                    name = "Mine de cristal";
                    break;
                case BuildingName.invocation:
                    name = "Cercle d'invocation";
                    break;
                case BuildingName.laboratory:
                    name = "Laboratoire";
                    break;
                case BuildingName.library:
                    name = "Bibliothèque";
                    break;
                case BuildingName.magicField:
                    name = "Champ de magie";
                    break;
                case BuildingName.metalField:
                    name = "Mine de métal";
                    break;
                case BuildingName.shield:
                    name = "Bouclier";
                    break;
                case BuildingName.tower:
                    name = "Tour de Mage";
                    break;
                case BuildingName.woodField:
                    name = "Camp de bucherons";
                    break;
                default:
                    name = "Error";
                    break;
            }
            return name;
        }

        private bool IsBuildingUnique( BuildingName buildingName )
        {
            bool isUnique;
            switch ( buildingName )
            {
                case BuildingName.cristalField:
                case BuildingName.magicField:
                case BuildingName.metalField:
                case BuildingName.woodField:
                    isUnique = false;
                    break;

                default:
                    isUnique = true;
                    break;
            }
            return isUnique;
        }

        private List<Building> GetBuildingsOnCurrentIsland()
        {
            if ( _lastCurrentIsland != currentIslandId )
            {
                _buildingsOnIlsand = CurrentContext.Islands
                    .Include( i => i.Buildings )
                    .Where( i => i.IslandId == _currentIslandId ).SingleOrDefault().Buildings.ToList();
                _lastCurrentIsland = currentIslandId;
            }
            return _buildingsOnIlsand;
            
        }
    }
}
