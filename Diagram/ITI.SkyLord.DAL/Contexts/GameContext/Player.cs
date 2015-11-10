using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public class Player
    {
        public Player() { }

        public long PlayerId { get; set; }
        public string Name { get; set; }
        public Guild Guild { get; set; }
        public IList<Island> Islands { get; set; }

        public IList<Technology> Technologies { get; set; }
        public World World { get; set; }

        //public long ProfilId { get; set; }
        //[Required]
        public virtual Profil Profil { get; set; }

    }
}
