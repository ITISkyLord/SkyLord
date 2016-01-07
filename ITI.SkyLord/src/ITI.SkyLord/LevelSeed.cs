using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class LevelSeed
    {

        public void SeedBarrackLevels()
        {
            using ( LevelContext context = new LevelContext() )
            {
                // Add BarrackCosts
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

                // Add BarrackRequirements
                Requirement barrackLevel2Requirement = AddBuildingRequirement( context, BuildingName.barrack, 1 );
                Requirement barrackLevel3Requirement = AddBuildingRequirement( context, BuildingName.barrack, 2 );
                Requirement barrackLevel4Requirement = AddBuildingRequirement( context, BuildingName.barrack, 3 );
                Requirement barrackLevel5Requirement = AddBuildingRequirement( context, BuildingName.barrack, 4 );
                Requirement barrackLevel6Requirement = AddBuildingRequirement( context, BuildingName.barrack, 5 );
                Requirement barrackLevel7Requirement = AddBuildingRequirement( context, BuildingName.barrack, 6 );
                Requirement barrackLevel8Requirement = AddBuildingRequirement( context, BuildingName.barrack, 7 );
                Requirement barrackLevel9Requirement = AddBuildingRequirement( context, BuildingName.barrack, 8 );
                Requirement barrackLevel10Requirement = AddBuildingRequirement( context, BuildingName.barrack, 9 );

                // Set up Levels
                BuildingLevel barrackLevel1 = AddBuildingLevel( context, BuildingName.barrack, 1, barrackLevel1Cost, null);
                BuildingLevel barrackLevel2 = AddBuildingLevel( context, BuildingName.barrack, 2, barrackLevel2Cost, CreateRequirementList( barrackLevel2Requirement) );
                BuildingLevel barrackLevel3 = AddBuildingLevel( context, BuildingName.barrack, 3, barrackLevel3Cost, CreateRequirementList( barrackLevel3Requirement) );
                BuildingLevel barrackLevel4 = AddBuildingLevel( context, BuildingName.barrack, 4, barrackLevel3Cost, CreateRequirementList( barrackLevel4Requirement ) );
                BuildingLevel barrackLevel5 = AddBuildingLevel( context, BuildingName.barrack, 5, barrackLevel5Cost, CreateRequirementList( barrackLevel5Requirement ) );
                BuildingLevel barrackLevel6 = AddBuildingLevel( context, BuildingName.barrack, 6, barrackLevel6Cost, CreateRequirementList( barrackLevel6Requirement ) );
                BuildingLevel barrackLevel7 = AddBuildingLevel( context, BuildingName.barrack, 7, barrackLevel7Cost, CreateRequirementList( barrackLevel7Requirement ) );
                BuildingLevel barrackLevel8 = AddBuildingLevel( context, BuildingName.barrack, 8, barrackLevel8Cost, CreateRequirementList( barrackLevel8Requirement ) );
                BuildingLevel barrackLevel9 = AddBuildingLevel( context, BuildingName.barrack, 9, barrackLevel9Cost, CreateRequirementList( barrackLevel9Requirement ) );
                BuildingLevel barrackLevel10 = AddBuildingLevel( context, BuildingName.barrack, 10, barrackLevel10Cost, CreateRequirementList( barrackLevel10Requirement ) );

                context.SaveChanges();
            }
        }

        BuildingLevel AddBuildingLevel( LevelContext context, BuildingName buildingName, int number, Ressource cost, List<Requirement> requirements )
        {
            BuildingLevel buildingLevel = new BuildingLevel
            {
                Number = number,
                BuildingName = buildingName,
                Cost = cost,
                Requirements = requirements
            };
            context.Add( buildingLevel );
            return buildingLevel;
        }

        Requirement AddBuildingRequirement( LevelContext context, BuildingName buildingName, int number )
        {
            Requirement requirement = new Requirement
            {
                BuildingName = buildingName,
                Number = number
            };
            context.Add( requirement );
            return requirement;
        }

        List<Requirement> CreateRequirementList( params Requirement[] args )
        {
            List<Requirement> requirements = new List<Requirement>();
            foreach( Requirement r in args )
            {
                requirements.Add( r );
            }
            return requirements;
        }

        Ressource AddRessource( LevelContext context, int wood, int metal, int cristal, int magic )
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

        Ressource AddRessourceTimes2( LevelContext context, Ressource initialRessource )
        {
            Ressource multipliedRessoure = Multiplyressource( initialRessource, 2 );
            context.Add( multipliedRessoure );
            return multipliedRessoure;
        }

        Ressource Multiplyressource( Ressource initialRessource, int factor )
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
