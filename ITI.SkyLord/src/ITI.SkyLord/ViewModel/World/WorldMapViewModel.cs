using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.World
{
    public class WorldMapViewModel : StandardViewModel
    {
        public Dictionary<string, Island> Map { get; set; }
    }
}
