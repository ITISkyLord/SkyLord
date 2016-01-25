﻿using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Filters;
using System.Security.Claims;
using ITI.SkyLord.Services;

namespace ITI.SkyLord.Controllers
{
    public class GenericController : Controller
    {
        [FromServices]
        public SetupContext SetupContext { get; set; }


        public override void OnActionExecuting( ActionExecutingContext context )
        {
            base.OnActionExecuting( context );

            var player = SetupContext.GetPlayer(User.GetUserId());

            // Resolve all events from curent player
            EventManager em = new EventManager(SetupContext, new EventPackManager(SetupContext));
            em.ResolveAllForPlayer( player.PlayerId );


            // resolve ressources
            foreach( Island island in SetupContext.GetAllIslands( player.PlayerId ) )
            {
                RessourceManager.ResolveResources( island, SetupContext );
            }

            // Resolve Loyalty
            LoyaltyManager loyaltyManager = new LoyaltyManager(SetupContext);
            foreach( Island island in SetupContext.GetAllIslands( player.PlayerId ) )
            {
                loyaltyManager.ResolveLoyalty( island );
            }
        }



    }
}
