using ITI.SkyLord.Models.Entity_Framework.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests
{
    [TestFixture]
    public class SeeOtherPlayers
    {
        [Test]
        public void Display_other_players()
        {
            using (PlayerContext pc = new PlayerContext())
            {
                foreach(Player p in pc.Players)
                {
                    if(p.Name == "Loic")
                    {
                        continue;
                    }
                   else Console.WriteLine("Name : {0}, WorldId : {1}",p.Name,p.World);
                }
            }
        }

        [Test]
        public void See_information_of_a_player()
        {
            using (PlayerContext pc = new PlayerContext())
            {
                var t = pc.Profils.First();
                //var player = pc.Players.First( p => p.Profil.ProfilId == t.ProfilId);
                var Loic = pc.Players.First();
                Console.WriteLine("Name : {0}, Profil : {1}, Monde : {2}", Loic.Name, Loic.Profil.ProfilId, Loic.World);
            }
        }
    }
}
