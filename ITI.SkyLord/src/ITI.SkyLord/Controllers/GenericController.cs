using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Filters;
using System.Security.Claims;

namespace ITI.SkyLord.Controllers
{
    public class GenericController : Controller
    {
        [FromServices]
        public SetupContext SetupContext { get; set; }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // On résoud tout les events du player courrant
            EventManager em = new EventManager(SetupContext, new EventPackManager(SetupContext));
            var Player = SetupContext.GetPlayer(User.GetUserId());
            em.ResolveAllForPlayer(Player.PlayerId);
        }



    }
}
