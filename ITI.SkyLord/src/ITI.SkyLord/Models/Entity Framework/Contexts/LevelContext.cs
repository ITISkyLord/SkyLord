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
    public class LevelContext : CustomContext
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

        public override DbSet<Player> Players { get; set; }
        public override DbSet<Island> Islands { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<BuildingLevel> BuildingLevels { get; set; }
        public DbSet<TechnologyLevel> TechnologyLevels { get; set; }
        public DbSet<FieldLevel> FieldLevels { get; set; }
        public DbSet<Requirement> Requirements { get; set; }

    }
}
