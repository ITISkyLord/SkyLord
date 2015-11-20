using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class TechnologyLevel : Level
    {
        private long _technologyLevelId;

        [Key]
        public long TechnologyLevelId { get; set; }
    }
}