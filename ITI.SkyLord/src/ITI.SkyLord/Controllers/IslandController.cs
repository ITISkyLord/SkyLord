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
using ITI.SkyLord.ViewModel.Technologies;

namespace ITI.SkyLord.Controllers
{
    public class IslandController : GenericController
    {
        public IActionResult OverView(long islandId)
        {
            OverViewViewModel ovvm = new OverViewViewModel();

            SetupContext.GetIsland(islandId, SetupContext.GetPlayer(User));
            SetupContext.ValidateIsland(islandId, SetupContext.GetPlayer(User));

            SetupContext.FillStandardVM(ovvm, SetupContext.GetPlayer(User.GetUserId()).PlayerId, SetupContext.GetPlayer(User));
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

            SeeIslandViewModel islandViewModel = CreateBuildingViewModel(currentIsland.IslandId, SetupContext.GetPlayer(User.GetUserId()).PlayerId);

            return View(islandViewModel);
        }

        private Island GetIsland( long islandId )
        {
            if ( islandId == 0 )
            {
                long activePlayerId = SetupContext.GetPlayer( User);
                return SetupContext.Islands
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
                long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
                return SetupContext.Islands
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
            LevelManager levelManager = new LevelManager( SetupContext );
            BuildingManager buildingManager = new BuildingManager( SetupContext, levelManager);
            ArmyManager armyManager = new ArmyManager(SetupContext, new BonusManager(SetupContext));

            // Fill Standard
            SetupContext.FillStandardVM(model, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            // Current Player & Island
            model.Layout.CurrentPlayer = SetupContext.GetPlayer(User.GetUserId());
            Island currentIsland = SetupContext.GetIsland(islandId, model.Layout.CurrentPlayer.PlayerId);
            model.CurrentIsland = currentIsland;

            // Tous les buildings sur l'island
            model.Buildings = buildingManager.GetBuildingsOnCurrentIsland(islandId, playerId);
            model.DicoBuildings = new Dictionary<string, Building>();
            foreach(var building in model.Buildings)
            {
                model.DicoBuildings.Add(building.Position.ToString(), building);
            }

            // Tout les nexts level de chaque batiments
            model.NextLevel = new Dictionary<int, Level>();
            foreach (Building building in model.Buildings)
            { 
                // Si le next level existe => on l'ajoute au dico OTD
                var nextLevel = levelManager.FindNextLevel(building.Level);
                if(nextLevel != null)
                {
                    model.NextLevel.Add(building.Position, nextLevel);
                }
            }

            // Tout les bâtiments qui existent
            model.AvailableBuildings = buildingManager.GetAvailableBuildings();

            // On retire tout les bâtiments qui sont uniques et que l'on a déjà construit sur notre island
            model.AvailableBuildings = buildingManager.RemoveAlreadyBuiltBuilding(model.AvailableBuildings, model.Buildings);

            // Army sur l'island
            model.CurrentArmy = armyManager.GetCurrentDefenseArmy(islandId);

            // Toutes les unités possibles  
            model.AllUnits = SetupContext.Units.Include(u => u.UnitCost).ToList();
            model.AvailableUnit = SetupContext.Units.Where(u => levelManager.IsUnitAvailable(u, islandId)).ToList();

            return model;
        }
        private SeeIslandViewModel CreateBuildingViewModel(long islandId, long playerId)
        {
            SeeIslandViewModel model = new SeeIslandViewModel();
            SetupContext.FillStandardVM(model, SetupContext.GetPlayer(User), islandId);

            return CreateBuildingViewModel(model, islandId, playerId);
        }


        private TechnologyViewModel CreateTechnologyViewModel( TechnologyViewModel model, long islandId, long playerId )
        {
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            model.Layout.CurrentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Island currentIsland = SetupContext.GetIsland( islandId, model.Layout.CurrentPlayer.PlayerId );

            LevelManager levelManager = new LevelManager( SetupContext );
            TechnologyManager technologyManager = new TechnologyManager( SetupContext, levelManager, new BonusManager( SetupContext ) );


            model.Technologies = technologyManager.GetPlayersTechnologies( playerId );
            model.NextLevelCosts = new Dictionary<TechnologyName, Ressource>();
            foreach( Technology technology in model.Technologies )
            {
                model.NextLevelCosts.Add( technology.TechnologyName, levelManager.FindNextLevel( technology.Level ).Cost );
            }
            // model.AvailableTechnologies = .GetAvailableBuildings(); ISTechnologyAvailable à récupérer auprès de Tristan
            model.ExistingTechnologies = SetupContext.Technologies.ToList();
            model.AvailableTechnologies = SetupContext.Technologies.ToList();

            return model;
        }

        private TechnologyViewModel CreateTechnologyViewModel( long islandId, long playerId )
        {
            TechnologyViewModel model = new TechnologyViewModel();
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return CreateTechnologyViewModel( model, islandId, playerId );
        }

    }
}