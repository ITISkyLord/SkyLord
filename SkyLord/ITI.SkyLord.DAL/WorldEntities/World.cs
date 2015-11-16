using ITI.SkyLord.DAL.PlayerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.WorldEntities
{
    public class World
    {
        public long WorldId { get; set; }

        public ICollection<Player> Players { get; set; }
        public virtual Map Map { get; set; }
    }
}
