using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Services;
using ITI.SkyLord.ViewModel;
using Microsoft.AspNet.Mvc;

namespace ITI.SkyLord.Controllers
{
    public class BuildingController : GenericController
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// See buildings from the current island.
        /// </summary>
        /// <param name="id">Current Island Id</param>
        /// <returns>Buildings view</returns>
        public IActionResult SeeBuildings( long islandId = 0 )
        {
            BuildingViewModel buildingViewModel = new BuildingViewModel();
            long playerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
            Island currentIsland = SetupContext.GetIsland(islandId, playerId);


            List<Building> buildings = new List<Building>();
            buildings = currentIsland.Buildings.ToList();
            buildingViewModel.Buildings = buildings;

            BuildingManager buildingManager = new BuildingManager(SetupContext, islandId, playerId);
            buildingViewModel.AvailableBuildings = buildingManager.GetAvailableBuildings();



            SetupContext.FillStandardVM( buildingViewModel, playerId, islandId );
            return View( buildingViewModel );
        }

        public IActionResult AddBuilding( BuildingViewModel model, long islandId = 0)
        {
            BuildingViewModel buildingViewModel = new BuildingViewModel();
            long playerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;

            BuildingManager buildingManager = new BuildingManager(SetupContext, islandId, playerId);

            if( buildingManager.AddBuildingToIsland( model.BuildingToBuild ) )
            {
                SetupContext.SaveChanges();
            }
            return RedirectToAction( "SeeBuildings", new { islandId = islandId } );

        }
    }
}
