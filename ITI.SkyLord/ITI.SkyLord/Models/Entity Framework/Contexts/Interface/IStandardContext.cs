using ITI.SkyLord.ViewModel;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        void FillStandardVM( StandardViewModel vm, long playerId, long islandId );
    }
}
