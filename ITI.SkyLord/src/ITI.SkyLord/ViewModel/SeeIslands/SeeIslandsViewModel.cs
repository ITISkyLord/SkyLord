using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.SeeIslands
{
    public class SeeIslandsViewModel : StandardViewModel
    {
        public long CurrentIslandId { get; set; }
        public Island Island { get; set; }
        public Player Owner { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
