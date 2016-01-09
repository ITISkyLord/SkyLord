using System;
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

namespace ITI.SkyLord.Controllers
{
    public class BuildingController : Controller
    {
        [FromServices]
        public LevelContext LevelContext { get; set; }
        [FromServices]
        public IslandContext IslandContext { get; set; }
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        public override void OnActionExecuting( ActionExecutingContext context )
        {
            if ( User.IsSignedIn() )
            {
                // On executing any controller check if the query holds the islandId
                if ( Request.Query.ContainsKey( "islandId" ) )
                {
                    // If islandId is present, check it with ValidateIsland method
                    long activePlayerId = LevelContext.GetPlayer( User.GetUserId() ).PlayerId;
                    LevelContext.ValidateIsland( long.Parse( Request.Query[ "islandId" ] ), activePlayerId );
                }
                //else
                //{
                //    // If islandId is not present, add it to the query
                //    long activePlayerId = LevelContext.GetPlayer( User.GetUserId() ).PlayerId;
                //    Island userCapital = LevelContext.GetIsland( 0, activePlayerId );
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
            long playerId = LevelContext.GetPlayer( User.GetUserId() ).PlayerId;

            BuildingViewModel buildingViewModel = CreateBuildingViewModel( islandId, playerId );
            return View( buildingViewModel );

        }

        /// <summary>
        /// Adds a new building
        /// </summary>
        /// <param name="model"></param>
        /// <param name="islandId">Current Island Id</param>
        /// <returns>Redirects to SeeBuildings</returns>
        public IActionResult AddBuilding( BuildingViewModel model, long islandId = 0 )
        {
            BuildingViewModel buildingViewModel = new BuildingViewModel();
            long playerId = LevelContext.GetPlayer( User.GetUserId() ).PlayerId;

            BuildingManager buildingManager = new BuildingManager( LevelContext, new LevelManager( LevelContext ), new RessourceManager( LevelContext ) );

            if ( !buildingManager.IsEnoughForFirstLevel( model.TargetBuilding, islandId, playerId ) )
            {
                return RedirectToAction( "SeeBuildings", new { islandId = islandId } );
            }

            if ( buildingManager.AddBuildingToIsland( model.TargetBuilding, islandId, model.Position ) )
            {
                LevelContext.SaveChanges();
            }
            return RedirectToAction( "SeeBuildings", new { islandId = islandId } );
        }

        public IActionResult LevelUpBuilding( BuildingViewModel model, long islandId = 0 )
        {
            long playerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            BuildingViewModel buildingViewModel = new BuildingViewModel();

            // TODO Changer en SetupContext

            BuildingManager buildingManager = new BuildingManager( LevelContext, new LevelManager( LevelContext ), new RessourceManager( LevelContext ) );

            if( !buildingManager.IsEnoughForNextLevel( model.TargetBuilding, islandId, playerId, model.Position ) )
            {
                return RedirectToAction( "SeeBuildings", new { islandId = islandId } );
            }

            if ( buildingManager.LevelUpBuilding( model.TargetBuilding, islandId, playerId, model.Position ) )
            {
                LevelContext.SaveChanges();
            }
            return RedirectToAction( "SeeBuildings", new { islandId = islandId } );
        }

        private BuildingViewModel CreateBuildingViewModel( BuildingViewModel model, long islandId, long playerId )
        {
            LevelContext.FillStandardVM( model, LevelContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            model.Layout.CurrentPlayer = LevelContext.GetPlayer( User.GetUserId() );
            Island currentIsland = IslandContext.GetIsland( islandId, model.Layout.CurrentPlayer.PlayerId );

            RessourceManager ressourceManager = new RessourceManager( LevelContext );
            LevelManager levelManager = new LevelManager( LevelContext );
            BuildingManager buildingManager = new BuildingManager( LevelContext, levelManager, ressourceManager );

            model.Buildings = buildingManager.GetBuildingsOnCurrentIsland( islandId, playerId );
            model.NextLevelCosts = new Dictionary<int, Ressource>();
            foreach( Building building in model.Buildings )
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
            LevelContext.FillStandardVM( model, LevelContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return CreateBuildingViewModel( model, islandId, playerId ) ;
        }
    }
}
