using Diagram;
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
    class RessourceTests
    {
        [Test]
        public void Create_ressource()
        {
            Ressource ressource = new Ressource( 100, 200, 300, 400 );
            Assert.IsNotNull( ressource.Cristal );
        }

        [Test]
        public void ressource_out_of_range_constructor_throws_ArgumentOutOfRangeException()
        {
            // Wood under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new Ressource( -1, 100, 100, 100) );
            // Metal under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new Ressource( 100, -1, 100, 100 ) );
            // Cristal under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new Ressource( 100, 100, -1, 100 ) );
            // Magic under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new Ressource( 100, 100, 100, -1 ) );
        }
    }
}
