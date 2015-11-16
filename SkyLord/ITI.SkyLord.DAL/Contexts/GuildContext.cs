using ITI.SkyLord.DAL.GuildEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts
{
    public partial class GuildContext : DbContext
    {
        public GuildContext()
            :base("SkyLordDB")
        { }
        DbSet<Guild> Guilds { get; set; }
        DbSet<GuildRole> GuildRoles { get; set; }

    }
}
