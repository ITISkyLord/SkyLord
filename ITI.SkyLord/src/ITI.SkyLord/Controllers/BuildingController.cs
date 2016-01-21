﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Services;
using ITI.SkyLord.ViewModel;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Rendering;
using ITI.SkyLord.ViewModel.Partial;

namespace ITI.SkyLord.Controllers
{
    public class BuildingController : GenericController
    {
        public override void OnActionExecuting( ActionExecutingContext context )
        {
            if ( User.IsSignedIn() )
            {
                // On executing any controller check if the query holds the islandId
                if ( Request.Query.ContainsKey( "islandId" ) )
                {
                    // If islandId is present, check it with ValidateIsland method
                    long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
                    SetupContext.ValidateIsland( long.Parse( Request.Query[ "islandId" ] ), activePlayerId );
                }
                //else
                //{
                //    // If islandId is not present, add it to the query
                //    long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
                //    Island userCapital = SetupContext.GetIsland( 0, activePlayerId );
                //    Request.QueryString.Add( "islandId", userCapital.IslandId.ToString() );
                //}
                base.OnActionExecuting( context );
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// See buildings of the current island.
        /// </summary>
        /// <param name="id">Current Island Id</param>
        /// <returns>Buildings view</returns>
        public IActionResult SeeBuildings( long islandId )
        {
            long playerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
            BuildingViewModel model = CreateBuildingViewModel( islandId, playerId );
            return View( model );

        }

        /// <summary>
        /// Adds a new building
        /// </summary>
        /// <param name="model"></param>
        /// <param name="islandId">Current Island Id</param>
        /// <returns>Redirects to SeeBuildings</returns>
        [HttpPost]
        public IActionResult AddBuilding( BuildingPartialViewModel model, long islandId = 0 )
        { 

            BuildingViewModel buildingViewModel = new BuildingViewModel();

            long playerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
            Island island = SetupContext.GetIsland(islandId, playerId);

            BuildingManager buildingManager = new BuildingManager( SetupContext, new LevelManager( SetupContext ));
            EventManager eventManager = new EventManager(SetupContext, new EventPackManager(SetupContext));

            // Test assez de ressource
            if ( !buildingManager.IsEnoughForFirstLevel( model.TargetBuilding, islandId, playerId ) )
            {
                return RedirectToAction( "SeeBuildings", new { islandId = islandId } );
            }

            // Maintenant création de l'event pour la construction du building
            eventManager.AddBuildingEvent(SetupContext, model.TargetBuilding, island, model.Position);

            return RedirectToAction( "SeeMyIsland", "Island", new { islandId = islandId } );
        }

        public IActionResult LevelUpBuilding( int buildingPosition, long islandId = 0  )
        {
            long playerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
            BuildingViewModel buildingViewModel = new BuildingViewModel();
            BuildingManager buildingManager = new BuildingManager( SetupContext, new LevelManager( SetupContext ));

            // Récupère le building ciblé
            Building buildingTarget = buildingManager.GetBuildingOnPosition(islandId, buildingPosition);

            // Si pas assez de ressource
            if ( !buildingManager.IsEnoughForNextLevel(buildingTarget.BuildingName, islandId, playerId, buildingTarget.Position ) )
            {
                return RedirectToAction( "SeeMyIsland", "Island", new { islandId = islandId } );
            }

            // Si tout est bon => LevelUp
            else if ( buildingManager.LevelUpBuilding(buildingTarget.BuildingName, islandId, playerId, buildingTarget.Position ) )
            {
                SetupContext.SaveChanges();
            }

            return RedirectToAction( "SeeMyIsland", "Island", new { islandId = islandId } );
        }

        private BuildingViewModel CreateBuildingViewModel( BuildingViewModel model, long islandId, long playerId )
        {
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            model.Layout.CurrentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Island currentIsland = SetupContext.GetIsland( islandId, model.Layout.CurrentPlayer.PlayerId );

            LevelManager levelManager = new LevelManager( SetupContext );
            BuildingManager buildingManager = new BuildingManager( SetupContext, levelManager );

            model.Buildings = buildingManager.GetBuildingsOnCurrentIsland( islandId, playerId );
            model.NextLevelCosts = new Dictionary<int, Ressource>();
            foreach ( Building building in model.Buildings )
            {
                model.NextLevelCosts.Add( building.Position, levelManager.FindNextLevel( building.Level ).Cost );
            }
            model.AvailableBuildings = buildingManager.GetAvailableBuildings();
            model.AvailableConstructionBuildings = new SelectList( model.AvailableBuildings.Select( b => b.BuildingName ) );

            return model;
        }

        private BuildingViewModel CreateBuildingViewModel( long islandId, long playerId )
        {
            BuildingViewModel model = new BuildingViewModel();
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return CreateBuildingViewModel( model, islandId, playerId );
        }
    }
}
