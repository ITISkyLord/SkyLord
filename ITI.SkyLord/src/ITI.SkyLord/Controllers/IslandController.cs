using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.ViewModel.SeeIslands;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Controllers
{
    public class IslandController : Controller
    {
        [FromServices]
        public PlayerContext PlayerContext { get; set; }

        [FromServices]
        public IslandContext IslandContext { get; set; }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// See the island(s) of the current player
        /// </summary>
        /// <param name="id">Mail of player</param>
        /// <returns></returns>
        public IActionResult SeeMyIsland(string id)
        {
             Player owner = PlayerContext.Players.Where(p => p.Mail == id).SingleOrDefault();
            List<Island> islands = IslandContext.Islands.Include(p => p.Owner).ToList();

            //Island myIsland = islands.First();
            //myIsland.Owner = owner;

            Island myIsland = islands.Where(i => i.Owner.PlayerId == owner.PlayerId).SingleOrDefault();
            List<Coordinate> coordinates = IslandContext.Coordinates.ToList();
            Coordinate coord = coordinates.Where(o => o.CoordinateId == myIsland.Coordinates.CoordinateId).SingleOrDefault();

            SeeIslands island = new SeeIslands();

            int X = coord.X;
            int Y = coord.Y;

            island.ListIslands = islands;
            island.Island = myIsland;
            island.Owner = owner;
            island.X = X;
            island.Y = Y;

            return View(island);
        }
        [Obsolete]
        public IActionResult AddIsland()
        {
            Player playerActive = PlayerContext.Players.Where(p => p.Name == "Kevin").SingleOrDefault();
            Island lastIsland = IslandContext.Islands.Where(i => i.Owner.PlayerId == playerActive.PlayerId).LastOrDefault();
            List<Coordinate> coordinate = IslandContext.Coordinates.ToList();
            Coordinate lastCoord = coordinate.Where(o => o.CoordinateId == lastIsland.Coordinates.CoordinateId).SingleOrDefault();
            Coordinate coordinateOfNewIsland = new Coordinate { X = lastCoord.X + 1, Y = lastCoord.Y + 1 };

            IslandContext.Coordinates.Add(coordinateOfNewIsland);
            Island newIsland = new Island { Name = "Default", IsCapital = false, Loyalty = 0, Owner = playerActive, Coordinates = coordinateOfNewIsland };
            IslandContext.Islands.Add(newIsland);
            IslandContext.SaveChanges();
            return View();
        }
    }
}