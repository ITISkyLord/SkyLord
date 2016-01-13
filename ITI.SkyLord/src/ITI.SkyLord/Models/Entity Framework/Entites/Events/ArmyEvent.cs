using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public class ArmyEvent : Event
    {
        public Army Army { get; set; }
        public Island Destination { get; set; }
        public ArmyMovement ArmyMovement { get; set; }

        // Super Visitor Pattern OTD
        public override void Accept(EventManager em) => em.Resolve(this);





    }
}
