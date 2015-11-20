using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITI.SkyLord.Models.Entity_Framework.Entites
{
    public class EWorld
    {
        [Key]
        public long WorldId { get; set; }

        public ICollection<EPlayer> Players { get; set; }
    }
}
