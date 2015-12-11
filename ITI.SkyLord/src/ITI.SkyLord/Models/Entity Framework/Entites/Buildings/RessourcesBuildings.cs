using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites
{
    public abstract class RessourcesBuildings //: IBuilding
    {
        public int BuildingId { get; set; }

        public BuildingLevel Level { get; set; }

        public string Name { get; set; }

        public Ressource ProductionByHour { get; set; }
    }
}
