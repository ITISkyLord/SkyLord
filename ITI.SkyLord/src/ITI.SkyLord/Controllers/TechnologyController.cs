using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITI.SkyLord.Services;
using ITI.SkyLord.ViewModel.Technologies;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Mvc.Rendering;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.Controllers
{
    public class TechnologyController : GenericController
    {
        public override void OnActionExecuting( ActionExecutingContext context )
        {
            if( User.IsSignedIn() )
            {
                // On executing any controller check if the query holds the islandId
                if( Request.Query.ContainsKey( "islandId" ) )
                {
                    // If islandId is present, check it with ValidateIsland method
                    long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
                    SetupContext.ValidateIsland( long.Parse( Request.Query["islandId"] ), activePlayerId );
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

        public IActionResult SeeTechnologies( long islandId )
        {
            long playerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
            TechnologyViewModel model = CreateTechnologyViewModel( islandId, playerId );
            return View( model );

        }

        public IActionResult AddTechnology( long islandId, TechnologyViewModel model )
        {

            EventManager em = new EventManager( SetupContext, new EventPackManager( SetupContext ) );
            BonusManager bonusManager = new BonusManager( SetupContext );

            TechnologyManager techManager = new TechnologyManager( SetupContext, new LevelManager( SetupContext ), bonusManager );
            long playerId = SetupContext.GetPlayer( User );
            Island island = SetupContext.GetIsland( islandId, playerId );
            if ( !techManager.IsEnoughForFirstLevel( model.TargetTechnology, islandId, playerId ) )
            {
                ModelState.AddModelError( String.Empty, "Vous n'avez pas assez de ressources." );
            }
            else
            {
                TechnologyLevel technologyLevel = SetupContext.TechnologyLevels.Single( t => t.TechnologyName == model.TargetTechnology && t.Number == 1 );
                RessourceManager.RemoveRessource( island.AllRessources, technologyLevel.Cost );

                em.AddTechnologyEvent( SetupContext, model.TargetTechnology, technologyLevel.Duration, island );
                //SetupContext.Ressources.Update( island.AllRessources );
                SetupContext.SaveChanges();
            }
            return RedirectToAction( "SeeMyIsland", "Island", new
            {
                islandId = islandId
            } );
        }

        public IActionResult LevelUpTechnology( long islandId, TechnologyViewModel model )
        {

            EventManager em = new EventManager( SetupContext, new EventPackManager( SetupContext ) );
            BonusManager bonusManager = new BonusManager( SetupContext );
            TechnologyManager techManager = new TechnologyManager( SetupContext, new LevelManager( SetupContext ), bonusManager );

            long playerId = SetupContext.GetPlayer( User );
            Island island = SetupContext.GetIsland( islandId, playerId );

            if ( !techManager.IsEnoughForNextLevel( model.TargetTechnology, islandId, playerId ) )
            {
                ModelState.AddModelError( String.Empty, "Vous n'avez pas assez de ressources." );
            }
            else
            {
                TechnologyLevel technologyLevel = techManager.GetPlayersTechnologies( playerId ).Single( tl => tl.TechnologyName == model.TargetTechnology ).Level;
                RessourceManager.RemoveRessource( island.AllRessources, technologyLevel.Cost );

                em.AddTechnologyEvent( SetupContext, model.TargetTechnology, technologyLevel.Duration, island );
                
                SetupContext.SaveChanges();
            }
            return RedirectToAction( "SeeMyIsland", "Island", new
            {
                islandId = islandId
            } );
        }

        private TechnologyViewModel CreateTechnologyViewModel( TechnologyViewModel model, long islandId, long playerId )
        {
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            //model.Layout.CurrentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Island currentIsland = SetupContext.GetIsland( islandId, model.Layout.CurrentPlayer.PlayerId );

            LevelManager levelManager = new LevelManager( SetupContext );
            BonusManager bonusManager = new BonusManager( SetupContext );
            TechnologyManager technologyManager = new TechnologyManager( SetupContext, levelManager, new BonusManager( SetupContext ) );

            List<TechnologyLevel> availableTechnologies = technologyManager.GetAvailableTechnologies();
            List<TechnologyLevel> playersTechnologies = technologyManager.GetPlayersTechnologies( playerId ).Select( t => t.Level).ToList();
            foreach( TechnologyLevel technologyLevel in availableTechnologies )
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
                else if( levelManager.GetAvailablility( technologyFound, islandId ).IsItemAvailable )
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

        private TechnologyViewModel CreateTechnologyViewModel( long islandId, long playerId )
        {
            TechnologyViewModel model = new TechnologyViewModel();
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return CreateTechnologyViewModel( model, islandId, playerId );
        }
    }
}
