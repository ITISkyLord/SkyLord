using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.ViewModel;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class IslandContext : IdentityDbContext, IStandardContext
    {
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

        public void FillStandardVM( StandardViewModel vm, long playerId, long islandId )
        {
            vm.Layout = new LayoutViewModel();

            vm.Layout.AllIslands = Islands.Include( i => i.Owner).Where( i => i.Owner.PlayerId == playerId ).ToList();
            vm.Layout.CurrentIsland = GetIsland( islandId, playerId );
            vm.Layout.CurrentPlayer = Players.Single( p => p.PlayerId == playerId );
            vm.Layout.IslandId = islandId;
        }

        private Island GetIsland( long islandId, long playerId )
        {
            if ( islandId == 0 )
            {
                long activePlayerId = playerId;
                return Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId );
            }
            else
            {
                long activePlayerId = playerId;
                return Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IslandId == islandId && i.Owner.PlayerId == activePlayerId );
            }

        }
        public DbSet<Island> Islands { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
