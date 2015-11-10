using ITI.SkyLord.DAL.Contexts.GameContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageContext
{
    public class Players
    {
        public long PlayerId { get; set; }
        public string Name { get; set; }

        public long Guild_GuildId { get; set; }
        public ICollection<long> Islands { get; set; }
        public Profil Profil { get; set; }
        public ICollection<long> Technologies { get; set; }
        public long World_WorldId { get; set; }
    }
}
