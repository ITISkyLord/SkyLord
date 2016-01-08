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
        /// See buildings from the current island.
        /// </summary>
        /// <param name="id">Current Island Id</param>
        /// <returns>Buildings view</returns>
        public IActionResult SeeBuildings( long islandId )
        {
            BuildingViewModel buildingViewModel = new BuildingViewModel();
            long playerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            Island currentIsland = IslandContext.GetIsland( islandId, playerId );

            List<Building> buildings = new List<Building>();
            buildings = currentIsland.Buildings.ToList();
            buildingViewModel.Buildings = buildings;

            BuildingManager buildingManager = new BuildingManager(LevelContext, islandId, playerId);
            buildingViewModel.AvailableBuildings = buildingManager.GetAvailableBuildings();



            IslandContext.FillStandardVM( buildingViewModel, playerId, islandId );
            return View( buildingViewModel );
        }

        public IActionResult AddBuilding( BuildingViewModel model, int position, long islandId = 0 )
        {
            BuildingViewModel buildingViewModel = new BuildingViewModel();
            long playerId = LevelContext.GetPlayer( User.GetUserId() ).PlayerId;

            BuildingManager buildingManager = new BuildingManager(LevelContext, islandId, playerId);

            if ( buildingManager.AddBuildingToIsland( model.BuildingToBuild, islandId, position ) )
            {
                LevelContext.SaveChanges();
            }
            return RedirectToAction( "SeeBuildings", new { islandId = islandId } );
        }
    }
}
