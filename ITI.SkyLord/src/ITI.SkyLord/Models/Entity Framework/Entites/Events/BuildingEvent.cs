using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public class BuildingEvent : Event
    {
        public Building BuildingToBuild { get; set; }
        public int Indice { get; set; }

        public override void Accept(EventManager em) => em.Resolve(this);

    }
}
