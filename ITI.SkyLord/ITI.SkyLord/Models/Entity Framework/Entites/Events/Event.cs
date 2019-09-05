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
        public DateTime BegginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool Done { get; set; }
        public Island Island { get; set; }

        abstract public void Accept(EventManager visitor);
    }
}
