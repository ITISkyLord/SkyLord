using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Armies
{
    public class SetAttackingArmyViewModel : StandardViewModel
    {
        // For UI in WorldMap
        public long EnnemyIslandId { get; set; }

        public List<ArmyEvent> AllPlayerArmiesEvent { get; set; }


        public Island AttackingIsland { get; set; }
        public Army CurrentDefenseArmy { get; set; }
        public Dictionary<string, int> UnitsToSend { get; set; }
        public List<Island> EnnemyIslands { get; set; }
        public long Target { get; set; }

    }
}
