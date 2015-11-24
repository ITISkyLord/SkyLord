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
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.Controllers
{
    public class ConnectionController : Controller
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
        public ActionResult connectionForm( string name, string mail, string password)
        {
            // if( String.IsNullOrWhiteSpace( name ) ) return View();
            PlayerContext pc = new PlayerContext();

            pc.AddPlayer( new Player( pc.GetWorld(), name, mail, password ) );
            return View("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginForm(string name, string password )
        {
            PlayerContext pc = new PlayerContext();
            if ( pc.IsPlayerValid( name, password ) )
            {
                return View( "../Logged/Index" );
            }
            // TODO Login devient Index quand login/inscription sont proprement faits
            return View( "Login" );
        }


        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }

    
}
