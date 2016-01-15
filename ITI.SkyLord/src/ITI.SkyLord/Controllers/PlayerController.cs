﻿using ITI.SkyLord.Models.Entity_Framework.Contexts;
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
    public class PlayerController : Controller
    {
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        public IActionResult Index(long islandId = 0)
        {
            StandardViewModel svm = new StandardViewModel();
            PlayerContext.FillStandardVM(svm, PlayerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            return View();
        }

        /// <summary>
        /// See all players in the game SkyLord
        /// </summary>
        /// <returns></returns>
        public IActionResult SeePlayers(long islandId = 0)
        {
            Player player = PlayerContext.GetPlayer(User.GetUserId());
            if (player != null)
            {
                List<Player> othersPlayer = PlayerContext.Players
                    .Include(p => p.Profil)
                    .Include(i => i.Islands)
                    .Where(pl => pl.PlayerId != player.PlayerId).ToList();

                SeePlayersViewModel sp = new SeePlayersViewModel();
                sp.Players = othersPlayer;
                sp.ActivePlayer = player;

                PlayerContext.FillStandardVM(sp, PlayerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

                return View(sp);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult SeeInformationOfAnPlayer(int id, long islandId = 0)
        {
            Player playerChoosen = PlayerContext.Players
                .Include(a => a.Islands).ThenInclude(i => i.Coordinates)
                .Include(p => p.Profil)
                .Where(p => p.PlayerId == id)
                .SingleOrDefault();

            SeePlayersViewModel sp = new SeePlayersViewModel();
            sp.ActivePlayer = playerChoosen;

            PlayerContext.FillStandardVM(sp, PlayerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            return View(sp);
        }

        public IActionResult SeeInformationOfAnPlayerString(string namePlayer, long islandId = 0)
        {
            Player playerChoosen = PlayerContext.Players
                .Include(a => a.Islands).ThenInclude(i => i.Coordinates)
                .Include(p => p.Profil)
                .Where(p => p.Name == namePlayer)
                .SingleOrDefault();

            SeePlayersViewModel sp = new SeePlayersViewModel();
            sp.ActivePlayer = playerChoosen;

            PlayerContext.FillStandardVM(sp, PlayerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);

            return View(sp);
        }



        public IActionResult Players(string name)
        {
            Player player = PlayerContext.GetPlayer(User.GetUserId());
            var result = PlayerContext.Players.Where(p => p.PlayerId != player.PlayerId).Select(p => p.Name).ToArray();

            if (result.Contains(name)) return Json(result.Where(x => x.StartsWith(name, StringComparison.CurrentCultureIgnoreCase)).ToArray());

            else return RedirectToAction("SeeMyIsland", "Island");
        }
    }
}
