using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.SkyLord;
using ITI.SkyLord.Model;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class MessageTests
    {
        World _world = new World();
        [Test]
        public void Create_new_message()
        {
            Message message = new Message(new Player(_world,"Thanur"), new Player(_world,"Cora"), "Introduction","Voilà l'introduction que tu voulais. Bisous");
            Assert.That( message.MessageObject == "Introduction" );
        }
    }
}
