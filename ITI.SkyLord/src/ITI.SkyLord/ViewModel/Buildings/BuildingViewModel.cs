using System.Collections.Generic;

namespace ITI.SkyLord.ViewModel
{
    public class BuildingViewModel : StandardViewModel
    {
        public List<Building> Buildings { get; set; }
        public List<Building> AvailableBuildings { get; set; }
        public BuildingName BuildingToBuild { get; set; }
        public BuildingName BuildingToLevelUp { get; set; }
    }
}
