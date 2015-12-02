﻿using System;
using Microsoft.AspNet.Mvc;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Cryptography.KeyDerivation;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Controllers
{
    public class ConnectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginForm( string mail, string password )
        {
            // TODO : RENDRE MAIL UNIQUE !!
            using ( PlayerContext context = new PlayerContext() )
            {
                if ( context.IsPlayerValid( mail, ProtectPassword( password ) ) )
                {
                    Player playerFound = context.FindPlayer( mail );
                    Response.Cookies.Append( "PlayerId", playerFound.PlayerId.ToString() );
                    Console.WriteLine( "Cookie Current PlayerId (login) = " + Request.Cookies[ "PlayerId" ] );
                    return RedirectToAction( "Index", "Logged" );
                }
            }
           
           return RedirectToAction( "Index", "Connection" );
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignupForm( string name, string mail, string password )
        {
            // TODO : RENDRE MAIL UNIQUE !!
            using ( PlayerContext context = new PlayerContext() )
            {
                password = ProtectPassword( password );
                Player p = new Player { World = context.GetWorld(), Name = name, Mail = mail, Password = password };

                if ( ModelState.IsValid )
                {
                    context.AddPlayer( p );
                }
            }
            return RedirectToAction( "Index", "Connection" );
        }

        public IActionResult Logout()
        {
            if ( !String.IsNullOrEmpty( Request.Cookies[ "PlayerId" ] ) )
                Response.Cookies.Delete( "PlayerId" );

            string cookie = Request.Cookies[ "PlayerId" ];
            Console.WriteLine( "Cookie current PlayerId (Logout) = " + cookie );
            return RedirectToAction( "Index", "Home" );
        }

        public IActionResult Error()
        {
            return View( "~/Views/Shared/Error.cshtml" );
        }

        private string ProtectPassword( string clearpassword )
        {
            // generate a 128-bit salt using a secure PRNG
            byte[ ] salt = new byte[ 128 / 8 ];
            //using ( var rng = RandomNumberGenerator.Create() )
            //{
            //    rng.GetBytes( salt );
            //}

            // TODO : garded le salt dans la table Player

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String( KeyDerivation.Pbkdf2(
                password: clearpassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8 ) );
        }
    }
}
