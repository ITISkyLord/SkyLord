using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public class TechnologyEvent : Event
    {
        public int Duration { get; set; }
        public TechnologyName TechnologyName { get; set; }
        public override void Accept(EventManager em) => em.Resolve(this);

    }
}
