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
        public IActionResult Index( long islandId = 0 )
        {
            //Récupérer la description dans la BDD

            Player p = SetupContext.GetPlayer(User.GetUserId());
            p = SetupContext.Players.Include( z => z.Profil ).Where( x => x.PlayerId == p.PlayerId ).First();
            ViewData["name"] = p.Name;

            ProfilViewModel profilViewModel = new ProfilViewModel();
            if( !String.IsNullOrEmpty( p.Profil.Description ) )
                profilViewModel.Description = p.Profil.Description;
            else
                profilViewModel.Description = "Aucune description";

            SetupContext.FillStandardVM( profilViewModel, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( profilViewModel );
        }

        public IActionResult ProfilOfOtherPlayer( long islandId, long EnnemyIslandId )
        {
            //Récupérer la description dans la BDD
            Player currentPlayer = SetupContext.GetPlayer(User.GetUserId());
            Island ennemyIsland = SetupContext.Islands.Include(i => i.Owner).ThenInclude( pl => pl.Profil ).Where(i => i.IslandId == EnnemyIslandId).FirstOrDefault();

            ProfilViewModel model = new ProfilViewModel();
            // Si le joueur existe => On affiche le profil
            if( ennemyIsland != null )
            {
                Player p = ennemyIsland.Owner;
                model.Name = p.Name;
                model.Mail = p.Mail;
                model.PlayerId = p.PlayerId;
                model.CurrentPlayerId = currentPlayer.PlayerId;
                //ViewData["name"] = p.Name;
                //ViewData["mail"] = p.Mail;
                //ViewData["id"] = p.PlayerId;
                //ViewData["currentPlayerId"] = currentPlayer.PlayerId;

                if( !String.IsNullOrEmpty( p.Profil.Description ) )
                    model.Description = p.Profil.Description;
                else
                    model.Description = "Aucune description.";
            }
            // Sinon on dit que c'est un barbare et puis voilà :-) (Loïc)
            else
            {
                model.Name = "Barbare";
                model.Mail = "barbare@skylord.fr";
                model.PlayerId = 0;
                model.CurrentPlayerId = 0;
                //ViewData["name"] = "Barbare";
                //ViewData["mail"] = "barbare@skylord.fr";
                //ViewData["id"] = 0;
                //ViewData["currentPlayerId"] = 0;
                model.Description = "Cette île est inconnue";
            }

            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( model );
        }
        public IActionResult ProfilOfAPlayer( long islandId, long playerId )
        {
            //Récupérer la description dans la BDD
            Player currentPlayer = SetupContext.GetPlayer(User.GetUserId());
            Player p = SetupContext.Players.Single( pe=> pe.PlayerId == playerId);
            ProfilViewModel model = new ProfilViewModel();
            // Si le joueur existe => On affiche le profil
            model.Name = p.Name;
            model.Mail = p.Mail;
            model.PlayerId = p.PlayerId;
            model.CurrentPlayerId = currentPlayer.PlayerId;
            //ViewData["name"] = p.Name;
            //ViewData["mail"] = p.Mail;
            //ViewData["id"] = p.PlayerId;
            //ViewData["currentPlayerId"] = currentPlayer.PlayerId;

            if( !String.IsNullOrEmpty( p.Profil.Description ) )
                model.Description = p.Profil.Description;
            else
                model.Description = "Aucune description.";


            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( model );
        }

        public IActionResult ChangeProfil( long islandId = 0 )
        {
            Player p = SetupContext.GetPlayer(User.GetUserId());
            p = SetupContext.Players.Include( z => z.Profil ).Where( x => x.PlayerId == p.PlayerId ).First();
            ViewData["name"] = p.Name;

            ProfilViewModel model = new ProfilViewModel();
            model.Name = p.Name;
            model.Description = (!String.IsNullOrEmpty( p.Profil.Description )) ? p.Profil.Description : "Aucune description.";
            model.Mail = p.Mail;

            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( model );
        }

        [HttpPost]
        public IActionResult changeDescription( string description, long islandId = 0 )
        {
            // Ajouter la description dans la BDD

            Player p = SetupContext.GetPlayer(User.GetUserId());
            p = SetupContext.Players.Include( z => z.Profil ).Where( x => x.PlayerId == p.PlayerId ).First();
            Profil oldProfil = p.Profil;
            oldProfil.Description = description;
            SetupContext.SaveChanges();

            ViewData["name"] = p.Name;
            ProfilViewModel profilViewModel = new ProfilViewModel();
            profilViewModel.Description = (!String.IsNullOrEmpty( p.Profil.Description )) ? p.Profil.Description : "Aucune description";

            SetupContext.FillStandardVM( profilViewModel, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( "Index", profilViewModel );
        }
    }
}
