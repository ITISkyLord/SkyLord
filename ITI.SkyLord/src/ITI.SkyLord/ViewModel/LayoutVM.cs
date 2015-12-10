﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel
{
    public class LayoutVM
    {
        public Player CurrentPlayer { get; }

        /// <summary>
        /// Need Ressources
        /// </summary>
        public Island CurrentIsland { get; }
        public List<Island> AllIslands { get; }

        public List<Army> AttackerArmy{ get; }



    }
}
