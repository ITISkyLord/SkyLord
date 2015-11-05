using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{ 
   public class Map
    {
        public long MapId { get; set; }

        public ICollection<Island> Island { get; set; }
        //public World World { get; set; }
    }
}
