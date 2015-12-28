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
        public void FindNextLevel_method_returns_next_level_in_BuildingDefinition()
        {
            using ( var context = new BuildingContext() )
            {
                #region Seed

                Ressource barrackLevel1Cost = null;
                Ressource barrackLevel2Cost = null;
                Ressource barrackLevel3Cost = null;

                Requirement barrackLevel2Requirement = null;
                Requirement barrackLevel3Requirement = null;

                Level barrackLevel1 = null;
                Level barrackLevel2 = null;
                Level barrackLevel3 = null;

                Level technoLevel1 = null;

                Building barrack = null;

                Island defaultIsland = null;
                #endregion


                try
                {
                    #region Seed
                    barrackLevel2Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 1 };
                    barrackLevel3Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 2 };
                    context.Add( barrackLevel2Requirement );
                    context.Add( barrackLevel3Requirement );

                    barrackLevel1Cost = new Ressource { Wood = 100, Metal = 50 };
                    barrackLevel2Cost = Multiplyressource( barrackLevel1Cost, 2 );
                    barrackLevel3Cost = Multiplyressource( barrackLevel2Cost, 2 );
                    context.Add( barrackLevel1Cost );
                    context.Add( barrackLevel2Cost );
                    context.Add( barrackLevel3Cost );

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
                        Requirements = new List<Requirement> { barrackLevel3Requirement }
                    };
                    technoLevel1 = new TechnologyLevel
                    {
                        Number = 3,
                        TechnologyName = TechnologyName.armor
                    };
                    context.Add( barrackLevel1 );
                    context.Add( barrackLevel2 );
                    context.Add( barrackLevel3 );
                    context.Add( technoLevel1 );

                    barrack = new Building
                    {
                        Level = (BuildingLevel)barrackLevel1,
                        Name = "TestBarrack"
                    };
                    context.Add( barrack );

                    defaultIsland = new Island
                    {
                        Name = "defaultIsland",
                        IsCapital = true,
                        Buildings = new List<Building> { barrack }
                    };
                    context.Add( defaultIsland );

                    context.SaveChanges();
                    #endregion

                    BuildingManager bm = new BuildingManager( context, defaultIsland );
                    Assert.That( bm.isNextLevelAvailable( barrack.Level ) );
                }
                finally
                {
                    context.Remove( defaultIsland );
                    context.Remove( barrack );

                    context.Remove( barrackLevel1 );
                    context.Remove( barrackLevel2 );
                    context.Remove( barrackLevel3 );
                    context.Remove( technoLevel1 );

                    context.Remove( barrackLevel1Cost );
                    context.Remove( barrackLevel2Cost );
                    context.Remove( barrackLevel3Cost );

                    context.Remove( barrackLevel2Requirement );
                    context.Remove( barrackLevel3Requirement );

                    context.SaveChanges();
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
