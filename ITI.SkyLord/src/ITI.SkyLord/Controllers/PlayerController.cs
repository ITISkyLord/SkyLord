using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.SeePlayers;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using ITI.SkyLord.ViewModel;

namespace ITI.SkyLord.Controllers
{
    public class PlayerController : GenericController
    {
        public PlayerController( [FromServices]SetupContext setupcontext )
            :base(setupcontext)
        {
        }

        public IActionResult Index(long islandId = 0)
        {
            StandardViewModel svm = new StandardViewModel();
            SetupContext.FillStandardVM(svm, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            return View();
        }

        /// <summary>
        /// See all players in the game SkyLord
        /// </summary>
        /// <returns></returns>
        public IActionResult SeePlayers(long islandId = 0)
        {
            Player player = SetupContext.GetPlayer(User.GetUserId());
            if (player != null)
            {
                List<Player> othersPlayer = SetupContext.Players
                    .Include(p => p.Profil)
                    .Include(i => i.Islands)
                    .Where(pl => pl.PlayerId != player.PlayerId).ToList();

                SeePlayersViewModel sp = new SeePlayersViewModel();
                sp.Players = othersPlayer;
                sp.ActivePlayer = player;

                SetupContext.FillStandardVM(sp, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

                return View(sp);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult SeeInformationOfAnPlayer(int id, long islandId = 0)
        {
            Player playerChoosen = SetupContext.Players
                .Include(a => a.Islands).ThenInclude(i => i.Coordinates)
                .Include(p => p.Profil)
                .Where(p => p.PlayerId == id)
                .SingleOrDefault();

            SeePlayersViewModel sp = new SeePlayersViewModel();
            sp.ActivePlayer = playerChoosen;

            SetupContext.FillStandardVM(sp, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            return View(sp);
        }

        public IActionResult SeeInformationOfAnPlayerString(string namePlayer, long islandId = 0)
        {
            Player playerChoosen = SetupContext.Players
                .Include(a => a.Islands).ThenInclude(i => i.Coordinates)
                .Include(p => p.Profil)
                .Where(p => p.Name == namePlayer)
                .SingleOrDefault();

            SeePlayersViewModel sp = new SeePlayersViewModel();
            sp.ActivePlayer = playerChoosen;

            SetupContext.FillStandardVM(sp, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            return View(sp);
        }



        public IActionResult Players(string name)
        {
            Player player = SetupContext.GetPlayer(User.GetUserId());
            var result = SetupContext.Players.Where(p => p.PlayerId != player.PlayerId).Select(p => p.Name).ToArray();

            if (result.Contains(name)) return Json(result.Where(x => x.StartsWith(name, StringComparison.CurrentCultureIgnoreCase)).ToArray());

            else return RedirectToAction("SeeMyIsland", "Island");
        }
    }
}
