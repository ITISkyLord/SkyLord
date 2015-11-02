using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageModel
{
     public partial class Player
    {

        public long PlayerId { get; set; }
        public string Name { get; set; }
        public long World { get; set; }

        public Guild Guild { get; set; }
        public ICollection<Island> Island { get; set; }
        public Profil AccountProfil { get; set; }
        public ICollection<Technology> Technology { get; set; }
        public World AccountWorld { get; set; }
    }
}
