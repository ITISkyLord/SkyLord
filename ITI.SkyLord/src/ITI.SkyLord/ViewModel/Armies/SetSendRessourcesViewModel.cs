using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Armies
{
    public class SetSendRessourcesViewModel : StandardViewModel
    {
        // For UI in WorldMap
        public long TargetIslandId { get; set; }

        // Différencie-t-on les évènements pour l'envoi de marchandise ?
        public List<ArmyEvent> AllPlayerArmiesEvent { get; set; }


        public Island SenderIsland { get; set; }
        public Army CurrentTransportorArmy { get; set; }
        public int NumberOfTransportorToSend { get; set; }

        /// <summary>
        /// Îles qui sont habitées et sur lesquelles on peut envoyer des ressources.
        /// </summary>
        public List<Island> SendableIslands { get; set; }
        public long Target { get; set; }
        public long AllyIslandId { get; set; }


    }
}
