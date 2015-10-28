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
    class ArmyTests
    {
        World _world;
        Player _defaultPlayer;
        Island _defaultIsland;

        public ArmyTests()
        {
            _world = new World();
            _defaultPlayer = new Player( "Thanur" );
            _world.addNewIsland( "Ivry", new Coordinate(), true );
            _defaultIsland = _world.Map.Islands.Values.First();

        }
   //     [Test]
        //public void Create_new_army()
        //{
        //    Army army = new Army(new List<Regiment>(), ArmyState.defense, _defaultIsland);
        //    Assert.IsNotNull( army.Regiments );
        //}

        //[Test]
        //public void Add_units_in_army()
        //{
        //    Diagram.Guard guard = new Diagram.Guard();
        //    Regiment regiment = new Regiment();
        //    regiment.Units.Add( guard.Name, 50 );
        //    List<Regiment> regiments = new List<Regiment>();
        //    regiments.Add( regiment );
        //    Army army = new Army( regiments, ArmyState.defense, _defaultIsland );
        //    Assert.IsNotNull( army.Regiments );
        //}
    }
}
