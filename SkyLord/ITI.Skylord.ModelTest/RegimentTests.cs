using ITI.Skylord;
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

        [Test]
        public void Create_new_RegimentList()
        {
            RegimentList regimentList = new RegimentList();

            Assert.IsNotNull( regimentList );
        }

        [Test]
        public void Add_a_regiment_to_RegimentList_using_normal_Add_Method()
        {
            Regiment reg = new Regiment( _warrior, 10 );
            RegimentList regimentList = new RegimentList();

            regimentList.Add( reg );

            Assert.That( reg.Unit == _warrior && reg.Number == 10 );
        }

        [Test]
        public void Add_a_regiment_to_RegimentList_using_overloaded_Add_Method()
        {
            RegimentList regimentList = new RegimentList();

            regimentList.Add( _warrior, 10 );

            Assert.That( regimentList[0].Unit == _warrior && regimentList[0].Number == 10 );
        }
    }
}
