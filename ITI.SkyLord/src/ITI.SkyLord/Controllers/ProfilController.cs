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

            Player p = SetupContext.GetPlayer( User.GetUserId() );
            p = SetupContext.Players.Include( z => z.Profil ).Where( x => x.PlayerId == p.PlayerId ).First();
            ViewData[ "name" ] = p.Name;

            ProfilViewModel profilViewModel = new ProfilViewModel();
            if ( !String.IsNullOrEmpty( p.Profil.Description ) )
                profilViewModel.Description = p.Profil.Description;
            else
                profilViewModel.Description = "Aucune description";

            SetupContext.FillStandardVM( profilViewModel, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( profilViewModel );
        }

        public IActionResult ProfilOfOtherPlayer( ProfilViewModel model, long islandId, long otherPlayerId )
        {
            Player otherPlayer = null;

            if ( otherPlayerId == 0 )
            {
                otherPlayer = SetupContext.Islands.Include( i => i.Owner ).ThenInclude( p => p.Profil ).Include( i => i.Coordinates )
                    .First( i => i.IslandId == model.EnnemyIslandId ).Owner;
            }
            else
            {
                otherPlayer = SetupContext.Players.Include( pl => pl.Profil ).Include( pl => pl.Islands ).ThenInclude( i => i.Coordinates )
                .FirstOrDefault( pl => pl.PlayerId == otherPlayerId );
            }

            //Récupérer la description dans la BDD
            Player currentPlayer = SetupContext.GetPlayer( User.GetUserId() );

            // Si le joueur existe => On affiche le profil
            if ( otherPlayer != null )
            {
                model.Name = otherPlayer.Name;
                model.Mail = otherPlayer.Mail;
                model.PlayerId = otherPlayer.PlayerId;
                model.CurrentPlayerId = currentPlayer.PlayerId;
                model.OtherIslands = otherPlayer.Islands.ToList();

                if ( !String.IsNullOrEmpty( otherPlayer.Profil.Description ) )
                    model.Description = otherPlayer.Profil.Description;
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

            SetupContext.FillStandardVM( model, currentPlayer.PlayerId, islandId );
            return View( model );
        }

        public IActionResult ProfilOfAPlayer( long islandId, long playerId )
        {
            //Récupérer la description dans la BDD
            Player currentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Player p = SetupContext.Players.Include( pro => pro.Profil ).Single( pe => pe.PlayerId == playerId );
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

            if ( !String.IsNullOrEmpty( p.Profil.Description ) )
                model.Description = p.Profil.Description;
            else
                model.Description = "Aucune description.";


            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( "ProfilOfOtherPlayer", model );
        }

        public IActionResult ChangeProfil( long islandId = 0 )
        {
            Player p = SetupContext.GetPlayer( User.GetUserId() );
            p = SetupContext.Players.Include( z => z.Profil ).Where( x => x.PlayerId == p.PlayerId ).First();
            ViewData[ "name" ] = p.Name;

            ProfilViewModel model = new ProfilViewModel();
            model.Name = p.Name;
            model.Description = ( !String.IsNullOrEmpty( p.Profil.Description ) ) ? p.Profil.Description : "Aucune description.";
            model.Mail = p.Mail;

            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( model );
        }

        [HttpPost]
        public IActionResult changeDescription( string description, long islandId = 0 )
        {
            // Ajouter la description dans la BDD

            Player p = SetupContext.GetPlayer( User.GetUserId() );
            p = SetupContext.Players.Include( z => z.Profil ).Where( x => x.PlayerId == p.PlayerId ).First();
            Profil oldProfil = p.Profil;
            oldProfil.Description = description;
            SetupContext.SaveChanges();

            ViewData[ "name" ] = p.Name;
            ProfilViewModel profilViewModel = new ProfilViewModel();
            profilViewModel.Description = ( !String.IsNullOrEmpty( p.Profil.Description ) ) ? p.Profil.Description : "Aucune description";

            SetupContext.FillStandardVM( profilViewModel, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( "Index", profilViewModel );
        }
    }
}
