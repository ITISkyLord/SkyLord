using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.SeeIslands
{
    public class SeeIslandViewModel : StandardViewModel
    {
        public Island CurrentIsland { get; set; }
        public List<Building> Buildings { get; set; }
        public Dictionary<string, Building> DicoBuildings { get; set; }
        public Dictionary<int, Level> NextLevel { get; set; } // int is building.position
        public SelectList AvailableConstructionBuildings { get; set; }
        public BuildingName TargetBuilding { get; set; }
        public int Position { get; set; }
        
        // For EmptyBuilding
        public List<Building> AvailableBuildings { get; set; }

        // For InvocationBuilding
        public Army CurrentArmy { get; set; }
        public List<Unit> AllUnits { get; set; }
        public List<Unit> AvailableUnit { get; set; }

        // For LaboratoryBuilding
        public List<Technology> AvailableTechnologies { get; set; }
        //public Technology CurrentTechnology { get; set; }
        //public TechnologyName TargetTechnology { get; set; }

    }
}
