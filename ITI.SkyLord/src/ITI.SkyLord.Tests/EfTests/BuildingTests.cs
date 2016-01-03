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
    public class BuildingTests
    {
        [Test]
        public void IsNextLevelAvailable_returns_true_if_conditions_are_met()
        {
            using ( var context = new BuildingContext() )
            {
                #region Seed

                Ressource barrackLevel1Cost = null;
                Ressource barrackLevel2Cost = null;
                Ressource barrackLevel3Cost = null;

                Requirement barrackLevel2Requirement = null;
                Requirement barrackLevel3Requirement = null;
                Requirement barrackLevel3Requirement2 = null;

                Level barrackLevel1 = null;
                Level barrackLevel2 = null;
                Level barrackLevel3 = null;
                Level towerLevel2 = null;

                Building barrack = null;
                Building tower = null;

                Island defaultIsland = null;
                #endregion


                try
                {
                    #region Seed

                    // Set up requirements
                    barrackLevel2Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 1 };
                    barrackLevel3Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 2 };
                    barrackLevel3Requirement2 = new Requirement { BuildingName = BuildingName.tower, Number = 2 };
                    context.Add( barrackLevel2Requirement );
                    context.Add( barrackLevel3Requirement );
                    context.Add( barrackLevel3Requirement2 );

                    // Set up LevelCosts
                    barrackLevel1Cost = new Ressource { Wood = 100, Metal = 50 };
                    barrackLevel2Cost = Multiplyressource( barrackLevel1Cost, 2 );
                    barrackLevel3Cost = Multiplyressource( barrackLevel2Cost, 2 );
                    context.Add( barrackLevel1Cost );
                    context.Add( barrackLevel2Cost );
                    context.Add( barrackLevel3Cost );

                    // Set up Levels
                    barrackLevel1 = new BuildingLevel
                    {
                        Number = 1,
                        BuildingName = BuildingName.barrack,
                        Cost = barrackLevel1Cost
                    };
                    barrackLevel2 = new BuildingLevel
                    {
                        Number = 2,
                        BuildingName = BuildingName.barrack,
                        Cost = barrackLevel2Cost,
                        Requirements = new List<Requirement> { barrackLevel2Requirement }
                    };
                    barrackLevel3 = new BuildingLevel
                    {
                        Number = 3,
                        BuildingName = BuildingName.barrack,
                        Cost = barrackLevel3Cost,
                        Requirements = new List<Requirement> { barrackLevel3Requirement, barrackLevel3Requirement2 }
                    };
                    towerLevel2 = new BuildingLevel
                    {
                        Number = 2,
                        BuildingName = BuildingName.tower
                    };
                    context.Add( barrackLevel1 );
                    context.Add( barrackLevel2 );
                    context.Add( barrackLevel3 );
                    context.Add( towerLevel2 );

                    // Set up Buildings
                    barrack = new Building
                    {
                        Name = "TestBarrack",
                        BuildingName = BuildingName.barrack,
                        Level = (BuildingLevel)barrackLevel2
                    };
                    context.Add( barrack );

                    tower = new Building
                    {
                        Name = "TestTower",
                        BuildingName = BuildingName.tower,
                        Level = (BuildingLevel)towerLevel2
                    };
                    context.Add( tower );

                    // Set up Island
                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        IsCapital = true,
                        Buildings = new List<Building> { barrack, tower }
                    };
                    context.Add( defaultIsland );

                    context.SaveChanges();
                    #endregion

                    // Assert that IsNextLevelAvailable retruns true, because all requirements are met
                    BuildingManager bm = new BuildingManager( context, defaultIsland.IslandId );
                    Assert.That( bm.IsNextLevelAvailable( barrack.Level ) );
                }
                finally
                {
                    #region Delete
                    context.Remove( defaultIsland );
                    context.Remove( barrack );
                    context.Remove( tower );

                    context.Remove( barrackLevel1 );
                    context.Remove( barrackLevel2 );
                    context.Remove( barrackLevel3 );
                    context.Remove( towerLevel2 );

                    context.Remove( barrackLevel1Cost );
                    context.Remove( barrackLevel2Cost );
                    context.Remove( barrackLevel3Cost );

                    context.Remove( barrackLevel2Requirement );
                    context.Remove( barrackLevel3Requirement );
                    context.Remove( barrackLevel3Requirement2 );

                    context.SaveChanges(); 
                    #endregion
                }
            }
        }

        [Test]
        public void IsNextLevelAvailable_returns_false_if_conditions_are_not_met()
        {
            using ( var context = new BuildingContext() )
            {
                #region Seed

                Ressource barrackLevel1Cost = null;
                Ressource barrackLevel2Cost = null;
                Ressource barrackLevel3Cost = null;

                Requirement barrackLevel2Requirement = null;
                Requirement barrackLevel3Requirement = null;
                Requirement barrackLevel3Requirement2 = null;

                Level barrackLevel1 = null;
                Level barrackLevel2 = null;
                Level barrackLevel3 = null;

                Building barrack = null;

                Island defaultIsland = null;
                #endregion


                try
                {
                    #region Seed

                    // Set up requirements
                    barrackLevel2Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 1 };
                    barrackLevel3Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 2 };
                    barrackLevel3Requirement2 = new Requirement { BuildingName = BuildingName.tower, Number = 2 };
                    context.Add( barrackLevel2Requirement );
                    context.Add( barrackLevel3Requirement );
                    context.Add( barrackLevel3Requirement2 );

                    // Set up LevelCosts
                    barrackLevel1Cost = new Ressource { Wood = 100, Metal = 50 };
                    barrackLevel2Cost = Multiplyressource( barrackLevel1Cost, 2 );
                    barrackLevel3Cost = Multiplyressource( barrackLevel2Cost, 2 );
                    context.Add( barrackLevel1Cost );
                    context.Add( barrackLevel2Cost );
                    context.Add( barrackLevel3Cost );

                    // Set up Levels
                    barrackLevel1 = new BuildingLevel
                    {
                        Number = 1,
                        BuildingName = BuildingName.barrack,
                        Cost = barrackLevel1Cost
                    };
                    barrackLevel2 = new BuildingLevel
                    {
                        Number = 2,
                        BuildingName = BuildingName.barrack,
                        Cost = barrackLevel2Cost,
                        Requirements = new List<Requirement> { barrackLevel2Requirement }
                    };
                    barrackLevel3 = new BuildingLevel
                    {
                        Number = 3,
                        BuildingName = BuildingName.barrack,
                        Cost = barrackLevel3Cost,
                        Requirements = new List<Requirement> { barrackLevel3Requirement, barrackLevel3Requirement2 }
                    };

                    context.Add( barrackLevel1 );
                    context.Add( barrackLevel2 );
                    context.Add( barrackLevel3 );

                    // Set up Buildings WITHOUT THE TOWER
                    barrack = new Building
                    {
                        Name = "TestBarrack",
                        BuildingName = BuildingName.barrack,
                        Level = (BuildingLevel)barrackLevel2
                    };
                    context.Add( barrack );

                    // Set up Island
                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        IsCapital = true,
                        Buildings = new List<Building> { barrack }
                    };
                    context.Add( defaultIsland );

                    context.SaveChanges();
                    #endregion

                    // Assert that IsNextLevelAvailable retruns false, because there is no tower
                    BuildingManager bm = new BuildingManager( context, defaultIsland.IslandId );
                    Assert.That( !bm.IsNextLevelAvailable( barrack.Level ) );
                }
                finally
                {
                    #region Delete
                    context.Remove( defaultIsland );
                    context.Remove( barrack );

                    context.Remove( barrackLevel1 );
                    context.Remove( barrackLevel2 );
                    context.Remove( barrackLevel3 );

                    context.Remove( barrackLevel1Cost );
                    context.Remove( barrackLevel2Cost );
                    context.Remove( barrackLevel3Cost );

                    context.Remove( barrackLevel2Requirement );
                    context.Remove( barrackLevel3Requirement );
                    context.Remove( barrackLevel3Requirement2 );

                    context.SaveChanges();
                    #endregion
                }
            }
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
    }
}
