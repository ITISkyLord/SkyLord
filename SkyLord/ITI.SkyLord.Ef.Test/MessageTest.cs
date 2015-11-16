using ITI.SkyLord.DAL.Contexts;
using ITI.SkyLord.DAL.Contexts.MessageContext;
using ITI.SkyLord.DAL.MessageEntities;
using ITI.SkyLord.DAL.PlayerEntities;
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
            Profile p1 = new Profile { Mail = "tarfoullah" };
            Profile p2 = new Profile { Mail = "tarfoullahBis" };
            Player testPlayer = new Player {PlayerId = 541, Name = "Loic"};
            Player testPlayer2 = new Player {PlayerId = 448, Name = "Aizen"};
            Message msg = new Message
            {
                MessageObject = "Coucou",
                CoreMessage = "Hello bg",
                Receiver = testPlayer,
                Sender = testPlayer2
            };

            using (PlayerContext context = new PlayerContext())
            {
                testPlayer.Profil = p1;
                testPlayer2.Profil = p2;
                context.Players.Add(testPlayer);
                context.Players.Add(testPlayer2);
                context.SaveChanges();
            }
            using (MessageContext context = new MessageContext())
            {
                context.Messages.Add(msg);
                context.SaveChanges();
            }
            Console.WriteLine("Message Saved");
        }
    }
}
