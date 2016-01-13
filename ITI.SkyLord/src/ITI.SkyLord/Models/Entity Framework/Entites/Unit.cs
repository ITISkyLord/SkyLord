using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class Unit
    {
        public int UnitId { get; set; }

        public string Name { get; set;}

        public UnitName UnitName { get; set; }

        public UnitDamageType UnitDamageType { get; set; }

        public UnitType UnitType { get; set; }

        public UnitStatistics UnitStatistics { get; set; }

        public Ressource UnitCost { get; set; }
        
        public int Duration { get; set; }
        public bool IsModel { get; set; }
    }
}
