using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites
{
    public class CustomClaims : ClaimsPrincipal
    {
        public ModelStateDictionary ModelState { get; set; }
    }
}
