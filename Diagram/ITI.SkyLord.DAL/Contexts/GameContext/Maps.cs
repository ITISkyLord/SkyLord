using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{ 
   public class Maps
    {
        public long ID { get; set; }

        public ICollection<Islands> Island { get; set; }
//        public Worlds World { get; set; }
    }
}
