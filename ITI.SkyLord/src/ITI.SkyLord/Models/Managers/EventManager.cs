using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Models.Managers
{
    public class EventManager
    {
        public void AddUnitEvent( Unit unit, Island island )
        {
            
        }

        public List<Event> Get(EventType et, IEventContext ctx, int IslandId)
        {
            return ctx.Events.Include(e => e.island).Where(e => e.island.IslandId == IslandId).ToList();
        }


        public void ResolveAll(int islandId, IEventContext ctx)
        {
            List<Event> t = ctx.Events.Where( e => e.done==false && e.endingDate > DateTime.Now).OrderBy(e=>e.endingDate).ToList();

            foreach(var a in t)
            {
                a.Accept(this);
                a.done = true;
            }

        }

        #region Resolve
        public void Resolve(UnitEvent ue)
        {

        }

        public void Resolve(TechnologyEvent te)
        {

        }

        public void Resolve(BuildingEvent be)
        {

        }

        public void Resolve(UpgradeEvent ue)
        {

        }

        public void Resolve(ArmyEvent ae)
        {

        }
        #endregion

    }
}
