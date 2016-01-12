using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class BuildingLevel : Level
    {
        public BuildingName BuildingName { get; set; }
        public IList<BonusBuilding> BuildingBonuses { get; set; }
    }
}