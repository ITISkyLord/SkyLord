using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;

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
                //ViewData["description"] = context.Profils.FirstOrDefault(p => p.;

            }
            return View();
        }

        public ActionResult changeDescription( string description )
        {
            // Ajouter la description dans la BDD
            return View("Index");
        }
    }
}
