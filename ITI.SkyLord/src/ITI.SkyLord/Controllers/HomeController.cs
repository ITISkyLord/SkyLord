using ITI.SkyLord.ViewModel.Account;
using ITI.SkyLord.ViewModels.Account;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;

namespace ITI.SkyLord.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(AccountViewModel avm)
        {

            if( User.IsSignedIn() )
            {
                return RedirectToAction( "SeeMyIsland", "Island" );
            }

            return View(avm);
        }
        

    }

    
}
