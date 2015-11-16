using ITI.SkyLord.DAL.WorldEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts
{
   public partial  class WorldContext : DbContext
    {
        public WorldContext()
            :base("SkyLordDB")
        { }

       public DbSet<World> Worlds { get; set; }
        public DbSet<Map> Maps { get; set; }
    }
}
