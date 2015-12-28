using System;
using ITI.SkyLord.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public abstract class CustomContext : IdentityDbContext, IStandardContext
    {
        public virtual DbSet<Island> Islands { get; set; }

        public virtual DbSet<Player> Players { get; set; }


        public void FillStandardVM( StandardViewModel svm, long playerId, long islandId )
        {
            svm.Layout = new LayoutViewModel();

            svm.Layout.AllIslands = Islands.Include( i => i.Owner ).Where( i => i.Owner.PlayerId == playerId ).ToList();
            svm.Layout.CurrentIsland = GetIsland( islandId, playerId );
            svm.Layout.CurrentPlayer = Players.Single( p => p.PlayerId == playerId );
            svm.Layout.IslandId = islandId;
        }

        public Player GetPlayer( string userId )
        {
            return Players.Where( pl => pl.UserPlayer.User.Id == userId ).First();
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
    }
}