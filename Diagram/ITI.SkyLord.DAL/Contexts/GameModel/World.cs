
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
    public class World
    {
        public long WorldId { get; set; }

        public ICollection<Player> Players { get; set; }
        public Map Map { get; set; }
    }
}
