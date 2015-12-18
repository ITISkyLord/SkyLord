using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.Armies;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITI.SkyLord.Services;

namespace ITI.SkyLord.Controllers
{
    public class ArmyController : Controller
    {
        [FromServices]
        public IslandContext IslandContext { get; set; }
        [FromServices]
        public PlayerContext PlayerContext { get; set; }
        [FromServices]
        public ArmyContext ArmyContext { get; set; }

        public IActionResult Index( long IslandId = 0 )
        {
            return View( CreateArmyViewModel( IslandId ) );
        }

        public IActionResult AddUnit( ArmyViewModel model, long islandId = 0 )
        {
            ArmyManager am = new ArmyManager( ArmyContext );
            if( model.UnitsToAdd.Count( kvp => kvp.Value == 0 || kvp.Value < 0 ) != model.UnitsToAdd.Count() )
            {
                foreach ( KeyValuePair<string, int> kvp in model.UnitsToAdd )
                {
                    if ( kvp.Value > 0 )
                    {
                        UnitName uN = (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true );
                        am.AddUnit
                            (
                                ArmyContext.Units.Where( u => u.UnitName == uN ).Single(),
                                kvp.Value,
                                GetIsland( islandId )
                            );
                    }
                }
                ArmyContext.SaveChanges();
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
            return View( CreateSetAttackingArmyViewModel( model ) );
        }

        public IActionResult Fight( SetAttackingArmyViewModel model, long islandId = 0 )
        {
            if ( model.UnitsToSend.Count( kvp => kvp.Value == 0 || kvp.Value < 0 ) != model.UnitsToSend.Count() )
            {
                Island island = ArmyContext.Islands.Include( i => i.Owner )
                .Include( i => i.AllRessources )
                .Include( i => i.Armies ).ThenInclude( a => a.Regiments )
                .ThenInclude( r => r.Unit ).ThenInclude( r => r.UnitStatistics )
                .Where( i => i.IslandId == model.Target ).FirstOrDefault();
                ArmyManager am = new ArmyManager( ArmyContext );
                Island attackingIsland = GetIsland( islandId );
                Army attackingArmy = am.CreateArmy( model.UnitsToSend, attackingIsland );
                Army defendingArmy = island.Armies.Where( a => a.ArmyState == ArmyState.defense ).SingleOrDefault();
                if ( defendingArmy == null )
                    defendingArmy = new Army { Island = island, Regiments = new List<Regiment>(), ArmyState = ArmyState.defense };

                CombatResult combatResult = am.ResolveCombat( attackingArmy, defendingArmy );
                ArmyContext.SaveChanges();

                CombatReportViewModel combatReport = new CombatReportViewModel { CombatResult = combatResult };
                return View( "Fight", combatReport );
            }
            else
            {
                if ( model.UnitsToSend.Any( kvp => kvp.Value < 0 ) )
                    ModelState.AddModelError( "UnitsToSend", "Les unités ne peuvent pas être négatives." );
                else
                    ModelState.AddModelError( "UnitsToSend", "Aucune unité sélectionnée." );

                return View( "SetAttackingArmy", CreateSetAttackingArmyViewModel( model ) );
            }

        }

        public IActionResult RejoinArmies( long id, long islandId = 0 )
        {
            ArmyManager am = new ArmyManager( ArmyContext );
            Army attackingArmy = am.GetArmy( id );
            
            if( attackingArmy != null )
            {
                am.JoinArmies( attackingArmy.Island.Armies.SingleOrDefault( a => a.ArmyState == ArmyState.defense ), attackingArmy );
                ArmyContext.SaveChanges();
            }

            return RedirectToAction( "Index", new { islandId = islandId } );
        }

        private Island GetIsland( long islandId )
        {
            if ( islandId == 0 )
            {
                long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
                return ArmyContext.Islands
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
                return ArmyContext.Islands
                    .Include( i => i.Armies)
                    .ThenInclude( a => a.Regiments)
                    .ThenInclude( r => r.Unit)
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IslandId == islandId );
            }

        }

        private SetAttackingArmyViewModel CreateSetAttackingArmyViewModel( SetAttackingArmyViewModel model, long islandId = 0 )
        {
            Island currentIsland = GetIsland( islandId );
            model.CurrentDefenseArmy = ArmyContext.Armies
                                    .Include( a => a.Island )
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .ThenInclude( r => r.UnitStatistics )
                                    .Where( a => a.Island.IslandId == currentIsland.IslandId && a.ArmyState == ArmyState.defense )
                                    .SingleOrDefault();

            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            model.EnnemyIslands = ArmyContext.Islands.Include( i => i.Owner ).Include( i => i.Coordinates ).Where( i => i.Owner.PlayerId != activePlayerId && i.Owner != null ).ToList();
            return model;
        }

        /// <summary>
        /// Creates an ArmyViewModel containing all available units and the armies contained on the island
        /// </summary>
        /// <param name="islandId">The id of the Island examined</param>
        /// <returns>An ArmyViewModel containing all available units and the armies contained on the island</returns>
        private ArmyViewModel CreateArmyViewModel( long islandId )
        {
            //long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            //long capitalId = ArmyContext.Islands.SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId ).IslandId;

            //List<Army> currentIslandArmies = null;
            //if( islandId == 0 )
            //    currentIslandArmies = ArmyContext.Armies.Include( a => a.Regiments ).ThenInclude( r => r.Unit ).Where( a => a.Island.IslandId == capitalId ).ToList();
            //else
            //    currentIslandArmies = ArmyContext.Armies.Include( a => a.Regiments ).ThenInclude( r => r.Unit ).Where( a => a.Island.IslandId == islandId ).ToList();

            return new ArmyViewModel
            {
                AvailableUnits = ArmyContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).ToList(),
                CurrentIslandArmies = GetIsland( islandId ).Armies.ToList()
            };
        }
    }


}
