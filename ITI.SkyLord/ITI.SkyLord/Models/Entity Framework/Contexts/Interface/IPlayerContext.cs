using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface IPlayerContext : ICustomContext
    {
        Player GetPlayer(string userId);

        DbSet<Profil> Profils { get; set; }
        DbSet<World> Worlds { get; set; }
        DbSet<User_Player> User_Players { get; set; }

        World GetWorld();
        Player FindPlayer(long playerId);
        Player FindPlayer(string playerMail);

    }
}
