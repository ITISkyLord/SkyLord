using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class BonusTechnology
    {
        public long BonusTechnologyId { get; set; }
        public int Modifier { get; set; }
        public BonusType BonusType { get; set; }
    }
}
