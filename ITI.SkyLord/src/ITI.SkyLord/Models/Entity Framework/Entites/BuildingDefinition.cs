using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class BuildingDefinition
    {
        public long BuildingDefinitionId { get; set; }
        public string Name { get; set; }
        public IList<BuildingLevel> PossibleLevels { get; set; }
    }
}
