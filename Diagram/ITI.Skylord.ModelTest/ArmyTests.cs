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
        Player _defaultPlayer = new Player();
        Island _defaultIsland = new Island("Ivry", new Player(), new List<Army>(), new Mage(), new List<Building>(), new Coordinate(), true);

        [Test]
        public void Create_new_army()
        {
            Army army = new Army(new List<Regiment>(), ArmyState.defense);
            Assert.IsNotNull( army.Regiments );
        }

        [Test]
        public void Add_units_in_army()
        {
            
            Diagram.Guard guard = new Diagram.Guard(_defaultIsland);
            Regiment regiment = new Regiment();
            regiment.Units.Add( guard.Name, 50 );
            List<Regiment> regiments = new List<Regiment>();
            regiments.Add( regiment );
            Army army = new Army(regiments,ArmyState.defense);
            Assert.IsNotNull( army.Regiments );
        }
    }
}
