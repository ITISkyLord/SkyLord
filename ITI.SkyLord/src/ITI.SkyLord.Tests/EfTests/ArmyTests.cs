using ITI.SkyLord.Models.Entity_Framework;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
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

        void AddPlayerAndUser( Player p )
        {
            ApplicationUser appUser = new ApplicationUser();
            User_Player userPlayer = new User_Player( p, appUser );

            using ( var context = new PlayerContext() )
            {
                context.Users.Add( appUser );
                context.Players.Add( p );
                context.User_Players.Add( userPlayer );
                context.SaveChanges();
            }
        }

        void RemovePlayerAndUser( Player p )
        {
            using ( var context = new PlayerContext() )
            {
                context.Remove( p );
                context.User_Players.Remove( p.UserPlayer );
                context.SaveChanges();

                context.Users.Remove( p.UserPlayer.User );
                context.SaveChanges();
                Console.WriteLine( "C'est bon ! " );
            }
        }

        [Test]
        public void regiment_cascade_delete()
        {
            Island defaultIsland = null;
            Ressource islandDefaultRessource = null;
            Coordinate islandDefaultCoordinate = null;

            Unit guard = null;
            Ressource guardCost = null;
            UnitStatistics guardStatistics = null;

            Unit necromancer = null;
            Ressource necromancerCost = null;
            UnitStatistics necromancerStatistics = null;

            using ( var context = new IslandContext() )
            {
                // TODO Créer ses propres objets à réutiliser pour les tests

                islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                context.Ressources.Add( islandDefaultRessource );
                islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
                context.Coordinates.Add( islandDefaultCoordinate );
                context.SaveChanges();

                defaultIsland = new Island
                {
                    Name = "defaultIsland",
                    Coordinates = islandDefaultCoordinate,
                    AllRessources = islandDefaultRessource,
                    IsCapital = true,
                    Loyalty = 100
                };
                context.Add( defaultIsland );
                context.SaveChanges();
            }

            using ( var context = new ArmyContext() )
            {
                Regiment regiment1 = new Regiment { Unit = context.Units.FirstOrDefault( u => u.UnitName == UnitName.guard ), Number = 50 };
                Regiment regiment2 = new Regiment { Unit = context.Units.FirstOrDefault( u => u.UnitName == UnitName.necromancer ), Number = 25 };
                Army army = new Army
                {
                    Island = defaultIsland,
                    ArmyState = ArmyState.defense,
                    Regiments = new List<Regiment>()
                };

                army.Regiments.Add( regiment1 );
                army.Regiments.Add( regiment2 );

                context.Add( army );
                context.SaveChanges();

                context.Remove( army );
                context.SaveChanges();

                context.Remove( defaultIsland );
                context.SaveChanges();

                Army armyFromDB = context.Armies.FirstOrDefault( a => a.ArmyId == army.ArmyId );
                Assert.IsNull( armyFromDB );
            }
         }

        [Test]
        public void unit_cascade_delete()
        {
            using ( ArmyContext context = new ArmyContext() )
            {
                Ressource guardCost = new Ressource { Wood = 200, Metal = 100 };
                context.Ressources.Add( guardCost );
                UnitStatistics guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                context.UnitStatistics.Add( guardStatistics );
                context.SaveChanges();

                Unit guard = new Unit
                {
                    UnitType = UnitType.soldier,
                    UnitName = UnitName.guard,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = guardCost,
                    UnitStatistics = guardStatistics,
                };
                context.Units.Add( guard );
                context.SaveChanges();

                Ressource necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                context.Ressources.Add( necromancerCost );
                UnitStatistics necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                context.UnitStatistics.Add( necromancerStatistics );
                context.SaveChanges();

                Unit necromancer = new Unit
                {
                    UnitType = UnitType.magic,
                    UnitName = UnitName.necromancer,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = necromancerCost,
                    UnitStatistics = necromancerStatistics,
                };
                context.Units.Add( necromancer );
                context.SaveChanges();

                context.Remove( guard );
                context.Remove( necromancer );
                context.SaveChanges();

                Assert.IsNull( context.Units.FirstOrDefault( u => u.UnitId == guard.UnitId ) );
                Assert.IsNull( context.Units.FirstOrDefault( u => u.UnitId == necromancer.UnitId ) );
                Assert.IsNull( context.UnitStatistics.FirstOrDefault( us => us.UnitStatisticsId == guardStatistics.UnitStatisticsId ) );
                Assert.IsNull( context.UnitStatistics.FirstOrDefault( us => us.UnitStatisticsId == necromancerStatistics.UnitStatisticsId ) );
                Assert.IsNull( context.Ressources.FirstOrDefault( r => r.RessourceId == guardCost.RessourceId ) );
                Assert.IsNull( context.Ressources.FirstOrDefault( r => r.RessourceId == necromancerCost.RessourceId ) );
            }
        }

        [Test]
        public void Add_a_unit_with_addUnit_method_creates_a_defense_army_if_no_defense_army_exists()
        {
            #region Seed
            Player defaultPlayer = null;

            Island defaultIsland = null;
            Ressource islandDefaultRessource = null;
            Coordinate islandDefaultCoordinate = null;

            Unit guard = null;
            Ressource guardCost = null;
            UnitStatistics guardStatistics = null;

            Unit necromancer = null;
            Ressource necromancerCost = null;
            UnitStatistics necromancerStatistics = null;
            #endregion

            try
            {
                #region Seed
                // Add a player to DB
                using ( PlayerContext context = new PlayerContext() )
                {
                    defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
                    AddPlayerAndUser( defaultPlayer );
                }

                // Add an Island to the player in DB
                using ( IslandContext context = new IslandContext() )
                {
                    islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                    context.Ressources.Add( islandDefaultRessource );
                    islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
                    context.Coordinates.Add( islandDefaultCoordinate );
                    context.SaveChanges();

                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        Owner = defaultPlayer,
                        Coordinates = islandDefaultCoordinate,
                        AllRessources = islandDefaultRessource,
                        IsCapital = true,
                        Loyalty = 100
                    };
                    context.Add( defaultIsland );
                    context.SaveChanges();
                }

                // Add guard and necromancer units to DB
                using ( ArmyContext context = new ArmyContext() )
                {
                    guardCost = new Ressource { Wood = 200, Metal = 100 };
                    context.Ressources.Add( guardCost );
                    guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                    context.UnitStatistics.Add( guardStatistics );
                    context.SaveChanges();

                    guard = new Unit
                    {
                        UnitType = UnitType.soldier,
                        UnitName = UnitName.guard,
                        UnitDamageType = UnitDamageType.physical,
                        UnitCost = guardCost,
                        UnitStatistics = guardStatistics,
                    };
                    context.Units.Add( guard );
                    context.SaveChanges();

                    necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                    context.Ressources.Add( necromancerCost );
                    necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                    context.UnitStatistics.Add( necromancerStatistics );
                    context.SaveChanges();

                    necromancer = new Unit
                    {
                        UnitType = UnitType.magic,
                        UnitName = UnitName.necromancer,
                        UnitDamageType = UnitDamageType.magical,
                        UnitCost = necromancerCost,
                        UnitStatistics = necromancerStatistics,
                    };
                    context.Units.Add( necromancer );
                    context.SaveChanges();
                    #endregion

                    // Assert that and army and regiment are properly created
                    context.AddUnit( guard, 10, defaultIsland );
                    Assert.That( context.Armies.Any( a => a.Island.IslandId == defaultIsland.IslandId ) );
                }
            }
            finally
            {
                #region Delete
                using ( var context = new ArmyContext() )
                {
                    Army armyCreated = context.Armies.Include( a => a.Regiments ).Include( a => a.Island )
                        .SingleOrDefault( a => a.Island.IslandId == defaultIsland.IslandId && a.ArmyState == ArmyState.defense );
                    context.Remove( armyCreated );
                    context.SaveChanges();

                    context.Remove( guard );
                    context.Remove( necromancer );
                    context.SaveChanges();
                }
                using ( var context = new IslandContext() )
                {
                    context.Remove( defaultIsland );
                    context.SaveChanges();

                    context.Remove( islandDefaultRessource );
                    context.Remove( islandDefaultCoordinate );
                    context.SaveChanges();
                }
                using ( var context = new PlayerContext() )
                {
                    RemovePlayerAndUser( defaultPlayer );
                }
                #endregion
            }
        }

        [Test]
        public void Add_a_unit_with_addUnit_method_creates_a_regiment_if_a_defense_army_exists()
        {
            #region Seed
            Player defaultPlayer = null;

            Island defaultIsland = null;
            Ressource islandDefaultRessource = null;
            Coordinate islandDefaultCoordinate = null;

            Army defaultArmy = null;

            Unit guard = null;
            Ressource guardCost = null;
            UnitStatistics guardStatistics = null;

            Unit necromancer = null;
            Ressource necromancerCost = null;
            UnitStatistics necromancerStatistics = null;
            #endregion

            try
            {
                #region Seed
                // Add a player to DB
                using ( PlayerContext context = new PlayerContext() )
                {
                    defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
                    AddPlayerAndUser( defaultPlayer );
                }

                // Add an Island to DB
                using ( IslandContext context = new IslandContext() )
                {
                    islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                    context.Ressources.Add( islandDefaultRessource );
                    islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
                    context.Coordinates.Add( islandDefaultCoordinate );
                    context.SaveChanges();

                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        Owner = defaultPlayer,
                        Coordinates = islandDefaultCoordinate,
                        AllRessources = islandDefaultRessource,
                        IsCapital = true,
                        Loyalty = 100
                    };
                    context.Add( defaultIsland );
                    context.SaveChanges();
                }

                // Add an army, a guard and necromancer units to DB
                using ( ArmyContext context = new ArmyContext() )
                {

                    defaultArmy = new Army
                    {
                        ArmyState = ArmyState.defense,
                        Island = defaultIsland
                    };

                    context.Add( defaultArmy );

                    guardCost = new Ressource { Wood = 200, Metal = 100 };
                    context.Ressources.Add( guardCost );
                    guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                    context.UnitStatistics.Add( guardStatistics );
                    context.SaveChanges();

                    guard = new Unit
                    {
                        UnitType = UnitType.soldier,
                        UnitName = UnitName.guard,
                        UnitDamageType = UnitDamageType.physical,
                        UnitCost = guardCost,
                        UnitStatistics = guardStatistics,
                    };
                    context.Units.Add( guard );
                    context.SaveChanges();

                    necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                    context.Ressources.Add( necromancerCost );
                    necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                    context.UnitStatistics.Add( necromancerStatistics );
                    context.SaveChanges();

                    necromancer = new Unit
                    {
                        UnitType = UnitType.magic,
                        UnitName = UnitName.necromancer,
                        UnitDamageType = UnitDamageType.magical,
                        UnitCost = necromancerCost,
                        UnitStatistics = necromancerStatistics,
                    };
                    context.Units.Add( necromancer );
                    context.SaveChanges();
                    #endregion

                    // Assert that and army and regiment are properly created
                    context.AddUnit( guard, 10, defaultIsland );
                    context.AddUnit( necromancer, 15, defaultIsland );

                    Assert.That( context.Regiments.Include( r => r.Unit ).FirstOrDefault( r => r.ArmyId == defaultArmy.ArmyId && r.Unit.UnitName == UnitName.guard ).Unit.UnitId == guard.UnitId );
                    Assert.That( context.Regiments.Include( r => r.Unit ).FirstOrDefault( r => r.ArmyId == defaultArmy.ArmyId && r.Unit.UnitName == UnitName.necromancer ).Unit.UnitId == necromancer.UnitId );
                }
            }
            finally
            {
                #region Delete
                using ( var context = new ArmyContext() )
                {
                    defaultArmy = context.Armies.Include( a => a.Regiments ).Include( a => a.Island )
                        .SingleOrDefault( a => a.Island.IslandId == defaultIsland.IslandId && a.ArmyState == ArmyState.defense );
                    context.Remove( defaultArmy );
                    context.SaveChanges();

                    context.Remove( guard );
                    context.Remove( necromancer );
                    context.SaveChanges();
                }
                using ( var context = new ArmyContext() )
                {
                    context.Remove( defaultIsland );
                    context.SaveChanges();

                    context.Remove( islandDefaultRessource );
                    context.Remove( islandDefaultCoordinate );
                    context.SaveChanges();
                }
                using ( var context = new PlayerContext() )
                {
                    RemovePlayerAndUser( defaultPlayer );
                }
                #endregion
            }
        }

        [Test]
        public void Add_a_unit_with_addUnit_method_adds_to_regiment_if_a_defense_army_with_regiment_exists()
        {
            #region Seed
            Player defaultPlayer = null;

            Island defaultIsland = null;
            Ressource islandDefaultRessource = null;
            Coordinate islandDefaultCoordinate = null;

            Army defaultArmy = null;
            Regiment defaultRegiment = null;
            Regiment defaultRegiment2 = null;

            Unit guard = null;
            Ressource guardCost = null;
            UnitStatistics guardStatistics = null;

            Unit necromancer = null;
            Ressource necromancerCost = null;
            UnitStatistics necromancerStatistics = null;
            #endregion

            try
            {
                #region Seed
                // Add a player to DB
                using ( PlayerContext context = new PlayerContext() )
                {
                    defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
                    AddPlayerAndUser( defaultPlayer );
                }

                // Add an Island to the player in DB
                using ( IslandContext context = new IslandContext() )
                {
                    islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                    context.Ressources.Add( islandDefaultRessource );
                    islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
                    context.Coordinates.Add( islandDefaultCoordinate );
                    context.SaveChanges();

                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        Owner = defaultPlayer,
                        Coordinates = islandDefaultCoordinate,
                        AllRessources = islandDefaultRessource,
                        IsCapital = true,
                        Loyalty = 100
                    };
                    context.Add( defaultIsland );
                    context.SaveChanges();
                }

                // Add an army, a guard and necromancer units to DB
                using ( ArmyContext context = new ArmyContext() )
                {
                    guardCost = new Ressource { Wood = 200, Metal = 100 };
                    context.Ressources.Add( guardCost );
                    guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                    context.UnitStatistics.Add( guardStatistics );
                    context.SaveChanges();

                    guard = new Unit
                    {
                        UnitType = UnitType.soldier,
                        UnitName = UnitName.guard,
                        UnitDamageType = UnitDamageType.physical,
                        UnitCost = guardCost,
                        UnitStatistics = guardStatistics,
                    };
                    context.Units.Add( guard );
                    context.SaveChanges();

                    necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                    context.Ressources.Add( necromancerCost );
                    necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                    context.UnitStatistics.Add( necromancerStatistics );
                    context.SaveChanges();

                    necromancer = new Unit
                    {
                        UnitType = UnitType.magic,
                        UnitName = UnitName.necromancer,
                        UnitDamageType = UnitDamageType.magical,
                        UnitCost = necromancerCost,
                        UnitStatistics = necromancerStatistics,
                    };
                    context.Units.Add( necromancer );
                    context.SaveChanges();

                    defaultRegiment = new Regiment { Unit = guard, Number = 50 };
                    defaultRegiment2 = new Regiment { Unit = necromancer, Number = 25 };
                    defaultArmy = new Army
                    {
                        Island = defaultIsland,
                        ArmyState = ArmyState.defense,
                        Regiments = new List<Regiment>()
                    };

                    defaultArmy.Regiments.Add( defaultRegiment );
                    defaultArmy.Regiments.Add( defaultRegiment2 );

                    context.Add( defaultArmy );
                    context.SaveChanges();

                    #endregion

                    // Assert that and army and regiment are properly created
                    context.AddUnit( guard, 10, defaultIsland );
                    context.AddUnit( necromancer, 15, defaultIsland );

                    int i = context.Regiments.Include( r => r.Unit ).Count( r => r.ArmyId == defaultArmy.ArmyId && r.Unit.UnitName == UnitName.guard );

                    Assert.That( context.Regiments.Include( r => r.Unit ).FirstOrDefault( r => r.ArmyId == defaultArmy.ArmyId && r.Unit.UnitName == UnitName.guard ).Number == 60 );
                    Assert.That( context.Regiments.Include( r => r.Unit ).FirstOrDefault( r => r.ArmyId == defaultArmy.ArmyId && r.Unit.UnitName == UnitName.necromancer ).Number == 40 );
                }
            }
            finally
            {
                #region Delete
                using ( var context = new ArmyContext() )
                {
                    defaultArmy = context.Armies.Include( a => a.Regiments ).Include( a => a.Island )
                        .SingleOrDefault( a => a.Island.IslandId == defaultIsland.IslandId && a.ArmyState == ArmyState.defense );
                    context.Remove( defaultArmy );
                    context.SaveChanges();

                    context.Remove( guard );
                    context.Remove( necromancer );
                    context.SaveChanges();
                }
                using ( var context = new ArmyContext() )
                {
                    context.Remove( defaultIsland );
                    context.SaveChanges();

                    context.Remove( islandDefaultRessource );
                    context.Remove( islandDefaultCoordinate );
                    context.SaveChanges();
                }
                using ( var context = new PlayerContext() )
                {
                    RemovePlayerAndUser( defaultPlayer );
                }
                #endregion
            }
        }

        [Test]
        public void Remove_a_unit_with_RemoveUnit_method_substracts_from_number_property_in_the_corresponding_regiment()
        {
            #region Seed
            Player defaultPlayer = null;

            Island defaultIsland = null;
            Ressource islandDefaultRessource = null;
            Coordinate islandDefaultCoordinate = null;

            Unit guard = null;
            Ressource guardCost = null;
            UnitStatistics guardStatistics = null;

            Unit necromancer = null;
            Ressource necromancerCost = null;
            UnitStatistics necromancerStatistics = null;

            Army defaultArmy = null;
            Regiment defaultRegiment = null;
            Regiment defaultRegiment2 = null;
            #endregion

            try
            {
                #region Seed
                // Add a player to DB
                using ( PlayerContext context = new PlayerContext() )
                {
                    defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
                    AddPlayerAndUser( defaultPlayer );
                }

                // Add an Island to the player in DB
                using ( IslandContext context = new IslandContext() )
                {
                    islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                    context.Ressources.Add( islandDefaultRessource );
                    islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
                    context.Coordinates.Add( islandDefaultCoordinate );
                    context.SaveChanges();

                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        Owner = defaultPlayer,
                        Coordinates = islandDefaultCoordinate,
                        AllRessources = islandDefaultRessource,
                        IsCapital = true,
                        Loyalty = 100
                    };
                    context.Add( defaultIsland );
                    context.SaveChanges();
                }

                using ( ArmyContext context = new ArmyContext() )
                {
                    // Add guard and necromancer units to DB

                    guardCost = new Ressource { Wood = 200, Metal = 100 };
                    context.Ressources.Add( guardCost );
                    guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                    context.UnitStatistics.Add( guardStatistics );
                    context.SaveChanges();

                    guard = new Unit
                    {
                        UnitType = UnitType.soldier,
                        UnitName = UnitName.guard,
                        UnitDamageType = UnitDamageType.physical,
                        UnitCost = guardCost,
                        UnitStatistics = guardStatistics,
                    };
                    context.Units.Add( guard );
                    context.SaveChanges();

                    necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                    context.Ressources.Add( necromancerCost );
                    necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                    context.UnitStatistics.Add( necromancerStatistics );
                    context.SaveChanges();

                    necromancer = new Unit
                    {
                        UnitType = UnitType.magic,
                        UnitName = UnitName.necromancer,
                        UnitDamageType = UnitDamageType.magical,
                        UnitCost = necromancerCost,
                        UnitStatistics = necromancerStatistics,
                    };
                    context.Units.Add( necromancer );
                    context.SaveChanges();

                    // Add an Army and a Regiment to DB
                    defaultRegiment = new Regiment { Unit = guard, Number = 50 };
                    defaultRegiment2 = new Regiment { Unit = necromancer, Number = 25 };

                    context.Regiments.Add( defaultRegiment );
                    context.Regiments.Add( defaultRegiment2 );
                    context.SaveChanges();

                    defaultArmy = new Army
                    {
                        Island = defaultIsland,
                        ArmyState = ArmyState.defense,
                        Regiments = new List<Regiment> { defaultRegiment, defaultRegiment2 }
                    };
                    context.Armies.Add( defaultArmy );
                    context.SaveChanges();
                    #endregion

                    // Assert that the Number property is properly updated
                    context.RemoveUnit( guard, 10, defaultIsland, defaultArmy );

                    int numberOfGuards = context.Armies.Include( a => a.Regiments )
                        .First( a => a.ArmyId == defaultArmy.ArmyId ).Regiments
                        .Single( r => r.Unit.UnitId == guard.UnitId ).Number;

                    int numberOfNecromancers = context.Armies.Include( a => a.Regiments )
                        .First( a => a.ArmyId == defaultArmy.ArmyId ).Regiments
                        .Single( r => r.Unit.UnitId == necromancer.UnitId ).Number;

                    Assert.That( numberOfGuards == 40 );
                    Assert.That( numberOfNecromancers == 25 );
                }
            }
            finally
            {
                #region Delete
                using ( var context = new ArmyContext() )
                {
                    context.Remove( defaultArmy );
                    context.SaveChanges();

                    context.Remove( guard );
                    context.Remove( necromancer );
                    context.SaveChanges();
                }
                using ( var context = new IslandContext() )
                {
                    context.Remove( defaultIsland );
                    context.SaveChanges();

                    context.Remove( islandDefaultRessource );
                    context.Remove( islandDefaultCoordinate );
                    context.SaveChanges();
                }
                using ( var context = new PlayerContext() )
                {
                    RemovePlayerAndUser( defaultPlayer );
                }
                #endregion
            }
        }

        [Test]
        public void Remove_a_unit_with_RemoveUnit_method_deletes_the_corresponding_regiment_if_number_property_reaches_0()
        {
            #region Seed
            Player defaultPlayer = null;

            Island defaultIsland = null;
            Ressource islandDefaultRessource = null;
            Coordinate islandDefaultCoordinate = null;

            Unit guard = null;
            Ressource guardCost = null;
            UnitStatistics guardStatistics = null;

            Unit necromancer = null;
            Ressource necromancerCost = null;
            UnitStatistics necromancerStatistics = null;

            Army defaultArmy = null;
            Regiment defaultRegiment = null;
            Regiment defaultRegiment2 = null;
            #endregion

            try
            {
                #region Seed
                // Add a player to DB
                using ( PlayerContext context = new PlayerContext() )
                {
                    defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
                    AddPlayerAndUser( defaultPlayer );
                }

                // Add an Island to the player in DB
                using ( IslandContext context = new IslandContext() )
                {
                    islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                    context.Ressources.Add( islandDefaultRessource );
                    islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
                    context.Coordinates.Add( islandDefaultCoordinate );
                    context.SaveChanges();

                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        Owner = defaultPlayer,
                        Coordinates = islandDefaultCoordinate,
                        AllRessources = islandDefaultRessource,
                        IsCapital = true,
                        Loyalty = 100
                    };
                    context.Add( defaultIsland );
                    context.SaveChanges();
                }

                using ( ArmyContext context = new ArmyContext() )
                {
                    // Add guard and necromancer units to DB

                    guardCost = new Ressource { Wood = 200, Metal = 100 };
                    context.Ressources.Add( guardCost );
                    guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                    context.UnitStatistics.Add( guardStatistics );
                    context.SaveChanges();

                    guard = new Unit
                    {
                        UnitType = UnitType.soldier,
                        UnitName = UnitName.guard,
                        UnitDamageType = UnitDamageType.physical,
                        UnitCost = guardCost,
                        UnitStatistics = guardStatistics,
                    };
                    context.Units.Add( guard );
                    context.SaveChanges();

                    necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                    context.Ressources.Add( necromancerCost );
                    necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                    context.UnitStatistics.Add( necromancerStatistics );
                    context.SaveChanges();

                    necromancer = new Unit
                    {
                        UnitType = UnitType.magic,
                        UnitName = UnitName.necromancer,
                        UnitDamageType = UnitDamageType.magical,
                        UnitCost = necromancerCost,
                        UnitStatistics = necromancerStatistics,
                    };
                    context.Units.Add( necromancer );
                    context.SaveChanges();

                    // Add an Army and a Regiment to DB
                    defaultRegiment = new Regiment { Unit = guard, Number = 50 };
                    defaultRegiment2 = new Regiment { Unit = necromancer, Number = 25 };

                    context.Regiments.Add( defaultRegiment );
                    context.Regiments.Add( defaultRegiment2 );
                    context.SaveChanges();

                    defaultArmy = new Army
                    {
                        Island = defaultIsland,
                        ArmyState = ArmyState.defense,
                        Regiments = new List<Regiment> { defaultRegiment, defaultRegiment2 }
                    };
                    context.Armies.Add( defaultArmy );
                    context.SaveChanges();
                    #endregion

                    // Assert that the regiment is properly deleted
                    context.RemoveUnit( guard, 50, defaultIsland, defaultArmy );

                    Regiment guardRegiment = context.Armies.Include( a => a.Regiments )
                        .First( a => a.ArmyId == defaultArmy.ArmyId ).Regiments
                        .SingleOrDefault( r => r.Unit.UnitId == guard.UnitId );

                    int numberOfNecromancers = context.Armies.Include( a => a.Regiments )
                        .First( a => a.ArmyId == defaultArmy.ArmyId ).Regiments
                        .Single( r => r.Unit.UnitId == necromancer.UnitId ).Number;

                    Assert.IsNull( guardRegiment );
                    Assert.That( numberOfNecromancers == 25 );
                }
            }
            finally
            {
                #region Delete
                using ( var context = new ArmyContext() )
                {
                    context.Remove( defaultArmy );
                    context.SaveChanges();

                    context.Remove( guard );
                    context.Remove( necromancer );
                    context.SaveChanges();
                }
                using ( var context = new IslandContext() )
                {
                    context.Remove( defaultIsland );
                    context.SaveChanges();

                    context.Remove( islandDefaultRessource );
                    context.Remove( islandDefaultCoordinate );
                    context.SaveChanges();
                }
                using ( var context = new PlayerContext() )
                {
                    RemovePlayerAndUser( defaultPlayer );
                }
                #endregion
            }
        }

        [Test]
        public void Remove_a_unit_with_RemoveUnit_method_deletes_the_corresponding_army_if_no_regiment_is_left_after_removal()
        {
            #region Seed
            Player defaultPlayer = null;

            Island defaultIsland = null;
            Ressource islandDefaultRessource = null;
            Coordinate islandDefaultCoordinate = null;

            Unit guard = null;
            Ressource guardCost = null;
            UnitStatistics guardStatistics = null;

            Unit necromancer = null;
            Ressource necromancerCost = null;
            UnitStatistics necromancerStatistics = null;

            Army defaultArmy = null;
            Regiment defaultRegiment = null;
            Regiment defaultRegiment2 = null;
            #endregion

            try
            {
                #region Seed
                // Add a player to DB
                using ( PlayerContext context = new PlayerContext() )
                {
                    defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
                    AddPlayerAndUser( defaultPlayer );
                }

                // Add an Island to the player in DB
                using ( IslandContext context = new IslandContext() )
                {
                    islandDefaultRessource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                    context.Ressources.Add( islandDefaultRessource );
                    islandDefaultCoordinate = new Coordinate { X = 1, Y = 1 };
                    context.Coordinates.Add( islandDefaultCoordinate );
                    context.SaveChanges();

                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        Owner = defaultPlayer,
                        Coordinates = islandDefaultCoordinate,
                        AllRessources = islandDefaultRessource,
                        IsCapital = true,
                        Loyalty = 100
                    };
                    context.Add( defaultIsland );
                    context.SaveChanges();
                }

                using ( ArmyContext context = new ArmyContext() )
                {
                    // Add guard and necromancer units to DB

                    guardCost = new Ressource { Wood = 200, Metal = 100 };
                    context.Ressources.Add( guardCost );
                    guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                    context.UnitStatistics.Add( guardStatistics );
                    context.SaveChanges();

                    guard = new Unit
                    {
                        UnitType = UnitType.soldier,
                        UnitName = UnitName.guard,
                        UnitDamageType = UnitDamageType.physical,
                        UnitCost = guardCost,
                        UnitStatistics = guardStatistics,
                    };
                    context.Units.Add( guard );
                    context.SaveChanges();

                    necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                    context.Ressources.Add( necromancerCost );
                    necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                    context.UnitStatistics.Add( necromancerStatistics );
                    context.SaveChanges();

                    necromancer = new Unit
                    {
                        UnitType = UnitType.magic,
                        UnitName = UnitName.necromancer,
                        UnitDamageType = UnitDamageType.magical,
                        UnitCost = necromancerCost,
                        UnitStatistics = necromancerStatistics,
                    };
                    context.Units.Add( necromancer );
                    context.SaveChanges();

                    // Add an Army and a Regiment to DB
                    defaultRegiment = new Regiment { Unit = guard, Number = 50 };
                    defaultRegiment2 = new Regiment { Unit = necromancer, Number = 25 };

                    context.Regiments.Add( defaultRegiment );
                    context.Regiments.Add( defaultRegiment2 );
                    context.SaveChanges();

                    defaultArmy = new Army
                    {
                        Island = defaultIsland,
                        ArmyState = ArmyState.defense,
                        Regiments = new List<Regiment> { defaultRegiment, defaultRegiment2 }
                    };
                    context.Armies.Add( defaultArmy );
                    context.SaveChanges();
                    #endregion

                    // Assert that the army and regiment are properly deleted
                    context.RemoveUnit( guard, 50, defaultIsland, defaultArmy );
                    context.RemoveUnit( necromancer, 25, defaultIsland, defaultArmy );

                    Army armyFound = context.Armies.SingleOrDefault( a => a.ArmyId == defaultArmy.ArmyId );

                    Regiment guardRegiment = defaultArmy.Regiments
                        .SingleOrDefault( r => r.ArmyId == defaultArmy.ArmyId && r.Unit.UnitId == guard.UnitId );

                    Regiment necromancerRegiment = defaultArmy.Regiments
                        .SingleOrDefault( r => r.ArmyId == defaultArmy.ArmyId && r.Unit.UnitId == necromancer.UnitId );

                    Assert.IsNull( guardRegiment );
                    Assert.IsNull( necromancerRegiment );
                    Assert.IsNull( armyFound );
                }
            }
            finally
            {
                #region Delete
                using ( var context = new ArmyContext() )
                {
                    //context.Remove( defaultArmy );
                    //context.SaveChanges();

                    context.Remove( guard );
                    context.Remove( necromancer );
                    context.SaveChanges();
                }
                using ( var context = new IslandContext() )
                {
                    context.Remove( defaultIsland );
                    context.SaveChanges();

                    context.Remove( islandDefaultRessource );
                    context.Remove( islandDefaultCoordinate );
                    context.SaveChanges();
                }
                using ( var context = new PlayerContext() )
                {
                    RemovePlayerAndUser( defaultPlayer );
                }
                #endregion
            }
        }
    }
}