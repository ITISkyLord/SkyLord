using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public class Army
    {
        public long ArmyId { get; set; }

        public ArmyState ArmyState { get; set; }
        public Island Island { get; set; }
        public ICollection<Regiment> Regiment { get; set; }
    }
}
