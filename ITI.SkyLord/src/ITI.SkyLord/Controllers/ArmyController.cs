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

namespace ITI.SkyLord.Controllers
{
    public class ArmyController : GenericController
    {

        public IActionResult Index( long IslandId = 0 )
        {
            return View( CreateArmyViewModel( IslandId ) );
        }

        public IActionResult AddUnit( ArmyViewModel model, long islandId = 0 )
        {
            ArmyManager am = new ArmyManager( SetupContext );
            if( model.UnitsToAdd.Count( kvp => kvp.Value == 0 ) != model.UnitsToAdd.Count() && !model.UnitsToAdd.Any( kvp => kvp.Value < 0 ) )
            {
                EventManager em = new EventManager( SetupContext, new EventPackManager( SetupContext ));

                foreach( KeyValuePair<string, int> kvp in model.UnitsToAdd )
                {
                    if( kvp.Value > 0 )
                    {
                        UnitName uN = (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true );
                        em.AddUnitEvent( SetupContext, SetupContext.Units.Where( u => u.UnitName == uN ).Single(), kvp.Value, GetIsland( islandId ) );
                        //am.AddUnit
                        //    (
                        //        SetupContext.Units.Where( u => u.UnitName == uN ).Single(),
                        //        kvp.Value,
                        //        GetIsland( islandId )
                        //    );
                    }
                }
                SetupContext.SaveChanges();
            }
            else
            {
                if( model.UnitsToAdd.Any( kvp => kvp.Value < 0) )
                    ModelState.AddModelError( "UnitsToAdd", "Les unités ne peuvent pas être négatives." );
                else
                    ModelState.AddModelError( "UnitsToAdd", "Aucune unité sélectionnée." );
            }

            return View( "Index", CreateArmyViewModel( islandId ) );
        }

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


            if ( model.UnitsToSend.Count( kvp => kvp.Value == 0 ) != model.UnitsToSend.Count() && !model.UnitsToSend.Any( kvp => kvp.Value < 0 )
                && !model.UnitsToSend.Any( kvp => kvp.Value > 
                defendingArmyFromAttacker.Regiments.Single( r => r.Unit.UnitName == (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true ) ).Number )
                && model.Target != 0
                )
            {
                Island island = SetupContext.Islands.Include( i => i.Owner )
                .Include( i => i.AllRessources )
                .Include( i => i.Armies ).ThenInclude( a => a.Regiments )
                .ThenInclude( r => r.Unit ).ThenInclude( r => r.UnitStatistics )
                .Where( i => i.IslandId == model.Target ).FirstOrDefault();
                ArmyManager am = new ArmyManager(SetupContext);
                Island attackingIsland = GetIsland( islandId );
                Army attackingArmy = am.CreateArmy( model.UnitsToSend, attackingIsland );
                Army defendingArmy = island.Armies.Where( a => a.ArmyState == ArmyState.defense ).SingleOrDefault();
                if ( defendingArmy == null )
                    defendingArmy = new Army { Island = island, Regiments = new List<Regiment>(), ArmyState = ArmyState.defense };

                CombatResult combatResult = am.ResolveCombat( attackingArmy, defendingArmy );
                SetupContext.SaveChanges();

                CombatReportViewModel combatReportViewModel = new CombatReportViewModel { CombatResult = combatResult };
                SetupContext.FillStandardVM( combatReportViewModel, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

                return View( "Fight", combatReportViewModel );
            }
            else
            {
                if ( model.UnitsToSend.Any( kvp => kvp.Value < 0 ) )
                    ModelState.AddModelError( "UnitsToSend", "Les unités ne peuvent pas être négatives." );
                else if ( model.UnitsToSend.Count( kvp => kvp.Value == 0) == model.UnitsToSend.Count() )
                    ModelState.AddModelError( "UnitsToSend", "Aucune unité sélectionnée." );
                else if ( model.Target == 0 )
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
            ArmyManager am = new ArmyManager(SetupContext);
            Army attackingArmy = am.GetArmy( id );
            
            if( attackingArmy != null )
            {
                am.JoinArmies( attackingArmy.Island.Armies.SingleOrDefault( a => a.ArmyState == ArmyState.defense ), attackingArmy );
                SetupContext.SaveChanges();
            }
            return RedirectToAction( "Index", new { islandId = islandId } );
        }

        private Island GetIsland( long islandId )
        {
            if ( islandId == 0 )
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
                    .Include( i => i.Armies)
                    .ThenInclude( a => a.Regiments)
                    .ThenInclude( r => r.Unit)
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
            ArmyViewModel avm =  new ArmyViewModel
            {
                AvailableUnits = SetupContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).ToList(),
                CurrentIslandArmies = GetIsland( islandId ).Armies.ToList()
            };
            SetupContext.FillStandardVM( avm, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return avm;
        }
    }


}
