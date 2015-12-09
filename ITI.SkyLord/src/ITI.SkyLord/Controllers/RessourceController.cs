using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.SeeRessources;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Controllers
{
    public class RessourceController : Controller
    {
        [FromServices]
        public IslandContext IslandContext { get; set; }

        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        public IActionResult Index(int id)
        {
            SeeRessources sr = new SeeRessources();

            Island islandChossen = IslandContext.Islands.Include(p => p.Owner).Where(i => i.IslandId.Equals(id)).SingleOrDefault();
            List<Ressource> ressources = IslandContext.Ressources.ToList();
            sr.Ressources = ressources;

            return View(sr);
        }

        public IActionResult SeeRessources(int id)
        {
            SeeRessources sr = new SeeRessources();

            Island islandChossen = IslandContext.Islands.Include(p => p.Owner).Where(i => i.IslandId.Equals(id)).SingleOrDefault();
            List<Ressource> ressources = IslandContext.Ressources.ToList();
            sr.Ressources = ressources;

            return View(sr);
        }

        public IActionResult AddRessources()
        {

            return View();
        }
    }
}
