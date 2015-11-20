using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using ITI.SkyLord.Models;

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
