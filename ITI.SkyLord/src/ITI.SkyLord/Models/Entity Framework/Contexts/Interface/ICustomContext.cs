using ITI.SkyLord.ViewModel;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface ICustomContext : IGenericContext, IDbContext
    {
       DbSet<Island> Islands { get; set; }

       DbSet<Player> Players { get; set; }

       void FillStandardVM(StandardViewModel svm, long playerId, long islandId);

       Island GetIsland(long islandId, long playerId);

    }
}
