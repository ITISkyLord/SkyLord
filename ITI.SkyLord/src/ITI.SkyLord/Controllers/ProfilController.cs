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
        // GET: /<controller>/
        public IActionResult Index()
        {
            //Récupérer la description dans la BDD
            using( PlayerContext context = new PlayerContext() )
            {
                
                // Bidouille: Include( p => p.Profil ) dans PlayerContext fails...
                Player p = context.GetPlayer(User.GetUserId());
                p = context.Players.Include(z => z.Profil).Where( x => x.PlayerId == p.PlayerId ).First();
                ViewData["name"] = p.Name;

                ProfilViewModel profilViewModel = new ProfilViewModel();
                if( !String.IsNullOrEmpty( p.Profil.Description ) ) profilViewModel.Description = p.Profil.Description;
                else profilViewModel.Description = "Aucune description";
                return View(profilViewModel);


            }
        }
        [HttpPost]
        public IActionResult changeDescription( ProfilViewModel model )
        {

            // Ajouter la description dans la BDD
            using( PlayerContext context = new PlayerContext() )
            {
                Player p = context.GetPlayer(User.GetUserId());
                p = context.Players.Include( z => z.Profil ).Where( x => x.PlayerId == p.PlayerId ).First();
                Profil oldProfil = p.Profil;
                oldProfil.Description = model.Description;
                context.SaveChanges();

                ViewData["name"] = p.Name;
                ProfilViewModel profilViewModel = new ProfilViewModel();
                if( !String.IsNullOrEmpty( p.Profil.Description ) ) profilViewModel.Description = p.Profil.Description;
                else profilViewModel.Description = "Aucune description";
                return View("Index", profilViewModel );


            }
     //       return RedirectToAction( nameof( HomeController.Index ), "Home" );
        }
    }
}
