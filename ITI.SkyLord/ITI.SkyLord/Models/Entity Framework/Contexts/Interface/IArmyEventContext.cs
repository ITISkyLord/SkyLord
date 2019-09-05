using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public interface IArmyEventContext : IDisposable, IInfrastructure<IServiceProvider>
    {
        DbSet<ArmyEvent> ArmyEvents { get; set; }
        DbSet<Army> Armies { get; set; }
        ArmyMovement ArmyMovements { get; set; }
        int SaveChanges();
    }
}
