﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel
{
    public class LayoutViewModel
    {
        public Player CurrentPlayer { get; set; }

        /// <summary>
        /// Need Ressources
        /// </summary>
        /// 
        public long IslandId { get; set; }

        public Island CurrentIsland { get; set; }

        public List<Island> AllIslands { get; set; }

        public List<Army> AttackerArmies{ get; set; }



    }
}
