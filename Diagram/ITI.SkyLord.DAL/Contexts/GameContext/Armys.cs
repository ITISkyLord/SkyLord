using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public class Armys
    {
        public long ID { get; set; }

        public ArmyState ArmyState { get; set; }
        public Islands Island { get; set; }
        public ICollection<KeyValuePair<Units,int>> Regiment { get; set; }
    }
}
