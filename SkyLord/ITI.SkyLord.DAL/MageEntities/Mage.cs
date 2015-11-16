using ITI.SkyLord.DAL.IslandEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.MageEntities
{
     public class Mage
    {
        public long MageId { get; set; }
        public string Name { get; set; }
       

        public ICollection<Apprentice> Apprentices { get; set; }
        public Island Island { get; set; }
        public MageLevel MageLevel { get; set; }

    }
}
