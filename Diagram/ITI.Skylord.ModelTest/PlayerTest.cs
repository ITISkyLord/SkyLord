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
    class PlayerTest
    {
        [Test]
        public void Create_new_player()
        {
            Player _player = new Player();
            Assert.IsNotNull( _player );
        }
    }
}
