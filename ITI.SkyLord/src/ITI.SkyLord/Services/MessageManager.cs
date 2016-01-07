using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ITI.SkyLord
{
    public class MessageManager
    {
        private MessageContext messageContext { get; }

        public MessageManager(MessageContext mc)
        {
            messageContext = mc;
        }

        public bool SendMessage(Player sender, Player receiver, string CoreMessage, string ObjectMessage, bool read)
        {
            bool success = false;

            if (String.IsNullOrWhiteSpace(CoreMessage) || String.IsNullOrWhiteSpace(ObjectMessage) || String.IsNullOrEmpty(CoreMessage) || String.IsNullOrEmpty(ObjectMessage))
            {
                Console.WriteLine("Vos données sont incorrectes.");
            }
            else
            {
                Message message = new Message { Receiver = receiver, Sender = sender, CoreMessage = CoreMessage, MessageObject = ObjectMessage, Read = read };
                messageContext.Add(message);
                success = true;
            }
            return success;
        }

        public IList<Message> GetAllMessage(Player player)
        {
            List<Message> listMessages = messageContext.Messages.Where(m => m.Receiver.PlayerId == player.PlayerId).ToList();
            return listMessages;
        }

        public IList<Message> GetAllMessageSent(Player player)
        {
            List<Message> listMessagesSend = messageContext.Messages.Where(m => m.Sender.PlayerId == player.PlayerId).ToList();
            return listMessagesSend;
        }

        public bool AnswerMessage(Message message, string coreMessage, Player sender)
        {
            bool succes = false;

            if (message != null && message.Receiver == sender)
            {
                SendMessage(message.Receiver, message.Sender, coreMessage, message.MessageObject, false);
                succes = true;
            }
            return succes;
        }

        public bool DeleteMessage(Message message)
        {
            bool success = false;

            messageContext.Remove(message);
            success = true;

            return success;
        }

        public bool DeleteMessage(long messageId)
        {
            Message message = messageContext.Messages.FirstOrDefault(m => m.MessageId == messageId);
            return DeleteMessage(message);
        }

        public IList<Message> GetAllUnreadMessage(Player player)
        {
            return GetAllMessagesWhereReadIs(false, player);
        }

        public IList<Message> GetAllMessageRead(Player player)
        {
            return GetAllMessagesWhereReadIs(true, player);
        }

        private IList<Message> GetAllMessagesWhereReadIs(bool value, Player player)
        {
            List<Message> listMessagesRead = messageContext.Messages.Where(m => m.Receiver.PlayerId == player.PlayerId).Where(m => m.Read == value).ToList();
            return listMessagesRead;
        }

        public Message ReadAMessage(long messageId)
        {
            Message message = messageContext.Messages.FirstOrDefault(m => m.MessageId == messageId);
            return message;
        }

    }
}
