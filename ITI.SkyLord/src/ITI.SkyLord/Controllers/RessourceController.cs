using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Services;
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
    public class RessourceController : Controller
    {
        [FromServices]
        public IslandContext IslandContext { get; set; }

        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        public IActionResult Index(/*int id*/)
        {
            return View();
        }

        public IActionResult SeeRessources(int id)
        {
            SeeRessourcesViewModel sr = new SeeRessourcesViewModel();

            Island islandChoosen = IslandContext.Islands.Include(p => p.Owner).Include(r => r.AllRessources).Where(i => i.IslandId.Equals(id)).SingleOrDefault();
            Ressource ressources = islandChoosen.AllRessources;
            sr.Ressources = ressources;

            return View(sr);
        }

        public IActionResult SeeAllRessources()
        {
            SeeRessourcesViewModel sr = new SeeRessourcesViewModel();
            Player player = PlayerContext.GetPlayer(User.GetUserId());
            List<Ressource> ressources = IslandContext.Islands.Include(r => r.AllRessources).Where(i => i.Owner.PlayerId == player.PlayerId).Select(i => i.AllRessources).ToList();

            sr.AllRessources = ressources;
            return View(sr);
        }
        /// <summary>
        /// Add ressources in island
        /// </summary>
        /// <param name="name"> The name of ressource</param>
        /// <param name="quantity">Quantity which added</param>
        /// <returns></returns>
        public IActionResult AddRessources( string name, string quantity)
        {
            int x = 0;
            int.TryParse(quantity,out x);
            IslandServices island = new IslandServices(IslandContext, PlayerContext);
            Player owner = PlayerContext.GetPlayer(User.GetUserId());
            SeeRessourcesViewModel sr = new SeeRessourcesViewModel();

            island.AddRessources(name, owner.PlayerId, x);
            return RedirectToAction("SeeAllRessources");
        }
    }
}
