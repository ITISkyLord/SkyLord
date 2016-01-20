using ITI.SkyLord.Models.Entity_Framework.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class StandardViewModelTests
    {
        World _world;
        Player GoodPlayer;


        [SetUp]
        public void Initialisation()
        {
            using (SetupContext context = new SetupContext())
            {
                _world = context.GetWorld();


            }
        }

        [TearDown]
        public void End()
        {
            using (SetupContext context = new SetupContext())
            {

            }

        }





    }
}
