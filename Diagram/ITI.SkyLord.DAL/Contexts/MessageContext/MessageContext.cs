using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageContext
{
    public partial class MessageContext : DbContext
    {
        public MessageContext()
            :base("SkyLordDb")
        { }
        

        public DbSet<Message> Messages { get; set; }

        public DbSet<PlayerForMessageContext> Players { get; set; }

    }
}
