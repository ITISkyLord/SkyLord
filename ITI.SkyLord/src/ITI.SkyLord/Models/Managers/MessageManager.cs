using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ITI.SkyLord.Models.Managers
{
    public class MessageManager
    {
        private MessageContext messageContext { get; }
        public MessageManager(MessageContext mc)
        {
            messageContext = mc;
        }
        public bool SendMessage(Player sender, Player receiver, string CoreMessage, string ObjectMessage)
        {
            bool success = false;

            if(String.IsNullOrWhiteSpace(CoreMessage) || String.IsNullOrWhiteSpace(ObjectMessage) || String.IsNullOrEmpty(CoreMessage) || String.IsNullOrEmpty(ObjectMessage))
            {
                Console.WriteLine("Vos données sont incorrectes.");
                return success;
            }
            Message message = new Message { Receiver = receiver, Sender = sender, CoreMessage = CoreMessage, MessageObject = ObjectMessage };
            messageContext.Add(message);
            messageContext.SaveChanges();
            success = true;

            return success;
        }

        public IList<Message> GetAllMessage(Player player)
        {
            throw new NotImplementedException();
        }
        public bool AnswerMessage(Message message, string CoreMessage)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMessage(long messageId)
        {
            throw new NotImplementedException();
        }

        public IList<Message> GetAllUnreadMessage(Player player)
        {
            throw new NotImplementedException();

        }
    }
}
