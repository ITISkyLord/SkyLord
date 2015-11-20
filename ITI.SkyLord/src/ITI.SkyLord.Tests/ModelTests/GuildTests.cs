using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ITI.SkyLord;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class GuildTests
    {
        [Test]
        public void Create_guild()
        {
            Guild guild = new Guild( "Les manchots" );
            Assert.IsNotNull( guild.Name );
        }

        [Test]
        public void Create_guild_with_null_or_white_space_name_throws_argumentException()
        {
            Assert.Throws<ArgumentException>( () => new Guild( "" ) );
            Assert.Throws<ArgumentException>( () => new Guild( null ) );
        }
    }
}
