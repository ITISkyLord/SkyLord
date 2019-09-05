using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;

namespace ITI.SkyLord.ViewModel
{
    public class StandardViewModel
    {
        public LayoutViewModel Layout { get; set; }
        public List<ArmyEvent> AttacksTowardThisPlayer { get; set; }
        public List<ArmyEvent> AttacksFromThisPlayer { get; set; }

    }
}
