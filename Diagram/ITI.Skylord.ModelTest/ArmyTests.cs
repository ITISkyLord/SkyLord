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
            _defaultPlayer = new Player( _world, "Thanur" );
            _world.addNewIsland( new Coordinate() );
            _defaultIsland = _world.Map.Islands.Values.First();
        }
        [Test]
        public void Create_new_army()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland);
            Assert.IsNotNull( army.Regiments );
        }

        [Test]
        public void Add_unit_in_army()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( new Diagram.Guard(), 1 );
            Assert.That( army.Regiments.Count == 1 );
        }
        [Test]
        public void Add_different_units_in_army()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( new Diagram.Guard(), 1 );
            army.Regiments.Add( new Necromancer(), 10 );
            army.Regiments.Add( new Warrior(), 15 );
            Assert.That( army.Regiments.Count == 3 );
        }
        [Test]
        public void Find_regiment_in_army_with_FindRegiment_method()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            Warrior war = new Warrior();
            army.Regiments.Add( war, 50 );
            KeyValuePair<Unit, int> KvP = army.FindRegiment( new Diagram.Warrior() );

            Assert.That( KvP.Key == war && KvP.Value == 50 );
        }

        [Test]
        public void Get_physical_regiments()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            Warrior war = new Warrior();
            Cyclop cyc = new Cyclop();
            army.Regiments.Add( war, 50 );
            army.Regiments.Add( cyc, 10 );

            Dictionary<Unit, int> dic = army.GetPhysicalRegiments();

            Assert.That( dic.Count == 1 );
            Assert.That( dic.ContainsKey( war) && !dic.ContainsKey( cyc ));
        }

        [Test]
        public void Get_magical_regiments()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            Warrior war = new Warrior();
            Cyclop cyc = new Cyclop();
            army.Regiments.Add( war, 50 );
            army.Regiments.Add( cyc, 10 );

            Dictionary<Unit, int> dic = army.GetMagicalRegiments();

            Assert.That( dic.Count == 1 );
            Assert.That( dic.ContainsKey( cyc ) && !dic.ContainsKey( war ) );
        }
    }
}
