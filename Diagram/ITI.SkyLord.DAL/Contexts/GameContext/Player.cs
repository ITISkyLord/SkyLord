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
        public long PlayerId { get; set; }
        public string Name { get; set; }

        public Guild Guild { get; set; }
        public ICollection<Island> Islands { get; set; }
        [Required]
        public Profil Profil { get; set; }
        public ICollection<Technology> Technologies { get; set; }
        public World World { get; set; }

    }
}
