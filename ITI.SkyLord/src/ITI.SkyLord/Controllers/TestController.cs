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
    public class TestController : Controller
    {

        [FromServices]
        public SetupContext SetupContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult YoloTestAdd()
        {
            TechnologyEvent te = new TechnologyEvent();
            te.begginningDate = DateTime.Now;
            te.endingDate = DateTime.Now.AddHours(2);
            te.done = false;
            te.EventType = EventDiscrimator.TechnologyEvent;

            te.island = SetupContext.Islands.Where(i=>i.IslandId==1).FirstOrDefault();

            SetupContext.Events.Add(te);
            SetupContext.SaveChanges();

            return View();
        }

        public IActionResult YoloTest()
        {

            EventManager t = new EventManager(SetupContext);

            var a = t.Get(EventType.Technology, SetupContext, 1);


            return View();
        }

        public IActionResult YoloResolve()
        {
            EventManager t = new EventManager(SetupContext);
            t.ResolveAll(1, SetupContext);

            return View();
        }


    }
}
