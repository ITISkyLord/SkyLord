using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
     public class Mages
    {
        public long ID { get; set; }
        public string Name { get; set; }
       

        public ICollection<Apprentices> Apprentice { get; set; }
        public Islands Island { get; set; }
        public MagesLevel MageLevel { get; set; }

    }
}
