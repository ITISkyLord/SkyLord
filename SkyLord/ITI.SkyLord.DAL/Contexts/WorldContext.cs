using ITI.SkyLord.DAL.WorldEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts
{
   public partial  class WorldContext : DbContext
    {
        public WorldContext()
            :base("SkyLordDB")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //// Configure WorldId as PK for Map
            //modelBuilder.Entity<Map>()
            //    .HasKey(e => e.WorldId);

            //// Configure WorldId as FK for Map
            //modelBuilder.Entity<Map>()
            //            .HasRequired(s => s.World) // Mark Map is optional for World
            //            .WithOptional(ad => ad.Map); // Create inverse relationship

        }

        public DbSet<World> Worlds { get; set; }
        public DbSet<Map> Maps { get; set; }
    }
}
