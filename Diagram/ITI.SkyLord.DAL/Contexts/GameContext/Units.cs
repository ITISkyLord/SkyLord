﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public class Units
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public UnitStatistics UnitStatistics { get; set; }
        public UnitType UnitType { get; set; }
        public UnitDamageType UnitDamageType { get; set;}
    }
}
