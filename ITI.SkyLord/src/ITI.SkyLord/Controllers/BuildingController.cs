
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Controllers
{
    public class BuildingController : Controller
    {
        [FromServices]
        BuildingContext BuildingContext { get; set; }
        [FromServices]
        PlayerContext PlayerContext { get; set; }

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
            Island currentIsland = GetIsland(islandId);
            List<Building> buildings = currentIsland.Buildings.ToList();

            buildingViewModel.Buildings = buildings;
            return View( buildingViewModel );
        }
        private Island GetIsland( long islandId )
        {
            if( islandId == 0 )
            {
                long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
                return BuildingContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId );
            }
            else
            {
                return BuildingContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IslandId == islandId );
            }

        }

    }
}
