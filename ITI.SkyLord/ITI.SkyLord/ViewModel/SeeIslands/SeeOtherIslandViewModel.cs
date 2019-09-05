using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.SeeIslands
{
    public class SeeOtherIslandViewModel : StandardViewModel
    {
        public long IslandIdOfOther { get; set; }
        public bool HasOwner { get; set; }
        public Island TargetIsland { get; set; }
    }
}
