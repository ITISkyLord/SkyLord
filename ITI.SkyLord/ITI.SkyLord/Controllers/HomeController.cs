using ITI.SkyLord.ViewModel.Account;
using ITI.SkyLord.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITI.SkyLord.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(AccountViewModel avm, bool error )
        {

            if( User.Identity.IsAuthenticated )
            {
                return RedirectToAction( "SeeMyIsland", "Island" );
            }

            if( error )
            {
                ModelState.AddModelError( string.Empty, "Le login ou le mot de passe est incorrect." );
            }

            return View( avm );
        }
        

    }

    
}
