using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Managers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class MessageTest
    {

        [Test]
        [TestCase("Jesuisunmessagedetest", "Coucou", true)]
        [TestCase("", "Coucou", false)]
        [TestCase("Jesuisunmessagedetest", "", false)]
        [TestCase(" ", "Coucou", false)]
        [TestCase("Jesuisunmessagedetest", " ", false)]
        [TestCase("  ", "Coucou", false)]
        [TestCase("Jesuisunmessagedetest", "  ", false)]
        public void SendMessage(string coreMessage, string messageObject, bool returnSendMessageExpected)
        {
            MessageContext mc = new MessageContext();
            MessageManager mm = new MessageManager(mc);

            try
            {
                Player sender = mc.Players.First(p => p.Name == "LoicD");
                Player receiver = mc.Players.First(a => a.Name == "Spi");

                var result = mm.SendMessage(sender, receiver, coreMessage, messageObject);

                Assert.That(result == returnSendMessageExpected);
                if (returnSendMessageExpected == true) Assert.That(mc.Messages.Where(m => m.CoreMessage == coreMessage && m.MessageObject == messageObject).First() != null);

            }
            finally
            {
                if (returnSendMessageExpected == true)
                {
                    var t = mc.Messages.Where(m => m.CoreMessage == coreMessage).FirstOrDefault();
                    if (t != null) mc.Messages.Remove(t);
                    mc.SaveChanges();
                    mc.Dispose();
                }

                else mc.Dispose();
            }
        }
        [Test]
        public void DeleteMessage()
        {
            MessageContext mc = new MessageContext();
            MessageManager mm = new MessageManager(mc);

            Player sender = mc.Players.First(p => p.Name == "LoicD");
            Player receiver = mc.Players.First(a => a.Name == "Spi");

            Message message = new Message { Receiver = receiver, Sender = sender, CoreMessage = "Test", MessageObject = "Test" };
            mc.Add(message);
            mc.SaveChanges();

            Assert.That(mm.DeleteMessage(message) == true);
        }

        [Test]
        public void ReadAllUnreadMessage()
        {
            MessageContext mc = new MessageContext();
            MessageManager mm = new MessageManager(mc);

            // Create the Message
            Player sender = mc.Players.First(p => p.Name == "Spi");
            Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

            Message message = new Message { Receiver = currentPLayer, Sender = sender, CoreMessage = "Test", MessageObject = "Test" };
            Message message2 = new Message { Receiver = currentPLayer, Sender = sender, CoreMessage = "Hello it's me", MessageObject = "Nouvelle" };
            mc.Add(message);
            mc.Add(message2);
            mc.SaveChanges();

            var unreadMessage = mm.GetAllUnreadMessage(currentPLayer);

            foreach (Message msg in unreadMessage)
            {
                Console.WriteLine("Objet : {0}, Message : {1}, Envoyeur : {2}, Receveur : {3} ", msg.MessageObject, msg.CoreMessage, msg.Sender.Name, msg.Receiver.Name);
            }

            Assert.That(unreadMessage.Count() != 0);

            mm.DeleteMessage(message);
            mm.DeleteMessage(message2);
        }

        [Test]
        public void ReadAllMessageRead()
        {
            MessageContext mc = new MessageContext();
            MessageManager mm = new MessageManager(mc);

            // Create the Message
            Player sender = mc.Players.First(p => p.Name == "Spi");
            Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

            Message message = new Message { Receiver = currentPLayer, Sender = sender, CoreMessage = "Test", MessageObject = "Test", Read = true };
            Message message2 = new Message { Receiver = currentPLayer, Sender = sender, CoreMessage = "Hello it's me", MessageObject = "Nouvelle", Read = true };
            mc.Add(message);
            mc.Add(message2);
            mc.SaveChanges();

            var messageRead = mm.GetAllMessageRead(currentPLayer);

            foreach (Message msg in messageRead)
            {
                Console.WriteLine("Objet : {0}, Message : {1}, Envoyeur : {2}, Receveur : {3} ", msg.MessageObject, msg.CoreMessage, msg.Sender.Name, msg.Receiver.Name);
            }

            Assert.That(messageRead.Count() != 0);

            mm.DeleteMessage(message);
            mm.DeleteMessage(message2);
        }

        [Test]
        public void ReadAllMessage()
        {
            MessageContext mc = new MessageContext();
            MessageManager mm = new MessageManager(mc);

            // Create the Message
            Player sender = mc.Players.First(p => p.Name == "Spi");
            Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

            Message message = new Message { Receiver = currentPLayer, Sender = sender, CoreMessage = "Test", MessageObject = "Test", Read = true };
            Message message2 = new Message { Receiver = currentPLayer, Sender = sender, CoreMessage = "Hello it's me", MessageObject = "Nouvelle", Read = true };
            mc.Add(message);
            mc.Add(message2);
            mc.SaveChanges();

            var allMessage = mm.GetAllMessage(currentPLayer);

            foreach (Message msg in allMessage)
            {
                Console.WriteLine("Objet : {0}, Message : {1}, Envoyeur : {2}, Receveur : {3} ", msg.MessageObject, msg.CoreMessage, msg.Sender.Name, msg.Receiver.Name);
            }

            Assert.That(allMessage.Count() != 0);

            mm.DeleteMessage(message);
            mm.DeleteMessage(message2);
        }
    }
}