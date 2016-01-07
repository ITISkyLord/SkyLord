using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class LevelSeed
    {
        Requirement barrackLevel2Requirement = null;
        Requirement barrackLevel3Requirement = null;
        Requirement barrackLevel3Requirement2 = null;

        Level barrackLevel1 = null;
        Level barrackLevel2 = null;
        Level barrackLevel3 = null;

        public void SeedBarrackLevels()
        {
            using ( LevelContext context = new LevelContext() )
            {
                // Add BarracksCosts
                Ressource barrackLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
                Ressource barrackLevel2Cost = AddRessourceTimes2( context, barrackLevel1Cost );
                Ressource barrackLevel3Cost = AddRessourceTimes2( context, barrackLevel2Cost );
                Ressource barrackLevel4Cost = AddRessourceTimes2( context, barrackLevel3Cost );
                Ressource barrackLevel5Cost = AddRessourceTimes2( context, barrackLevel4Cost );
                Ressource barrackLevel6Cost = AddRessourceTimes2( context, barrackLevel5Cost );
                Ressource barrackLevel7Cost = AddRessourceTimes2( context, barrackLevel6Cost );
                Ressource barrackLevel8Cost = AddRessourceTimes2( context, barrackLevel7Cost );
                Ressource barrackLevel9Cost = AddRessourceTimes2( context, barrackLevel8Cost );
                Ressource barrackLevel10Cost = AddRessourceTimes2( context, barrackLevel9Cost );

                barrackLevel2Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 1 };
                barrackLevel3Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 2 };
                barrackLevel3Requirement2 = new Requirement { BuildingName = BuildingName.tower, Number = 2 };
                context.Add( barrackLevel2Requirement );
                context.Add( barrackLevel3Requirement );
                context.Add( barrackLevel3Requirement2 );

                // Set up Levels
                barrackLevel1 = new BuildingLevel
                {
                    Number = 1,
                    BuildingName = BuildingName.barrack,
                    Cost = barrackLevel1Cost
                };
            }
        }

        public void AddBuildingLevel( LevelContext context, BuildingName buildingName, int number, Ressource cost, List<Requirement> requirements )
        {
            BuildingLevel buildingLevel = new BuildingLevel
            {
                Number = number,
                BuildingName = buildingName,
                Cost = cost,
                Requirements = requirements
            };
            context.Add( buildingLevel );
        }

        public Ressource AddRessource( LevelContext context, int wood, int metal, int cristal, int magic )
        {
            Ressource ressource = new Ressource {
                Wood = wood,
                Metal = wood,
                Cristal = cristal,
                Magic = magic
            };
            context.Add( ressource );
            return ressource;
        }

        public Ressource AddRessourceTimes2( LevelContext context, Ressource initialRessource )
        {
            Ressource multipliedRessoure = Multiplyressource( initialRessource, 2 );
            context.Add( multipliedRessoure );
            return multipliedRessoure;
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
