using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Island
    {
        #region Properties
        [Key]
        public long IslandId { get; set; }

        public string Name { get; set; }

        public IList<Army> Armies { get; set; }

        public Player Owner { get; set; }

        public IList<Building> Buildings { get; set; }

        public Ressource AllRessources { get; set; }

        public Coordinate Coordinates { get; set; }

        public int Loyalty { get; set; }

        public bool IsCapital { get; set; }
        #endregion
    }
}
