using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public interface IEventContext: IDisposable, IInfrastructure<IServiceProvider>
    { 
        DbSet<Event> Events { get; set; }
        DbSet<UnitEvent> UnitEvents { get; set; }
    }
}
