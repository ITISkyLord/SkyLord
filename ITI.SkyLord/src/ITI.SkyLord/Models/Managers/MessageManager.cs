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

            if (String.IsNullOrWhiteSpace(CoreMessage) || String.IsNullOrWhiteSpace(ObjectMessage) || String.IsNullOrEmpty(CoreMessage) || String.IsNullOrEmpty(ObjectMessage))
            {
                Console.WriteLine("Vos données sont incorrectes.");
            }
            else
            {
                Message message = new Message { Receiver = receiver, Sender = sender, CoreMessage = CoreMessage, MessageObject = ObjectMessage };
                messageContext.Add(message);
                messageContext.SaveChanges();
                success = true;
            }
            return success;
        }

        public IList<Message> GetAllMessage(Player player)
        {
            List<Message> listMessages = messageContext.Messages.Where(m => m.Receiver.PlayerId == player.PlayerId).ToList();
            return listMessages;
        }

        public IList<Message> GetAllMessageSend(Player player)
        {
            List<Message> listMessagesSend = messageContext.Messages.Where(m => m.Sender.PlayerId == player.PlayerId).ToList();
            return listMessagesSend;
        }
        public bool AnswerMessage(Message message, string CoreMessage)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMessage(Message message)
        {
            bool success = false;

            messageContext.Remove(message);
            messageContext.SaveChanges();
            success = true;

            return success;
        }

        public bool DeleteMessage(long messageId)
        {
            bool success = false;

            Message message = messageContext.Messages.FirstOrDefault(m => m.MessageId == messageId);
            messageContext.Remove(message);
            messageContext.SaveChanges();
            success = true;

            return success;
        }

        public IList<Message> GetAllUnreadMessage(Player player)
        {
            List<Message> listMessagesUnread = messageContext.Messages.Where(m => m.Receiver.PlayerId == player.PlayerId).Where(m => m.Read == false).ToList();
            return listMessagesUnread;
        }

        public IList<Message> GetAllMessageRead(Player player)
        {
            List<Message> listMessagesRead = messageContext.Messages.Where(m => m.Receiver.PlayerId == player.PlayerId).Where(m => m.Read == true).ToList();
            return listMessagesRead;
        }
    }
}
