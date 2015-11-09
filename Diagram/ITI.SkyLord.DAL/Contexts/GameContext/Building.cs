using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public class Building
    {
        public long BuildingId { get; set; }
        public string Name { get; set; }

        public BuildingLevel BuildingLevel { get; set; }
    }
}
