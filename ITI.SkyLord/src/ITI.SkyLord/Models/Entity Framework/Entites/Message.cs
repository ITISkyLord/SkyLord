using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Message
    {
        private string _messageObject;
        private string _coreMessage;
        private Player _receiver;
        private Player _sender; // À voir si l'admin doit lancer un message à tout le monde.
 
        /// <summary>
        /// Create new message.
        /// </summary>
        /// <param name="receiver">The receiver of the message.</param>
        /// <param name="sender">The sender of the message.</param>
        /// <param name="messageObject">The object of the message.</param>
        /// <param name="coreMessage">The message.</param>
        public Message( Player receiver, Player sender, string messageObject, string coreMessage )
        {
            if( receiver == null ) throw new ArgumentNullException( "Receiver is null" );
            if( sender == null ) throw new ArgumentNullException( "sender is null" );

            if( String.IsNullOrWhiteSpace( messageObject ) )
            {
                messageObject = "[Aucun objet]";
            }
            this.MessageObject = messageObject;
            this.CoreMessage = coreMessage;
            this.Receiver = receiver;
            this.Sender = sender;
        }
        #region Properties                          
        [Key]
        public long MessageId { get; set; }

        public string MessageObject
        {
            get
            {
                return _messageObject;
            }

            set
            {
                _messageObject = value;
            }
        }

        public string CoreMessage
        {
            get
            {
                return _coreMessage;
            }

            set
            {
                _coreMessage = value;
            }
        }

        public Player Receiver
        {
            get
            {
                return _receiver;
            }

            set
            {
                _receiver = value;
            }
        }

        public Player Sender
        {
            get
            {
                return _sender;
            }

            set
            {
                _sender = value;
            }
        }


        #endregion
    }
}