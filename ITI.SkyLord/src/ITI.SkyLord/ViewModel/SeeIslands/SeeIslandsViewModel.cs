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
        public Dictionary<int, Ressource> NextLevelCosts { get; set; }
        public List<Building> AvailableBuildings { get; set; }
        public SelectList AvailableConstructionBuildings { get; set; }
        public BuildingName TargetBuilding { get; set; }
        public int Position { get; set; }
    }
}
