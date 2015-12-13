//using ITI.SkyLord.Models.Entity_Framework.Contexts;
//using Microsoft.AspNet.Mvc;
//using Microsoft.Data.Entity;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ITI.SkyLord.Tests.EfTests
//{
//    [TestFixture]
//    public class RessourceTest
//    {
//        [Test]
//        public void SeeRessources()
//        {
//            Ressource testRessource = null;
//            try
//            {               
//                using (IslandContext context = new IslandContext())
//                {
//                    testRessource = new Ressource { Magic = 10, Cristal = 10, Metal = 10, Wood = 10 };
//                    context.Add(testRessource);
//                    context.SaveChanges();

//                    Console.WriteLine("ok");
//                }
//            }
//            finally
//            {
//                using (IslandContext context = new IslandContext())
//                {
//                    context.Remove(testRessource);
//                    context.SaveChanges();
//                }
//            }          
//        }

//        [Test]
//        public void AddRessources()
//        {
//            using (PlayerContext context = new PlayerContext())
//            {
//                Player playerActive = context.Players.Include(pro => pro.Profil).Include(isl => isl.Islands).Include(w => w.World).Where(p => p.Name == "LoicD").SingleOrDefault();
//                using (IslandContext islandContext = new IslandContext())
//                {
//                    int quantRessource = 10;
//                    Ressource ressource = islandContext.Islands.Include(r => r.AllRessources).Where(i => i.Owner.PlayerId == playerActive.PlayerId).Select(res => res.AllRessources).SingleOrDefault();

//                    ressource.ChangeCristal(quantRessource);
//                    ressource.ChangeMagic(quantRessource);
//                    ressource.ChangeMetal(quantRessource);
//                    ressource.ChangeWood(quantRessource);
//                    islandContext.SaveChanges();

//                    Console.WriteLine("Les ressources de cristal ajoutés : " + ressource.Cristal.ToString());
//                    Console.WriteLine("Les ressources de magie ajoutés : "+ ressource.Magic.ToString());
//                    Console.WriteLine("Les ressources de metal ajoutés : " + ressource.Metal.ToString());
//                    Console.WriteLine("Les ressources de bois ajoutés : " + ressource.Wood.ToString());

//                    ressource.WithdrawCristal(quantRessource);
//                    ressource.WithdrawMagic(quantRessource);
//                    ressource.WithdrawMetal(quantRessource);
//                    ressource.WithdrawWood(quantRessource);
//                    islandContext.SaveChanges();

//                    Console.WriteLine(ressource.Cristal.ToString());
//                    Console.WriteLine(ressource.Magic.ToString());
//                    Console.WriteLine(ressource.Metal.ToString());
//                    Console.WriteLine(ressource.Wood.ToString());

//                }     
//            }               
//        }
//        [Test]
//        public void WithdrawRessources()
//        {
//            using (PlayerContext context = new PlayerContext())
//            {
//                Player playerActive = context.Players.Include(pro => pro.Profil).Include(isl => isl.Islands).Include(w => w.World).Where(p => p.Name == "LoicD").SingleOrDefault();
//                using (IslandContext islandContext = new IslandContext())
//                {
//                    int quantRessource = 50;
//                    Ressource ressource = islandContext.Islands.Include(r => r.AllRessources).Where(i => i.Owner.PlayerId == playerActive.PlayerId).Select(res => res.AllRessources).SingleOrDefault();

//                    ressource.WithdrawCristal(quantRessource);
//                    ressource.WithdrawMagic(quantRessource);
//                    ressource.WithdrawMetal(quantRessource);
//                    ressource.WithdrawWood(quantRessource);
//                    islandContext.SaveChanges();

//                    Console.WriteLine("Les ressources de cristals sont de maintenant : " + ressource.Cristal.ToString());
//                    Console.WriteLine("Les ressources de magies sont de maintenant : " + ressource.Magic.ToString());
//                    Console.WriteLine("Les ressources de metal sont de maintenant : " + ressource.Metal.ToString());
//                    Console.WriteLine("Les ressources de bois sont de maintenant : " + ressource.Wood.ToString());
//                }
//            }
//        }
//    }
//}
