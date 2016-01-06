using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public interface IUnitEventContext : IDisposable, IInfrastructure<IServiceProvider>
    {
        DbSet<UnitEvent> UnitEvents { get; set; }
        DbSet<Unit> Units { get; set; }
        DbSet<UnitStatistics> UnitStatistics { get; set; }
        UnitDamageType UnitDamageTypes { get; set; }
        UnitName UnitName { get; set; }
        UnitType UnitType { get; set; }

        int SaveChanges();
    }
}
