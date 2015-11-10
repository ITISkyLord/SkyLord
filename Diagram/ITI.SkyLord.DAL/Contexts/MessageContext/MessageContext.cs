using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<Players> Players { get; set; }

    }
}
