using ITI.SkyLord.ViewModel.Account;
using ITI.SkyLord.ViewModels.Account;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.Controllers
{
    public class HomeController : GenericController
    {
        public HomeController( [FromServices]SetupContext setupcontext )
            :base(setupcontext)
        {
        }
        public IActionResult Index(AccountViewModel avm)
        {

            if( User.IsSignedIn() )
            {
                return RedirectToAction( "SeeMyIsland", "Island" );
            }

            return View(avm);
        }

        public IActionResult TestDb()
        {
            //using (var db = new ApplicationDbContext())
            //{
            //    db.Merdes.Add(new Merde() { Message = "coucou" });
            //    db.SaveChanges();
            //}
            return View();
            
        }


        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }

    
}
