//using ITI.SkyLord.DAL.Contexts.MessageModel;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ITI.SkyLord.Ef.Test
//{
//    [TestFixture]
//    public class MessageTest
//    {
//        [Test]
//        public void createMessage()
//        {
//           Player testPlayer = new Player { Name = "Loic" };
//            Player testPlayer2 = new Player { Name = "Aizen" };
//            Message meg = new Message { ObjectMessage = "Coucou",
//                CoreMessage = "Hello bg",
//                Receiver = testPlayer,
//                Sender = testPlayer2 };

//            using (MessageContext context = new MessageContext())
//            {
//                context.Players.Add(testPlayer);
//                context.Players.Add(testPlayer2);
//                context.Messages.Add(meg);
//                context.SaveChanges();
//            }
//            Console.WriteLine("Message Saved");
//        }
//    }
//}
