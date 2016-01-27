using ITI.SkyLord.ViewModel.Account;
using ITI.SkyLord.ViewModels.Account;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;

namespace ITI.SkyLord.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(AccountViewModel avm, bool error )
        {

            if( User.IsSignedIn() )
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
