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
        public int PossiblePositions { get; set; }

        /// <summary>
        /// Last time resources production was checked
        /// </summary>
        public DateTime CheckTime { get; set; }

        #endregion
    }
}
