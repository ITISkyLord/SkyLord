using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Services;

namespace ITI.SkyLord.ViewModel.Partial
{
    public class BuildingPartialViewModel
    {

        public LayoutViewModel Layout { get; set; }

        // Next Level of Current Building
        public Level NextBuildingLevel { get; set; }

        // Empty
        public List<Building> AvailableBuildings { get; set; }
        public Building CurrentBuilding { get; set; }
        public int Position { get; set; }
        public BuildingName TargetBuilding { get; set; }
        public List<BuildingEvent> EventsOnBuilding { get; set; }

        // Invocation
        public Army CurrentArmy { get; set; }
        public List<Unit> AllUnits { get; set; }
        public List<Unit> AvailableUnit { get; set; }
        public int UnitAmount { get; set; }
        public UnitName UnitTarget { get; set; }
        public List<UnitQueue> UnitsQueue { get; set; }

        // Technologies
        public List<TechnologyDisplay> TechnologyDisplays { get; set; }
        public TechnologyManager TechnologyManager { get; set; }
        public TechnologyName TargetTechnology { get; set; }

    }
}
