using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;

namespace ITI.SkyLord.Controllers
{

    /// <summary>
    /// /!\ Ce controller ne sert qu'a faire des tests. Merci de respecter cette règle.
    /// </summary>
    public class TestController : GenericController
    {

        public TestController( [FromServices]SetupContext setupcontext )
            :base(setupcontext)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult YoloTestAdd()
        {
            TechnologyEvent te = new TechnologyEvent();
            te.BegginningDate = DateTime.Now;
            te.EndingDate = DateTime.Now.AddHours(2);
            te.Done = false;
            te.EventType = EventDiscrimator.TechnologyEvent;

            te.Island = SetupContext.Islands.Where(i=>i.IslandId==1).FirstOrDefault();

            SetupContext.Events.Add(te);
            SetupContext.SaveChanges();

            return View();
        }

        public IActionResult YoloTest()
        {
            EventManager t = new EventManager( SetupContext, new EventPackManager( SetupContext) );
            var a = t.Get(EventType.Technology, SetupContext, 1);


            return View();
        }

        public IActionResult YoloResolve()
        {
            EventManager t = new EventManager( SetupContext , new EventPackManager( SetupContext) );
            t.ResolveAllForPlayer( 2 );
            return View();
        }


    }
}
