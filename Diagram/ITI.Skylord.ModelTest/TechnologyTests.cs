using Diagram;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class TechnologyTests
    {
        [Test]
        public void create_technology()
        {
            Technology techno = new Technology("Info");
            Assert.IsNotNull( techno.Name );
            Assert.IsNotNull( techno.Level );
        }

        [Test]
        public void create_technoogy_with_null_or_white_space_name_throws_argumentException()
        {
            Assert.Throws<ArgumentException>( () => new Technology( "" ) );
            Assert.Throws<ArgumentException>( () => new Technology( null ) );
        }
    }
}
