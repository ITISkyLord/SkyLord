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
            if ( User.IsSignedIn() )
            {
                // On executing any controller check if the query holds the islandId
                if ( Request.Query.ContainsKey( "islandId" ) )
                {
                    // If islandId is present, check it with ValidateIsland method
                    long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
                    SetupContext.ValidateIsland( long.Parse( Request.Query[ "islandId" ] ), activePlayerId );
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

                em.AddTechnologyEvent( SetupContext, model.TargetTechnology, technologyLevel.Duration, island, technologyLevel );
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
            LevelManager levelManager = new LevelManager( SetupContext );
            TechnologyManager techManager = new TechnologyManager( SetupContext, levelManager, bonusManager );

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

                TechnologyLevel nextLevel = (TechnologyLevel)levelManager.FindNextLevel( technologyLevel );
                if ( nextLevel == null )
                {
                    throw new InvalidOperationException( " The next level does not exist !" );
                }

                em.AddTechnologyEvent( SetupContext, model.TargetTechnology, technologyLevel.Duration, island, nextLevel );

                SetupContext.SaveChanges();
            }
            return RedirectToAction( "SeeMyIsland", "Island", new
            {
                islandId = islandId
            } );
        }
    }
}
