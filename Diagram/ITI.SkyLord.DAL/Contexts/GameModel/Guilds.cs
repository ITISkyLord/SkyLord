using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public class Guilds
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public string InternalMessage { get; set; }
        public string Name { get; set; }
        public string PublicMessage { get; set; }
        public bool Recrutement { get; set; }

        public ICollection<KeyValuePair<Players,GuildRoles>> Members { get; set; }
    }
}
