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
        //[Test]
        //public void FindNextLevel_returns_next_level()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed

        //        Level barrackLevel1 = null;
        //        Level barrackLevel2 = null;

        //        Building barrack = null;

        //        Island defaultIsland = null;
        //        #endregion


        //        try
        //        {
        //            #region Seed

        //            // Set up Levels
        //            barrackLevel1 = new BuildingLevel
        //            {
        //                Number = 1,
        //                BuildingName = BuildingName.barrack
        //            };
        //            barrackLevel2 = new BuildingLevel
        //            {
        //                Number = 2,
        //                BuildingName = BuildingName.barrack
        //            };
        //            context.Add( barrackLevel1 );
        //            context.Add( barrackLevel2 );

        //            // Set up Building
        //            barrack = new Building
        //            {
        //                Name = "TestBarrack",
        //                BuildingName = BuildingName.barrack,
        //                Level = (BuildingLevel)barrackLevel1
        //            };
        //            context.Add( barrack );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Buildings = new List<Building> { barrack }
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that IsNextLevelAvailable retruns false, because there is no tower
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( bm.FindNextLevel( barrack.Level ) == barrackLevel2 );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            context.Remove( defaultIsland );
        //            context.Remove( barrack );

        //            context.Remove( barrackLevel1 );
        //            context.Remove( barrackLevel2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }

        //}
        //[Test]
        //public void FindNextLevel_returns_null_if_there_is_no_next_level_in_DB()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed

        //        Level barrackLevel1 = null;
        //        Level barrackLevel2 = null;

        //        Building barrack = null;

        //        Island defaultIsland = null;
        //        #endregion


        //        try
        //        {
        //            #region Seed

        //            // Set up Levels
        //            barrackLevel1 = new BuildingLevel
        //            {
        //                Number = 1,
        //                BuildingName = BuildingName.barrack
        //            };
        //            barrackLevel2 = new BuildingLevel
        //            {
        //                Number = 2,
        //                BuildingName = BuildingName.barrack
        //            };
        //            context.Add( barrackLevel1 );
        //            context.Add( barrackLevel2 );

        //            // Set up Building
        //            barrack = new Building
        //            {
        //                Name = "TestBarrack",
        //                BuildingName = BuildingName.barrack,
        //                Level = (BuildingLevel)barrackLevel2
        //            };
        //            context.Add( barrack );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Buildings = new List<Building> { barrack }
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that IsNextLevelAvailable retruns false, because there is no tower
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( bm.FindNextLevel( barrack.Level ) == null );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            context.Remove( defaultIsland );
        //            context.Remove( barrack );

        //            context.Remove( barrackLevel1 );
        //            context.Remove( barrackLevel2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }

        //}
        //[Test]
        //public void IsNextLevelAvailable_for_building_returns_true_if_conditions_are_met()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed

        //        Ressource barrackLevel1Cost = null;
        //        Ressource barrackLevel2Cost = null;
        //        Ressource barrackLevel3Cost = null;

        //        Requirement barrackLevel2Requirement = null;
        //        Requirement barrackLevel3Requirement = null;
        //        Requirement barrackLevel3Requirement2 = null;

        //        Level barrackLevel1 = null;
        //        Level barrackLevel2 = null;
        //        Level barrackLevel3 = null;
        //        Level towerLevel2 = null;

        //        Building barrack = null;
        //        Building tower = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion


        //        try
        //        {
        //            #region Seed

        //            // Set up requirements
        //            barrackLevel2Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 1 };
        //            barrackLevel3Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 2 };
        //            barrackLevel3Requirement2 = new Requirement { BuildingName = BuildingName.tower, Number = 2 };
        //            context.Add( barrackLevel2Requirement );
        //            context.Add( barrackLevel3Requirement );
        //            context.Add( barrackLevel3Requirement2 );

        //            // Set up LevelCosts
        //            barrackLevel1Cost = new Ressource { Wood = 100, Metal = 50 };
        //            barrackLevel2Cost = Multiplyressource( barrackLevel1Cost, 2 );
        //            barrackLevel3Cost = Multiplyressource( barrackLevel2Cost, 2 );
        //            context.Add( barrackLevel1Cost );
        //            context.Add( barrackLevel2Cost );
        //            context.Add( barrackLevel3Cost );

        //            // Set up Levels
        //            barrackLevel1 = new BuildingLevel
        //            {
        //                Number = 1,
        //                BuildingName = BuildingName.barrack,
        //                Cost = barrackLevel1Cost
        //            };
        //            barrackLevel2 = new BuildingLevel
        //            {
        //                Number = 2,
        //                BuildingName = BuildingName.barrack,
        //                Cost = barrackLevel2Cost,
        //                Requirements = new List<Requirement> { barrackLevel2Requirement }
        //            };
        //            barrackLevel3 = new BuildingLevel
        //            {
        //                Number = 3,
        //                BuildingName = BuildingName.barrack,
        //                Cost = barrackLevel3Cost,
        //                Requirements = new List<Requirement> { barrackLevel3Requirement, barrackLevel3Requirement2 }
        //            };
        //            towerLevel2 = new BuildingLevel
        //            {
        //                Number = 2,
        //                BuildingName = BuildingName.tower
        //            };
        //            context.Add( barrackLevel1 );
        //            context.Add( barrackLevel2 );
        //            context.Add( barrackLevel3 );
        //            context.Add( towerLevel2 );

        //            // Set up Buildings
        //            barrack = new Building
        //            {
        //                Name = "TestBarrack",
        //                BuildingName = BuildingName.barrack,
        //                Level = (BuildingLevel)barrackLevel2
        //            };
        //            context.Add( barrack );

        //            tower = new Building
        //            {
        //                Name = "TestTower",
        //                BuildingName = BuildingName.tower,
        //                Level = (BuildingLevel)towerLevel2
        //            };
        //            context.Add( tower );

        //            defaultPlayer = new Player { Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Buildings = new List<Building> { barrack, tower },
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that IsNextLevelAvailable retruns true, because all requirements are met
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( bm.IsNextLevelAvailable( barrack.Level ) );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );
        //            context.Remove( barrack );
        //            context.Remove( tower );

        //            context.Remove( barrackLevel1 );
        //            context.Remove( barrackLevel2 );
        //            context.Remove( barrackLevel3 );
        //            context.Remove( towerLevel2 );

        //            context.Remove( barrackLevel1Cost );
        //            context.Remove( barrackLevel2Cost );
        //            context.Remove( barrackLevel3Cost );

        //            context.Remove( barrackLevel2Requirement );
        //            context.Remove( barrackLevel3Requirement );
        //            context.Remove( barrackLevel3Requirement2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}

        //[Test]
        //public void IsNextLevelAvailable_for_building_returns_false_if_conditions_are_not_met()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed
        //        Requirement barrackLevel2Requirement = null;
        //        Requirement barrackLevel3Requirement = null;
        //        Requirement barrackLevel3Requirement2 = null;

        //        Level barrackLevel1 = null;
        //        Level barrackLevel2 = null;
        //        Level barrackLevel3 = null;

        //        Building barrack = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion


        //        try
        //        {
        //            #region Seed

        //            // Set up requirements
        //            barrackLevel2Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 1 };
        //            barrackLevel3Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 2 };
        //            barrackLevel3Requirement2 = new Requirement { BuildingName = BuildingName.tower, Number = 2 };
        //            context.Add( barrackLevel2Requirement );
        //            context.Add( barrackLevel3Requirement );
        //            context.Add( barrackLevel3Requirement2 );

        //            // Set up Levels
        //            barrackLevel1 = new BuildingLevel
        //            {
        //                Number = 1,
        //                BuildingName = BuildingName.barrack
        //            };
        //            barrackLevel2 = new BuildingLevel
        //            {
        //                Number = 2,
        //                BuildingName = BuildingName.barrack,
        //                Requirements = new List<Requirement> { barrackLevel2Requirement }
        //            };
        //            barrackLevel3 = new BuildingLevel
        //            {
        //                Number = 3,
        //                BuildingName = BuildingName.barrack,
        //                Requirements = new List<Requirement> { barrackLevel3Requirement, barrackLevel3Requirement2 }
        //            };

        //            context.Add( barrackLevel1 );
        //            context.Add( barrackLevel2 );
        //            context.Add( barrackLevel3 );

        //            // Set up Buildings WITHOUT THE TOWER
        //            barrack = new Building
        //            {
        //                Name = "TestBarrack",
        //                BuildingName = BuildingName.barrack,
        //                Level = (BuildingLevel)barrackLevel2
        //            };
        //            context.Add( barrack );

        //            // Set up Player
        //            defaultPlayer = new Player { Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Buildings = new List<Building> { barrack },
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that IsNextLevelAvailable retruns false, because there is no tower
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( !bm.IsNextLevelAvailable( barrack.Level ) );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );
        //            context.Remove( barrack );

        //            context.Remove( barrackLevel1 );
        //            context.Remove( barrackLevel2 );
        //            context.Remove( barrackLevel3 );

        //            context.Remove( barrackLevel2Requirement );
        //            context.Remove( barrackLevel3Requirement );
        //            context.Remove( barrackLevel3Requirement2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}

        //[Test]
        //public void IsNextLevelAvailable_for_technlogy_returns_true_if_conditions_are_met()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed
        //        Requirement armorLevel2Requirement = null;
        //        Requirement armorLevel3Requirement = null;
        //        Requirement armorLevel3Requirement2 = null;

        //        Level armorLevel1 = null;
        //        Level armorLevel2 = null;
        //        Level armorLevel3 = null;
        //        Level propulsionLevel1 = null;

        //        Technology armor = null;
        //        Technology propulsion = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion

        //        try
        //        {
        //            #region Seed

        //            // Set up requirements
        //            armorLevel2Requirement = new Requirement { TechnologyName = TechnologyName.armor, Number = 1 };
        //            armorLevel3Requirement = new Requirement { TechnologyName = TechnologyName.armor, Number = 2 };
        //            armorLevel3Requirement2 = new Requirement { TechnologyName = TechnologyName.propulsion, Number = 1 };
        //            context.Add( armorLevel2Requirement );
        //            context.Add( armorLevel3Requirement );
        //            context.Add( armorLevel3Requirement2 );

        //            // Set up Levels
        //            armorLevel1 = new TechnologyLevel
        //            {
        //                Number = 1,
        //                TechnologyName = TechnologyName.armor
        //            };
        //            armorLevel2 = new TechnologyLevel
        //            {
        //                Number = 2,
        //                TechnologyName = TechnologyName.armor,
        //                Requirements = new List<Requirement> { armorLevel2Requirement }
        //            };
        //            armorLevel3 = new TechnologyLevel
        //            {
        //                Number = 3,
        //                TechnologyName = TechnologyName.armor,
        //                Requirements = new List<Requirement> { armorLevel3Requirement, armorLevel3Requirement2 }
        //            };
        //            propulsionLevel1 = new TechnologyLevel
        //            {
        //                Number = 1,
        //                TechnologyName = TechnologyName.propulsion
        //            };
        //            context.Add( armorLevel1 );
        //            context.Add( armorLevel2 );
        //            context.Add( armorLevel3 );
        //            context.Add( propulsionLevel1 );

        //            // Set up Technologies
        //            armor = new Technology
        //            {
        //                Name = "TestBarrack",
        //                TechnologyName = TechnologyName.armor,
        //                Level = (TechnologyLevel)armorLevel2
        //            };
        //            propulsion = new Technology
        //            {
        //                Name = "TestBarrack",
        //                TechnologyName = TechnologyName.propulsion,
        //                Level = (TechnologyLevel)propulsionLevel1
        //            };
        //            context.Add( armor );
        //            context.Add( propulsion );

        //            // Set up Player
        //            defaultPlayer = new Player
        //            {
        //                Name = "Thanur",
        //                Mail = "toto@intechinfo.fr",
        //                Password = "toto",
        //                Technologies = new List<Technology> { armor, propulsion }
        //            };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that IsNextLevelAvailable retruns true
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( bm.IsNextLevelAvailable( armor.Level ) );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );

        //            context.Remove( armor );
        //            context.Remove( propulsion );

        //            context.Remove( armorLevel1 );
        //            context.Remove( armorLevel2 );
        //            context.Remove( armorLevel3 );
        //            context.Remove( propulsionLevel1 );

        //            context.Remove( armorLevel2Requirement );
        //            context.Remove( armorLevel3Requirement );
        //            context.Remove( armorLevel3Requirement2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}

        //[Test]
        //public void IsNextLevelAvailable_for_technlogy_returns_false_if_conditions_are_not_met()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed
        //        Requirement armorLevel2Requirement = null;
        //        Requirement armorLevel3Requirement = null;
        //        Requirement armorLevel3Requirement2 = null;

        //        Level armorLevel1 = null;
        //        Level armorLevel2 = null;
        //        Level armorLevel3 = null;

        //        Technology armor = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion

        //        try
        //        {
        //            #region Seed

        //            // Set up requirements
        //            armorLevel2Requirement = new Requirement { TechnologyName = TechnologyName.armor, Number = 1 };
        //            armorLevel3Requirement = new Requirement { TechnologyName = TechnologyName.armor, Number = 2 };
        //            armorLevel3Requirement2 = new Requirement { TechnologyName = TechnologyName.propulsion, Number = 1 };
        //            context.Add( armorLevel2Requirement );
        //            context.Add( armorLevel3Requirement );
        //            context.Add( armorLevel3Requirement2 );

        //            // Set up Levels
        //            armorLevel1 = new TechnologyLevel
        //            {
        //                Number = 1,
        //                TechnologyName = TechnologyName.armor
        //            };
        //            armorLevel2 = new TechnologyLevel
        //            {
        //                Number = 2,
        //                TechnologyName = TechnologyName.armor,
        //                Requirements = new List<Requirement> { armorLevel2Requirement }
        //            };
        //            armorLevel3 = new TechnologyLevel
        //            {
        //                Number = 3,
        //                TechnologyName = TechnologyName.armor,
        //                Requirements = new List<Requirement> { armorLevel3Requirement, armorLevel3Requirement2 }
        //            };
        //            context.Add( armorLevel1 );
        //            context.Add( armorLevel2 );
        //            context.Add( armorLevel3 );

        //            // Set up Technologies
        //            armor = new Technology
        //            {
        //                Name = "TestBarrack",
        //                TechnologyName = TechnologyName.armor,
        //                Level = (TechnologyLevel)armorLevel2
        //            };
        //            context.Add( armor );

        //            // Set up Player
        //            defaultPlayer = new Player
        //            {
        //                Name = "Thanur",
        //                Mail = "toto@intechinfo.fr",
        //                Password = "toto",
        //                Technologies = new List<Technology> { armor }
        //            };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that IsNextLevelAvailable retruns false, because there is no propulsion technology
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( !bm.IsNextLevelAvailable( armor.Level ) );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );

        //            context.Remove( armor );

        //            context.Remove( armorLevel1 );
        //            context.Remove( armorLevel2 );
        //            context.Remove( armorLevel3 );

        //            context.Remove( armorLevel2Requirement );
        //            context.Remove( armorLevel3Requirement );
        //            context.Remove( armorLevel3Requirement2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}

        //[Test]
        //public void LevelUp_method_for_building_returns_true_and_increments_level_if_a_superior_level_exists()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed
        //        Level barrackLevel1 = null;
        //        Level barrackLevel2 = null;
        //        Level barrackLevel3 = null;

        //        Building barrack = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion


        //        try
        //        {
        //            #region Seed
        //            // Set up Levels
        //            barrackLevel1 = new BuildingLevel
        //            {
        //                Number = 1,
        //                BuildingName = BuildingName.barrack
        //            };
        //            barrackLevel2 = new BuildingLevel
        //            {
        //                Number = 2,
        //                BuildingName = BuildingName.barrack
        //            };
        //            barrackLevel3 = new BuildingLevel
        //            {
        //                Number = 3,
        //                BuildingName = BuildingName.barrack
        //            };
        //            context.Add( barrackLevel1 );
        //            context.Add( barrackLevel2 );
        //            context.Add( barrackLevel3 );

        //            // Set up Buildings
        //            barrack = new Building
        //            {
        //                Name = "TestBarrack",
        //                BuildingName = BuildingName.barrack,
        //                Level = (BuildingLevel)barrackLevel2
        //            };
        //            context.Add( barrack );

        //            defaultPlayer = new Player { Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Buildings = new List<Building> { barrack },
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that LevelUp returns true and that the buildingLevel has been incremented
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( bm.LevelUp( barrack ) );
        //            context.SaveChanges();
        //            Assert.That( barrack.Level == barrackLevel3 );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );
        //            context.Remove( barrack );

        //            context.Remove( barrackLevel1 );
        //            context.Remove( barrackLevel2 );
        //            context.Remove( barrackLevel3 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}

        //[Test]
        //public void LevelUp_method_for_building_returns_false_and_does_not_increments_level_if_a_no_superior_level_exists()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed
        //        Level barrackLevel1 = null;
        //        Level barrackLevel2 = null;

        //        Building barrack = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion


        //        try
        //        {
        //            #region Seed
        //            // Set up Levels
        //            barrackLevel1 = new BuildingLevel
        //            {
        //                Number = 1,
        //                BuildingName = BuildingName.barrack
        //            };
        //            barrackLevel2 = new BuildingLevel
        //            {
        //                Number = 2,
        //                BuildingName = BuildingName.barrack
        //            };
        //            context.Add( barrackLevel1 );
        //            context.Add( barrackLevel2 );

        //            // Set up Buildings
        //            barrack = new Building
        //            {
        //                Name = "TestBarrack",
        //                BuildingName = BuildingName.barrack,
        //                Level = (BuildingLevel)barrackLevel2
        //            };
        //            context.Add( barrack );

        //            defaultPlayer = new Player { Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Buildings = new List<Building> { barrack },
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that LevelUp returns false and that the buildingLevel has not been incremented
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( !bm.LevelUp( barrack ) );
        //            context.SaveChanges();
        //            Assert.That( barrack.Level.Number == 2 );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );
        //            context.Remove( barrack );

        //            context.Remove( barrackLevel1 );
        //            context.Remove( barrackLevel2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}

        //[Test]
        //public void LevelUp_method_for_technology_returns_true_and_increments_level_if_a_superior_level_exists()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed
        //        Level armorLevel1 = null;
        //        Level armorLevel2 = null;
        //        Level armorLevel3 = null;

        //        Technology armor = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion

        //        try
        //        {
        //            #region Seed
        //            // Set up Levels
        //            armorLevel1 = new TechnologyLevel
        //            {
        //                Number = 1,
        //                TechnologyName = TechnologyName.armor
        //            };
        //            armorLevel2 = new TechnologyLevel
        //            {
        //                Number = 2,
        //                TechnologyName = TechnologyName.armor
        //            };
        //            armorLevel3 = new TechnologyLevel
        //            {
        //                Number = 3,
        //                TechnologyName = TechnologyName.armor
        //            };
        //            context.Add( armorLevel1 );
        //            context.Add( armorLevel2 );
        //            context.Add( armorLevel3 );

        //            // Set up Technologies
        //            armor = new Technology
        //            {
        //                Name = "TestBarrack",
        //                TechnologyName = TechnologyName.armor,
        //                Level = (TechnologyLevel)armorLevel2
        //            };
        //            context.Add( armor );

        //            // Set up Player
        //            defaultPlayer = new Player
        //            {
        //                Name = "Thanur",
        //                Mail = "toto@intechinfo.fr",
        //                Password = "toto",
        //                Technologies = new List<Technology> { armor }
        //            };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that LevelUp returns true and that the technologyLevel has been incremented
        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( bm.LevelUp( armor ) );
        //            context.SaveChanges();
        //            Assert.That( armor.Level == armorLevel3 );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );

        //            context.Remove( armor );

        //            context.Remove( armorLevel1 );
        //            context.Remove( armorLevel2 );
        //            context.Remove( armorLevel3 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}

        //[Test]
        //public void LevelUp_method_for_technology_returns_false_and_does_not_increments_level_if_a_no_superior_level_exists()
        //{
        //    using ( var context = new LevelContext() )
        //    {
        //        #region Seed
        //        Level armorLevel1 = null;
        //        Level armorLevel2 = null;

        //        Technology armor = null;

        //        Island defaultIsland = null;
        //        Player defaultPlayer = null;
        //        #endregion

        //        try
        //        {
        //            #region Seed
        //            // Set up Levels
        //            armorLevel1 = new TechnologyLevel
        //            {
        //                Number = 1,
        //                TechnologyName = TechnologyName.armor
        //            };
        //            armorLevel2 = new TechnologyLevel
        //            {
        //                Number = 2,
        //                TechnologyName = TechnologyName.armor
        //            };
        //            context.Add( armorLevel1 );
        //            context.Add( armorLevel2 );

        //            // Set up Technologies
        //            armor = new Technology
        //            {
        //                Name = "TestBarrack",
        //                TechnologyName = TechnologyName.armor,
        //                Level = (TechnologyLevel)armorLevel2
        //            };
        //            context.Add( armor );

        //            // Set up Player
        //            defaultPlayer = new Player
        //            {
        //                Name = "Thanur",
        //                Mail = "toto@intechinfo.fr",
        //                Password = "toto",
        //                Technologies = new List<Technology> { armor }
        //            };
        //            AddPlayerAndUser( defaultPlayer, context );

        //            // Set up Island
        //            defaultIsland = new Island
        //            {
        //                Name = "defaultIsland",
        //                IsCapital = true,
        //                Owner = defaultPlayer
        //            };
        //            context.Add( defaultIsland );

        //            context.SaveChanges();
        //            #endregion

        //            // Assert that LevelUp returns true and that the technologyLevel has been incremented

        //            LeveManager bm = new LeveManager( context, defaultIsland.IslandId );
        //            Assert.That( !bm.LevelUp( armor ) );
        //            context.SaveChanges();
        //            Assert.That( armor.Level.Number == 2 );
        //        }
        //        finally
        //        {
        //            #region Delete
        //            RemovePlayerAndUser( defaultPlayer, context );
        //            context.Remove( defaultIsland );

        //            context.Remove( armor );

        //            context.Remove( armorLevel1 );
        //            context.Remove( armorLevel2 );

        //            context.SaveChanges();
        //            #endregion
        //        }
        //    }
        //}


        #region Seed

        //[Test]
        //public void SeedAllTheThings()
        //{
        //    LevelSeed levelSeed = new LevelSeed();
        //    UnitSeed unitSeed = new UnitSeed();
        //    levelSeed.SeedLevels();
        //    unitSeed.SeedUnits();
        //}

        [Test]
        public void RessourceTest()
        {
            Ressource source = new Ressource { Wood = 100, Metal = 50, Cristal = 50, Magic = 100 };
            Ressource cost = new Ressource { Wood = 25, Metal = 50, Cristal = 10, Magic = 0 };

            RessourceManager.GetCoefficient( source, cost );
        }
        #endregion

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
