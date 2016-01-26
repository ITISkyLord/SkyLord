﻿using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.SeeIslands;
using ITI.SkyLord.ViewModel.SeeRessources;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITI.SkyLord.Controllers
{
    public class RessourceController : GenericController
    {
        public RessourceController( [FromServices]SetupContext setupcontext )
            :base(setupcontext)
        {
        }

        public IActionResult Index(/*int id*/)
        {
            return View();
        }

        public IActionResult SeeRessources(int islandId)
        {
            SeeRessourcesViewModel sr = new SeeRessourcesViewModel();
            SetupContext.FillStandardVM( sr, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            Island islandChoosen = SetupContext.Islands.Include(p => p.Owner).Include(r => r.AllRessources).Where(i => i.IslandId.Equals(islandId)).SingleOrDefault();
            Ressource ressources = islandChoosen.AllRessources;
            sr.Ressources = ressources;

            return View(sr);
        }

        public IActionResult SeeAllRessources(int islandId)
        {
            SeeRessourcesViewModel sr = new SeeRessourcesViewModel();
            Player player = SetupContext.GetPlayer(User.GetUserId());
            SetupContext.FillStandardVM( sr, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            List<Ressource> ressources = SetupContext.Islands.Include(r => r.AllRessources).Where(i => i.Owner.PlayerId == player.PlayerId).Select(i => i.AllRessources).ToList();

            sr.AllRessources = ressources;
            return View(sr);
        }
        /// <summary>
        /// Add ressources in island
        /// </summary>
        /// <param name="name"> The name of ressource</param>
        /// <param name="quantity">Quantity which added</param>
        /// <returns></returns>
        [Obsolete]
        public IActionResult AddRessources( string name, string quantity, int islandId)
        {
            int x = 0;
            int.TryParse(quantity,out x);
            IslandServices island = new IslandServices(SetupContext);
            Player owner = SetupContext.GetPlayer(User.GetUserId());
            SeeRessourcesViewModel sr = new SeeRessourcesViewModel();

            island.AddRessources(name, owner.PlayerId, x);
            return RedirectToAction("SeeAllRessources", islandId );
        }
    }
}
