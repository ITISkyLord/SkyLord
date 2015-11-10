using ITI.SkyLord.DAL.Contexts.MessageContext;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.Ef.Test
{
    [TestFixture]
    public class MessageTest
    {
        [Test]
        public void createMessage()
        {
            Players testPlayer = new Players {PlayerId = 541, Name = "Loic"};
            Players testPlayer2 = new Players {PlayerId = 448, Name = "Aizen"};
            Message meg = new Message
            {
                MessageObject = "Coucou",
                CoreMessage = "Hello bg",
                Receiver = testPlayer,
                Sender = testPlayer2
            };

            using (MessageContext context = new MessageContext())
            {
                context.Players.Add(testPlayer);
                context.Players.Add(testPlayer2);
                context.Messages.Add(meg);
                context.SaveChanges();
            }
            Console.WriteLine("Message Saved");
        }
    }
}
