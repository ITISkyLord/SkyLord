using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.SeePlayers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using ITI.SkyLord.ViewModel;

namespace ITI.SkyLord.Controllers
{
    public class PlayerController : Controller
    {
        [FromServices]
        public SetupContext PlayerContext { get; set; }

        public IActionResult Index( long islandId = 0 )
        {
            StandardViewModel svm = new StandardViewModel();
            PlayerContext.FillStandardVM( svm, PlayerContext.GetPlayer( ClaimTypes.NameIdentifier ).PlayerId, islandId );

            return View();
        }

        /// <summary>
        /// See all players in the game SkyLord
        /// </summary>
        /// <returns></returns>
        public IActionResult SeePlayers( long islandId = 0 )
        {
            Player player = PlayerContext.GetPlayer(ClaimTypes.NameIdentifier);
            if( player != null )
            {
                // Pourquoi ne pas inclure le joueur courant ? On ne se voit pas dans la liste des joueurs !!! wtf
                List<Player> othersPlayer = PlayerContext.Players
                    .Include(p => p.Profil)
                    .Include(i => i.Islands)
                    .ToList();

                SeePlayersViewModel sp = new SeePlayersViewModel();
                sp.Players = othersPlayer;
                sp.ActivePlayer = player;

                PlayerContext.FillStandardVM( sp, PlayerContext.GetPlayer( ClaimTypes.NameIdentifier ).PlayerId, islandId );

                return View( sp );
            }
            else
            {
                return RedirectToAction( "Index" );
            }
        }

        public IActionResult SeeInformationOfAnPlayer( int id, long islandId = 0 )
        {
            Player playerChoosen = PlayerContext.Players
                .Include(a => a.Islands).ThenInclude(i => i.Coordinates)
                .Include(p => p.Profil)
                .Where(p => p.PlayerId == id)
                .SingleOrDefault();

            SeePlayersViewModel sp = new SeePlayersViewModel();
            sp.ActivePlayer = playerChoosen;

            PlayerContext.FillStandardVM( sp, PlayerContext.GetPlayer( ClaimTypes.NameIdentifier ).PlayerId, islandId );

            return View( sp );
        }

        public IActionResult SeeInformationOfAnPlayerString( string namePlayer, long islandId = 0 )
        {
            Player playerChoosen = PlayerContext.Players
                .Include(a => a.Islands).ThenInclude(i => i.Coordinates)
                .Include(p => p.Profil)
                .Where(p => p.Name == namePlayer)
                .SingleOrDefault();

            SeePlayersViewModel sp = new SeePlayersViewModel();
            sp.ActivePlayer = playerChoosen;

            PlayerContext.FillStandardVM( sp, PlayerContext.GetPlayer( ClaimTypes.NameIdentifier ).PlayerId, islandId );

            return View( sp );
        }



        public IActionResult Players( string name )
        {
            Player player = PlayerContext.GetPlayer(ClaimTypes.NameIdentifier);
            var result = PlayerContext.Players.Where(p => p.PlayerId != player.PlayerId).Select(p => p.Name).ToArray();

            if( result.Contains( name ) ) return Json( result.Where( x => x.StartsWith( name, StringComparison.CurrentCultureIgnoreCase ) ).ToArray() );

            else return RedirectToAction( "SeeMyIsland", "Island" );
        }
    }
}
