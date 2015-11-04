using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
    public class Worlds
    {
        public long ID { get; set; }

        public ICollection<Players> Players { get; set; }
        public Maps Map { get; set; }
    }
}
