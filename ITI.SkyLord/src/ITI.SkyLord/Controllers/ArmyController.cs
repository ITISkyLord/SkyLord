using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.Armies;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Managers;

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
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .ThenInclude(r => r.UnitStatistics)
                                    .Where( a => a.Island.IslandId == currentIsland.IslandId && a.ArmyState == ArmyState.defense )
                                    .SingleOrDefault()
                
            };
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;

            model.EnnemyIslands = ArmyContext.Islands.Include(i => i.Owner).Include( i => i.Coordinates ).Where( i=> i.Owner.PlayerId != activePlayerId && i.Owner != null ).ToList();

            return View( model );
        }
        public IActionResult Fight( SetAttackingArmyViewModel model)
        {
            ArmyManager am = new ArmyManager( ArmyContext );
            Army attackingArmy = am.CreateArmy( model.UnitsToSend);
            Army defendingArmy = model.Target.Armies.Where(a => a.ArmyState == ArmyState.defense).SingleOrDefault();
            am.ResolveCombat();
        }
        private Island GetCapital()
        {
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            return IslandContext.Islands.SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId );
        }
        private Island GetIsland( long islandId )
        {
            return IslandContext.Islands.SingleOrDefault( i => i.IslandId == islandId );
        }

        /// <summary>
        /// Creates an ArmyViewModel containing all available units and the armies contained on the active player's capital island
        /// </summary>
        /// <returns>An ArmyViewModel containing all available units and the armies contained on the active player's capital island </returns>
        private ArmyViewModel CreateArmyViewModel()
        {
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            long capitalId = IslandContext.Islands.SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId ).IslandId;

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
            long capitalId = IslandContext.Islands.SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId ).IslandId;

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
