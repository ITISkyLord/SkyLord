
using ITI.SkyLord;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class CombatResultTests
    {
        Island _island;
        Island _island2;

        public CombatResultTests()
        {
            _island = new Island( "TestIsland", new Coordinate(), true );
            _island2 = new Island( "TestIsland2", new Coordinate( 1, 1 ), true );
        }

        [Test]
        public void Create_new_combatResult()
        {
            Army army = new Army( ArmyState.defense, _island );
            Army army2 = new Army( ArmyState.movement, _island2 );

            CombatResult combatResult = new CombatResult( army, army2 );

            Assert.That( combatResult.WinningArmy == army && combatResult.LoosingArmy == army2 );
        }

        [Test]
        public void Contructor_throws_ArgumentNullException_if_armies_are_null()
        {
            Army army = null;

            Assert.Throws<ArgumentNullException>( () => new CombatResult( new Army( ArmyState.defense, _island ), army ) );
            Assert.Throws<ArgumentNullException>( () => new CombatResult( army, new Army( ArmyState.defense, _island ) ) );
        }

        // TODO : gestion du pillage de ressources
        //[Test]
        //public void get_pillaged_ressources_works()
        //{
        //    Army army = new Army( ArmyState.defense, _island );
        //    Army army2 = new Army( ArmyState.movement, _island2 );

        //    CombatResult combatResult = new CombatResult( army, army2 );

        //    Assert.That( combatResult.PillagedRessources.Metal == 100 && combatResult.PillagedRessources.Wood == 200 );
        //}
    }
}
