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

        [HttpGet]
        public IActionResult Index( long IslandId )
        {
            return View( CreateArmyViewModel( IslandId ) );
        }

        public IActionResult AddUnit( ArmyViewModel model )
        {
            foreach( KeyValuePair<string, int> kvp in model.UnitsToAdd )
            {
                if( kvp.Value > 0 )
                {
                    UnitName uN = (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true );
                    ArmyContext.AddUnit
                        (
                            ArmyContext.Units.Where( u => u.UnitName == uN ).Single(),
                            kvp.Value,
                            GetCapital()
                        );
                }
            }
            return View( "Index", CreateArmyViewModel() );
        }

        public IActionResult SetAttackingArmy( long islandId )
        {
            Island currentIsland;
            if( islandId == 0 )
            {
                currentIsland = GetCapital();
            }
            else
            {
                currentIsland = GetIsland( islandId );
            }
            SetAttackingArmyViewModel model =  new SetAttackingArmyViewModel
            {
                CurrentDefenseArmy = ArmyContext.Armies
                                    .Include( a => a.Island )
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .ThenInclude(r => r.UnitStatistics)
                                    .Where( a => a.Island.IslandId == currentIsland.IslandId && a.ArmyState == ArmyState.defense )
                                    .FirstOrDefault() //REMETTRE SINGLE ! ON NE PEUT PAS AVOIR PLUSIEURS ARMÉES DE DÉFENSE
                
            };

            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;

            model.EnnemyIslands = ArmyContext.Islands.Include(i => i.Owner).Include( i => i.Coordinates ).Where( i=> i.Owner.PlayerId != activePlayerId && i.Owner != null ).ToList();

            return View( model );
        }
        public IActionResult Fight( SetAttackingArmyViewModel model )
        {
            Island island = ArmyContext.Islands.Include(i=> i.Owner).Include( i => i.AllRessources).Include(i => i.Armies).ThenInclude( a => a.Regiments).Where(i => i.IslandId == model.Target).FirstOrDefault();
            ArmyManager am = new ArmyManager( ArmyContext );
            Island attackingIsland = GetCapital();
            Army attackingArmy = am.CreateArmy( model.UnitsToSend, attackingIsland );
            Army defendingArmy = island.Armies.Where(a => a.ArmyState == ArmyState.defense).SingleOrDefault();
            if( defendingArmy == null )
                defendingArmy = new Army { Island = island, Regiments = new List<Regiment>(), ArmyState = ArmyState.defense };

            CombatResult cr = am.ResolveCombat( attackingArmy, defendingArmy );
            am.JoinArmies( attackingIsland.Armies.SingleOrDefault( a => a.ArmyState == ArmyState.defense ), attackingArmy );

            return View( "Fight", new CombatReportViewModel { CombatResult = cr } );
        }
        private Island GetCapital()
        {
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            return ArmyContext.Islands.Include( i => i.AllRessources).Include( i => i.Owner).Include(i => i.Coordinates).SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId );
        }
        private Island GetIsland( long islandId )
        {
            return ArmyContext.Islands.SingleOrDefault( i => i.IslandId == islandId );
        }

        /// <summary>
        /// Creates an ArmyViewModel containing all available units and the armies contained on the active player's capital island
        /// </summary>
        /// <returns>An ArmyViewModel containing all available units and the armies contained on the active player's capital island </returns>
        private ArmyViewModel CreateArmyViewModel()
        {
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            long capitalId = ArmyContext.Islands.SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId ).IslandId;

            return new ArmyViewModel
            {
                AvailableUnits = ArmyContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).ToList(),
                CurrentIslandArmies = ArmyContext.Armies.Include( a => a.Regiments ).ThenInclude( r => r.Unit ).Where( a => a.Island.IslandId == capitalId ).ToList()
            };
        }

        /// <summary>
        /// Creates an ArmyViewModel containing all available units and the armies contained on the island
        /// </summary>
        /// <param name="islandId">The id of the Island examined</param>
        /// <returns>An ArmyViewModel containing all available units and the armies contained on the island</returns>
        private ArmyViewModel CreateArmyViewModel( long islandId )
        {
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            long capitalId = ArmyContext.Islands.SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId ).IslandId;

            List<Army> currentIslandArmies = null;
            if( islandId == 0 )
                currentIslandArmies = ArmyContext.Armies.Include( a => a.Regiments ).ThenInclude( r => r.Unit ).Where( a => a.Island.IslandId == capitalId ).ToList();
            else
                currentIslandArmies = ArmyContext.Armies.Include( a => a.Regiments ).ThenInclude( r => r.Unit ).Where( a => a.Island.IslandId == islandId ).ToList();
            return new ArmyViewModel
            {
                AvailableUnits = ArmyContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).ToList(),
                CurrentIslandArmies = currentIslandArmies
            };
        }
    }


}
