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
        public virtual DbSet<User_Player> User_Players { get; set; }

        public void FillStandardVM( StandardViewModel svm, long playerId, long islandId )
        {
            svm.Layout = new LayoutViewModel();

            svm.Layout.AllIslands = Islands.Include( i => i.Owner ).Where( i => i.Owner.PlayerId == playerId ).ToList();
            svm.Layout.CurrentIsland = GetIsland( islandId, playerId );
            svm.Layout.CurrentPlayer = Players.Single( p => p.PlayerId == playerId );
            svm.Layout.IslandId = islandId;
        }
        public void FillStandardVM(StandardViewModel svm, long playerId)
        {
            svm.Layout = new LayoutViewModel();
            svm.Layout.AllIslands = Islands.Include(i => i.Owner).Where(i => i.Owner.PlayerId == playerId).ToList();
            svm.Layout.CurrentIsland = null;
            svm.Layout.CurrentPlayer = Players.Single(p => p.PlayerId == playerId);
            svm.Layout.IslandId = 0;
        }

        public Player GetPlayer( string userId )
        {
            return Players.Where( pl => pl.UserPlayer.User.Id == userId ).First();
        }

        public Island GetIsland( long islandId, long playerId )
        {
            ValidateIsland( islandId, playerId );

            if ( islandId == 0 )
            {
                return Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .Include( i => i.Buildings )
                    .ThenInclude( b => b.Level )
                    //.ThenInclude( r => r.Requirements )
                    .SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == playerId );
            }
            else
            {
                return Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .Include( i => i.Buildings )
                    .ThenInclude( b => b.Level )
                    //.ThenInclude( r => r.Requirements )
                    .SingleOrDefault( i => i.IslandId == islandId && i.Owner.PlayerId == playerId );
            }
        }

        public void ValidateIsland( long islandId, long playerId )
        {
            bool islandIsFound = false;
            if ( islandId == 0 )
            {
                islandIsFound = Islands.Any( i => i.IsCapital && i.Owner.PlayerId == playerId );
            }
            else
            {
                islandIsFound = Islands.Any( i => i.IslandId == islandId && i.Owner.PlayerId == playerId );
            }

            if( !islandIsFound )
            {
                throw new ArgumentException( "The islandId does not belong to the connected player !" );
            }
        }
    }
}