using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageModel
{
   public partial class Message
    {
        public long MessageId { get; set; }
        public string CoreMessage { get; set; }
        public string ObjectMessage { get; set; }

        public Player Receiver { get; set; }
        public Player Sender { get; set; } 
    }
}
