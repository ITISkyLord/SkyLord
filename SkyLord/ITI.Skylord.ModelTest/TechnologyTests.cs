using ITI.SkyLord;
using ITI.SkyLord.Model;
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
        public void Create_technology()
        {
            Technology technology = new Technology("Info");
            Assert.IsNotNull( technology.Name );
            Assert.IsNotNull( technology.Level );
        }

        [Test]
        public void Create_technology_with_null_or_white_space_name_throws_argumentException()
        {
            Assert.Throws<ArgumentException>( () => new Technology( "" ) );
            Assert.Throws<ArgumentException>( () => new Technology( null ) );
        }
    }
}
