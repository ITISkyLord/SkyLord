using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Message
    {
        private readonly string _messageObject;
        private readonly string _coreMessage;
        private readonly Player _receiver;
        private readonly Player _sender; // À voir si l'admin doit lancer un message à tout le monde.
        // Lu ou pas lu

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
        /// <summary>
        /// Gets the message object.
        /// </summary>
        public string MessageObject
        {
            get
            {
                return _messageObject;
            }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string CoreMessage
        {
            get
            {
                return _coreMessage;
            }
        }

        /// <summary>
        /// Gets the receiver of the message.
        /// </summary>
        public Player Receiver
        {
            get
            {
                return _receiver;
            }
        }

        /// <summary>
        /// Gets the sender of the message.
        /// </summary>
        public Player Sender
        {
            get
            {
                return _sender;
            }
        } 
        #endregion
    }
}