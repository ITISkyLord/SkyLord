using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.SeePlayers
{
    public class SeePlayersViewModel
    {
        /// <summary>
        /// Require Island(Require Coordoniate)
        /// </summary>
        public List<Player> Players { get; set; }

        public Player ActivePlayer { get; set; }

        public List<Island> IslandsOfOthersPlayers { get; set; }
    }
}
