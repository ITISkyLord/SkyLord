using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.ViewModel.SeeIslands;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using System.Security.Claims;
using ITI.SkyLord.ViewModel;

namespace ITI.SkyLord.Controllers
{
    public class IslandController : Controller
    {
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        [FromServices]
        public IslandContext IslandContext { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// See the island(s) of the current player
        /// </summary>
        /// <param name="id">Mail of player</param>
        /// <returns></returns>
        public IActionResult SeeMyIsland( long islandId = 0 )
        {
            Island currentIsland = GetIsland( islandId );
            SeeIslandsViewModel islandViewModel = new SeeIslandsViewModel
            {
                CurrentIsland = currentIsland
            };
            IslandContext.FillStandardVM( islandViewModel, PlayerContext.GetPlayer( User.GetUserId() ).PlayerId, currentIsland.IslandId );
            return View( islandViewModel );
        }

        private Island GetIsland( long islandId )
        {
            if ( islandId == 0 )
            {
                long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
                return IslandContext.Islands
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
                long activePlayerId = PlayerContext.GetPlayer( User.GetUserId() ).PlayerId;
                return IslandContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IslandId == islandId && i.Owner.PlayerId == activePlayerId );
            }
        }
    }
}