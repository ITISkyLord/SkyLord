using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;

namespace ITI.SkyLord.ViewModel
{
    public class BuildingViewModel : StandardViewModel
    {
        public List<Building> Buildings { get; set; }
        public Dictionary<int, Ressource> NextLevelCosts { get; set; }
        public List<Building> AvailableBuildings { get; set; }
        public SelectList AvailableConstructionBuildings { get; set; }
        public BuildingName TargetBuilding { get; set; }
        public int Position { get; set; }
    }
}
