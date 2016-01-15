using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface IIslandContext : ICustomContext
    {
        DbSet<Ressource> Ressources { get; set; }
        DbSet<Coordinate> Coordinates { get; set; }
        DbSet<Building> Building { get; set; }
        DbSet<Technology> Technologies { get; set; }
        DbSet<Unit> Units { get; set; }

    }
}
