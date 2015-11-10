using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
   public class Profil
    {
        //[Key,ForeignKey("Player")]
        public long PlayerId { get; set; }
        public long ProfilId { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public virtual Player Owner { get; set; }

    }
}
