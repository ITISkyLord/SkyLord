using ITI.SkyLord.DAL.Contexts.GameContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageContext
{
    public class PlayerForMessageContext
    {
        public long PlayerId { get; set; }
        public string Name { get; set; }

        public int Guild { get; set; }
        public ICollection<int> Islands { get; set; }
        public int Profil { get; set; }
        public ICollection<int> Technologies { get; set; }
        public int World { get; set; }
    }
}
