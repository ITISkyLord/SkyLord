using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Configuration;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class PlayerContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Configure PlayerId as PK for Profile
            //builder.Entity<EProfil>()
            //    .HasKey(e => e.ProfilId);

            //// Configure PlayerId as FK for Profile
            //builder.Entity<EProfil>()
            //    .HasAlternateKey(p => p.Owner);
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
        public DbSet<Player> Players { get; set; }
        public DbSet<World> Worlds { get; set; }

        public void AddPlayer( Player p )
        {
            using( PlayerContext context = new PlayerContext() )
            {
                context.Add( p );
                context.Add( p.Profil );
                context.Add(GetWorld());
                context.SaveChanges();
            }
        }

        public World GetWorld()
        {
            using( PlayerContext context = new PlayerContext() )
            {
                return context.Worlds.FirstOrDefault();
            }
        }

        /// <summary>
        /// Check if a player with the same name, mail and password can be found in the database
        /// </summary>
        /// <param name="player">The player to check</param>
        /// <returns>True if the player is found, false if it is not</returns>
        public bool IsPlayerValid( string name, string password )
        {
            bool valid = false;
            Player playerFound;
            using ( this )
            {
                valid = this.Players.Any( p => p.Name == name
                && p.Profil.Password == password );
            }

            return valid;
        }

    }
}
