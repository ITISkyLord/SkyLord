using ITI.SkyLord.DAL.IslandEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts
{
   public partial class IslandContext : DbContext
    {
        public IslandContext()
            :base("SkyLordDB")
        { }

        DbSet<Army> Armies { get; set; }
        DbSet<Building> Buildings { get; set; }
        DbSet<CombatReport> CombatReport { get; set; }
        DbSet<Coordinate> Coordinates { get; set; }
        DbSet<Island> Islands { get; set; }
        DbSet<Level> Levels { get; set; }
        DbSet<Regiment> Regiments { get; set; }
        DbSet<Ressource> Ressources { get; set; }
        DbSet<Technology> Technologies { get; set; }


    }
}
