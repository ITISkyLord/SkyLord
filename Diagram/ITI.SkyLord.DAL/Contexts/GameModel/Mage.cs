using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
     public partial class Mage
    {
        public long MageId { get; set; }
        public string Name { get; set; }
       

        public ICollection<Apprentice> Apprentice { get; set; }
        public Island Island { get; set; }
        public MageLevel MageLevel { get; set; }

    }
}
