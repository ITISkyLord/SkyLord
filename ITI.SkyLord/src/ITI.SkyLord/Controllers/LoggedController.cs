using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.Armies;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.SkyLord.Controllers
{
    public class LoggedController : Controller
    {
        [FromServices]
        public IslandContext IslandContext { get; set; }
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ArmyDetails()
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
