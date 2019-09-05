using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    /// <summary>
    /// Contains information about a requirment avalability
    /// </summary>
    public class RequirementAvailability
    {
        public bool IsItemAvailable
        {
            get
            {
                return !Availabilities.Any( a => !a.Available );
            }
        }

        public List<Availability> Availabilities { get; set; }

        public RequirementAvailability()
        {
            Availabilities = new List<Availability>();
        }
    }
}
