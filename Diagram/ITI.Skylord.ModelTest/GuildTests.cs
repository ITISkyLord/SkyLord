using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagram;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class GuildTests
    {
        [Test]
        public void create_guild()
        {
            Guild guild = new Guild( "Les manchots" );
            Assert.IsNotNull( guild.Name );
        }

        [Test]
        public void create_guild_with_null_or_white_space_name_throws_argumentException()
        {
            Assert.Throws<ArgumentException>( () => new Guild( "" ) );
            Assert.Throws<ArgumentException>( () => new Guild( null ) );
        }
    }
}
