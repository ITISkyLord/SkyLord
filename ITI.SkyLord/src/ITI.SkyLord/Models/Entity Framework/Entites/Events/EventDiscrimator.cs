using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites.Events
{
    public static class EventDiscrimator
    {
        public const string TechnologyEvent = "TechnologyEvent";
        public const string BuildingEvent = "BuildingEvent";
        public const string UnitEvent = "UnitEvent";
        public const string UpgradeEvent = "UpgradeEvent";
        public const string ArmyEvent = "ArmyEvent";
    }
}
