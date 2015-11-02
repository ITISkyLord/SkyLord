using ITI.SkyLord.DAL.Contexts.AccountsModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
    public partial class GameEntity : DbContext
    {
        public GameEntity()
        {          
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Profil> Profil { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<World> World { get; set; }
        public virtual DbSet<Apprentice> Apprentice { get; set; }
        public virtual DbSet<Army> Army { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Guild> Guild { get; set; }
        public virtual DbSet<Coordinate> Coordinate { get; set; }
        public virtual DbSet<GuildRole> GuildRole { get; set; }
        public virtual DbSet<Island> Island { get; set; }
        public virtual DbSet<Mage> Mage { get; set; }
        public virtual DbSet<Ressources> Ressources { get; set; }
        public virtual DbSet<Spell> Spell { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Technology> Technology { get; set; }
        public virtual DbSet<UnitStatistics> UnitStatistics { get; set; }
    }
}
