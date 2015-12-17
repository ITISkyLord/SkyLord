using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public interface IStandardContext : IDisposable, IInfrastructure<IServiceProvider> // Un DbContext implémente le bordel
    {

        DbSet<Player> Players { get; set; }

        DbSet<Island> Islands { get; set; }

    }
}
