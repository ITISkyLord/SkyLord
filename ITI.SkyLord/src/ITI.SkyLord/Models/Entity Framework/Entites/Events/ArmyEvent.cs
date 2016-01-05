using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public class ArmyEvent : Event
    {
        public Army army { get; set; }
        public Island destination { get; set; }
        public ArmyMovement armyMovement { get; set; }

    }
}
