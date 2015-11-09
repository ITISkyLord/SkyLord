using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageContext
{
   public class Message
    {
        public long MessageId { get; set; }
        public string MessageObject { get; set; }
        public string CoreMessage { get; set; }
        public PlayerForMessageContext Receiver { get; set; }
        public PlayerForMessageContext Sender { get; set; }
    }
}
