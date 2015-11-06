using ITI.SkyLord.DAL.Contexts.GameModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageModel
{
   public partial class MessageContext : DbContext
    {
        public MessageContext()
            :base("SkyLordDb")
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Player> Players { get; set; }
    }
}
