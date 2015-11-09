using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
    public class Islands
    {
        [Key]
        public int ID { get; set; }
        public bool IsCapital { get; set; }
        public int Loyalty { get; set; }
        public string Name { get; set; }


        public Ressources Ressources { get; set; }
        public ICollection<Armys> Army { get; set; }
        public ICollection<Buildings> Building { get; set; }
        public Coordinates Coordinate { get; set; }    
        public Players Owner { get; set; }

    }
}
