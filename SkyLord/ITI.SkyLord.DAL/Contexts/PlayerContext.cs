using ITI.SkyLord.DAL.PlayerEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts
{
    public partial class PlayerContext : DbContext
    {
        public PlayerContext()
            : base("SkyLordDB"){ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure PlayerId as PK for Profile
            modelBuilder.Entity<Profile>()
                .HasKey(e => e.PlayerId);

            // Configure PlayerId as FK for Profile
            modelBuilder.Entity<Player>()
                        .HasOptional(s => s.Profil) // Mark Profile is optional for Player
                        .WithRequired(ad => ad.Owner); // Create inverse relationship
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Player> Players { get; set; } 
    }
}
