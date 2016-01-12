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

        //[Test]
        //[TestCase("Jesuisunmessagedetest", "Coucou", true)]
        //[TestCase("", "Coucou", false)]
        //[TestCase("Jesuisunmessagedetest", "", false)]
        //[TestCase(" ", "Coucou", false)]
        //[TestCase("Jesuisunmessagedetest", " ", false)]
        //[TestCase("  ", "Coucou", false)]
        //[TestCase("Jesuisunmessagedetest", "  ", false)]
        //public void SendMessage(string coreMessage, string messageObject, bool returnSendMessageExpected)
        //{
        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);

        //    try
        //    {
        //        Player sender = mc.Players.First(p => p.Name == "LoicD");
        //        Player receiver = mc.Players.First(a => a.Name == "Spi");

        //        var result = mm.SendMessage(sender, receiver, coreMessage, messageObject, returnSendMessageExpected);
        //        mc.SaveChanges();

        //        Assert.That(result == returnSendMessageExpected);
        //        if (returnSendMessageExpected == true) Assert.That(mc.Messages.Where(m => m.CoreMessage == coreMessage && m.MessageObject == messageObject).First() != null);

        //    }
        //    finally
        //    {
        //        if (returnSendMessageExpected == true)
        //        {
        //            var t = mc.Messages.Where(m => m.CoreMessage == coreMessage).FirstOrDefault();
        //            if (t != null) mc.Messages.Remove(t);
        //            mc.SaveChanges();
        //            mc.Dispose();
        //        }

        //        else mc.Dispose();
        //    }
        //}

        //[Test]
        //public void DeleteMessage()
        //{
        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);
        //    string coreMessage = "Test";
        //    string messageObject = "Test";

        //    Player sender = mc.Players.First(p => p.Name == "LoicD");
        //    Player receiver = mc.Players.First(a => a.Name == "Spi");

        //    mm.SendMessage(sender, receiver, coreMessage, messageObject, false);
        //    mc.SaveChanges();
        //    Message message = mc.Messages.FirstOrDefault(m => m.MessageObject == messageObject);

        //    Assert.That(mm.DeleteMessage(message) == true);
        //    mc.SaveChanges();
        //}

        //[Test]
        //public void GetAllUnreadMessage()
        //{
        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);

        //    string messageObject = "Nouvelle";
        //    string coreMessage = "OH";
        //    string coreMessage2 = "HA";

        //    // Create the Message
        //    Player otherPlayer = mc.Players.First(p => p.Name == "Spi");
        //    Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage, messageObject, false);
        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage2, messageObject, false);
        //    mc.SaveChanges();

        //    Message message = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage);
        //    Message message2 = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage2);
        //    var allMessages = mm.GetAllUnreadMessage(currentPLayer);

        //    foreach (Message msg in allMessages)
        //    {
        //        Console.WriteLine("Objet : {0}, Message : {1}, Envoyeur : {2}, Receveur : {3} ", msg.MessageObject, msg.CoreMessage, msg.Sender.Name, msg.Receiver.Name);
        //    }

        //    Assert.That(allMessages.Count() != 0);

        //    mm.DeleteMessage(message);
        //    mm.DeleteMessage(message2);
        //    mc.SaveChanges();
        //}

        //[Test]
        //public void GetAllMessageRead()
        //{
        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);

        //    string messageObject = "Nouvelle";
        //    string coreMessage = "Yo";
        //    string coreMessage2 = "WOO";

        //    // Create the Message
        //    Player otherPlayer = mc.Players.First(p => p.Name == "Spi");
        //    Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage, messageObject, true);
        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage2, messageObject, true);
        //    mc.SaveChanges();

        //    Message message = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage);
        //    Message message2 = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage2);

        //    var messageRead = mm.GetAllMessageRead(currentPLayer);

        //    foreach (Message msg in messageRead)
        //    {
        //        Console.WriteLine("Objet : {0}, Message : {1}, Envoyeur : {2}, Receveur : {3} ", msg.MessageObject, msg.CoreMessage, msg.Sender.Name, msg.Receiver.Name);
        //    }

        //    Assert.That(messageRead.Count() != 0);

        //    mm.DeleteMessage(message);
        //    mm.DeleteMessage(message2);
        //    mc.SaveChanges();
        //}

        //[Test]
        //public void GetAllMessage()
        //{
        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);
        //    string messageObject = "Nouvelle";
        //    string coreMessage = "Zozo";
        //    string coreMessage2 = "tata";

        //    // Create the Message
        //    Player otherPlayer = mc.Players.First(p => p.Name == "Spi");
        //    Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage, messageObject, true);
        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage2, messageObject, false);
        //    mc.SaveChanges();

        //    Message message = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage);
        //    Message message2 = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage2);

        //    var allMessage = mm.GetAllMessage(currentPLayer);

        //    foreach (Message msg in allMessage)
        //    {
        //        Console.WriteLine("Objet : {0}, Message : {1}, Envoyeur : {2}, Receveur : {3} ", msg.MessageObject, msg.CoreMessage, msg.Sender.Name, msg.Receiver.Name);
        //    }

        //    Assert.That(allMessage.Count() != 0);

        //    mm.DeleteMessage(message);
        //    mm.DeleteMessage(message2);
        //    mc.SaveChanges();
        //}

        //[Test]
        //public void GetAllMessageSent()
        //{
        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);
        //    string messageObject = "One piece";
        //    string coreMessage = "Boom";
        //    string coreMessage2 = "Money for nothing";

        //    // Create the Message
        //    Player otherPlayer = mc.Players.First(p => p.Name == "Spi");
        //    Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

        //    mm.SendMessage(currentPLayer, otherPlayer, coreMessage, messageObject, true);
        //    mm.SendMessage(currentPLayer, otherPlayer, coreMessage2, messageObject, true);
        //    mc.SaveChanges();

        //    Message message = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage);
        //    Message message2 = mc.Messages.FirstOrDefault(m => m.CoreMessage == coreMessage2);

        //    var allMessageSent = mm.GetAllMessageSent(currentPLayer);

        //    foreach (Message msg in allMessageSent)
        //    {
        //        Console.WriteLine("Objet : {0}, Message : {1}, Envoyeur : {2}, Receveur : {3} ", msg.MessageObject, msg.CoreMessage, msg.Sender.Name, msg.Receiver.Name);
        //    }

        //    Assert.That(allMessageSent.Count() != 0);

        //    mm.DeleteMessage(message);
        //    mm.DeleteMessage(message2);
        //    mc.SaveChanges();
        //}

        //[Test]
        //public void AnswerMessage()
        //{
        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);
        //    string answercoreMessage = "Band bang bang bang";
        //    string coreMessage = "Lucious";
        //    string messageObject = "Cookie";

        //    // Create the Message
        //    Player otherPlayer = mc.Players.First(p => p.Name == "Spi");
        //    Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage, messageObject, true);
        //    mc.SaveChanges();
        //    Message message = mc.Messages.FirstOrDefault(m => m.MessageObject == messageObject);

        //    Assert.That(mm.AnswerMessage(message, answercoreMessage, currentPLayer) == true);
        //    mc.SaveChanges();

        //    Message answer = mc.Messages.First(m => m.CoreMessage == answercoreMessage);
        //    Assert.That(answer.Receiver.PlayerId == message.Sender.PlayerId && answer.Sender.PlayerId == message.Receiver.PlayerId);

        //    mm.DeleteMessage(message);
        //    mm.DeleteMessage(answer);
        //    mc.SaveChanges();
        //}

        //[Test]
        //public void ReadAMessage()
        //{

        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);

        //    string coreMessage = "goku";
        //    string messageObject = "Empire";

        //    // Create the Message
        //    Player otherPlayer = mc.Players.First(p => p.Name == "Spi");
        //    Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage, messageObject, true);
        //    mc.SaveChanges();
        //    Message message = mc.Messages.FirstOrDefault(m => m.MessageObject == messageObject);

        //    Message msg = mm.ReadAMessage(message.MessageId);

        //    Assert.NotNull(msg);

        //    mm.DeleteMessage(msg);
        //    mc.SaveChanges();
        //}

        //[Test]
        //public void DeleteWithIntParameter()
        //{

        //    MessageContext mc = new MessageContext();
        //    MessageManager mm = new MessageManager(mc);

        //    string coreMessage = "saitama";
        //    string messageObject = "yeah";

        //    // Create the Message
        //    Player otherPlayer = mc.Players.First(p => p.Name == "Spi");
        //    Player currentPLayer = mc.Players.First(a => a.Name == "LoicD");

        //    mm.SendMessage(otherPlayer, currentPLayer, coreMessage, messageObject, true);
        //    mc.SaveChanges();
        //    Message message = mc.Messages.FirstOrDefault(m => m.MessageObject == messageObject);

        //    Assert.That(mm.DeleteMessage(message.MessageId) == true);
        //    mc.SaveChanges();
        //}

    }
}