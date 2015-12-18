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
using ITI.SkyLord.ViewModel;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class ArmyContext : IdentityDbContext, IStandardContext
    {
        public void FillStandardVM( StandardViewModel vm, long playerId, long islandId = 0 )
        {
            vm.Layout.CurrentPlayer = Players.Single( p => p.PlayerId == playerId );
            vm.Layout.AllIslands = Islands.Where( i => i.Owner.PlayerId == playerId ).ToList();
            vm.Layout.CurrentIsland = Islands.Single( i => i.IslandId == islandId );
        }

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
                .HasMany( a => a.Regiments )
                .WithOne( r => r.Army )
                .HasForeignKey( r => r.ArmyId );
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Army> Armies { get; set; }
        public DbSet<Island> Islands { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Regiment> Regiments { get; set; }
        public DbSet<UnitStatistics> UnitStatistics { get; set; }

        public EntityEntry Remove( Unit unit )
        {
            base.Remove( unit );
            SaveChanges();
            base.Remove( unit.UnitCost );
            return base.Remove( unit.UnitStatistics );
        }

        //public EntityEntry Remove( Army army )
        //{
        //    if ( army.Regiments == null )
        //        return base.Remove( army );
        //    else
        //    {
        //        foreach(Regiment r in army.Regiments )
        //        {
        //            base.Remove( r );
        //        }
        //        SaveChanges();

        //        return base.Remove( army );
        //    }
        //}
    }
}
