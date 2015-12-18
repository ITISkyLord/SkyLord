﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Armies
{
    public class SetAttackingArmyViewModel
    {
        public Island AttackingIsland { get; set; }
        public Army CurrentDefenseArmy { get; set; }
        public Dictionary<string, int> UnitsToSend { get; set; }
        public List<Island> EnnemyIslands { get; set; }
        public long Target { get; set; }
    }
}
