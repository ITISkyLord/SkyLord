using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public abstract class Building
    {
        private long _buildingId;
        private string _name;
        private BuildingLevel _level;

        [Key]
        public int BuildingId { get; set; }

        public string Name { get; set; }

        public BuildingLevel Level { get; set; }

    }
}