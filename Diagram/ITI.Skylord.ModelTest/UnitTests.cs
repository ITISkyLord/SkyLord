using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagram;
using NUnit.Framework;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    public class UnitTests
    {

        Player _defaultPlayer = new Player();
        Island _defaultIsland = new Island("Ivry", new Player(), new List<Army>(), new Mage(), new List<Building>(), new Coordinate(), true);
        [Test]
        public void Create_new_soldier()
        {
            Diagram.Guard guard = new Diagram.Guard( _defaultIsland );
            Assert.IsNotNull( guard );
        }

        [Test]
        public void Create_new_soldier_with_good_stats()
        {
            Diagram.Guard guard = new Diagram.Guard( _defaultIsland );
            Assert.That( guard.UnitStat.Attack == 70 );
        }

        [Test]
        public void Create_stats_works()
        {
            UnitStat unitStat = new UnitStat( 70, 50, 20, 15, 15, 5 );
            Assert.IsNotNull( unitStat );
        }
    }
}
