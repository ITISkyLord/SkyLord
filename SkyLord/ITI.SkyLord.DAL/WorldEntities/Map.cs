using ITI.SkyLord.DAL.IslandEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.WorldEntities
{ 
   public class Map
    {
        [Key, ForeignKey("World")]
        public long WorldId { get; set; }
        public virtual World World { get; set; }

       public ICollection<Island> Islands { get; set; }
    }
}
