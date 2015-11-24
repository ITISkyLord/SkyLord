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
using System.Security.Cryptography;
using System.Text;

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
        public ActionResult connectionForm( string name, string mail, string password )
        {
            // if( String.IsNullOrWhiteSpace( name ) ) return View();
            PlayerContext pc = new PlayerContext();
            try
            {
                Console.WriteLine( password );

                password = ProtectPassword( password );
                Console.WriteLine( password );
                Player p = new Player( pc.GetWorld(), name, mail, password );
                pc.AddPlayer( p );
            }
            catch( ArgumentException e )
            {
                ViewData["mailError"] = "Erreur de mail";
            }
            return View( "Index" );
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
            return View( "~/Views/Shared/Error.cshtml" );
        }

        private string ProtectPassword( string clearPassword )
        {
            byte[] bytes = Encoding.UTF8.GetBytes(clearPassword);
            byte[] protectedBytes;
#if DNX451
            protectedBytes =  ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
#else
            protectedBytes = bytes;
#endif
            return Convert.ToBase64String( protectedBytes );
        }

        private string UnprotectPassword( string protectedPassword )
        {
            byte[] protectedBytes = Convert.FromBase64String(protectedPassword);
            byte[] bytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString( bytes );
        }
    }
}
