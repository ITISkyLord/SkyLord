using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public partial class Unit
    {
        public long UnitId { get; set; }
        public string Name { get; set; }
        public UnitStatistics UnitStatistics { get; set; }
        public UnitType UnitType { get; set; }
        public UnitDamageType UnitDamageType { get; set;}
    }
}
