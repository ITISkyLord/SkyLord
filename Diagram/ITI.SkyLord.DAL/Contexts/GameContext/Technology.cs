using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public class Technology
    {
        public long TechnologyId { get; set; }
        public string Name { get; set; }

        public TechnologyLevel TechnologyLevel { get; set; }

    }
}
