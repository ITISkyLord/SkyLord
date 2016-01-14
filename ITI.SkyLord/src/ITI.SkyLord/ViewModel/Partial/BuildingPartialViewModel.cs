using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Partial
{
    public class BuildingPartialViewModel
    {
        public List<Building> AvailableBuildings { get; set; }
        public Building CurrentBuilding { get; set; }
        public int Position { get; set; }

    }
}
