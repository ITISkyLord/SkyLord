using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class Requirement
    {
        public long RequirementId { get; set; }
        public int Number { get; set; }
        public BuildingName BuildingName { get; set; }
        public TechnologyName TechnologyName { get; set; }
    }
}
