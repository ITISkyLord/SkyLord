using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Message
    {
        #region Properties                          
        [Key]
        public long MessageId { get; set; }

        public string MessageObject { get; set; }

        public string CoreMessage { get; set; }

        public Player Receiver { get; set; }

        public Player Sender { get; set; }
        public bool Read { get; set; }
        #endregion
    }
}