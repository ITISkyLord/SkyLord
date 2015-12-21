using Microsoft.AspNet.Mvc;
using System.Security.Claims;

namespace ITI.SkyLord.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if( User.IsSignedIn() )
            {
                return RedirectToAction( "SeeMyIsland", "Island" );
            }
            return View();
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
