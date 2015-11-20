using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Message
    {
        private long _messageId;
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
            this._messageObject = messageObject;
            this._coreMessage = coreMessage;
            this._receiver = receiver;
            this._sender = sender;
        }
        #region Properties                          
        [Key]
        public long MessageId { get; set; }


        /// <summary>
        /// Gets the message object.
        /// </summary>
        public string MessageObject { get; set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string CoreMessage { get; set; }

        /// <summary>
        /// Gets the receiver of the message.
        /// </summary>
        public Player Receiver { get; set; }

        /// <summary>
        /// Gets the sender of the message.
        /// </summary>
        public Player Sender { get; set; }


        #endregion
    }
}