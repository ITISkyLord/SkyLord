using ITI.SkyLord.Models.Entity_Framework.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class ArmyTests
    {
        World _world;
        public ArmyTests()
        {
            using ( PlayerContext context = new PlayerContext() )
            {
                _world = context.GetWorld();
            }
        }

        [Test]
        public void cascade_delete()
        {
            Player p = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
            Player playerFromDatabase;

            try
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    context.AddPlayer( p );
                }
            }
            finally
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    context.Remove( p );
                    context.SaveChanges();

                    playerFromDatabase = context.FindPlayer( p.PlayerId );
                    Assert.IsNull( playerFromDatabase );
                    Assert.IsNull( playerFromDatabase.Profil );
                }
            }
        }
        //[Test]
        //public void Add_a_unit_with_addUnit_method_creates_a_defense_army_if_no_defense_army_exists()
        //{
        //    Player defaultPlayer = null;

        //    Island defaultIsland = null;
        //    Ressource islandDefaultRessource = null;
        //    Coordinate islandDefaultCoordinate = null;

        //    Unit guard = null;
        //    Ressource guardCost = null;
        //    UnitStatistics guardStatistics = null;

        //    Unit necromancer = null;
        //    Ressource necromancerCost = null;
        //    UnitStatistics necromancerStatistics = null;

        //    try
        //    {
        //        // Add a player to DB
        //        using ( PlayerContext context = new PlayerContext() )
        //        {
        //            defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
        //            context.AddPlayer( defaultPlayer );
        //        }

        //        // Add an Island to the player in DB
        //        using ( IslandContext context = new IslandContext() )
        //        {
        //                islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
        //                context.Ressources.Add( islandDefaultRessource );
        //                islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
        //                context.Coordinates.Add( islandDefaultCoordinate );
        //                context.SaveChanges();

        //                defaultIsland = new Island
        //                {
        //                    Name = "defaultIsland",
        //                    Owner = defaultPlayer,
        //                    Coordinates = islandDefaultCoordinate,
        //                    AllRessources = islandDefaultRessource,
        //                    IsCapital = true,
        //                    Loyalty = 100
        //                };
        //                context.Add( defaultIsland );
        //                context.SaveChanges();
        //        }

        //        // Add guard and necromancer units to DB
        //        using ( ArmyContext context = new ArmyContext() )
        //        {
        //            guardCost = new Ressource { Wood = 200, Metal = 100 };
        //            context.Ressources.Add( guardCost );
        //            guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
        //            context.UnitStatistics.Add( guardStatistics );
        //            context.SaveChanges();

        //            guard = new Unit
        //            {
        //                UnitType = UnitType.soldier,
        //                UnitName = UnitName.guard,
        //                UnitDamageType = UnitDamageType.physical,
        //                UnitCost = guardCost,
        //                UnitStatistics = guardStatistics,
        //            };
        //            context.Units.Add( guard );
        //            context.SaveChanges();

        //            necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
        //            context.Ressources.Add( necromancerCost );
        //            necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
        //            context.UnitStatistics.Add( necromancerStatistics );
        //            context.SaveChanges();

        //            necromancer = new Unit
        //            {
        //                UnitType = UnitType.magic,
        //                UnitName = UnitName.necromancer,
        //                UnitDamageType = UnitDamageType.magical,
        //                UnitCost = necromancerCost,
        //                UnitStatistics = necromancerStatistics,
        //            };
        //            context.Units.Add( necromancer );
        //            context.SaveChanges();

        //            // Assert that and army and regiment are properly created
        //            context.AddUnit( guard, 10, defaultIsland );
        //            Assert.That( context.Armies.Any( a => a.Island == defaultIsland ) );
        //        }
        //    }
        //    finally
        //    {
        //        using(var context = new ArmyContext() )
        //        {
        //            context.Remove( defaultIsland );
        //            context.Remove( islandDefaultRessource );
        //            context.Remove( islandDefaultCoordinate );
        //            context.SaveChanges();

        //            context.Remove( guard );
        //            context.Remove( guardCost );
        //            context.Remove( guardStatistics );
        //            context.SaveChanges();

        //            context.Remove( necromancer );
        //            context.Remove( necromancerCost );
        //            context.Remove( necromancerStatistics );
        //            context.SaveChanges();
        //        }
        //        using ( var context = new PlayerContext() )
        //        {
        //            context.RemovePlayer( defaultPlayer.PlayerId );
        //        }
        //    }
        //}
    }
}