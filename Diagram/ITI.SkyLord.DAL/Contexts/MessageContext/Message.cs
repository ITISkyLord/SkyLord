using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMappedAttribute]
        public Players Receiver { get; set; }
        [NotMappedAttribute]
        public Players Sender { get; set; }
    }
}
