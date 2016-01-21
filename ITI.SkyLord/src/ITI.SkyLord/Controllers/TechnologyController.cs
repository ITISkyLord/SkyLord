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

        private TechnologyViewModel CreateTechnologyViewModel( TechnologyViewModel model, long islandId, long playerId )
        {
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            //model.Layout.CurrentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Island currentIsland = SetupContext.GetIsland( islandId, model.Layout.CurrentPlayer.PlayerId );

            LevelManager levelManager = new LevelManager( SetupContext );
            BonusManager bonusManager = new BonusManager( SetupContext );
            TechnologyManager technologyManager = new TechnologyManager( SetupContext, levelManager, new BonusManager( SetupContext ) );


            model.OwnTechnologies = technologyManager.GetPlayersTechnologies( playerId );
            model.NextLevelCosts = new Dictionary<TechnologyName, Ressource>();
            foreach( Technology technology in model.OwnTechnologies )
            {
                model.NextLevelCosts.Add( technology.TechnologyName, levelManager.FindNextLevel( technology.Level ).Cost );
            }
            model.AvailableTechnologies = technologyManager.GetAvailableTechnologies();
            //    model.AvailableSearchingTechnologies = new SelectList( model.AvailableTechnologies.Where( t => levelManager.GetNextLevelAvailablility( t., islandId ).IsItemAvailable ) );

            return model;
        }

        private TechnologyViewModel CreateTechnologyViewModel( long islandId, long playerId )
        {
            TechnologyViewModel model = new TechnologyViewModel();
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return CreateTechnologyViewModel( model, islandId, playerId );
        }

        internal IActionResult AddTechnology( long islandId, TechnologyViewModel model )
        {

            EventManager em = new EventManager( SetupContext, new EventPackManager( SetupContext ) );
            BonusManager bonusManager = new BonusManager( SetupContext );

            TechnologyManager techManager = new TechnologyManager( SetupContext, new LevelManager( SetupContext ), bonusManager);
            long playerId = model.Layout.CurrentPlayer.PlayerId;
            Island island = SetupContext.GetIsland( islandId, playerId );
            if(  techManager.IsEnoughForFirstLevel(model.TargetTechnology, islandId, playerId ))
            {
                ModelState.AddModelError( String.Empty, "Vous n'avez pas assez de ressources." );
            }
            else
            {
                TechnologyLevel target = SetupContext.TechnologyLevels.Single( t => t.TechnologyName == model.TargetTechnology && t.Number == 1);
                RessourceManager.RemoveRessource( island.AllRessources, target.Cost );
                Technology technology = new Technology() { Level = target, Name = techManager.TechnologyNameToName( model.TargetTechnology ), TechnologyName = model.TargetTechnology };
                em.AddTechnologyEvent( SetupContext, technology, island );
                //SetupContext.Ressources.Update( island.AllRessources );
                SetupContext.SaveChanges();
            }
            return RedirectToAction( "SeeMyIsland", "Island", new
            {
                islandId = islandId
            } );
        }
    }
}
