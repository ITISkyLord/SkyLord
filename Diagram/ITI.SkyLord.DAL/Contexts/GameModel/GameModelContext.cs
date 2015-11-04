using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
    public   class GameEntity : DbContext
    {
        public GameEntity()
        {          
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Profils> Profils { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Worlds> Worlds { get; set; }
        public virtual DbSet<Apprentices> Apprentices { get; set; }
        public virtual DbSet<Armys> Armys { get; set; }
        public virtual DbSet<Buildings> Buildings { get; set; }
        public virtual DbSet<Guilds> Guilds { get; set; }
        public virtual DbSet<Coordinates> Coordinates { get; set; }
        public virtual DbSet<GuildRoles> GuildRoles { get; set; }
        public virtual DbSet<Islands> Islands { get; set; }
        public virtual DbSet<Mages> Mages { get; set; }
        public virtual DbSet<Ressources> Ressources { get; set; }
        public virtual DbSet<Spells> Spells { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Technologys> Technologys { get; set; }
        public virtual DbSet<UnitStatistics> UnitStatistics { get; set; }
    }
}
