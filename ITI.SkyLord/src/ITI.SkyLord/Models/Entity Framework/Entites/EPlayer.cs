using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.SkyLord.Models.Entity_Framework.Entites
{
    public class EPlayer
    {
        [Key]
        public long PlayerId { get; set; }
        public string Name { get; set; }

        //public Guild Guild { get; set; }
        //public ICollection<Island> Islands { get; set; }

        [ForeignKey("ProfilId")]
        public EProfil Profil { get; set; }
        //public ICollection<Technology> Technologies { get; set; }
        public EWorld World { get; set; }
    }
}
