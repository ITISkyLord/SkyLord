﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Services
{
    public class BuildingManager
    {
        long _lastCurrentIsland = 0;
        List<Building> _buildingsOnIlsand;

        public LevelContext CurrentContext { get; }
        public LevelManager LevelManager { get; set; }
        public RessourceManager RessourceManager { get; set; }

        public BuildingManager( LevelContext currentContext, LevelManager levelManager, RessourceManager ressourceManager )
        {
            CurrentContext = currentContext;
            LevelManager = levelManager;
            RessourceManager = ressourceManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingName"></param>
        /// <returns>True : building was added. False : building not allowed.</returns>
        public bool AddBuildingToIsland( BuildingName buildingName, long currentIslandId, int position )
        {
            long playerId = CurrentContext.Islands.Include( i => i.Owner ).SingleOrDefault( i => i.IslandId == currentIslandId ).Owner.PlayerId;

            // If the building already exists AND is supposed to be unique on the island, or if the position is not free, don't add it and return false
            if ( GetBuildingsOnCurrentIsland( currentIslandId, playerId ).Any( b => buildingName == b.BuildingName ) && IsBuildingUnique( buildingName )
                || !IsPositionAvailable( position, currentIslandId, playerId ) )
            {
                return false;
            }

            Island currentIsland = CurrentContext.GetIsland( currentIslandId, playerId );
            if ( currentIsland.Buildings == null )
            {
                currentIsland.Buildings = new List<Building>();
            }

            Building buildingToAdd;
            buildingToAdd = new Building
            {
                Name = BuildingNameToName( buildingName ),
                BuildingName = buildingName,
                Level = CurrentContext.BuildingLevels.Where( bl => bl.BuildingName == buildingName && bl.Number == 1 ).Single(),
                Position = position
            };
            currentIsland.Buildings.Add( buildingToAdd );

            return true;
        }

        public bool LevelUpBuilding( BuildingName buildingNameToLevelUp, long currentIslandId, long playerId, int position )
        {
            Building buildingToLevelUp = GetBuildingsOnCurrentIsland( currentIslandId, playerId ).Single( b => b.BuildingName == buildingNameToLevelUp && b.Position == position );

            if ( LevelManager.IsNextLevelAvailable( buildingToLevelUp.Level, currentIslandId ) )
            {
                Level nextLevel = LevelManager.FindNextLevel( buildingToLevelUp.Level );

                RessourceManager.RemoveRessource( CurrentContext.GetIsland( currentIslandId, playerId ).AllRessources, nextLevel.Cost );
                return LevelManager.LevelUp( buildingToLevelUp );
            }
            return false;
        }

        public bool IsPositionAvailable( int position, long currentIslandId, long playerId )
        {
            return !GetBuildingsOnCurrentIsland( currentIslandId, playerId ).Any( b => b.Position == position );
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

        public bool IsEnough( Ressource ressourceToChange, Ressource cost )
        {
            return RessourceManager.IsEnough( ressourceToChange, cost );
        }

        public bool IsEnoughForNextLevel( BuildingName buildingName, long islandId, long playerId, int position )
        {
            Building buildingToLevelUp = GetBuildingsOnCurrentIsland( islandId, playerId ).Single( b => b.BuildingName == buildingName && b.Position == position );

            Level nextLevel = LevelManager.FindNextLevel( buildingToLevelUp.Level );
            if( nextLevel != null )
            {
                return RessourceManager.IsEnough( CurrentContext.GetIsland( islandId, playerId ).AllRessources, nextLevel.Cost );
            }
            return false;
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

        public List<Building> GetBuildingsOnCurrentIsland( long currentIslandId, long playerId )
        {
            if( currentIslandId == 0 )
            {
                currentIslandId = CurrentContext.Islands.Include( i => i.Owner ).Single( i => i.IsCapital && i.Owner.PlayerId == playerId ).IslandId;
            }

            if ( _lastCurrentIsland != currentIslandId )
            {
                _buildingsOnIlsand = CurrentContext.Islands
                    .Include( i => i.Buildings )
                    .ThenInclude( b => b.Level )
                    .ThenInclude( l => l.Requirements )
                    .First( i => i.IslandId == currentIslandId ).Buildings.ToList();
                _lastCurrentIsland = currentIslandId;

                foreach( Building buiding in _buildingsOnIlsand )
                {
                    buiding.Level.Cost = CurrentContext.Buildings.Include( b => b.Level).ThenInclude( l => l.Cost)
                        .First( b => b.BuildingId == buiding.BuildingId ).Level.Cost;
                }
            }
            return _buildingsOnIlsand;
        }

        private bool LevelUpBuilding( Building buildingToLevelUp, long currentIslandId )
        {
            if ( LevelManager.IsNextLevelAvailable( buildingToLevelUp.Level, currentIslandId ) )
            {
                return LevelManager.LevelUp( buildingToLevelUp );
            }
            return false;
        }

        //private Building GetBuildingAtPosition( int position, long currentIslandId )
        //{
        //    return GetBuildingsOnCurrentIsland( currentIslandId ).SingleOrDefault( b => b.Position == position );
        //}
    }
}
