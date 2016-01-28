using ITI.SkyLord.Models.Entity_Framework;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class LevelTests
    {
        #region Seed

        [Test]
        public void SeedAllTheThings()
        {
            LevelSeed levelSeed = new LevelSeed();
            UnitSeed unitSeed = new UnitSeed();
            levelSeed.SeedLevels();
            unitSeed.SeedUnits();
        }

        #endregion

        [Test]
        public void RoundTest()
        {
            TimeSpan t = TimeSpan.FromSeconds( 500 );
            TimeSpan t2 = TimeSpan.FromDays( 5 );

            Console.WriteLine( t.CustomToString()  );
            Console.WriteLine( t2.CustomToString()  );
        }

        private Ressource Multiplyressource( Ressource initialRessource, int factor )
        {
            return new Ressource
            {
                Wood = initialRessource.Wood * factor,
                Metal = initialRessource.Metal * factor,
                Cristal = initialRessource.Cristal * factor,
                Magic = initialRessource.Magic * factor
            };
        }

        void AddPlayerAndUser( Player p, SetupContext context )
        {
            ApplicationUser appUser = new ApplicationUser();
            User_Player userPlayer = new User_Player( p, appUser );

            context.Users.Add( appUser );
            context.Players.Add( p );
            context.User_Players.Add( userPlayer );
        }

        void RemovePlayerAndUser( Player p, SetupContext context )
        {
            context.Remove( p );
            context.User_Players.Remove( p.UserPlayer );
            context.Users.Remove( p.UserPlayer.User );
        }
    }
}
