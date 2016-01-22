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
        public SetupContext SetupContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index(long islandId = 0)
        {
            //Récupérer la description dans la BDD

            Player p = SetupContext.GetPlayer(User.GetUserId());
            p = SetupContext.Players.Include(z => z.Profil).Where(x => x.PlayerId == p.PlayerId).First();
            ViewData["name"] = p.Name;

            ProfilViewModel profilViewModel = new ProfilViewModel();
            if (!String.IsNullOrEmpty(p.Profil.Description))
                profilViewModel.Description = p.Profil.Description;
            else
                profilViewModel.Description = "Aucune description";

            SetupContext.FillStandardVM(profilViewModel, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View(profilViewModel);
        }

        public IActionResult ProfilOfOtherPlayer(long islandId, long EnnemyIslandId)
        {
            //Récupérer la description dans la BDD
            Player currentPlayer = SetupContext.GetPlayer(User.GetUserId());
            Island ennemi = SetupContext.Islands.Include(i => i.Owner).Where(i => i.IslandId == EnnemyIslandId).FirstOrDefault();

            ProfilViewModel profilViewModel = new ProfilViewModel();
            // Si le joueur existe => On affiche le profil
            if(ennemi != null)
            {
                Player p = ennemi.Owner;
                ViewData["name"] = p.Name;
                ViewData["mail"] = p.Mail;
                ViewData["id"] = p.PlayerId;
                ViewData["currentPlayerId"] = currentPlayer.PlayerId;

                if (!String.IsNullOrEmpty(p.Profil.Description))
                    profilViewModel.Description = p.Profil.Description;
                else
                    profilViewModel.Description = "Aucune description";
            }
            // Sinon on dit que c'est un barbare et puis voilà :-)
            else
            {
                ViewData["name"] = "Barbare";
                ViewData["mail"] = "barbare@skylord.fr";
                ViewData["id"] = 0;
                ViewData["currentPlayerId"] = 0;
                profilViewModel.Description = "Cette île est inconnu";
            }

            SetupContext.FillStandardVM(profilViewModel, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View(profilViewModel);
        }

        public IActionResult ChangeProfil(long islandId = 0)
        {
            Player p = SetupContext.GetPlayer(User.GetUserId());
            p = SetupContext.Players.Include(z => z.Profil).Where(x => x.PlayerId == p.PlayerId).First();
            ViewData["name"] = p.Name;

            ProfilViewModel profilViewModel = new ProfilViewModel();

            profilViewModel.Description = (!String.IsNullOrEmpty(p.Profil.Description)) ? p.Profil.Description : "Aucune description";
            profilViewModel.Mail = p.Mail;

            SetupContext.FillStandardVM(profilViewModel, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View(profilViewModel);
        }

        [HttpPost]
        public IActionResult changeDescription(string description, long islandId = 0)
        {
            // Ajouter la description dans la BDD

            Player p = SetupContext.GetPlayer(User.GetUserId());
            p = SetupContext.Players.Include(z => z.Profil).Where(x => x.PlayerId == p.PlayerId).First();
            Profil oldProfil = p.Profil;
            oldProfil.Description = description;
            SetupContext.SaveChanges();

            ViewData["name"] = p.Name;
            ProfilViewModel profilViewModel = new ProfilViewModel();
            profilViewModel.Description = (!String.IsNullOrEmpty(p.Profil.Description)) ? p.Profil.Description : "Aucune description";

            SetupContext.FillStandardVM(profilViewModel, SetupContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View("Index", profilViewModel);
        }
    }
}
