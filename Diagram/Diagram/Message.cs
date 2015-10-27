using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Message
    {
    //    private int _id;
        private readonly string _messageObject;
        private readonly string _coreMessage;
        private readonly Player _receiver;
        private readonly Player _sender; // À voir si l'admin doit lancer un message à tout le monde.
        // Lu ou pas lu

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
        public string MessageObject
        {
            get
            {
                return _messageObject;
            }
        }

        public string CoreMessage
        {
            get
            {
                return _coreMessage;
            }
        }

        public Player Receiver
        {
            get
            {
                return _receiver;
            }
        }

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