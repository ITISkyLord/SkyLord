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
    public class PlayerContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Entity<Player>()
            //    .HasOne( p => p.Profil )
            //    .WithOne( pr => pr.Player )
            //    .OnDelete( DeleteBehavior.Cascade );
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
        public Player GetPlayer(string userId)
        {
            return Players.Where( pl => pl.UserPlayer.User.Id == userId ).First();
          //  return Players.Include( p => p.Profil ).FirstOrDefault( p => p.PlayerId == playerId );

        }
        public DbSet<Profil> Profils { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<User_Player> User_Players { get; set; }

        public void AddPlayer( Player p )
        {
            p.Profil = new Profil { Description = "Aucune description." };
            Add( p );
            Add( p.Profil );
            SaveChanges();
        }

        public World GetWorld()
        {
             return this.Worlds.FirstOrDefault();
        }

        public bool RemovePlayer ( long playerId )
        {
            Player playerFound = FindPlayer( playerId );
            if ( playerFound == null ) return false;

            Profils.Remove( playerFound.Profil );
            Players.Remove( playerFound );
            SaveChanges();

            if ( FindPlayer( playerId ) == null ) return true;
            else return false;
        }

        public Player FindPlayer( long playerId )
        {
            return Players.Include( p => p.Profil).FirstOrDefault( p => p.PlayerId == playerId );
        }

        public Player FindPlayer( string playerMail )
        {
            return Players.FirstOrDefault( p => p.Mail == playerMail );
        }

        /// <summary>
        /// Check if a player with the same name, mail and password can be found in the database
        /// </summary>
        /// <param name="player">The player to check</param>
        /// <returns>True if the player is found, false if it is not</returns>
        public bool IsPlayerValid( string mail, string password )
        {
            bool valid = false;

            valid = this.Players.Any( p => p.Mail == mail
            && p.Password == password );

            return valid;
        }

    }
}
