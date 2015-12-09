using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.Armies;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public IActionResult Index()
        {
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            long capitalId = IslandContext.Islands.SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId ).IslandId;
            List<Army> capitalArmies = ArmyContext.Armies.Include( a => a.Regiments).ThenInclude( r => r.Unit ).Where( a => a.Island.IslandId == capitalId ).ToList();

            return View( new SeeArmyViewModel { CurrentIslandArmies = capitalArmies } );
        }

        [HttpGet]
        public IActionResult Index( long IslandId )
        {
            long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
            List<Army> activeIslandArmies = ArmyContext.Armies.Include( a => a.Regiments ).ThenInclude( r => r.Unit )
                .Where( a => a.Island.IslandId == IslandId ).ToList();

            return View( new SeeArmyViewModel { CurrentIslandArmies = activeIslandArmies } );
        }

        public IActionResult ArmyDetails()
        {
            Player activePlayer = PlayerContext.Players.Where( p => p.PlayerId == long.Parse( Request.Cookies[ "PlayerId" ] ) ).SingleOrDefault();
            Army currentArmy = IslandContext.Islands.FirstOrDefault().Armies.Where( a => a.ArmyState == ArmyState.defense ).FirstOrDefault();

            SeeArmyViewModel avm = new SeeArmyViewModel { CurrentArmy = currentArmy };
            return View( avm );
        }

        public IActionResult ArmyAddUnit()
        {
            if ( Request.Cookies[ "PlayerId" ].Count != 0 )
            {
                Player playerActif = PlayerContext.Players.Where( p => p.PlayerId == long.Parse( Request.Cookies[ "PlayerId" ] ) ).SingleOrDefault();
                Army currentArmy = IslandContext.Islands.FirstOrDefault().Armies.Where( a => a.ArmyState == ArmyState.defense ).FirstOrDefault();

                SeeArmyViewModel avm = new SeeArmyViewModel { CurrentArmy = currentArmy };
                return View( avm );
            }
            else
                return RedirectToAction( "Index", "Connection" );
        }

    }
}
