using ITI.SkyLord.DAL.IslandEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.UnitEntities
{
   public class Unit
    {
        public long UnitId { get; set; }
        public UnitName Name { get; set; }
        public UnitStatistic UnitStatistics { get; set; }
        public UnitType UnitType { get; set; }
        public UnitDamageType UnitDamageType { get; set; }
        public Ressource UnitCost { get; set; }
    }
}
