using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public abstract class Building
    {
        [Key]
        public int BuildingId { get; set; }

        public string Name { get; set; }

        public BuildingLevel Level { get; set; }
    }
}