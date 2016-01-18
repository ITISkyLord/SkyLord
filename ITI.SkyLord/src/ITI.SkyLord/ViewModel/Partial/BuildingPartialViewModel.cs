using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Partial
{
    public class BuildingPartialViewModel
    {

        public LayoutViewModel Layout { get; set; }

        // Next Level of Current Building
        public Ressource NextBuildingLevelCost { get; set; }

        // Empty
        public List<Building> AvailableBuildings { get; set; }
        public Building CurrentBuilding { get; set; }
        public int Position { get; set; }
        public BuildingName TargetBuilding { get; set; }

        // Invocation
        public Army CurrentArmy { get; set; }
        public List<Unit> AllUnits { get; set; }
        public List<Unit> AvailableUnit { get; set; }
        public int UnitAmount { get; set; }
        public UnitName UnitTarget { get; set; }

    }
}
