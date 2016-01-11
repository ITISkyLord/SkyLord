using System.Linq;
using System.Security.Claims;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.SeeIslands;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using ITI.SkyLord.ViewModel;
using ITI.SkyLord.Services;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Rendering;

namespace ITI.SkyLord.Controllers
{
    public class IslandController : Controller
    {

        [FromServices]
        public SetupContext SetupContext { get; set; }

        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        [FromServices]
        public IslandContext IslandContext { get; set; }

        [FromServices]
        public LevelContext LevelContext { get; set; }

        public IActionResult OverView()
        {
            OverViewViewModel ovvm = new OverViewViewModel();

            PlayerContext.FillStandardVM(ovvm, PlayerContext.GetPlayer(User.GetUserId()).PlayerId);
            return View(ovvm);
        }

        public IActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// See the island(s) of the current player
        /// </summary>
        /// <param name="id">Mail of player</param>
        /// <returns></returns>
        public IActionResult SeeMyIsland( long islandId = 0 )
        {
            Island currentIsland = GetIsland( islandId );

            SeeIslandViewModel islandViewModel = CreateBuildingViewModel(currentIsland.IslandId, LevelContext.GetPlayer(User.GetUserId()).PlayerId);

            return View( islandViewModel );
        }

        private Island GetIsland( long islandId )
        {
            if ( islandId == 0 )
            {
                long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
                return IslandContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .Include(i => i.Buildings)
                    .SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId );
            }
            else
            {
                long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
                return IslandContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IslandId == islandId && i.Owner.PlayerId == activePlayerId );
            }
        }

        private SeeIslandViewModel CreateBuildingViewModel(SeeIslandViewModel model, long islandId, long playerId)
        {
            LevelContext.FillStandardVM(model, LevelContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            model.Layout.CurrentPlayer = LevelContext.GetPlayer(User.GetUserId());
            Island currentIsland = IslandContext.GetIsland(islandId, model.Layout.CurrentPlayer.PlayerId);

            RessourceManager ressourceManager = new RessourceManager(LevelContext);
            LevelManager levelManager = new LevelManager(LevelContext);
            BuildingManager buildingManager = new BuildingManager(LevelContext, levelManager, ressourceManager);

            model.Buildings = buildingManager.GetBuildingsOnCurrentIsland(islandId, playerId);
            model.NextLevelCosts = new Dictionary<int, Ressource>();
            foreach (Building building in model.Buildings)
            {
                model.NextLevelCosts.Add(building.Position, levelManager.FindNextLevel(building.Level).Cost);
            }
            model.AvailableBuildings = buildingManager.GetAvailableBuildings();
            model.AvailableConstructionBuildings = new SelectList(model.AvailableBuildings.Select(b => b.BuildingName));

            return model;
        }

        private SeeIslandViewModel CreateBuildingViewModel(long islandId, long playerId)
        {
            SeeIslandViewModel model = new SeeIslandViewModel();
            LevelContext.FillStandardVM(model, LevelContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            return CreateBuildingViewModel(model, islandId, playerId);
        }


    }
}