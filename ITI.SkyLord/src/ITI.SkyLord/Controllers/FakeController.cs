using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Controllers
{
    public class FakeController : Controller
    {
        public IActionResult AddMap()
        {
            using (var t = new SetupContext())
            {
                t.Maps.Add(new Map());
                t.SaveChanges();
            }
            return View();
        }
    }
}
