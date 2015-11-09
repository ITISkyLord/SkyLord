using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public class Island
    {
        public int IslandId { get; set; }
        public bool IsCapital { get; set; }
        public int Loyalty { get; set; }
        public string Name { get; set; }

        public Ressource AllRessources { get; set; }
        public ICollection<Army> Armies { get; set; }
        public ICollection<Building> Buildings { get; set; }
        public Coordinate Coordinates { get; set; }    
        public Player Owner { get; set; }

    }
}
