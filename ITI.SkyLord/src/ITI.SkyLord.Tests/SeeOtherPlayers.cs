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
        //[Test]
        //public void Display_other_players()
        //{
        //    using (PlayerContext pc = new PlayerContext())
        //    {
        //        foreach (Player p in pc.Players)
        //        {
        //            Console.WriteLine("Name : {0}", p.Name);
        //        }
        //    }
        //}

        [Test]
        public void See_information_of_a_player()
        {
            using (PlayerContext pc = new PlayerContext())
            {
                Player x = pc.Players.First(o => o.Name == "Marvin");
                Profil t = pc.Profils.First(u => u.ProfilId == x.Profil.ProfilId);

                //Profil t = pc.Profils.First(a => a.ProfilId == 6);
                //Player x = pc.Players.First(o => o.Profil.ProfilId == t.ProfilId);

                Console.WriteLine("Name : {0}, Password : {1}, Description {2}, Monde : {3}", x.Name, x.Profil.Password, x.Profil.Description, x.World);
            }
        }
    }
}
