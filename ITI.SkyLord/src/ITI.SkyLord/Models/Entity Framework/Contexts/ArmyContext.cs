using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class ArmyContext : IdentityDbContext
    {
        public IConfigurationRoot Configuration { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile( "appsettings.json" );

            Configuration = builder.Build();
            var appEnv = CallContextServiceLocator.Locator.ServiceProvider
                            .GetRequiredService<IApplicationEnvironment>();
            optionsBuilder.UseSqlServer( Configuration[ "Data:DefaultConnection:ConnectionString" ] );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Army>()
                .HasMany(a => a.Regiments)
                .WithOne(r => r.Army)
                .HasForeignKey( r => r.ArmyId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Army> Armies { get; set; }
        public DbSet<Island> Islands { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Regiment> Regiments { get; set; }
        public DbSet<UnitStatistics> UnitStatistics { get; set; }

        /// <summary>
        /// Adds a unit to the current island's army. Creates or updates the army and regiment accordingly.
        /// </summary>
        /// <param name="unit">The type of unit to add</param>
        /// <param name="number">The number of units to add</param>
        /// <param name="island">The current island</param>
        public void AddUnit( Unit unit, int number, Island island )
        {
            Island islandFound = Islands.Include( i => i.Armies).SingleOrDefault( i => i.IslandId == island.IslandId );
            if ( islandFound == null ) throw new ArgumentException( "The island does not exist in the Database" );

            Army armyFound = islandFound.Armies.Where( a => a.ArmyState == ArmyState.defense ).SingleOrDefault();

            if( armyFound == null )
            {
                Army newArmy = new Army
                {
                    ArmyState = ArmyState.defense,
                    Island = islandFound
                };
                Armies.Add( newArmy );
                SaveChanges();

                armyFound = newArmy;
            }

            if (armyFound.Regiments == null)
            {
                armyFound.Regiments = new List<Regiment>();
            }

            Regiment regimentFound = armyFound.Regiments.FirstOrDefault( r => r.Unit.UnitId == unit.UnitId );
            if( regimentFound == null )
            {
                Regiment newRegiment = new Regiment
                {
                    Unit = Units.SingleOrDefault( u => u.UnitId == unit.UnitId ),
                    Number = number,
                    ArmyId = armyFound.ArmyId
                };
                Regiments.Add( newRegiment );

                SaveChanges();
            }
            else
            {
                regimentFound.Number += number;
                SaveChanges();
            }
        }

        public EntityEntry Remove( Unit unit )
        {
            base.Remove( unit );
            SaveChanges();
            base.Remove( unit.UnitCost );
            return base.Remove( unit.UnitStatistics );
        }

        //public EntityEntry Remove( Army army )
        //{
        //    if(army.Regiments == null)
        //        return base.Remove( army );
        //    else
        //    {
        //        base.Remove( army );
        //        SaveChanges();
        //        return base.Remove( army.Regiments );
        //    }
        //}
    }
}
