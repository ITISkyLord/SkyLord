using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class EventManagerTest
    {
        [Test]
        public void Add_UnitEvent_with_correct_date()
        {
            SetupContext ctx = new SetupContext();
            EventManager em = new EventManager( ctx, new EventPackManager( ctx ));
            Island island = ctx.Islands.Include(i => i.Coordinates).Where(i => i.Owner == null ).OrderBy(i => IslandManager.DistanceFromCenter(i)).First();
            island.IsCapital = true;
            island.Name = "Île de test";
            ctx.SaveChanges();
            Unit u = new Unit()
            {
                UnitName = UnitName.guard,
                UnitDamageType = UnitDamageType.physical,
                UnitStatistics = new UnitStatistics() { Attack=10, Capacity=10, Consumption=5, MagicResist=20, PhysicResist=50, Speed=4, TimeToBuild = 30  },
                UnitCost = new Ressource() { Cristal = 100, Magic = 100, Metal = 100, Wood = 100 },
                UnitType = UnitType.soldier
            };
            em.AddUnitEvent( ctx, u, island );
            ctx.SaveChanges();
            Assert.That( ctx.UnitEvents != null );
        }
    }
}
