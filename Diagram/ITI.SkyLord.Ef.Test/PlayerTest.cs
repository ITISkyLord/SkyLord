using ITI.SkyLord.DAL.Contexts.GameContext;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ITI.SkyLord.Ef.Test
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void Add2Players()
        {
            Profil p1 = new Profil { Mail = "Tatasjioj" };
            Profil p2 = new Profil { Mail = "zajiazjioeji" };
            Player testPlayer = new Player {Name = "Zuzu", Profil = p1};
            Player testPlayer2 = new Player { Name = "Zaza", Profil = p2 };

            using (GameEntity context = new GameEntity())
            {
                context.Profils.Add(p1);
                context.Profils.Add(p2);
                context.Players.Add(testPlayer);
                context.Players.Add(testPlayer2);
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }
            Console.WriteLine("Player saved");
            Console.ReadLine();
        }

        [Test]
        public void DisplayPlayers()
        {
            using (GameEntity context = new GameEntity())
            {
                foreach(Player p in context.Players.Include(p => p.Profil))
                {
                    Console.WriteLine("Name: {0}, Mail : {1}", p.Name, p.Profil.Mail);
                }
            }
        }

        [Test]
        public void DisplayProfile()
        {
            using (GameEntity context = new GameEntity())
            {
                foreach (Profil p in context.Profils.Include(p => p.Owner))
                {
                    Console.WriteLine("Mail : {0}, Name : {1}", p.Mail, p.Owner.Name);
                }
            }
        }

        [Test]
        public void CreateWorld()
        {
            World w = new World();
            using (GameEntity context = new GameEntity())
            {
                context.Worlds.Add(w);
                context.SaveChanges();
            }
        }

        [Test]
        public void getWorld()
        {
            using (GameEntity context = new GameEntity())
            {
                foreach(World world in context.Worlds.Include(world => world.Map))
                {
                    Console.WriteLine(world.Map);
                }
            }
        }
    }
}
