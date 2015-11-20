using ITI.SkyLord.Models.Entity_Framework.Entites;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class SetupContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appEnv = CallContextServiceLocator.Locator.ServiceProvider
                            .GetRequiredService<IApplicationEnvironment>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet5-ITI.SkyLord-95eb6eea-d6f6-4f75-a574-ef819fbbd729;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<EProfil> Profils { get; set; }
        public DbSet<EPlayer> Players { get; set; }
        public DbSet<EWorld> Worlds { get; set; }
    }
}
