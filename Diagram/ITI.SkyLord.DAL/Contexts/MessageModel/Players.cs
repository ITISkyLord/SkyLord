using ITI.SkyLord.DAL.Contexts.GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageModel
{
     public class Players
    {
        public long PlayerId { get; set; }
        public string Name { get; set; }
        public long World { get; set; }

        public Guilds Guild { get; set; }
        public ICollection<Islands> Island { get; set; }
        public Profils AccountProfil { get; set; }
        public ICollection<Technologys> Technology { get; set; }
        public Worlds AccountWorld { get; set; }
    }
}
