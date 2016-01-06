using ITI.SkyLord.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public abstract class Event
    {

        public long EventId { get; set; }
        public string EventType { get; set; }
        public DateTime begginningDate { get; set; }
        public DateTime endingDate { get; set; }
        public bool done { get; set; }
        public Island island { get; set; }

        abstract public void Accept(EventManager visitor);

    }
}
