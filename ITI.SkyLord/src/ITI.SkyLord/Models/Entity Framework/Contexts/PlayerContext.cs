using Microsoft.Data.Entity;
using System.Linq;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity.Metadata;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class PlayerContext : CustomContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public IConfigurationRoot Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var appEnv = CallContextServiceLocator.Locator.ServiceProvider
                            .GetRequiredService<IApplicationEnvironment>();
            optionsBuilder.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
        }
        
        public DbSet<Profil> Profils { get; set; }
        public override DbSet<Island> Islands { get; set; }
        public override DbSet<Player> Players { get; set; }
        public DbSet<World> Worlds { get; set; }
        public override DbSet<User_Player> User_Players { get; set; }

        public World GetWorld()
        {
             return this.Worlds.FirstOrDefault();
        }

        public Player FindPlayer( long playerId )
        {
            return Players.Include( p => p.Profil).FirstOrDefault( p => p.PlayerId == playerId );
        }

        public Player FindPlayer( string playerMail )
        {
            return Players.FirstOrDefault( p => p.Mail == playerMail );
        }

    }
}
