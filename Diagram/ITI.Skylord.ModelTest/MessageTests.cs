using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Diagram;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class MessageTests
    {
        [Test]
        public void Create_new_message()
        {
            Message message = new Message(new Player("Thanur"), new Player("Cora"), "Introduction","Voilà l'introduction que tu voulais. Bisous");
            Assert.That( message.MessageObject == "Introduction" );
        }
    }
}
