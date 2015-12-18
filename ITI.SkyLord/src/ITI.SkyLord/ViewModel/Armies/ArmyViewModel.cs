using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITI.SkyLord.ViewModel.Armies
{
    public class ArmyViewModel
    {
        public long IslandId { get; set; }
        public Army CurrentArmy { get; set; }
        public List<Army> CurrentIslandArmies { get; set; }
        public List<Unit> AvailableUnits { get; set; }
        public Dictionary<string, int> UnitsToAdd { get; set; }
    }
}
