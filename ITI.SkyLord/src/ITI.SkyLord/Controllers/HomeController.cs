using Microsoft.AspNet.Mvc;

namespace ITI.SkyLord.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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
