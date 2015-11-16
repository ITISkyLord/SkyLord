using ITI.SkyLord.DAL.UnitEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.IslandEntities
{
    public class Regiment
    {
        public long RegimentId { get; set; }
        public int Number { get; set; }
        public Unit Unit { get; set; }
    }
}
