using ITI.SkyLord.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public class BuildingEvent : Event
    {
        public Building building { get; set; }


        public virtual void Accept(EventManager em) => em.Resolve(this);

    }
}
