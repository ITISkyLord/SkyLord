using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITI.SkyLord.ViewModel.Armies
{
    public class ArmyViewModel
    {
        public Army CurrentArmy { get; set; }
        public List<Army> CurrentIslandArmies { get; set; }
        public List<Unit> AvailableUnits { get; set; }
        public Dictionary<string, int> UnitsToAdd { get; set; }
        [Display( Name = "Garde" )]
        public int NumberGuard { get; set; }

        [Display( Name = "Nécromancien" )]
        public int NumberNecromancer { get; set; }



    }
}
