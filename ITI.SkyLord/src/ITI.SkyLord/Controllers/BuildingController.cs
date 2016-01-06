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
        public LevelContext LevelContext { get; set; }
        [FromServices]
        public IslandContext IslandContext { get; set; }
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

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
            long playerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;

            Island currentIsland = IslandContext.GetIsland(islandId, playerId);

            List<Building> buildings = new List<Building>();

            buildings = currentIsland.Buildings.ToList();

            buildingViewModel.Buildings = buildings;

            IslandContext.FillStandardVM( buildingViewModel, playerId, islandId );

            return View( buildingViewModel );
        }

    }
}
