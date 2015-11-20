using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.SkyLord.Models.Entity_Framework.Entites
{
    public class EProfil
    {
        [Key]
        public long ProfilId { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
