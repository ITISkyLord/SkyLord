using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.SkyLord;
using ITI.SkyLord.Model;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class CoordinateTests
    {
        [Test]
        public void Create_new_coordinate()
        {
            Coordinate coordinate = new Coordinate(100,150);
            Assert.That( coordinate.X == 100 );
        }

    }
}
