using Diagram;
using ITI.SkyLord;
using ITI.SkyLord.Units;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class RegimentTests
    {
        Unit _warrior;

        public RegimentTests()
        {
            _warrior = new Unit( UnitName.warrior, UnitDamageType.physical, UnitType.soldier, new UnitStatistics( 70, 50, 25, 25, 15, 5 ), new Ressource( 10, 10, 10, 10 ) );
        }

        [Test]
        public void Create_new_regiment()
        {
            Regiment reg = new Regiment( _warrior, 10 );

            Assert.IsNotNull( reg );
            Assert.That( reg.Unit == _warrior && reg.Number == 10 );
        }

        [Test]
        public void Constructor_throws_ArgumentOutOfRangeException_if_number_argument_is_under_1()
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => new Regiment( _warrior, -1 ) );

            Assert.Throws<ArgumentOutOfRangeException>( () => new Regiment( _warrior, 0 ) );
        }
    }
}
