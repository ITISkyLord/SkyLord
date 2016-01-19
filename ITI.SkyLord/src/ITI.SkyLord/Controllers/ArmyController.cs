using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.Armies;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITI.SkyLord.ViewModel;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Services;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc.Filters;
using ITI.SkyLord.ViewModel.Partial;

namespace ITI.SkyLord.Controllers
{
    public class ArmyController : GenericController
    {
        public IActionResult Index( long islandId = 0 )
        {
            if( SetupContext.Armies.Where( a => a.Island.IslandId == islandId ).Any() )
            {
            ModelState.AddModelError( string.Empty, "Vous n'avez pas d'armées." );
            return View( CreateArmyViewModel( islandId ) );
        }

        public IActionResult AddUnit( BuildingPartialViewModel model, long islandId = 0 )
        {
            ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
            EventManager em = new EventManager( SetupContext, new EventPackManager( SetupContext ) );

            if( model.UnitAmount <= 0 )
            {
                ModelState.AddModelError( "UnitsToAdd", "Les unités ne peuvent pas être négatives." );
            }
            else
            {
                Unit unit = SetupContext.Units.Include( u => u.UnitCost).Single( u => u.UnitName == model.UnitTarget && u.IsModel );
                Island island = GetIsland( islandId );
                if( unit.UnitCost.Wood * model.UnitAmount > island.AllRessources.Wood || unit.UnitCost.Metal * model.UnitAmount > island.AllRessources.Metal * model.UnitAmount || unit.UnitCost.Cristal * model.UnitAmount > island.AllRessources.Cristal || unit.UnitCost.Magic * model.UnitAmount > island.AllRessources.Magic )
                {
                    ModelState.AddModelError( "UnitsToAdd", "Vous n'avez pas assez de ressources." );

                }
                else
                {
                    RessourceManager.RemoveRessource( island.AllRessources, unit.UnitCost.Wood * model.UnitAmount, unit.UnitCost.Metal * model.UnitAmount, unit.UnitCost.Cristal * model.UnitAmount, unit.UnitCost.Magic * model.UnitAmount );
                    em.AddUnitEvent( SetupContext, unit, model.UnitAmount, island );
                    //SetupContext.Ressources.Update( island.AllRessources );
                    SetupContext.SaveChanges();
                }
            }


            return RedirectToAction( "SeeMyIsland", "Island", new
            {
                islandId = islandId
            } );

        }
        //public IActionResult AddUnit( ArmyViewModel model, long islandId = 0 )
        //{
        //    ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
        //    if( model.UnitsToAdd.Count( kvp => kvp.Value == 0 ) != model.UnitsToAdd.Count() && !model.UnitsToAdd.Any( kvp => kvp.Value < 0 ) )
        //    {
        //        EventManager eventManager = new EventManager( SetupContext, new EventPackManager( SetupContext ));

        //        foreach( KeyValuePair<string, int> kvp in model.UnitsToAdd )
        //        {
        //            if( kvp.Value > 0 )
        //            {
        //                UnitName uN = (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true );
        //                eventManager.AddUnitEvent( SetupContext, SetupContext.Units.Where( u => u.UnitName == uN ).Single(), kvp.Value, GetIsland( islandId ) );
        //            }
        //        }
        //        SetupContext.SaveChanges();
        //    }
        //    else
        //    {
        //        if( model.UnitsToAdd.Any( kvp => kvp.Value < 0 ) )
        //        {
        //            ModelState.AddModelError( "UnitsToAdd", "Les unités ne peuvent pas être négatives." );
        //        }
        //        else
        //        {
        //            ModelState.AddModelError( "UnitsToAdd", "Aucune unité sélectionnée." );
        //        }
        //    }
        //    return RedirectToAction( "Index", new { islandId = islandId } );
        //}

        public IActionResult SetAttackingArmy( SetAttackingArmyViewModel model, long islandId = 0 )
        {
            return View( CreateSetAttackingArmyViewModel( model, islandId ) );
        }


        public IActionResult Fight( SetAttackingArmyViewModel model, long islandId = 0 )
        {
            Army defendingArmyFromAttacker = SetupContext.Armies
                                    .Include( a => a.Island )
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .Where( a => a.Island.IslandId == islandId && a.ArmyState == ArmyState.defense )
                                    .SingleOrDefault();

            if( model.UnitsToSend.Count( kvp => kvp.Value == 0 ) != model.UnitsToSend.Count() && !model.UnitsToSend.Any( kvp => kvp.Value < 0 )
                && !model.UnitsToSend.Any( kvp => kvp.Value >
                defendingArmyFromAttacker.Regiments.Single( r => r.Unit.UnitName == (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true ) ).Number )
                && model.Target != 0
                )
            {
                EventManager eventManager = new EventManager( SetupContext, new EventPackManager( SetupContext ));

                Island island = SetupContext.Islands.Include( i => i.Owner )
                                                    .Include( i => i.Coordinates )
                                                    .Include( i => i.AllRessources )
                                                    .Include( i => i.Armies ).ThenInclude( a => a.Regiments )
                                                    .ThenInclude( r => r.Unit ).ThenInclude( r => r.UnitStatistics )
                                                    .Where( i => i.IslandId == model.Target ).FirstOrDefault();
                ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
                Island attackingIsland = GetIsland( islandId );
                Army attackingArmy = am.CreateArmy( model.UnitsToSend, attackingIsland );
                SetupContext.Armies.Add( attackingArmy );
                SetupContext.SaveChanges();
                eventManager.AddArmyEvent( SetupContext, attackingArmy, attackingIsland, ArmyMovement.attacking, island );
                SetupContext.SaveChanges();

                //Army defendingArmy = island.Armies.Where( a => a.ArmyState == ArmyState.defense ).SingleOrDefault();
                //if ( defendingArmy == null )
                //    defendingArmy = new Army { Island = island, Regiments = new List<Regiment>(), ArmyState = ArmyState.defense };

                //CombatResult combatResult = am.ResolveCombat( attackingArmy, defendingArmy );
                //SetupContext.SaveChanges();

                //SetupContext.FillStandardVM( combatReportViewModel, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

                return View( "Index", CreateArmyViewModel( islandId ) );
            }
            else
            {
                if( model.UnitsToSend.Any( kvp => kvp.Value < 0 ) )
                    ModelState.AddModelError( "UnitsToSend", "Les unités ne peuvent pas être négatives." );
                else if( model.UnitsToSend.Count( kvp => kvp.Value == 0 ) == model.UnitsToSend.Count() )
                    ModelState.AddModelError( "UnitsToSend", "Aucune unité sélectionnée." );
                else if( model.Target == 0 )
                {
                    ModelState.AddModelError( "UnitsToSend", "Aucune cible sélectionnée." );
                }
                else
                    ModelState.AddModelError( "UnitsToSend", "Vous ne pouvez pas envoyer plus d'unités que vous n'en possédez." );

                return View( "SetAttackingArmy", CreateSetAttackingArmyViewModel( model, islandId ) );
            }
        }

        public IActionResult RejoinArmies( long id, long islandId = 0 )
        {
            ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
            Army attackingArmy = am.GetArmy( id );

            if( attackingArmy != null )
            {
                am.JoinArmies( attackingArmy.Island.Armies.SingleOrDefault( a => a.ArmyState == ArmyState.defense ), attackingArmy );
                SetupContext.SaveChanges();
            }
            return RedirectToAction( "Index", new { islandId = islandId } );
        }

        public IActionResult UpgradeUnit( ArmyViewModel model, long islandId )
        {
            BonusTechnology bonusToAdd = new BonusTechnology
            {
                Modifier = 10,
                BonusType = BonusType.army_attack
            };

            return View();
        }

        private Island GetIsland( long islandId )
        {
            if( islandId == 0 )
            {
                long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
                return SetupContext.Islands
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
                return SetupContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IslandId == islandId );
            }

        }

        private SetAttackingArmyViewModel CreateSetAttackingArmyViewModel( SetAttackingArmyViewModel model, long islandId )
        {
            Island currentIsland = GetIsland( islandId );
            model.CurrentDefenseArmy = SetupContext.Armies
                                    .Include( a => a.Island )
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .ThenInclude( r => r.UnitStatistics )
                                    .Where( a => a.Island.IslandId == currentIsland.IslandId && a.ArmyState == ArmyState.defense )
                                    .SingleOrDefault();

            long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
            model.EnnemyIslands = SetupContext.Islands.Include( i => i.Owner ).Include( i => i.Coordinates ).Where( i => i.Owner.PlayerId != activePlayerId && i.Owner != null ).ToList();

            SetupContext.FillStandardVM( model, activePlayerId, islandId );
            return model;
        }

        /// <summary>
        /// Creates an ArmyViewModel containing all available units and the armies contained on the island
        /// </summary>
        /// <param name="islandId">The id of the Island examined</param>
        /// <returns>An ArmyViewModel containing all available units and the armies contained on the island</returns>
        private ArmyViewModel CreateArmyViewModel( long islandId )
        {
            return CreateArmyViewModel( islandId, new ArmyViewModel() );
        }

        private ArmyViewModel CreateArmyViewModel( long islandId, ArmyViewModel model )
        {
            List<Army> currentIslandArmies = SetupContext.Islands.Include( i => i.Armies ).ThenInclude( a => a.Regiments )
                .ThenInclude( r => r.Unit ).ThenInclude( u => u.UnitStatistics )
                .Single( i => i.IslandId == islandId ).Armies.Where( a => a.ArmyState != ArmyState.obsolete ).ToList();
            LevelManager levelManager = new LevelManager( SetupContext );


            model.AvailableUnits = new List<Unit>();
            //model.AvailableUnits = SetupContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).
            //    Where( u => levelManager.IsUnitAvailable( u, islandId) ).ToList();
            foreach( Unit unit in SetupContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).Include( u => u.Requirements ).ToList() )
            {
                if( levelManager.IsUnitAvailable( unit, islandId ) )
                    model.AvailableUnits.Add( unit );
            }
            model.CurrentIslandArmies = currentIslandArmies;

            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return model;
        }

        public abstract class ModelStateTempDataTransfer : ActionFilterAttribute
        {
            protected static readonly string Key = typeof( ModelStateTempDataTransfer ).FullName;
        }

        public class ExportModelStateToTempData : ModelStateTempDataTransfer
        {
            public override void OnActionExecuted( ActionExecutedContext filterContext )
            {
                //Only export when ModelState is not valid
                if( !filterContext.ModelState.IsValid )
                {
                    //Export if we are redirecting
                    if( (filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult) )
                    {
                        ((Controller)filterContext.Controller).TempData[Key] = filterContext.ModelState;
                    }
                }

                base.OnActionExecuted( filterContext );
            }
        }

        public class ImportModelStateFromTempData : ModelStateTempDataTransfer
        {
            public override void OnActionExecuted( ActionExecutedContext filterContext )
            {
                ModelStateDictionary modelState = ((Controller)filterContext.Controller).TempData[ Key ] as ModelStateDictionary;

                if( modelState != null )
                {
                    //Only Import if we are viewing
                    if( filterContext.Result is ViewResult )
                    {
                        filterContext.ModelState.Merge( modelState );
                    }
                    else
                    {
                        //Otherwise remove it.
                        ((Controller)filterContext.Controller).TempData.Remove( Key );
                    }
                }

                base.OnActionExecuted( filterContext );
            }
        }
    }


}
