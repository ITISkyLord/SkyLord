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
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;

namespace ITI.SkyLord.Controllers
{
    public class IslandController : GenericController
    {
        public IActionResult OverView( long islandId )
        {
            OverViewViewModel ovvm = new OverViewViewModel();

            SetupContext.GetIsland( islandId, SetupContext.GetPlayer( User ) );
            SetupContext.ValidateIsland( islandId, SetupContext.GetPlayer( User ) );

            SetupContext.FillStandardVM( ovvm, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, SetupContext.GetPlayer( User ) );
            return View( ovvm );
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

            SeeIslandViewModel islandViewModel = CreateBuildingViewModel( currentIsland.IslandId, SetupContext.GetPlayer( User.GetUserId() ).PlayerId );

            return View( islandViewModel );
        }

        private Island GetIsland( long islandId )
        {
            if ( islandId == 0 )
            {
                long activePlayerId = SetupContext.GetPlayer( User );
                return SetupContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .Include( i => i.Buildings )
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

        private SeeIslandViewModel CreateBuildingViewModel( SeeIslandViewModel model, long islandId, long playerId )
        {
            LevelManager levelManager = new LevelManager( SetupContext );
            BuildingManager buildingManager = new BuildingManager( SetupContext, levelManager );
            BonusManager bonusManager = new BonusManager( SetupContext );
            ArmyManager armyManager = new ArmyManager( SetupContext, bonusManager );
            EventManager eventManager = new EventManager( SetupContext, new EventPackManager( SetupContext ) );

            // Fill Standard
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            // Current Player & Island
            model.Layout.CurrentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Island currentIsland = SetupContext.GetIsland( islandId, model.Layout.CurrentPlayer.PlayerId );
            model.CurrentIsland = currentIsland;

            // Tous les buildings sur l'island (modifés par leurs bonus)
            model.Buildings = bonusManager.GetResolvedBuildings( buildingManager.GetBuildingsOnCurrentIsland( islandId, playerId ), playerId, islandId) ;
            model.DicoBuildings = new Dictionary<string, Building>();
            foreach ( var building in model.Buildings )
            {
                model.DicoBuildings.Add( building.Position.ToString(), building );
            }

            // Récupère tout les events de tout les batiments (de 0 à 10 donc)
            model.AllBuildingEventOnIsland = new Dictionary<int, List<BuildingEvent>>();
            for ( var i = 0; i <= 10; i++ )
            {
                model.AllBuildingEventOnIsland.Add( i, eventManager.GetBuildingEventsOnThisBuildingPosition( islandId, i ) );
            }

            // Tout les nexts level de chaque batiments
            model.NextLevel = new Dictionary<int, Level>();
            foreach ( Building building in model.Buildings )
            {
                // Si le next level existe => on l'ajoute au dico OTD
                var nextLevel = levelManager.FindNextLevel( building.Level );
                if ( nextLevel != null )
                {
                    model.NextLevel.Add( building.Position, bonusManager.ResolveBonuses( (BuildingLevel)nextLevel, playerId, islandId ) );
                }
            }

            // Tout les bâtiments qui existent
            model.AvailableBuildings = buildingManager.GetAvailableBuildings();

            // On retire tout les bâtiments qui sont uniques et que l'on a déjà construit sur notre island
            model.AvailableBuildings = buildingManager.RemoveAlreadyBuiltBuilding( model.AvailableBuildings, model.Buildings );

            // Army sur l'island
            model.CurrentArmy = armyManager.GetCurrentDefenseArmy( islandId );

            // Toutes les unités possibles  
            model.AllUnits = SetupContext.Units.Include( u => u.UnitCost ).ToList();
            model.AvailableUnit = armyManager.GetExistingUnits().Where( u => levelManager.GetAvailablility( u, islandId ).IsItemAvailable ).ToList();

            // La queue de création des unités
            model.UnitsQueue = eventManager.GetCurrentUnitQueue( islandId );

            // Toutes les technologies possibles
            TechnologyManager techManager = new TechnologyManager( SetupContext, levelManager, new BonusManager( SetupContext ) );
            model = CreateTechnologyItems( model, islandId, playerId );
            return model;
        }
        private SeeIslandViewModel CreateBuildingViewModel( long islandId, long playerId )
        {
            SeeIslandViewModel model = new SeeIslandViewModel();
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User ), islandId );

            return CreateBuildingViewModel( model, islandId, playerId );
        }

        private SeeIslandViewModel CreateTechnologyItems( SeeIslandViewModel model, long islandId, long playerId )
        {
            //model.Layout.CurrentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Island currentIsland = SetupContext.GetIsland( islandId, model.Layout.CurrentPlayer.PlayerId );

            LevelManager levelManager = new LevelManager( SetupContext );
            BonusManager bonusManager = new BonusManager( SetupContext );
            model.TechnologyManager = new TechnologyManager( SetupContext, levelManager, new BonusManager( SetupContext ) );

            List<TechnologyLevel> availableTechnologies = model.TechnologyManager.GetAvailableTechnologies();
            List<TechnologyLevel> playersTechnologies = model.TechnologyManager.GetPlayersTechnologies( playerId ).Select( t => t.Level ).ToList();
            model.TechnologyDisplays = new List<TechnologyDisplay>();
            foreach ( TechnologyLevel technologyLevel in availableTechnologies )
            {
                // Look for the technology in the player's technology list
                TechnologyLevel technologyFound = playersTechnologies.SingleOrDefault( tl => tl.TechnologyName == technologyLevel.TechnologyName );

                bool isResearched = false;
                bool isAvailable = false;
                TechnologyLevel levelToAdd = technologyLevel;
                Ressource CostToDisplay = technologyLevel.Cost;

                // If a technology was found, the player already has it, so we add the current level and the next level cost
                if ( technologyFound != null )
                {
                    isResearched = true;
                    isAvailable = true;
                    levelToAdd = technologyFound;
                    CostToDisplay = levelManager.FindNextLevel( technologyFound ).Cost;
                }
                // If no technology was found, we check if the technology is available to the player
                else if ( levelManager.GetAvailablility( technologyLevel, islandId ).IsItemAvailable )
                {
                    isAvailable = true;
                }

                model.TechnologyDisplays.Add( new TechnologyDisplay
                {
                    IsResearched = isResearched,
                    IsAvailable = isAvailable,
                    TechnologyLevel = levelToAdd,
                    Cost = CostToDisplay
                } );
            }

            return model;
        }

    }
}