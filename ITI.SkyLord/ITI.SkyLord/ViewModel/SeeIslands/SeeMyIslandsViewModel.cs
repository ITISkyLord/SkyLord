using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Services;

namespace ITI.SkyLord.ViewModel.SeeIslands
{
    public class SeeMyIslandViewModel : StandardViewModel
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
        public Dictionary<int, List<BuildingEvent>> AllBuildingEventOnIsland { get; set; } // int is BuildingPosition

        // For InvocationBuilding
        public Army DefenseArmy { get; set; }
        public List<Unit> AllUnits { get; set; }
        public List<Unit> AvailableUnit { get; set; }

        // For LaboratoryBuilding
        public List<Technology> OwnTechnologies { get; set; }
        public List<TechnologyLevel> AvailableTechnologies { get; set; }
        public List<TechnologyLevel> AllTechnologies { get; set; }
        public TechnologyManager TechnologyManager { get; set; }

        public List<TechnologyDisplay> TechnologyDisplays { get; set; }
        public TechnologyName TargetTechnology { get; set; }
        public TechnologyEvent CurrentResearch { get; set; }

        public List<UnitQueue> UnitsQueue { get; set; }


        //See details Island
        //public Island CurrentIsland { get; set; }
       // public List<UnitQueue> UnitsQueue { get; set; }
        public BuildingEvent BuildingDeveloping { get; set; }
        public List<ArmyEvent> MovementTowardThisIsland { get; set; }
        public List<ArmyEvent> MovementFromThisIsland { get; set; }
        public Ressource Production { get; set; }

    }
}
