using ITI.SkyLord.Models.Entity_Framework.Contexts;
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
                BuildingLevel buildingLevel1 = null;
                BuildingLevel buildingLevel2 = null;
                BuildingLevel buildingLevel3 = null;

                BuildingDefinition buildingDefinitionTest = null;
                Building testBuilding = null;
                try
                {
                    buildingLevel1 = new BuildingLevel { Number = 1 };
                    buildingLevel2 = new BuildingLevel { Number = 2 };
                    buildingLevel3 = new BuildingLevel { Number = 3 };
                    context.BuildingLevels.Add( buildingLevel1 );
                    context.BuildingLevels.Add( buildingLevel2 );
                    context.BuildingLevels.Add( buildingLevel3 );

                    buildingDefinitionTest = new BuildingDefinition
                    {
                        Name = "TestBuilding",
                        PossibleLevels = new List<BuildingLevel> { buildingLevel1, buildingLevel2, buildingLevel3 }
                    };
                    context.Add( buildingDefinitionTest );

                    testBuilding = new Building { Name = "Testbuilding", Level = buildingLevel1 };
                    context.Buildings.Add( testBuilding );

                    context.SaveChanges();
                }
                finally
                {
                    context.Remove( testBuilding );
                    context.Remove( buildingDefinitionTest );

                    context.Remove( buildingLevel1 );
                    context.Remove( buildingLevel2 );
                    context.Remove( buildingLevel3 );

                    context.SaveChanges();
                }
            }
        }
    }
}
