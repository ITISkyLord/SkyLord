using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Armies
{
    public class SetMovementArmyViewModel : StandardViewModel
    {
        public long TargetIslandId { get; set; }
        public Dictionary<string, int> UnitsToSend { get; set; }
        public Army CurrentDefenseArmy { get; set; }
    }
}
