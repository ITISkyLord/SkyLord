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
    class IslandTests
    {

        Player _defaultPlayer = new Player();
        [Test]
        public void Create_new_island()
        {
            Island island = new Island("Ivry", _defaultPlayer, new List<Army>(), new Mage(), new List<Building>(), new Coordinate(), true); //Erwan attention j'ai modifié le constrcteur
            Assert.IsNotNull( island.Name );
        }
    }
}
