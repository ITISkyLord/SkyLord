using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public class Profils
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public long Owner { get; set; }
        public string Password { get; set; }

        public Players Players { get; set; }

    }
}
