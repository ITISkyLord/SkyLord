using ITI.SkyLord.DAL.GuildEntities;
using ITI.SkyLord.DAL.IslandEntities;
using ITI.SkyLord.DAL.MageEntities;
using ITI.SkyLord.DAL.MessageEntities;
using ITI.SkyLord.DAL.PlayerEntities;
using ITI.SkyLord.DAL.UnitEntities;
using ITI.SkyLord.DAL.WorldEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts
{
    public partial class SetUpContext : DbContext
    {
        public SetUpContext()
            :base("SkyLordDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SetUpContext, ITI.SkyLord.DAL.Migrations.Configuration>("SkyLordDB"));     
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure PlayerId as PK for Profile
            modelBuilder.Entity<Profile>()
                .HasKey(e => e.PlayerId);

            // Configure PlayerId as FK for Profile
            modelBuilder.Entity<Player>()
                        .HasOptional(s => s.Profil) // Mark Profile is optional for Player
                        .WithRequired(ad => ad.Owner); // Create inverse relationship

            // Configure WorldId as PK for Map
            modelBuilder.Entity<Map>()
                .HasKey(e => e.WorldId);

            // Configure WorldId as FK for Mpa
            modelBuilder.Entity<World>()
                        .HasOptional(s => s.Map) // Mark Map is optional for World
                        .WithRequired(ad => ad.World); // Create inverse relationship

        }

        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<World> Worlds { get; set; }
        public virtual DbSet<Apprentice> Apprentices { get; set; }
        public virtual DbSet<Army> Armies { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Guild> Guilds { get; set; }
        public virtual DbSet<Coordinate> Coordinates { get; set; }
        public virtual DbSet<GuildRole> GuildRoles { get; set; }
        public virtual DbSet<Island> Islands { get; set; }
        public virtual DbSet<Mage> Mages { get; set; }
        public virtual DbSet<Ressource> Ressources { get; set; }
        public virtual DbSet<Spell> Spells { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Technology> Technologies { get; set; }
        public virtual DbSet<UnitStatistic> UnitStatistics { get; set; }
        public virtual DbSet<Regiment> Regiments { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<CombatReport> CombatReports { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Map> Maps { get; set; }
    }
}
