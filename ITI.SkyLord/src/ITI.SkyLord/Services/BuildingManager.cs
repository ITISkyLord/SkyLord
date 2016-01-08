﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using Microsoft.Data.Entity;

namespace ITI.SkyLord
{
    public class BuildingManager
    {
        ILevelContext _currentContext;
        long _currentIslandId;
        long _playerId;

        public BuildingManager( ILevelContext currentContext, long currentIslandId, long playerId )
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

            // If the building already exists AND is supposed to be unique on the island, don't add it and return false
            if ( GetBuildingsOnCurrentIsland().Any( b => buildingToAdd.BuildingName == b.BuildingName ) && IsBuildingUnique( buildingName ) )
            {
                return false;
            }

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
            string name;
            switch( buildingName )
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
                default :
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
                case BuildingName.cristalField :
                case BuildingName.magicField :
                case BuildingName.metalField :
                case BuildingName.woodField :
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
            return _currentContext.Islands
                    .Include( i => i.Buildings )
                    .Where( i => i.IslandId == _currentIslandId ).SingleOrDefault().Buildings.ToList();
        }

    }
}
