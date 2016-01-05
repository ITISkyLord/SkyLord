using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public class Event
    {
        public EventType eventType { get; set; }
        public DateTime begginningDate { get; set; }
        public DateTime endingDate { get; set; }
        public bool done { get; set; }
        public Island island { get; set; }

    }
}
