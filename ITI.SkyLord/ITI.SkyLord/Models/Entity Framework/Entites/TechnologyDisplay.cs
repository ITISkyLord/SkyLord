using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class TechnologyDisplay
    {
        public bool IsAvailable { get; set; }
        public bool IsResearched { get; set; }
        public TechnologyLevel CurrentLevel { get; set; }
        public TechnologyLevel NextLevel { get; set; }
    }
}
