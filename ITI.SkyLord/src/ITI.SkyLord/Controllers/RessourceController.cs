using ITI.SkyLord.Models.Entity_Framework.Contexts;
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

        public IActionResult AddRessources(int id)
        {
            //SeeRessources sr = new SeeRessources();

            Player owner = PlayerContext.GetPlayer(User.GetUserId());
            using (IslandContext islandContext = new IslandContext())
            {
                int quantRessource = 10;
                Ressource ressource = islandContext.Islands.Include(r => r.AllRessources).Where(i => i.Owner.PlayerId == owner.PlayerId).Select(res => res.AllRessources).SingleOrDefault();
                ressource.ChangeCristal(quantRessource);
                islandContext.SaveChanges();

                SeeRessourcesViewModel sr = new SeeRessourcesViewModel();
                sr.Ressources = ressource;
                return View("Index", sr);
            }        
        }
    }
}
