using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageModel
{
   public class Messages
    {
        public long MessageId { get; set; }
        public string CoreMessage { get; set; }
        public string ObjectMessage { get; set; }

        public Players Receiver { get; set; }
        public Players Sender { get; set; } 
    }
}
