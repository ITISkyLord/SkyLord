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
        //[TestCase("Jesuisunmessagedetest", "Coucou", true )]
        [TestCase("", "Coucou", false )]
        [TestCase("Jesuisunmessagedetest", "", false )]
        [TestCase(" ", "Coucou", false )]
        [TestCase("Jesuisunmessagedetest", " ", false )]
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
                Assert.That(mc.Messages.Where(m => m.CoreMessage == coreMessage && m.MessageObject == messageObject).First() != null);
                
            }
            finally
            {
                var t = mc.Messages.Where(m => m.CoreMessage == coreMessage).FirstOrDefault();
                if (t != null) mc.Messages.Remove(t);
                mc.SaveChanges();
                mc.Dispose();
            }
        }



    }


}