using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public interface ITechnologyEventContext : IDisposable, IInfrastructure<IServiceProvider>
    {
        DbSet<TechnologyEvent> TechnologyEvents { get; set; }

        int SaveChanges();
    }
}
