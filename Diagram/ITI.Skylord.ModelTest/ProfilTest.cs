using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Diagram;
using ITI.SkyLord;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    public class ProfilTest
    {
        Player _defaultPlayer;
        World _world;
        public ProfilTest()
        {
            _world = new World();
            _defaultPlayer = new Player( _world, "Thanur" );
        }

        [Test]
        public void Create_new_profile()
        {
            Profil profil = new Profil( _defaultPlayer, "toto@gmail.com", "password" );
            Assert.IsNotNull( profil.Owner );
        }

        [Test]
        public void Create_profile_with_null_or_white_space_password_throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>( () => new Profil( _defaultPlayer, "toto@gmail.com", "" ) );
        }

        [Test]
        public void Create_profile_with_invalid_mail_throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => new Profil( _defaultPlayer, "toto", "password" ) );
        }

        [Test]
        public void Create_profile_with_null_player_throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>( () => new Profil( null, "toto@gmail.com", "" ) );

        }

        [Test]
        public void Edit_description_works()
        {
            Profil profil = new Profil( _defaultPlayer, "toto@gmail.com", "password" );
            profil.Description = "Bonjour, je cherche une guilde.\n À bientôt.";
            Assert.That( profil.Description == "Bonjour, je cherche une guilde.\n À bientôt." );

        }
    }
}
