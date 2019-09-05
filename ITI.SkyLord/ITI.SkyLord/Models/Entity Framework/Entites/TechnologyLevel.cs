using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class TechnologyLevel : Level
    {
        public TechnologyName TechnologyName { get; set; }
    }
}