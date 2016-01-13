using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class BonusBuilding
    {
        public long BonusBuildingId { get; set; }
        public int Modifier { get; set; }
        public BonusType BonusType { get; set; }
    }
}
