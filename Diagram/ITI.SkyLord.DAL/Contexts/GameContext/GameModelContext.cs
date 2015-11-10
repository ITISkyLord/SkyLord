using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public partial class GameEntity : DbContext
    {
        public GameEntity()
            :base("SkyLordDb")
        {          
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure StudentId as PK for StudentAddress
            modelBuilder.Entity<Profil>()
                .HasKey(e => e.PlayerId);

            // Configure StudentId as FK for StudentAddress
            modelBuilder.Entity<Player>()
                        .HasOptional(s => s.Profil) // Mark StudentAddress is optional for Student
                        .WithRequired(ad => ad.Owner); // Create inverse relationship

        }

        public virtual DbSet<Profil> Profils { get; set; }
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
        public virtual DbSet<Technology> Technologys { get; set; }
        public virtual DbSet<UnitStatistic> UnitStatistics { get; set; }
        public virtual DbSet<Regiment> Regiments { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<CombatReport> CombatReports { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Map> Maps { get; set; }
    }
}
