using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.Skylord;
using ITI.SkyLord.Model;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class WorldTest
    {
        [Test]
        public void Create_new_world()
        {
            World world = new World();
            Assert.IsNotNull( world );
        }

        [Test]
        public void Create_new_world_has_a_map()
        {
            World world = new World();
            Assert.IsNotNull( world.Map );
        }
    }
}
