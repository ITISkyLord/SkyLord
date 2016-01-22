using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Armies
{
    public class SetColonisationViewModel : StandardViewModel
    {
        // For UI in WorldMap
        public long TargetIslandId { get; set; }

        // Différencie-t-on les évènements pour la colonisation ?
        public List<ArmyEvent> AllPlayerArmiesEvent { get; set; }


        public Island ColonisingIsland { get; set; }
        public Army CurrentApprenticeArmy { get; set; }
        public List<Unit> ApprenticeToSend { get; set; }
        public List<Island> PossibleColonisableIslands { get; set; }
        public long Target { get; set; }

    }
}
