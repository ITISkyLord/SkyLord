using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ITI.SkyLord.Controllers
{

    /// <summary>
    /// /!\ Ce controller ne sert qu'a faire des tests. Merci de respecter cette règle.
    /// </summary>
    public class TestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
