using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ITI.SkyLord.ViewModel.World;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.SkyLord.Controllers
{
    public class WorldController : GenericController
    {
        public IActionResult Index( long islandId )
        {
            WorldMapViewModel wmvm = new WorldMapViewModel();

            var listOfIsland = SetupContext.Islands.Include(i => i.Coordinates).ToList();
            wmvm.Map = new Dictionary<string, Island>();

            foreach(var island in listOfIsland)
            {
                wmvm.Map.Add(island.Coordinates.X+"_"+island.Coordinates.Y, island);
            }

            SetupContext.FillStandardVM(wmvm, SetupContext.GetPlayer(User), islandId);
            return View(wmvm);
        }
    }
}
