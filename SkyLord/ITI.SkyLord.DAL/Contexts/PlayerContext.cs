using ITI.SkyLord.DAL.PlayerEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts
{
    public partial class PlayerContext : DbContext
    {
        public PlayerContext()
            : base("SkyLordDB"){ }
        
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Player> Players { get; set; } 
    }
}
