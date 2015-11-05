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
            :base("SkyLordDb")
        {          
        }

        public virtual DbSet<Profil> Profils { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<World> Worlds { get; set; }
        public virtual DbSet<Apprentice> Apprentices { get; set; }
        public virtual DbSet<Army> Armys { get; set; }
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
    }
}
