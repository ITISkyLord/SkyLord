using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.SeePlayers;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Primitives;

namespace ITI.SkyLord.Controllers
{
    public class PlayerController : Controller
    {
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeePlayers()
        {
            Player playerActif;
            if ( Request.Cookies[ "PlayerId" ].Count != 0 )
            {
                playerActif = PlayerContext.Players.Where( p => p.PlayerId == long.Parse(Request.Cookies[ "PlayerId" ]) ).SingleOrDefault();

                List<Player> players = PlayerContext.Players.ToList();
                players.Remove( playerActif );

                SeePlayers sp = new SeePlayers();

                sp.Players = players;
                sp.ActivePlayer = playerActif;
                return View( sp );
            }
            else
                return RedirectToAction( "Index", "Connection" );

        }

        public IActionResult SeeInformationOfAnPlayer(int id)
        {
            Player choosen = PlayerContext.Players.Include(z => z.Profil).Where(p => p.PlayerId == id).SingleOrDefault();
            List<Player> player = new List<Player>();
            player.Add(choosen);
            SeePlayers sp = new SeePlayers();
            sp.Players = player;

            return View(sp);
        }
    }
}
