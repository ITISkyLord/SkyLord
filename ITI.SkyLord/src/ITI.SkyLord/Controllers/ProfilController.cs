using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using ITI.SkyLord.ViewModels.Profil;
using System.Security.Claims;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.SkyLord.Controllers
{
    public class ProfilController : Controller
    {
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index(long islandId = 0)
        {
            //Récupérer la description dans la BDD

            Player p = PlayerContext.GetPlayer(User.GetUserId());
            p = PlayerContext.Players.Include(z => z.Profil).Where(x => x.PlayerId == p.PlayerId).First();
            ViewData["name"] = p.Name;

            ProfilViewModel profilViewModel = new ProfilViewModel();
            if (!String.IsNullOrEmpty(p.Profil.Description))
                profilViewModel.Description = p.Profil.Description;
            else
                profilViewModel.Description = "Aucune description";

            PlayerContext.FillStandardVM(profilViewModel, PlayerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View(profilViewModel);
        }

        public IActionResult ChangeProfil(long islandId = 0)
        {
            Player p = PlayerContext.GetPlayer(User.GetUserId());
            p = PlayerContext.Players.Include(z => z.Profil).Where(x => x.PlayerId == p.PlayerId).First();
            ViewData["name"] = p.Name;

            ProfilViewModel profilViewModel = new ProfilViewModel();

            profilViewModel.Description = (!String.IsNullOrEmpty(p.Profil.Description)) ? p.Profil.Description : "Aucune description";
            profilViewModel.Mail = p.Mail;

            PlayerContext.FillStandardVM(profilViewModel, PlayerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View(profilViewModel);
        }

        [HttpPost]
        public IActionResult changeDescription(string description, long islandId = 0)
        {
            // Ajouter la description dans la BDD

            Player p = PlayerContext.GetPlayer(User.GetUserId());
            p = PlayerContext.Players.Include(z => z.Profil).Where(x => x.PlayerId == p.PlayerId).First();
            Profil oldProfil = p.Profil;
            oldProfil.Description = description;
            PlayerContext.SaveChanges();

            ViewData["name"] = p.Name;
            ProfilViewModel profilViewModel = new ProfilViewModel();
            profilViewModel.Description = (!String.IsNullOrEmpty(p.Profil.Description)) ? p.Profil.Description : "Aucune description";

            PlayerContext.FillStandardVM(profilViewModel, PlayerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View("Index", profilViewModel);
        }
    }
}
