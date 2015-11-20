using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.SkyLord;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]

    class ApprenticeTests
    {
        [Test]
        public void Create_new_apprentice()
        {
            Apprentice apprentice = new Apprentice();
            Assert.That( apprentice.Level.Number == 1 );
        }
    }
}
