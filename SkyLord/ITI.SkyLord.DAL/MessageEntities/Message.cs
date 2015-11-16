using ITI.SkyLord.DAL.PlayerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.MessageEntities
{
    public class Message
    {
        public long MessageId { get; set; }
        public string MessageObject { get; set; }
        public string CoreMessage { get; set; }
        public Player Receiver { get; set; }
        public Player Sender { get; set; }
    }
}
