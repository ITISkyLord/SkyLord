using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface IArmyContext : IGenericContext, ICustomContext
    {
        DbSet<Army> Armies { get; set; }
        DbSet<Ressource> Ressources { get; set; }
        DbSet<Unit> Units { get; set; }
        DbSet<Regiment> Regiments { get; set; }
        DbSet<UnitStatistics> UnitStatistics { get; set; }

    }
}
