﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.Armies;
using Microsoft.Data.Entity;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.SkyLord.Controllers
{
    public class LoggedController : GenericController
    {
        public LoggedController( [FromServices]SetupContext setupcontext )
            :base(setupcontext)
        {
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
