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

        public Island SenderIsland { get; set; }

        public int Wood { get; set; }
        public int Metal { get; set; }
        public int Cristal { get; set; }
        public int Magic { get; set; }
        public bool HasApprentice { get; set; }
        public List<Island> PossibleColonisableIslands { get; set; }
        public int CapacityOfCarrier { get; set; }
        public long Target { get; set; }
        public long BarbarianIslandId { get; set; }

    }
}
