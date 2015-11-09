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
        Island _defaultIsland;
        World _world;

        public PlayerTest()
        {
            _world = new World();
            _world.addNewIsland( new Coordinate() );
            _defaultIsland = _world.Map.Islands.First();
        }

        [Test]
        public void Create_new_player()
        {
            Player _player = new Player( _world, "Thanur" );
            Assert.IsNotNull( _player );
        }

        [Test]
        public void Create_new_player_with_name_more_than_50characters_throws_outOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => new Player( _world, "VariableNameQuiEstLePrénomAvecPlusDeCinquanteCaractèresDeLongueur" ));
        }
        [Test]
        public void Create_new_player_with_password_more_than_50characters_throws_outOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => new Player( _world, "VariableNameQuiEstLePrénom", "toto@gmail.com", "VariablePasswordQuiEstLeMotDePasseAvecPlusDeSoizanteQuinzeCaractèresDeLongueur" ) );
        }
        [Test]
        public void Create_new_player_with_invalid_email_throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => new Player( _world, "VariableNameQuiEstLePrénom", "toto" ) );
        }
        [Test]
        public void Create_new_player_with_null_or_whiteSpaceName_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>( () => new Player( _world, " " ) );
        }
        [Test]
        public void Create_new_player_with_null_or_whiteSpacePassword_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>( () => new Player( _world, " Thanur ", "toto@gmail.com", " " ) );
        }
        [Test]
        public void Add_an_island_on_a_player_works()
        {
            Player _player = new Player( _world,"Thanur" );
            _player.Islands.Add( _defaultIsland );
            Assert.That( _player.Islands.Contains( _defaultIsland ) );
        }

    }
}
