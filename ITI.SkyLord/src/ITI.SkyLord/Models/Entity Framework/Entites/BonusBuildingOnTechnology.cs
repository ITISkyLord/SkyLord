using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class BonusBuildingOnTechnology : BonusBuilding
    {
        public TechnologyName TargetTechnology { get; set; }

    }
}
