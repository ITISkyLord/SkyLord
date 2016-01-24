using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class LevelSeed
    {
        public void SeedLevels()
        {
            SeedIslands();
            SeedBuildingLevels();
            SeedTechnologyLevels();
        }

        #region Island
        public void SeedIslands()
        {
            using (SetupContext context = new SetupContext())
            {
                Random random = new Random();
                if (context.Islands.Count() < 99)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        for(int j=0; j < 100; j++)
                        {
                            if(random.Next(1,6) == 1)
                            {
                                AddIsland(i, j, context);
                            }
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public void AddIsland(int x, int y, SetupContext context)
        {
            Ressource ressource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
            Coordinate coord = new Coordinate();
            coord.X = x;
            coord.Y = y;
            context.Ressources.Add(ressource);
            context.Coordinates.Add(coord);

            Island island = new Island();
            island.Loyalty = 100;
            island.Coordinates = coord;
            island.AllRessources = ressource;
            island.PossiblePositions = 10;

            context.Islands.Add(island);
        }
        #endregion Island

        public void SeedBuildingLevels()
        {
            SeedAcademyLevels();
            SeedBarrackLevels();
            SeedCacheLevels();
            SeedCristalLevels();
            SeedForgeLevels();
            SeedInvocationLevels();
            SeedLaboratoryLevels();
            SeedMagicLevels();
            SeedMetalLevels();
            SeedShieldLevels();
            SeedTowerLevels();
            SeedWoodLevels();
        }

        public void SeedTechnologyLevels()
        {
            SeedArmorLevels();
            SeedClawsLevels();
            SeedHoldLevels();
            SeedLevitationLevels();
            SeedMagicalProtectionLevels();
            SeedmagicalStaffLevels();
            SeedMagicBootsLevels();
            SeedPlatingLevels();
            SeedPropulsionLevels();
            SeedShellLevels();
            SeedWeaponsLevels();
            SeedWingsLevels();
        }


        #region Buildings

        public void SeedTowerLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add towerCosts
                Ressource towerLevel1Cost = AddRessource( context, 100, 100, 0, 0 );
                Ressource towerLevel2Cost = AddRessource( context, 200, 200, 0, 0 );
                Ressource towerLevel3Cost = AddMiltipliedRessource( context, towerLevel2Cost, 2.5 );
                Ressource towerLevel4Cost = AddMiltipliedRessource( context, towerLevel3Cost, 2.5 );
                Ressource towerLevel5Cost = AddMiltipliedRessource( context, towerLevel4Cost, 2.75 );
                Ressource towerLevel6Cost = AddMiltipliedRessource( context, towerLevel5Cost, 2.75 );
                Ressource towerLevel7Cost = AddMiltipliedRessource( context, towerLevel6Cost, 3.0 );
                Ressource towerLevel8Cost = AddMiltipliedRessource( context, towerLevel7Cost, 3.0 );
                Ressource towerLevel9Cost = AddMiltipliedRessource( context, towerLevel8Cost, 3.25 );
                Ressource towerLevel10Cost = AddMiltipliedRessource( context, towerLevel9Cost, 3.50 );

                // Add Bonuses ( 2% duration bonus on all buildings / (level > 2) of tower )
                BonusBuildingOnBuilding buildingsDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 4 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 6 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 8 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 10 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 12 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 14 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 16 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 18 );
                BonusBuildingOnBuilding buildingsDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 20 );

                // Set up Levels
                BuildingLevel towerLevel1 = AddBuildingLevel( context, BuildingName.tower, 1, towerLevel1Cost, 0, null, null );
                BuildingLevel towerLevel2 = AddBuildingLevel( context, BuildingName.tower, 2, towerLevel2Cost, 60,
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel2 ) );
                BuildingLevel towerLevel3 = AddBuildingLevel( context, BuildingName.tower, 3, towerLevel3Cost, (int)( towerLevel2.Duration * 2.5 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel3 ) );
                BuildingLevel towerLevel4 = AddBuildingLevel( context, BuildingName.tower, 4, towerLevel3Cost, (int)( towerLevel3.Duration * 2.5 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel4 ) );
                BuildingLevel towerLevel5 = AddBuildingLevel( context, BuildingName.tower, 5, towerLevel5Cost, (int)( towerLevel4.Duration * 2.75 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel5 ) );
                BuildingLevel towerLevel6 = AddBuildingLevel( context, BuildingName.tower, 6, towerLevel6Cost, (int)( towerLevel5.Duration * 2.75 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel6 ) );
                BuildingLevel towerLevel7 = AddBuildingLevel( context, BuildingName.tower, 7, towerLevel7Cost, (int)( towerLevel6.Duration * 2.75 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel7 ) );
                BuildingLevel towerLevel8 = AddBuildingLevel( context, BuildingName.tower, 8, towerLevel8Cost, (int)( towerLevel7.Duration * 3.0 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel8 ) );
                BuildingLevel towerLevel9 = AddBuildingLevel( context, BuildingName.tower, 9, towerLevel9Cost, (int)( towerLevel8.Duration * 3.0 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel9 ) );
                BuildingLevel towerLevel10 = AddBuildingLevel( context, BuildingName.tower, 10, towerLevel10Cost, (int)( towerLevel9.Duration * 3.25 ),
                    null,
                    CreateBonusBuildingList( buildingsDurationBonusLevel10 ) );

                context.SaveChanges();
            }
        }

        public void SeedBarrackLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add BarrackCosts
                Ressource barrackLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
                Ressource barrackLevel2Cost = AddMiltipliedRessource( context, barrackLevel1Cost, 2.5 );
                Ressource barrackLevel3Cost = AddMiltipliedRessource( context, barrackLevel2Cost, 2.5 );
                Ressource barrackLevel4Cost = AddMiltipliedRessource( context, barrackLevel3Cost, 2.75 );
                Ressource barrackLevel5Cost = AddMiltipliedRessource( context, barrackLevel4Cost, 2.75 );
                Ressource barrackLevel6Cost = AddMiltipliedRessource( context, barrackLevel5Cost, 2.75 );
                Ressource barrackLevel7Cost = AddMiltipliedRessource( context, barrackLevel6Cost, 3 );
                Ressource barrackLevel8Cost = AddMiltipliedRessource( context, barrackLevel7Cost, 3 );
                Ressource barrackLevel9Cost = AddMiltipliedRessource( context, barrackLevel8Cost, 3.25 );
                Ressource barrackLevel10Cost = AddMiltipliedRessource( context, barrackLevel9Cost, 3.5 );

                // Add BarrackRequirements
                Requirement barrackLevel1Requirement = AddBuildingRequirement( context, BuildingName.tower, 1 );
                Requirement barrackLevel2Requirement = AddBuildingRequirement( context, BuildingName.barrack, 1 );
                Requirement barrackLevel3Requirement = AddBuildingRequirement( context, BuildingName.barrack, 2 );
                Requirement barrackLevel4Requirement = AddBuildingRequirement( context, BuildingName.barrack, 3 );
                Requirement barrackLevel5Requirement = AddBuildingRequirement( context, BuildingName.barrack, 4 );
                Requirement barrackLevel6Requirement = AddBuildingRequirement( context, BuildingName.barrack, 5 );
                Requirement barrackLevel7Requirement = AddBuildingRequirement( context, BuildingName.barrack, 6 );
                Requirement barrackLevel8Requirement = AddBuildingRequirement( context, BuildingName.barrack, 7 );
                Requirement barrackLevel9Requirement = AddBuildingRequirement( context, BuildingName.barrack, 8 );
                Requirement barrackLevel10Requirement = AddBuildingRequirement( context, BuildingName.barrack, 9 );

                //Add Bonuses ( 1% duration bonus monster and soldier type units / barrack level )
                BonusBuildingOnUnit monsterDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 1 );
                BonusBuildingOnUnit monsterDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 2 );
                BonusBuildingOnUnit monsterDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 3 );
                BonusBuildingOnUnit monsterDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 4 );
                BonusBuildingOnUnit monsterDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 5 );
                BonusBuildingOnUnit monsterDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 6 );
                BonusBuildingOnUnit monsterDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 7 );
                BonusBuildingOnUnit monsterDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 8 );
                BonusBuildingOnUnit monsterDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 9 );
                BonusBuildingOnUnit monsterDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 10 );

                BonusBuildingOnUnit soldierDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 1 );
                BonusBuildingOnUnit soldierDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 2 );
                BonusBuildingOnUnit soldierDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 3 );
                BonusBuildingOnUnit soldierDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 4 );
                BonusBuildingOnUnit soldierDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 5 );
                BonusBuildingOnUnit soldierDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 6 );
                BonusBuildingOnUnit soldierDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 7 );
                BonusBuildingOnUnit soldierDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 8 );
                BonusBuildingOnUnit soldierDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 9 );
                BonusBuildingOnUnit soldierDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 10 );

                // Set up Levels
                BuildingLevel barrackLevel1 = AddBuildingLevel( context, BuildingName.barrack, 1, barrackLevel1Cost, 30,
                    CreateRequirementList( barrackLevel1Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel1, soldierDurationBonusLevel1 ) );
                BuildingLevel barrackLevel2 = AddBuildingLevel( context, BuildingName.barrack, 2, barrackLevel2Cost, (int)( barrackLevel1.Duration * 2.5 ),
                    CreateRequirementList( barrackLevel2Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel2, soldierDurationBonusLevel2 ) );
                BuildingLevel barrackLevel3 = AddBuildingLevel( context, BuildingName.barrack, 3, barrackLevel3Cost, (int)( barrackLevel2.Duration * 2.5 ),
                    CreateRequirementList( barrackLevel3Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel3, soldierDurationBonusLevel3 ) );
                BuildingLevel barrackLevel4 = AddBuildingLevel( context, BuildingName.barrack, 4, barrackLevel3Cost, (int)( barrackLevel3.Duration * 2.75 ),
                    CreateRequirementList( barrackLevel4Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel4, soldierDurationBonusLevel4 ) );
                BuildingLevel barrackLevel5 = AddBuildingLevel( context, BuildingName.barrack, 5, barrackLevel5Cost, (int)( barrackLevel4.Duration * 2.75 ),
                    CreateRequirementList( barrackLevel5Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel5, soldierDurationBonusLevel5 ) );
                BuildingLevel barrackLevel6 = AddBuildingLevel( context, BuildingName.barrack, 6, barrackLevel6Cost, (int)( barrackLevel5.Duration * 2.75 ),
                    CreateRequirementList( barrackLevel6Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel6, soldierDurationBonusLevel6 ) );
                BuildingLevel barrackLevel7 = AddBuildingLevel( context, BuildingName.barrack, 7, barrackLevel7Cost, (int)( barrackLevel6.Duration * 3.0 ),
                    CreateRequirementList( barrackLevel7Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel7, soldierDurationBonusLevel7 ) );
                BuildingLevel barrackLevel8 = AddBuildingLevel( context, BuildingName.barrack, 8, barrackLevel8Cost, (int)( barrackLevel7.Duration * 3.0 ),
                    CreateRequirementList( barrackLevel8Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel8, soldierDurationBonusLevel8 ) );
                BuildingLevel barrackLevel9 = AddBuildingLevel( context, BuildingName.barrack, 9, barrackLevel9Cost, (int)( barrackLevel8.Duration * 3.0 ),
                    CreateRequirementList( barrackLevel9Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel9, soldierDurationBonusLevel9 ) );
                BuildingLevel barrackLevel10 = AddBuildingLevel( context, BuildingName.barrack, 10, barrackLevel10Cost, (int)( barrackLevel9.Duration * 3.25 ),
                    CreateRequirementList( barrackLevel10Requirement ),
                    CreateBonusBuildingList( monsterDurationBonusLevel10, soldierDurationBonusLevel10 ) );

                context.SaveChanges();
            }
        }

        public void SeedForgeLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add forgeCosts
                Ressource forgeLevel1Cost = AddRessource( context, 125, 100, 50, 0 );
                Ressource forgeLevel2Cost = AddMiltipliedRessource( context, forgeLevel1Cost, 2.5 );
                Ressource forgeLevel3Cost = AddMiltipliedRessource( context, forgeLevel2Cost, 2.5 );
                Ressource forgeLevel4Cost = AddMiltipliedRessource( context, forgeLevel3Cost, 2.75 );
                Ressource forgeLevel5Cost = AddMiltipliedRessource( context, forgeLevel4Cost, 2.75 );
                Ressource forgeLevel6Cost = AddMiltipliedRessource( context, forgeLevel5Cost, 2.75 );
                Ressource forgeLevel7Cost = AddMiltipliedRessource( context, forgeLevel6Cost, 3 );
                Ressource forgeLevel8Cost = AddMiltipliedRessource( context, forgeLevel7Cost, 3 );
                Ressource forgeLevel9Cost = AddMiltipliedRessource( context, forgeLevel8Cost, 3.25 );
                Ressource forgeLevel10Cost = AddMiltipliedRessource( context, forgeLevel9Cost, 3.5 );

                // Add forgeRequirements
                Requirement forgeLevel1Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 1 );
                Requirement forgeLevel2Requirement = AddBuildingRequirement( context, BuildingName.forge, 1 );
                Requirement forgeLevel3Requirement = AddBuildingRequirement( context, BuildingName.forge, 2 );
                Requirement forgeLevel4Requirement = AddBuildingRequirement( context, BuildingName.forge, 3 );
                Requirement forgeLevel5Requirement = AddBuildingRequirement( context, BuildingName.forge, 4 );
                Requirement forgeLevel6Requirement = AddBuildingRequirement( context, BuildingName.forge, 5 );
                Requirement forgeLevel7Requirement = AddBuildingRequirement( context, BuildingName.forge, 6 );
                Requirement forgeLevel8Requirement = AddBuildingRequirement( context, BuildingName.forge, 7 );
                Requirement forgeLevel9Requirement = AddBuildingRequirement( context, BuildingName.forge, 8 );
                Requirement forgeLevel10Requirement = AddBuildingRequirement( context, BuildingName.forge, 9 );

                //Add Bonuses ( 1% duration bonus mecanical type units / forge level )
                BonusBuildingOnUnit mecanicalDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 1 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 2 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 3 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 4 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 5 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 6 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 7 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 8 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 9 );
                BonusBuildingOnUnit mecanicalDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, UnitType.mecanical, 10 );

                // Set up Levels
                BuildingLevel forgeLevel1 = AddBuildingLevel( context, BuildingName.forge, 1, forgeLevel1Cost, 30,
                    CreateRequirementList( forgeLevel1Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel1 ) );
                BuildingLevel forgeLevel2 = AddBuildingLevel( context, BuildingName.forge, 2, forgeLevel2Cost, (int)( forgeLevel1.Duration * 2.5 ),
                    CreateRequirementList( forgeLevel2Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel2 ) );
                BuildingLevel forgeLevel3 = AddBuildingLevel( context, BuildingName.forge, 3, forgeLevel3Cost, (int)( forgeLevel2.Duration * 2.5 ),
                    CreateRequirementList( forgeLevel3Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel3 ) );
                BuildingLevel forgeLevel4 = AddBuildingLevel( context, BuildingName.forge, 4, forgeLevel3Cost, (int)( forgeLevel3.Duration * 2.75 ),
                    CreateRequirementList( forgeLevel4Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel4 ) );
                BuildingLevel forgeLevel5 = AddBuildingLevel( context, BuildingName.forge, 5, forgeLevel5Cost, (int)( forgeLevel4.Duration * 2.75 ),
                    CreateRequirementList( forgeLevel5Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel5 ) );
                BuildingLevel forgeLevel6 = AddBuildingLevel( context, BuildingName.forge, 6, forgeLevel6Cost, (int)( forgeLevel5.Duration * 2.75 ),
                    CreateRequirementList( forgeLevel6Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel6 ) );
                BuildingLevel forgeLevel7 = AddBuildingLevel( context, BuildingName.forge, 7, forgeLevel7Cost, (int)( forgeLevel6.Duration * 3.0 ),
                    CreateRequirementList( forgeLevel7Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel7 ) );
                BuildingLevel forgeLevel8 = AddBuildingLevel( context, BuildingName.forge, 8, forgeLevel8Cost, (int)( forgeLevel7.Duration * 3.0 ),
                    CreateRequirementList( forgeLevel8Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel8 ) );
                BuildingLevel forgeLevel9 = AddBuildingLevel( context, BuildingName.forge, 9, forgeLevel9Cost, (int)( forgeLevel8.Duration * 3.0 ),
                    CreateRequirementList( forgeLevel9Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel9 ) );
                BuildingLevel forgeLevel10 = AddBuildingLevel( context, BuildingName.forge, 10, forgeLevel10Cost, (int)( forgeLevel9.Duration * 3.25 ),
                    CreateRequirementList( forgeLevel10Requirement ),
                    CreateBonusBuildingList( mecanicalDurationBonusLevel10 ) );

                context.SaveChanges();
            }
        }

        public void SeedLaboratoryLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add laboratoryCosts
                Ressource laboratoryLevel1Cost = AddRessource( context, 100, 100, 150, 50 );
                Ressource laboratoryLevel2Cost = AddMiltipliedRessource( context, laboratoryLevel1Cost, 2.5 );
                Ressource laboratoryLevel3Cost = AddMiltipliedRessource( context, laboratoryLevel2Cost, 2.5 );
                Ressource laboratoryLevel4Cost = AddMiltipliedRessource( context, laboratoryLevel3Cost, 2.75 );
                Ressource laboratoryLevel5Cost = AddMiltipliedRessource( context, laboratoryLevel4Cost, 2.75 );
                Ressource laboratoryLevel6Cost = AddMiltipliedRessource( context, laboratoryLevel5Cost, 2.75 );
                Ressource laboratoryLevel7Cost = AddMiltipliedRessource( context, laboratoryLevel6Cost, 3 );
                Ressource laboratoryLevel8Cost = AddMiltipliedRessource( context, laboratoryLevel7Cost, 3 );
                Ressource laboratoryLevel9Cost = AddMiltipliedRessource( context, laboratoryLevel8Cost, 3.25 );
                Ressource laboratoryLevel10Cost = AddMiltipliedRessource( context, laboratoryLevel9Cost, 3.5 );

                // Add laboratoryRequirements
                Requirement laboratoryLevel1Requirement = AddBuildingRequirement( context, BuildingName.tower, 3 );
                Requirement laboratoryLevel2Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 1 );
                Requirement laboratoryLevel3Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 2 );
                Requirement laboratoryLevel4Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 3 );
                Requirement laboratoryLevel5Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 4 );
                Requirement laboratoryLevel6Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 5 );
                Requirement laboratoryLevel7Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 6 );
                Requirement laboratoryLevel8Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 7 );
                Requirement laboratoryLevel9Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 8 );
                Requirement laboratoryLevel10Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 9 );

                //Add Bonuses ( 1% duration bonus all technology developpement / laboratory level )
                BonusBuildingOnTechnology technologyDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 1 );
                BonusBuildingOnTechnology technologyDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 2 );
                BonusBuildingOnTechnology technologyDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 3 );
                BonusBuildingOnTechnology technologyDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 4 );
                BonusBuildingOnTechnology technologyDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 5 );
                BonusBuildingOnTechnology technologyDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 6 );
                BonusBuildingOnTechnology technologyDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 7 );
                BonusBuildingOnTechnology technologyDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 8 );
                BonusBuildingOnTechnology technologyDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 9 );
                BonusBuildingOnTechnology technologyDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, TechnologyName.none, 10 );

                // Set up Levels
                BuildingLevel laboratoryLevel1 = AddBuildingLevel( context, BuildingName.laboratory, 1, laboratoryLevel1Cost, 30,
                    CreateRequirementList( laboratoryLevel1Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel1 ) );
                BuildingLevel laboratoryLevel2 = AddBuildingLevel( context, BuildingName.laboratory, 2, laboratoryLevel2Cost, (int)( laboratoryLevel1.Duration * 2.5 ),
                    CreateRequirementList( laboratoryLevel2Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel2 ) );
                BuildingLevel laboratoryLevel3 = AddBuildingLevel( context, BuildingName.laboratory, 3, laboratoryLevel3Cost, (int)( laboratoryLevel2.Duration * 2.5 ),
                    CreateRequirementList( laboratoryLevel3Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel3 ) );
                BuildingLevel laboratoryLevel4 = AddBuildingLevel( context, BuildingName.laboratory, 4, laboratoryLevel3Cost, (int)( laboratoryLevel3.Duration * 2.5 ),
                    CreateRequirementList( laboratoryLevel4Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel4 ) );
                BuildingLevel laboratoryLevel5 = AddBuildingLevel( context, BuildingName.laboratory, 5, laboratoryLevel5Cost, (int)( laboratoryLevel4.Duration * 2.75 ),
                    CreateRequirementList( laboratoryLevel5Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel5 ) );
                BuildingLevel laboratoryLevel6 = AddBuildingLevel( context, BuildingName.laboratory, 6, laboratoryLevel6Cost, (int)( laboratoryLevel5.Duration * 2.75 ),
                    CreateRequirementList( laboratoryLevel6Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel6 ) );
                BuildingLevel laboratoryLevel7 = AddBuildingLevel( context, BuildingName.laboratory, 7, laboratoryLevel7Cost, (int)( laboratoryLevel6.Duration * 2.75 ),
                    CreateRequirementList( laboratoryLevel7Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel7 ) );
                BuildingLevel laboratoryLevel8 = AddBuildingLevel( context, BuildingName.laboratory, 8, laboratoryLevel8Cost, (int)( laboratoryLevel7.Duration * 3.0 ),
                    CreateRequirementList( laboratoryLevel8Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel8 ) );
                BuildingLevel laboratoryLevel9 = AddBuildingLevel( context, BuildingName.laboratory, 9, laboratoryLevel9Cost, (int)( laboratoryLevel8.Duration * 3.0 ),
                    CreateRequirementList( laboratoryLevel9Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel9 ) );
                BuildingLevel laboratoryLevel10 = AddBuildingLevel( context, BuildingName.laboratory, 10, laboratoryLevel10Cost, (int)( laboratoryLevel9.Duration * 3.25 ),
                    CreateRequirementList( laboratoryLevel10Requirement ),
                    CreateBonusBuildingList( technologyDurationBonusLevel10 ) );

                context.SaveChanges();
            }
        }

        public void SeedInvocationLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add invocationCosts
                Ressource invocationLevel1Cost = AddRessource( context, 100, 100, 150, 50 );
                Ressource invocationLevel2Cost = AddMiltipliedRessource( context, invocationLevel1Cost, 2.5 );
                Ressource invocationLevel3Cost = AddMiltipliedRessource( context, invocationLevel2Cost, 2.5 );
                Ressource invocationLevel4Cost = AddMiltipliedRessource( context, invocationLevel3Cost, 2.75 );
                Ressource invocationLevel5Cost = AddMiltipliedRessource( context, invocationLevel4Cost, 2.75 );
                Ressource invocationLevel6Cost = AddMiltipliedRessource( context, invocationLevel5Cost, 2.75 );
                Ressource invocationLevel7Cost = AddMiltipliedRessource( context, invocationLevel6Cost, 3 );
                Ressource invocationLevel8Cost = AddMiltipliedRessource( context, invocationLevel7Cost, 3 );
                Ressource invocationLevel9Cost = AddMiltipliedRessource( context, invocationLevel8Cost, 3.25 );
                Ressource invocationLevel10Cost = AddMiltipliedRessource( context, invocationLevel9Cost, 3.5 );

                // Add invocationRequirements
                Requirement invocationLevel1Requirement = AddBuildingRequirement( context, BuildingName.tower, 3 );
                Requirement invocationLevel2Requirement = AddBuildingRequirement( context, BuildingName.invocation, 1 );
                Requirement invocationLevel3Requirement = AddBuildingRequirement( context, BuildingName.invocation, 2 );
                Requirement invocationLevel4Requirement = AddBuildingRequirement( context, BuildingName.invocation, 3 );
                Requirement invocationLevel5Requirement = AddBuildingRequirement( context, BuildingName.invocation, 4 );
                Requirement invocationLevel6Requirement = AddBuildingRequirement( context, BuildingName.invocation, 5 );
                Requirement invocationLevel7Requirement = AddBuildingRequirement( context, BuildingName.invocation, 6 );
                Requirement invocationLevel8Requirement = AddBuildingRequirement( context, BuildingName.invocation, 7 );
                Requirement invocationLevel9Requirement = AddBuildingRequirement( context, BuildingName.invocation, 8 );
                Requirement invocationLevel10Requirement = AddBuildingRequirement( context, BuildingName.invocation, 9 );

                //Add Bonuses ( 1% duration bonus on all unit production / invocation level )
                BonusBuildingOnUnit unitDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 1 );
                BonusBuildingOnUnit unitDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 2 );
                BonusBuildingOnUnit unitDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 3 );
                BonusBuildingOnUnit unitDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 4 );
                BonusBuildingOnUnit unitDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 5 );
                BonusBuildingOnUnit unitDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 6 );
                BonusBuildingOnUnit unitDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 7 );
                BonusBuildingOnUnit unitDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 8 );
                BonusBuildingOnUnit unitDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 9 );
                BonusBuildingOnUnit unitDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, UnitType.all, 10 );

                // Set up Levels
                BuildingLevel invocationLevel1 = AddBuildingLevel( context, BuildingName.invocation, 1, invocationLevel1Cost, 30,
                    CreateRequirementList( invocationLevel1Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel1 ) );
                BuildingLevel invocationLevel2 = AddBuildingLevel( context, BuildingName.invocation, 2, invocationLevel2Cost, (int)( invocationLevel1.Duration * 2.5 ),
                    CreateRequirementList( invocationLevel2Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel1 ) );
                BuildingLevel invocationLevel3 = AddBuildingLevel( context, BuildingName.invocation, 3, invocationLevel3Cost, (int)( invocationLevel2.Duration * 2.5 ),
                    CreateRequirementList( invocationLevel3Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel3 ) );
                BuildingLevel invocationLevel4 = AddBuildingLevel( context, BuildingName.invocation, 4, invocationLevel3Cost, (int)( invocationLevel3.Duration * 2.5 ),
                    CreateRequirementList( invocationLevel4Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel4 ) );
                BuildingLevel invocationLevel5 = AddBuildingLevel( context, BuildingName.invocation, 5, invocationLevel5Cost, (int)( invocationLevel4.Duration * 2.75 ),
                    CreateRequirementList( invocationLevel5Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel5 ) );
                BuildingLevel invocationLevel6 = AddBuildingLevel( context, BuildingName.invocation, 6, invocationLevel6Cost, (int)( invocationLevel5.Duration * 2.75 ),
                    CreateRequirementList( invocationLevel6Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel6 ) );
                BuildingLevel invocationLevel7 = AddBuildingLevel( context, BuildingName.invocation, 7, invocationLevel7Cost, (int)( invocationLevel6.Duration * 2.75 ),
                    CreateRequirementList( invocationLevel7Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel7 ) );
                BuildingLevel invocationLevel8 = AddBuildingLevel( context, BuildingName.invocation, 8, invocationLevel8Cost, (int)( invocationLevel7.Duration * 3.0 ),
                    CreateRequirementList( invocationLevel8Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel8 ) );
                BuildingLevel invocationLevel9 = AddBuildingLevel( context, BuildingName.invocation, 9, invocationLevel9Cost, (int)( invocationLevel8.Duration * 3.0 ),
                    CreateRequirementList( invocationLevel9Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel9 ) );
                BuildingLevel invocationLevel10 = AddBuildingLevel( context, BuildingName.invocation, 10, invocationLevel10Cost, (int)( invocationLevel9.Duration * 3.25 ),
                    CreateRequirementList( invocationLevel10Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel10 ) );

                context.SaveChanges();
            }
        }

        public void SeedAcademyLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add academyCosts
                Ressource academyLevel1Cost = AddRessource( context, 100, 50, 100, 75 );
                Ressource academyLevel2Cost = AddMiltipliedRessource( context, academyLevel1Cost, 2.5 );
                Ressource academyLevel3Cost = AddMiltipliedRessource( context, academyLevel2Cost, 2.5 );
                Ressource academyLevel4Cost = AddMiltipliedRessource( context, academyLevel3Cost, 2.75 );
                Ressource academyLevel5Cost = AddMiltipliedRessource( context, academyLevel4Cost, 2.75 );
                Ressource academyLevel6Cost = AddMiltipliedRessource( context, academyLevel5Cost, 2.75 );
                Ressource academyLevel7Cost = AddMiltipliedRessource( context, academyLevel6Cost, 3 );
                Ressource academyLevel8Cost = AddMiltipliedRessource( context, academyLevel7Cost, 3 );
                Ressource academyLevel9Cost = AddMiltipliedRessource( context, academyLevel8Cost, 3.25 );
                Ressource academyLevel10Cost = AddMiltipliedRessource( context, academyLevel9Cost, 3.5 );

                // Add academyRequirements
                Requirement academyLevel1Requirement = AddBuildingRequirement( context, BuildingName.tower, 2 );
                Requirement academyLevel2Requirement = AddBuildingRequirement( context, BuildingName.academy, 1 );
                Requirement academyLevel3Requirement = AddBuildingRequirement( context, BuildingName.academy, 2 );
                Requirement academyLevel4Requirement = AddBuildingRequirement( context, BuildingName.academy, 3 );
                Requirement academyLevel5Requirement = AddBuildingRequirement( context, BuildingName.academy, 4 );
                Requirement academyLevel6Requirement = AddBuildingRequirement( context, BuildingName.academy, 5 );
                Requirement academyLevel7Requirement = AddBuildingRequirement( context, BuildingName.academy, 6 );
                Requirement academyLevel8Requirement = AddBuildingRequirement( context, BuildingName.academy, 7 );
                Requirement academyLevel9Requirement = AddBuildingRequirement( context, BuildingName.academy, 8 );
                Requirement academyLevel10Requirement = AddBuildingRequirement( context, BuildingName.academy, 9 );

                //Add Bonuses ( 1% duration bonus magic type units / academy level )
                BonusBuildingOnUnit unitDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 1 );
                BonusBuildingOnUnit unitDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 2 );
                BonusBuildingOnUnit unitDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 3 );
                BonusBuildingOnUnit unitDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 4 );
                BonusBuildingOnUnit unitDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 5 );
                BonusBuildingOnUnit unitDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 6 );
                BonusBuildingOnUnit unitDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 7 );
                BonusBuildingOnUnit unitDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 8 );
                BonusBuildingOnUnit unitDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 9 );
                BonusBuildingOnUnit unitDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, UnitType.magic, 10 );

                // Set up Levels
                BuildingLevel academyLevel1 = AddBuildingLevel( context, BuildingName.academy, 1, academyLevel1Cost, 30,
                    CreateRequirementList( academyLevel1Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel1 ) );
                BuildingLevel academyLevel2 = AddBuildingLevel( context, BuildingName.academy, 2, academyLevel2Cost, (int)( academyLevel1.Duration * 2.5 ),
                    CreateRequirementList( academyLevel2Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel1 ) );
                BuildingLevel academyLevel3 = AddBuildingLevel( context, BuildingName.academy, 3, academyLevel3Cost, (int)( academyLevel2.Duration * 2.5 ),
                    CreateRequirementList( academyLevel3Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel3 ) );
                BuildingLevel academyLevel4 = AddBuildingLevel( context, BuildingName.academy, 4, academyLevel3Cost, (int)( academyLevel3.Duration * 2.5 ),
                    CreateRequirementList( academyLevel4Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel4 ) );
                BuildingLevel academyLevel5 = AddBuildingLevel( context, BuildingName.academy, 5, academyLevel5Cost, (int)( academyLevel4.Duration * 2.75 ),
                    CreateRequirementList( academyLevel5Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel5 ) );
                BuildingLevel academyLevel6 = AddBuildingLevel( context, BuildingName.academy, 6, academyLevel6Cost, (int)( academyLevel5.Duration * 2.75 ),
                    CreateRequirementList( academyLevel6Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel6 ) );
                BuildingLevel academyLevel7 = AddBuildingLevel( context, BuildingName.academy, 7, academyLevel7Cost, (int)( academyLevel6.Duration * 2.75 ),
                    CreateRequirementList( academyLevel7Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel7 ) );
                BuildingLevel academyLevel8 = AddBuildingLevel( context, BuildingName.academy, 8, academyLevel8Cost, (int)( academyLevel7.Duration * 3.0 ),
                    CreateRequirementList( academyLevel8Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel8 ) );
                BuildingLevel academyLevel9 = AddBuildingLevel( context, BuildingName.academy, 9, academyLevel9Cost, (int)( academyLevel8.Duration * 3.0 ),
                    CreateRequirementList( academyLevel9Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel9 ) );
                BuildingLevel academyLevel10 = AddBuildingLevel( context, BuildingName.academy, 10, academyLevel10Cost, (int)( academyLevel9.Duration * 3.25 ),
                    CreateRequirementList( academyLevel10Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel10 ) );

                context.SaveChanges();
            }
        }

        public void SeedCacheLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add cacheCosts
                Ressource cacheLevel1Cost = AddRessource( context, 100, 100, 50, 0 );
                Ressource cacheLevel2Cost = AddMiltipliedRessource( context, cacheLevel1Cost, 2.5 );
                Ressource cacheLevel3Cost = AddMiltipliedRessource( context, cacheLevel2Cost, 2.5 );
                Ressource cacheLevel4Cost = AddMiltipliedRessource( context, cacheLevel3Cost, 2.75 );
                Ressource cacheLevel5Cost = AddRessource( context, 1718, 1718, 859, 259 );
                Ressource cacheLevel6Cost = AddMiltipliedRessource( context, cacheLevel5Cost, 2.75 );
                Ressource cacheLevel7Cost = AddMiltipliedRessource( context, cacheLevel6Cost, 3 );
                Ressource cacheLevel8Cost = AddMiltipliedRessource( context, cacheLevel7Cost, 3 );
                Ressource cacheLevel9Cost = AddMiltipliedRessource( context, cacheLevel8Cost, 3.25 );
                Ressource cacheLevel10Cost = AddMiltipliedRessource( context, cacheLevel9Cost, 3.5 );

                // Add cacheRequirements
                Requirement cacheLevel1Requirement = AddBuildingRequirement( context, BuildingName.tower, 3 );
                Requirement cacheLevel2Requirement = AddBuildingRequirement( context, BuildingName.cache, 1 );
                Requirement cacheLevel3Requirement = AddBuildingRequirement( context, BuildingName.cache, 2 );
                Requirement cacheLevel4Requirement = AddBuildingRequirement( context, BuildingName.cache, 3 );
                Requirement cacheLevel5Requirement = AddBuildingRequirement( context, BuildingName.cache, 4 );
                Requirement cacheLevel6Requirement = AddBuildingRequirement( context, BuildingName.cache, 5 );
                Requirement cacheLevel7Requirement = AddBuildingRequirement( context, BuildingName.cache, 6 );
                Requirement cacheLevel8Requirement = AddBuildingRequirement( context, BuildingName.cache, 7 );
                Requirement cacheLevel9Requirement = AddBuildingRequirement( context, BuildingName.cache, 8 );
                Requirement cacheLevel10Requirement = AddBuildingRequirement( context, BuildingName.cache, 9 );

                //Add cacheSize Bonuses 
                BonusBuildingOnBuilding unitDurationBonusLevel1 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 200 );
                BonusBuildingOnBuilding unitDurationBonusLevel2 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 500 );
                BonusBuildingOnBuilding unitDurationBonusLevel3 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 1000 );
                BonusBuildingOnBuilding unitDurationBonusLevel4 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 2500 );
                BonusBuildingOnBuilding unitDurationBonusLevel5 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 5000 );
                BonusBuildingOnBuilding unitDurationBonusLevel6 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 10000 );
                BonusBuildingOnBuilding unitDurationBonusLevel7 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 25000 );
                BonusBuildingOnBuilding unitDurationBonusLevel8 = AddBonusBuilding( context, BonusType.cacheSize, BuildingName.cache, 50000 );

                // Set up Levels
                BuildingLevel cacheLevel1 = AddBuildingLevel( context, BuildingName.cache, 1, cacheLevel1Cost, 30,
                    CreateRequirementList( cacheLevel1Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel1 ) );
                BuildingLevel cacheLevel2 = AddBuildingLevel( context, BuildingName.cache, 2, cacheLevel2Cost, (int)( cacheLevel1.Duration * 2.5 ),
                    CreateRequirementList( cacheLevel2Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel1 ) );
                BuildingLevel cacheLevel3 = AddBuildingLevel( context, BuildingName.cache, 3, cacheLevel3Cost, (int)( cacheLevel2.Duration * 2.5 ),
                    CreateRequirementList( cacheLevel3Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel3 ) );
                BuildingLevel cacheLevel4 = AddBuildingLevel( context, BuildingName.cache, 4, cacheLevel3Cost, (int)( cacheLevel3.Duration * 2.5 ),
                    CreateRequirementList( cacheLevel4Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel4 ) );
                BuildingLevel cacheLevel5 = AddBuildingLevel( context, BuildingName.cache, 5, cacheLevel5Cost, (int)( cacheLevel4.Duration * 2.75 ),
                    CreateRequirementList( cacheLevel5Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel5 ) );
                BuildingLevel cacheLevel6 = AddBuildingLevel( context, BuildingName.cache, 6, cacheLevel6Cost, (int)( cacheLevel5.Duration * 2.75 ),
                    CreateRequirementList( cacheLevel6Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel6 ) );
                BuildingLevel cacheLevel7 = AddBuildingLevel( context, BuildingName.cache, 7, cacheLevel7Cost, (int)( cacheLevel6.Duration * 2.75 ),
                    CreateRequirementList( cacheLevel7Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel7 ) );
                BuildingLevel cacheLevel8 = AddBuildingLevel( context, BuildingName.cache, 8, cacheLevel8Cost, (int)( cacheLevel7.Duration * 3.0 ),
                    CreateRequirementList( cacheLevel8Requirement ),
                    CreateBonusBuildingList( unitDurationBonusLevel8 ) );

                context.SaveChanges();
            }
        }

        public void SeedWoodLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add woodCosts
                Ressource woodLevel1Cost = AddRessource( context, 100, 50, 25, 0 );
                Ressource woodLevel2Cost = AddMiltipliedRessource( context, woodLevel1Cost, 2.5 );
                Ressource woodLevel3Cost = AddMiltipliedRessource( context, woodLevel2Cost, 2.5 );
                Ressource woodLevel4Cost = AddMiltipliedRessource( context, woodLevel3Cost, 2.75 );
                Ressource woodLevel5Cost = AddMiltipliedRessource( context, woodLevel4Cost, 2.75 );
                Ressource woodLevel6Cost = AddMiltipliedRessource( context, woodLevel5Cost, 2.75 );
                Ressource woodLevel7Cost = AddMiltipliedRessource( context, woodLevel6Cost, 3 );
                Ressource woodLevel8Cost = AddMiltipliedRessource( context, woodLevel7Cost, 3 );
                Ressource woodLevel9Cost = AddMiltipliedRessource( context, woodLevel8Cost, 3.25 );
                Ressource woodLevel10Cost = AddMiltipliedRessource( context, woodLevel9Cost, 3.5 );

                // Add woodRequirements
                Requirement woodLevel2Requirement = AddBuildingRequirement( context, BuildingName.woodField, 1 );
                Requirement woodLevel3Requirement = AddBuildingRequirement( context, BuildingName.woodField, 2 );
                Requirement woodLevel4Requirement = AddBuildingRequirement( context, BuildingName.woodField, 3 );
                Requirement woodLevel5Requirement = AddBuildingRequirement( context, BuildingName.woodField, 4 );
                Requirement woodLevel6Requirement = AddBuildingRequirement( context, BuildingName.woodField, 5 );
                Requirement woodLevel7Requirement = AddBuildingRequirement( context, BuildingName.woodField, 6 );
                Requirement woodLevel8Requirement = AddBuildingRequirement( context, BuildingName.woodField, 7 );
                Requirement woodLevel9Requirement = AddBuildingRequirement( context, BuildingName.woodField, 8 );
                Requirement woodLevel10Requirement = AddBuildingRequirement( context, BuildingName.woodField, 9 );

                // Set up Levels
                FieldLevel woodLevel1 = AddFieldLevel( context, BuildingName.woodField, 1, woodLevel1Cost, 30,
                    null, 150 );
                FieldLevel woodLevel2 = AddFieldLevel( context, BuildingName.woodField, 2, woodLevel2Cost, (int)( woodLevel1.Duration * 2.5 ),
                    CreateRequirementList( woodLevel2Requirement ), (int)( woodLevel1.Production * 1.5 ) );
                FieldLevel woodLevel3 = AddFieldLevel( context, BuildingName.woodField, 3, woodLevel3Cost, (int)( woodLevel2.Duration * 2.5 ),
                    CreateRequirementList( woodLevel3Requirement ), (int)( woodLevel2.Production * 1.5 ) );
                FieldLevel woodLevel4 = AddFieldLevel( context, BuildingName.woodField, 4, woodLevel3Cost, (int)( woodLevel3.Duration * 2.5 ),
                    CreateRequirementList( woodLevel4Requirement ), (int)( woodLevel3.Production * 1.5 ) );
                FieldLevel woodLevel5 = AddFieldLevel( context, BuildingName.woodField, 5, woodLevel5Cost, (int)( woodLevel4.Duration * 2.75 ),
                    CreateRequirementList( woodLevel5Requirement ), (int)( woodLevel4.Production * 1.5 ) );
                FieldLevel woodLevel6 = AddFieldLevel( context, BuildingName.woodField, 6, woodLevel6Cost, (int)( woodLevel5.Duration * 2.75 ),
                    CreateRequirementList( woodLevel6Requirement ), (int)( woodLevel5.Production * 1.5 ) );
                FieldLevel woodLevel7 = AddFieldLevel( context, BuildingName.woodField, 7, woodLevel7Cost, (int)( woodLevel6.Duration * 2.75 ),
                    CreateRequirementList( woodLevel7Requirement ), (int)( woodLevel6.Production * 1.5 ) );
                FieldLevel woodLevel8 = AddFieldLevel( context, BuildingName.woodField, 8, woodLevel8Cost, (int)( woodLevel7.Duration * 3.0 ),
                    CreateRequirementList( woodLevel8Requirement ), (int)( woodLevel7.Production * 1.5 ) );
                FieldLevel woodLevel9 = AddFieldLevel( context, BuildingName.woodField, 9, woodLevel9Cost, (int)( woodLevel8.Duration * 3.0 ),
                    CreateRequirementList( woodLevel9Requirement ), (int)( woodLevel8.Production * 1.5 ) );
                FieldLevel woodLevel10 = AddFieldLevel( context, BuildingName.woodField, 10, woodLevel10Cost, (int)( woodLevel9.Duration * 3.25 ),
                    CreateRequirementList( woodLevel10Requirement ), (int)( woodLevel9.Production * 1.5 ) );

                context.SaveChanges();
            }
        }

        public void SeedMetalLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add metalCosts
                Ressource metalLevel1Cost = AddRessource( context, 125, 75, 50, 0 );
                Ressource metalLevel2Cost = AddMiltipliedRessource( context, metalLevel1Cost, 2.5 );
                Ressource metalLevel3Cost = AddMiltipliedRessource( context, metalLevel2Cost, 2.5 );
                Ressource metalLevel4Cost = AddMiltipliedRessource( context, metalLevel3Cost, 2.75 );
                Ressource metalLevel5Cost = AddMiltipliedRessource( context, metalLevel4Cost, 2.75 );
                Ressource metalLevel6Cost = AddMiltipliedRessource( context, metalLevel5Cost, 2.75 );
                Ressource metalLevel7Cost = AddMiltipliedRessource( context, metalLevel6Cost, 3 );
                Ressource metalLevel8Cost = AddMiltipliedRessource( context, metalLevel7Cost, 3 );
                Ressource metalLevel9Cost = AddMiltipliedRessource( context, metalLevel8Cost, 3.25 );
                Ressource metalLevel10Cost = AddMiltipliedRessource( context, metalLevel9Cost, 3.5 );

                // Add metalRequirements
                Requirement metalLevel2Requirement = AddBuildingRequirement( context, BuildingName.metalField, 1 );
                Requirement metalLevel3Requirement = AddBuildingRequirement( context, BuildingName.metalField, 2 );
                Requirement metalLevel4Requirement = AddBuildingRequirement( context, BuildingName.metalField, 3 );
                Requirement metalLevel5Requirement = AddBuildingRequirement( context, BuildingName.metalField, 4 );
                Requirement metalLevel6Requirement = AddBuildingRequirement( context, BuildingName.metalField, 5 );
                Requirement metalLevel7Requirement = AddBuildingRequirement( context, BuildingName.metalField, 6 );
                Requirement metalLevel8Requirement = AddBuildingRequirement( context, BuildingName.metalField, 7 );
                Requirement metalLevel9Requirement = AddBuildingRequirement( context, BuildingName.metalField, 8 );
                Requirement metalLevel10Requirement = AddBuildingRequirement( context, BuildingName.metalField, 9 );

                // Set up Levels
                FieldLevel metalLevel1 = AddFieldLevel( context, BuildingName.metalField, 1, metalLevel1Cost, 35,
                    null, 125 );
                FieldLevel metalLevel2 = AddFieldLevel( context, BuildingName.metalField, 2, metalLevel2Cost, (int)( metalLevel1.Duration * 2.5 ),
                    CreateRequirementList( metalLevel2Requirement ), (int)( metalLevel1.Production * 1.5 ) );
                FieldLevel metalLevel3 = AddFieldLevel( context, BuildingName.metalField, 3, metalLevel3Cost, (int)( metalLevel2.Duration * 2.5 ),
                    CreateRequirementList( metalLevel3Requirement ), (int)( metalLevel2.Production * 1.5 ) );
                FieldLevel metalLevel4 = AddFieldLevel( context, BuildingName.metalField, 4, metalLevel3Cost, (int)( metalLevel3.Duration * 2.5 ),
                    CreateRequirementList( metalLevel4Requirement ), (int)( metalLevel3.Production * 1.5 ) );
                FieldLevel metalLevel5 = AddFieldLevel( context, BuildingName.metalField, 5, metalLevel5Cost, (int)( metalLevel4.Duration * 2.75 ),
                    CreateRequirementList( metalLevel5Requirement ), (int)( metalLevel4.Production * 1.5 ) );
                FieldLevel metalLevel6 = AddFieldLevel( context, BuildingName.metalField, 6, metalLevel6Cost, (int)( metalLevel5.Duration * 2.75 ),
                    CreateRequirementList( metalLevel6Requirement ), (int)( metalLevel5.Production * 1.5 ) );
                FieldLevel metalLevel7 = AddFieldLevel( context, BuildingName.metalField, 7, metalLevel7Cost, (int)( metalLevel6.Duration * 2.75 ),
                    CreateRequirementList( metalLevel7Requirement ), (int)( metalLevel6.Production * 1.5 ) );
                FieldLevel metalLevel8 = AddFieldLevel( context, BuildingName.metalField, 8, metalLevel8Cost, (int)( metalLevel7.Duration * 3.0 ),
                    CreateRequirementList( metalLevel8Requirement ), (int)( metalLevel7.Production * 1.5 ) );
                FieldLevel metalLevel9 = AddFieldLevel( context, BuildingName.metalField, 9, metalLevel9Cost, (int)( metalLevel8.Duration * 3.0 ),
                    CreateRequirementList( metalLevel9Requirement ), (int)( metalLevel8.Production * 1.5 ) );
                FieldLevel metalLevel10 = AddFieldLevel( context, BuildingName.metalField, 10, metalLevel10Cost, (int)( metalLevel9.Duration * 3.25 ),
                    CreateRequirementList( metalLevel10Requirement ), (int)( metalLevel9.Production * 1.5 ) );

                context.SaveChanges();
            }
        }

        public void SeedCristalLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add cristalCosts
                Ressource cristalLevel1Cost = AddRessource( context, 125, 125, 100, 0 );
                Ressource cristalLevel2Cost = AddMiltipliedRessource( context, cristalLevel1Cost, 2.5 );
                Ressource cristalLevel3Cost = AddMiltipliedRessource( context, cristalLevel2Cost, 2.5 );
                Ressource cristalLevel4Cost = AddMiltipliedRessource( context, cristalLevel3Cost, 2.75 );
                Ressource cristalLevel5Cost = AddMiltipliedRessource( context, cristalLevel4Cost, 2.75 );
                Ressource cristalLevel6Cost = AddMiltipliedRessource( context, cristalLevel5Cost, 2.75 );
                Ressource cristalLevel7Cost = AddMiltipliedRessource( context, cristalLevel6Cost, 3 );
                Ressource cristalLevel8Cost = AddMiltipliedRessource( context, cristalLevel7Cost, 3 );
                Ressource cristalLevel9Cost = AddMiltipliedRessource( context, cristalLevel8Cost, 3.25 );
                Ressource cristalLevel10Cost = AddMiltipliedRessource( context, cristalLevel9Cost, 3.5 );

                // Add cristalRequirements
                Requirement cristalLevel2Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 1 );
                Requirement cristalLevel3Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 2 );
                Requirement cristalLevel4Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 3 );
                Requirement cristalLevel5Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 4 );
                Requirement cristalLevel6Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 5 );
                Requirement cristalLevel7Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 6 );
                Requirement cristalLevel8Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 7 );
                Requirement cristalLevel9Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 8 );
                Requirement cristalLevel10Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 9 );

                // Set up Levels
                FieldLevel cristalLevel1 = AddFieldLevel( context, BuildingName.cristalField, 1, cristalLevel1Cost, 45,
                    null, 100 );
                FieldLevel cristalLevel2 = AddFieldLevel( context, BuildingName.cristalField, 2, cristalLevel2Cost, (int)( cristalLevel1.Duration * 2.5 ),
                    CreateRequirementList( cristalLevel2Requirement ), (int)( cristalLevel1.Production * 1.5 ) );
                FieldLevel cristalLevel3 = AddFieldLevel( context, BuildingName.cristalField, 3, cristalLevel3Cost, (int)( cristalLevel2.Duration * 2.5 ),
                    CreateRequirementList( cristalLevel3Requirement ), (int)( cristalLevel2.Production * 1.5 ) );
                FieldLevel cristalLevel4 = AddFieldLevel( context, BuildingName.cristalField, 4, cristalLevel3Cost, (int)( cristalLevel3.Duration * 2.5 ),
                    CreateRequirementList( cristalLevel4Requirement ), (int)( cristalLevel3.Production * 1.5 ) );
                FieldLevel cristalLevel5 = AddFieldLevel( context, BuildingName.cristalField, 5, cristalLevel5Cost, (int)( cristalLevel4.Duration * 2.75 ),
                    CreateRequirementList( cristalLevel5Requirement ), (int)( cristalLevel4.Production * 1.5 ) );
                FieldLevel cristalLevel6 = AddFieldLevel( context, BuildingName.cristalField, 6, cristalLevel6Cost, (int)( cristalLevel5.Duration * 2.75 ),
                    CreateRequirementList( cristalLevel6Requirement ), (int)( cristalLevel5.Production * 1.5 ) );
                FieldLevel cristalLevel7 = AddFieldLevel( context, BuildingName.cristalField, 7, cristalLevel7Cost, (int)( cristalLevel6.Duration * 2.75 ),
                    CreateRequirementList( cristalLevel7Requirement ), (int)( cristalLevel6.Production * 1.5 ) );
                FieldLevel cristalLevel8 = AddFieldLevel( context, BuildingName.cristalField, 8, cristalLevel8Cost, (int)( cristalLevel7.Duration * 3.0 ),
                    CreateRequirementList( cristalLevel8Requirement ), (int)( cristalLevel7.Production * 1.5 ) );
                FieldLevel cristalLevel9 = AddFieldLevel( context, BuildingName.cristalField, 9, cristalLevel9Cost, (int)( cristalLevel8.Duration * 3.0 ),
                    CreateRequirementList( cristalLevel9Requirement ), (int)( cristalLevel8.Production * 1.5 ) );
                FieldLevel cristalLevel10 = AddFieldLevel( context, BuildingName.cristalField, 10, cristalLevel10Cost, (int)( cristalLevel9.Duration * 3.25 ),
                    CreateRequirementList( cristalLevel10Requirement ), (int)( cristalLevel9.Production * 1.5 ) );

                context.SaveChanges();
            }
        }

        public void SeedMagicLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add magicCosts
                Ressource magicLevel1Cost = AddRessource( context, 200, 150, 125, 0 );
                Ressource magicLevel2Cost = AddMiltipliedRessource( context, magicLevel1Cost, 2.5 );
                Ressource magicLevel3Cost = AddMiltipliedRessource( context, magicLevel2Cost, 2.5 );
                Ressource magicLevel4Cost = AddMiltipliedRessource( context, magicLevel3Cost, 2.75 );
                Ressource magicLevel5Cost = AddMiltipliedRessource( context, magicLevel4Cost, 2.75 );
                Ressource magicLevel6Cost = AddMiltipliedRessource( context, magicLevel5Cost, 2.75 );
                Ressource magicLevel7Cost = AddMiltipliedRessource( context, magicLevel6Cost, 3 );
                Ressource magicLevel8Cost = AddMiltipliedRessource( context, magicLevel7Cost, 3 );
                Ressource magicLevel9Cost = AddMiltipliedRessource( context, magicLevel8Cost, 3.25 );
                Ressource magicLevel10Cost = AddMiltipliedRessource( context, magicLevel9Cost, 3.5 );

                // Add magicRequirements
                Requirement magicLevel2Requirement = AddBuildingRequirement( context, BuildingName.magicField, 1 );
                Requirement magicLevel3Requirement = AddBuildingRequirement( context, BuildingName.magicField, 2 );
                Requirement magicLevel4Requirement = AddBuildingRequirement( context, BuildingName.magicField, 3 );
                Requirement magicLevel5Requirement = AddBuildingRequirement( context, BuildingName.magicField, 4 );
                Requirement magicLevel6Requirement = AddBuildingRequirement( context, BuildingName.magicField, 5 );
                Requirement magicLevel7Requirement = AddBuildingRequirement( context, BuildingName.magicField, 6 );
                Requirement magicLevel8Requirement = AddBuildingRequirement( context, BuildingName.magicField, 7 );
                Requirement magicLevel9Requirement = AddBuildingRequirement( context, BuildingName.magicField, 8 );
                Requirement magicLevel10Requirement = AddBuildingRequirement( context, BuildingName.magicField, 9 );

                // Set up Levels
                FieldLevel magicLevel1 = AddFieldLevel( context, BuildingName.magicField, 1, magicLevel1Cost, 35,
                    null, 125 );
                FieldLevel magicLevel2 = AddFieldLevel( context, BuildingName.magicField, 2, magicLevel2Cost, (int)( magicLevel1.Duration * 2.5 ),
                    CreateRequirementList( magicLevel2Requirement ), (int)( magicLevel1.Production * 1.5 ) );
                FieldLevel magicLevel3 = AddFieldLevel( context, BuildingName.magicField, 3, magicLevel3Cost, (int)( magicLevel2.Duration * 2.5 ),
                    CreateRequirementList( magicLevel3Requirement ), (int)( magicLevel2.Production * 1.5 ) );
                FieldLevel magicLevel4 = AddFieldLevel( context, BuildingName.magicField, 4, magicLevel3Cost, (int)( magicLevel3.Duration * 2.5 ),
                    CreateRequirementList( magicLevel4Requirement ), (int)( magicLevel3.Production * 1.5 ) );
                FieldLevel magicLevel5 = AddFieldLevel( context, BuildingName.magicField, 5, magicLevel5Cost, (int)( magicLevel4.Duration * 2.75 ),
                    CreateRequirementList( magicLevel5Requirement ), (int)( magicLevel4.Production * 1.5 ) );
                FieldLevel magicLevel6 = AddFieldLevel( context, BuildingName.magicField, 6, magicLevel6Cost, (int)( magicLevel5.Duration * 2.75 ),
                    CreateRequirementList( magicLevel6Requirement ), (int)( magicLevel5.Production * 1.5 ) );
                FieldLevel magicLevel7 = AddFieldLevel( context, BuildingName.magicField, 7, magicLevel7Cost, (int)( magicLevel6.Duration * 2.75 ),
                    CreateRequirementList( magicLevel7Requirement ), (int)( magicLevel6.Production * 1.5 ) );
                FieldLevel magicLevel8 = AddFieldLevel( context, BuildingName.magicField, 8, magicLevel8Cost, (int)( magicLevel7.Duration * 3.0 ),
                    CreateRequirementList( magicLevel8Requirement ), (int)( magicLevel7.Production * 1.5 ) );
                FieldLevel magicLevel9 = AddFieldLevel( context, BuildingName.magicField, 9, magicLevel9Cost, (int)( magicLevel8.Duration * 3.0 ),
                    CreateRequirementList( magicLevel9Requirement ), (int)( magicLevel8.Production * 1.5 ) );
                FieldLevel magicLevel10 = AddFieldLevel( context, BuildingName.magicField, 10, magicLevel10Cost, (int)( magicLevel9.Duration * 3.25 ),
                    CreateRequirementList( magicLevel10Requirement ), (int)( magicLevel9.Production * 1.5 ) );

                context.SaveChanges();
            }
        }

        public void SeedShieldLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add shieldCosts
                Ressource shieldLevel1Cost = AddRessource( context, 50, 50, 25, 0 );
                Ressource shieldLevel2Cost = AddRessource( context, 100, 100, 50, 0 );
                Ressource shieldLevel3Cost = AddMiltipliedRessource( context, shieldLevel2Cost, 2.5 );
                Ressource shieldLevel4Cost = AddMiltipliedRessource( context, shieldLevel3Cost, 2.75 );
                Ressource shieldLevel5Cost = AddMiltipliedRessource( context, shieldLevel4Cost, 2.75 );
                Ressource shieldLevel6Cost = AddMiltipliedRessource( context, shieldLevel5Cost, 2.75 );
                Ressource shieldLevel7Cost = AddRessource( context, 5200, 5200, 2600, 500 );
                Ressource shieldLevel8Cost = AddMiltipliedRessource( context, shieldLevel7Cost, 3 );
                Ressource shieldLevel9Cost = AddMiltipliedRessource( context, shieldLevel8Cost, 3.25 );
                Ressource shieldLevel10Cost = AddMiltipliedRessource( context, shieldLevel9Cost, 3.5 );

                // Add shieldRequirements
                Requirement shieldLevel2Requirement = AddBuildingRequirement( context, BuildingName.shield, 1 );
                Requirement shieldLevel3Requirement = AddBuildingRequirement( context, BuildingName.shield, 2 );
                Requirement shieldLevel4Requirement = AddBuildingRequirement( context, BuildingName.shield, 3 );
                Requirement shieldLevel5Requirement = AddBuildingRequirement( context, BuildingName.shield, 4 );
                Requirement shieldLevel6Requirement = AddBuildingRequirement( context, BuildingName.shield, 5 );
                Requirement shieldLevel7Requirement = AddBuildingRequirement( context, BuildingName.shield, 6 );
                Requirement shieldLevel8Requirement = AddBuildingRequirement( context, BuildingName.shield, 7 );
                Requirement shieldLevel9Requirement = AddBuildingRequirement( context, BuildingName.shield, 8 );
                Requirement shieldLevel10Requirement = AddBuildingRequirement( context, BuildingName.shield, 9 );

                // Set up Levels
                ShieldLevel shieldLevel1 = AddShieldLevel( context, BuildingName.shield, 1, shieldLevel1Cost, 30,
                    null, 25, 0 );
                ShieldLevel shieldLevel2 = AddShieldLevel( context, BuildingName.shield, 2, shieldLevel2Cost, 60,
                    CreateRequirementList( shieldLevel2Requirement ), 50, 0 );
                ShieldLevel shieldLevel3 = AddShieldLevel( context, BuildingName.shield, 3, shieldLevel3Cost, (int)( shieldLevel2.Duration * 2.5 ),
                    CreateRequirementList( shieldLevel3Requirement ), 100, 50 );
                ShieldLevel shieldLevel4 = AddShieldLevel( context, BuildingName.shield, 4, shieldLevel3Cost, (int)( shieldLevel3.Duration * 2.5 ),
                    CreateRequirementList( shieldLevel4Requirement ), 200, 100 );
                ShieldLevel shieldLevel5 = AddShieldLevel( context, BuildingName.shield, 5, shieldLevel5Cost, (int)( shieldLevel4.Duration * 2.75 ),
                    CreateRequirementList( shieldLevel5Requirement ), 400, 150 );
                ShieldLevel shieldLevel6 = AddShieldLevel( context, BuildingName.shield, 6, shieldLevel6Cost, (int)( shieldLevel5.Duration * 2.75 ),
                    CreateRequirementList( shieldLevel6Requirement ), 650, 200 );
                ShieldLevel shieldLevel7 = AddShieldLevel( context, BuildingName.shield, 7, shieldLevel7Cost, (int)( shieldLevel6.Duration * 2.75 ),
                    CreateRequirementList( shieldLevel7Requirement ), 1200, 300 );
                ShieldLevel shieldLevel8 = AddShieldLevel( context, BuildingName.shield, 8, shieldLevel8Cost, (int)( shieldLevel7.Duration * 3.0 ),
                    CreateRequirementList( shieldLevel8Requirement ), 2350, 600 );
                ShieldLevel shieldLevel9 = AddShieldLevel( context, BuildingName.shield, 9, shieldLevel9Cost, (int)( shieldLevel8.Duration * 3.0 ),
                    CreateRequirementList( shieldLevel9Requirement ), 5000, 1200 );
                ShieldLevel shieldLevel10 = AddShieldLevel( context, BuildingName.shield, 10, shieldLevel10Cost, (int)( shieldLevel9.Duration * 3.25 ),
                    CreateRequirementList( shieldLevel10Requirement ), 7500, 1500 );

                context.SaveChanges();
            }
        }
        #endregion

        #region Technologies

        public void SeedArmorLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add armorCosts
                Ressource armorLevel1Cost = AddRessource( context, 50, 200, 25, 0 );
                Ressource armorLevel2Cost = AddMiltipliedRessource( context, armorLevel1Cost, 2.5 );
                Ressource armorLevel3Cost = AddMiltipliedRessource( context, armorLevel2Cost, 2.5 );
                Ressource armorLevel4Cost = AddMiltipliedRessource( context, armorLevel3Cost, 2.75 );
                Ressource armorLevel5Cost = AddMiltipliedRessource( context, armorLevel4Cost, 2.75 );
                Ressource armorLevel6Cost = AddMiltipliedRessource( context, armorLevel5Cost, 2.75 );
                Ressource armorLevel7Cost = AddMiltipliedRessource( context, armorLevel6Cost, 3 );
                Ressource armorLevel8Cost = AddMiltipliedRessource( context, armorLevel7Cost, 3 );
                Ressource armorLevel9Cost = AddMiltipliedRessource( context, armorLevel8Cost, 3.25 );
                Ressource armorLevel10Cost = AddMiltipliedRessource( context, armorLevel9Cost, 3.5 );

                // Add armorRequirements
                Requirement armorLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 1 );
                Requirement armorLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 1 );
                Requirement armorLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 2 );
                Requirement armorLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 3 );
                Requirement armorLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 4 );
                Requirement armorLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 5 );
                Requirement armorLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 6 );
                Requirement armorLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 7 );
                Requirement armorLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 8 );
                Requirement armorLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.armor, 9 );

                //Add Bonuses ( 1% physical defense bonus soldier type units / armor level )
                BonusTechnologyOnUnit armorBonusLevel1 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 1 );
                BonusTechnologyOnUnit armorBonusLevel2 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 2 );
                BonusTechnologyOnUnit armorBonusLevel3 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 3 );
                BonusTechnologyOnUnit armorBonusLevel4 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 4 );
                BonusTechnologyOnUnit armorBonusLevel5 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 5 );
                BonusTechnologyOnUnit armorBonusLevel6 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 6 );
                BonusTechnologyOnUnit armorBonusLevel7 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 7 );
                BonusTechnologyOnUnit armorBonusLevel8 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 8 );
                BonusTechnologyOnUnit armorBonusLevel9 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 9 );
                BonusTechnologyOnUnit armorBonusLevel10 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.soldier, 10 );

                // Set up Levels
                TechnologyLevel armorLevel1 = AddTechnologyLevel( context, TechnologyName.armor, 1, armorLevel1Cost, 60,
                    CreateRequirementList( armorLevel1Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel1 ) );
                TechnologyLevel armorLevel2 = AddTechnologyLevel( context, TechnologyName.armor, 2, armorLevel2Cost, (int)( armorLevel1.Duration * 2.5 ),
                    CreateRequirementList( armorLevel2Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel2 ) );
                TechnologyLevel armorLevel3 = AddTechnologyLevel( context, TechnologyName.armor, 3, armorLevel3Cost, (int)( armorLevel2.Duration * 2.5 ),
                    CreateRequirementList( armorLevel3Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel2 ) );
                TechnologyLevel armorLevel4 = AddTechnologyLevel( context, TechnologyName.armor, 4, armorLevel3Cost, (int)( armorLevel3.Duration * 2.75 ),
                    CreateRequirementList( armorLevel4Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel3 ) );
                TechnologyLevel armorLevel5 = AddTechnologyLevel( context, TechnologyName.armor, 5, armorLevel5Cost, (int)( armorLevel4.Duration * 2.75 ),
                    CreateRequirementList( armorLevel5Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel4 ) );
                TechnologyLevel armorLevel6 = AddTechnologyLevel( context, TechnologyName.armor, 6, armorLevel6Cost, (int)( armorLevel5.Duration * 2.75 ),
                    CreateRequirementList( armorLevel6Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel5 ) );
                TechnologyLevel armorLevel7 = AddTechnologyLevel( context, TechnologyName.armor, 7, armorLevel7Cost, (int)( armorLevel6.Duration * 3.0 ),
                    CreateRequirementList( armorLevel7Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel6 ) );
                TechnologyLevel armorLevel8 = AddTechnologyLevel( context, TechnologyName.armor, 8, armorLevel8Cost, (int)( armorLevel7.Duration * 3.0 ),
                    CreateRequirementList( armorLevel8Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel7 ) );
                TechnologyLevel armorLevel9 = AddTechnologyLevel( context, TechnologyName.armor, 9, armorLevel9Cost, (int)( armorLevel8.Duration * 3.0 ),
                    CreateRequirementList( armorLevel9Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel8 ) );
                TechnologyLevel armorLevel10 = AddTechnologyLevel( context, TechnologyName.armor, 10, armorLevel10Cost, (int)( armorLevel9.Duration * 3.25 ),
                    CreateRequirementList( armorLevel10Requirement ),
                    CreateBonusTechnologyList( armorBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedWeaponsLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add weaponsCosts
                Ressource weaponsLevel1Cost = AddRessource( context, 50, 150, 25, 0 );
                Ressource weaponsLevel2Cost = AddMiltipliedRessource( context, weaponsLevel1Cost, 2.5 );
                Ressource weaponsLevel3Cost = AddMiltipliedRessource( context, weaponsLevel2Cost, 2.5 );
                Ressource weaponsLevel4Cost = AddMiltipliedRessource( context, weaponsLevel3Cost, 2.75 );
                Ressource weaponsLevel5Cost = AddMiltipliedRessource( context, weaponsLevel4Cost, 2.75 );
                Ressource weaponsLevel6Cost = AddMiltipliedRessource( context, weaponsLevel5Cost, 2.75 );
                Ressource weaponsLevel7Cost = AddMiltipliedRessource( context, weaponsLevel6Cost, 3 );
                Ressource weaponsLevel8Cost = AddMiltipliedRessource( context, weaponsLevel7Cost, 3 );
                Ressource weaponsLevel9Cost = AddMiltipliedRessource( context, weaponsLevel8Cost, 3.25 );
                Ressource weaponsLevel10Cost = AddMiltipliedRessource( context, weaponsLevel9Cost, 3.5 );

                // Add weaponsRequirements
                Requirement weaponsLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 1 );
                Requirement weaponsLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 1 );
                Requirement weaponsLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 2 );
                Requirement weaponsLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 3 );
                Requirement weaponsLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 4 );
                Requirement weaponsLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 5 );
                Requirement weaponsLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 6 );
                Requirement weaponsLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 7 );
                Requirement weaponsLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 8 );
                Requirement weaponsLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.weapons, 9 );

                //Add Bonuses ( 1% attack bonus soldier type units / weapons level )
                BonusTechnologyOnUnit weaponsBonusLevel1 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 1 );
                BonusTechnologyOnUnit weaponsBonusLevel2 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 2 );
                BonusTechnologyOnUnit weaponsBonusLevel3 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 3 );
                BonusTechnologyOnUnit weaponsBonusLevel4 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 4 );
                BonusTechnologyOnUnit weaponsBonusLevel5 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 5 );
                BonusTechnologyOnUnit weaponsBonusLevel6 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 6 );
                BonusTechnologyOnUnit weaponsBonusLevel7 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 7 );
                BonusTechnologyOnUnit weaponsBonusLevel8 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 8 );
                BonusTechnologyOnUnit weaponsBonusLevel9 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 9 );
                BonusTechnologyOnUnit weaponsBonusLevel10 = AddBonusTechnology( context, BonusType.army_attack, UnitType.soldier, 10 );

                // Set up Levels
                TechnologyLevel weaponsLevel1 = AddTechnologyLevel( context, TechnologyName.weapons, 1, weaponsLevel1Cost, 60,
                    CreateRequirementList( weaponsLevel1Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel1 ) );
                TechnologyLevel weaponsLevel2 = AddTechnologyLevel( context, TechnologyName.weapons, 2, weaponsLevel2Cost, (int)( weaponsLevel1.Duration * 2.5 ),
                    CreateRequirementList( weaponsLevel2Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel2 ) );
                TechnologyLevel weaponsLevel3 = AddTechnologyLevel( context, TechnologyName.weapons, 3, weaponsLevel3Cost, (int)( weaponsLevel2.Duration * 2.5 ),
                    CreateRequirementList( weaponsLevel3Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel2 ) );
                TechnologyLevel weaponsLevel4 = AddTechnologyLevel( context, TechnologyName.weapons, 4, weaponsLevel3Cost, (int)( weaponsLevel3.Duration * 2.75 ),
                    CreateRequirementList( weaponsLevel4Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel3 ) );
                TechnologyLevel weaponsLevel5 = AddTechnologyLevel( context, TechnologyName.weapons, 5, weaponsLevel5Cost, (int)( weaponsLevel4.Duration * 2.75 ),
                    CreateRequirementList( weaponsLevel5Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel4 ) );
                TechnologyLevel weaponsLevel6 = AddTechnologyLevel( context, TechnologyName.weapons, 6, weaponsLevel6Cost, (int)( weaponsLevel5.Duration * 2.75 ),
                    CreateRequirementList( weaponsLevel6Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel5 ) );
                TechnologyLevel weaponsLevel7 = AddTechnologyLevel( context, TechnologyName.weapons, 7, weaponsLevel7Cost, (int)( weaponsLevel6.Duration * 3.0 ),
                    CreateRequirementList( weaponsLevel7Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel6 ) );
                TechnologyLevel weaponsLevel8 = AddTechnologyLevel( context, TechnologyName.weapons, 8, weaponsLevel8Cost, (int)( weaponsLevel7.Duration * 3.0 ),
                    CreateRequirementList( weaponsLevel8Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel7 ) );
                TechnologyLevel weaponsLevel9 = AddTechnologyLevel( context, TechnologyName.weapons, 9, weaponsLevel9Cost, (int)( weaponsLevel8.Duration * 3.0 ),
                    CreateRequirementList( weaponsLevel9Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel8 ) );
                TechnologyLevel weaponsLevel10 = AddTechnologyLevel( context, TechnologyName.weapons, 10, weaponsLevel10Cost, (int)( weaponsLevel9.Duration * 3.25 ),
                    CreateRequirementList( weaponsLevel10Requirement ),
                    CreateBonusTechnologyList( weaponsBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedMagicBootsLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add magicBootsCosts
                Ressource magicBootsLevel1Cost = AddRessource( context, 100, 250, 50, 0 );
                Ressource magicBootsLevel2Cost = AddMiltipliedRessource( context, magicBootsLevel1Cost, 2.5 );
                Ressource magicBootsLevel3Cost = AddMiltipliedRessource( context, magicBootsLevel2Cost, 2.5 );
                Ressource magicBootsLevel4Cost = AddMiltipliedRessource( context, magicBootsLevel3Cost, 2.75 );
                Ressource magicBootsLevel5Cost = AddMiltipliedRessource( context, magicBootsLevel4Cost, 2.75 );
                Ressource magicBootsLevel6Cost = AddMiltipliedRessource( context, magicBootsLevel5Cost, 2.75 );
                Ressource magicBootsLevel7Cost = AddMiltipliedRessource( context, magicBootsLevel6Cost, 3 );
                Ressource magicBootsLevel8Cost = AddMiltipliedRessource( context, magicBootsLevel7Cost, 3 );
                Ressource magicBootsLevel9Cost = AddMiltipliedRessource( context, magicBootsLevel8Cost, 3.25 );
                Ressource magicBootsLevel10Cost = AddMiltipliedRessource( context, magicBootsLevel9Cost, 3.5 );

                // Add magicBootsRequirements
                Requirement magicBootsLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 1 );
                Requirement magicBootsLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 1 );
                Requirement magicBootsLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 2 );
                Requirement magicBootsLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 3 );
                Requirement magicBootsLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 4 );
                Requirement magicBootsLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 5 );
                Requirement magicBootsLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 6 );
                Requirement magicBootsLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 7 );
                Requirement magicBootsLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 8 );
                Requirement magicBootsLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.magicBoots, 9 );

                //Add Bonuses ( 1% speed bonus soldier type units / magicBoots level )
                BonusTechnologyOnUnit magicBootsBonusLevel1 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 1 );
                BonusTechnologyOnUnit magicBootsBonusLevel2 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 2 );
                BonusTechnologyOnUnit magicBootsBonusLevel3 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 3 );
                BonusTechnologyOnUnit magicBootsBonusLevel4 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 4 );
                BonusTechnologyOnUnit magicBootsBonusLevel5 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 5 );
                BonusTechnologyOnUnit magicBootsBonusLevel6 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 6 );
                BonusTechnologyOnUnit magicBootsBonusLevel7 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 7 );
                BonusTechnologyOnUnit magicBootsBonusLevel8 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 8 );
                BonusTechnologyOnUnit magicBootsBonusLevel9 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 9 );
                BonusTechnologyOnUnit magicBootsBonusLevel10 = AddBonusTechnology( context, BonusType.army_speed, UnitType.soldier, 10 );

                // Set up Levels
                TechnologyLevel magicBootsLevel1 = AddTechnologyLevel( context, TechnologyName.magicBoots, 1, magicBootsLevel1Cost, 60,
                    CreateRequirementList( magicBootsLevel1Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel1 ) );
                TechnologyLevel magicBootsLevel2 = AddTechnologyLevel( context, TechnologyName.magicBoots, 2, magicBootsLevel2Cost, (int)( magicBootsLevel1.Duration * 2.5 ),
                    CreateRequirementList( magicBootsLevel2Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel2 ) );
                TechnologyLevel magicBootsLevel3 = AddTechnologyLevel( context, TechnologyName.magicBoots, 3, magicBootsLevel3Cost, (int)( magicBootsLevel2.Duration * 2.5 ),
                    CreateRequirementList( magicBootsLevel3Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel2 ) );
                TechnologyLevel magicBootsLevel4 = AddTechnologyLevel( context, TechnologyName.magicBoots, 4, magicBootsLevel3Cost, (int)( magicBootsLevel3.Duration * 2.75 ),
                    CreateRequirementList( magicBootsLevel4Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel3 ) );
                TechnologyLevel magicBootsLevel5 = AddTechnologyLevel( context, TechnologyName.magicBoots, 5, magicBootsLevel5Cost, (int)( magicBootsLevel4.Duration * 2.75 ),
                    CreateRequirementList( magicBootsLevel5Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel4 ) );
                TechnologyLevel magicBootsLevel6 = AddTechnologyLevel( context, TechnologyName.magicBoots, 6, magicBootsLevel6Cost, (int)( magicBootsLevel5.Duration * 2.75 ),
                    CreateRequirementList( magicBootsLevel6Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel5 ) );
                TechnologyLevel magicBootsLevel7 = AddTechnologyLevel( context, TechnologyName.magicBoots, 7, magicBootsLevel7Cost, (int)( magicBootsLevel6.Duration * 3.0 ),
                    CreateRequirementList( magicBootsLevel7Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel6 ) );
                TechnologyLevel magicBootsLevel8 = AddTechnologyLevel( context, TechnologyName.magicBoots, 8, magicBootsLevel8Cost, (int)( magicBootsLevel7.Duration * 3.0 ),
                    CreateRequirementList( magicBootsLevel8Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel7 ) );
                TechnologyLevel magicBootsLevel9 = AddTechnologyLevel( context, TechnologyName.magicBoots, 9, magicBootsLevel9Cost, (int)( magicBootsLevel8.Duration * 3.0 ),
                    CreateRequirementList( magicBootsLevel9Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel8 ) );
                TechnologyLevel magicBootsLevel10 = AddTechnologyLevel( context, TechnologyName.magicBoots, 10, magicBootsLevel10Cost, (int)( magicBootsLevel9.Duration * 3.25 ),
                    CreateRequirementList( magicBootsLevel10Requirement ),
                    CreateBonusTechnologyList( magicBootsBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedMagicalProtectionLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add magicalProtectionCosts
                Ressource magicalProtectionLevel1Cost = AddRessource( context, 0, 50, 100, 25 );
                Ressource magicalProtectionLevel2Cost = AddMiltipliedRessource( context, magicalProtectionLevel1Cost, 2.5 );
                Ressource magicalProtectionLevel3Cost = AddMiltipliedRessource( context, magicalProtectionLevel2Cost, 2.5 );
                Ressource magicalProtectionLevel4Cost = AddMiltipliedRessource( context, magicalProtectionLevel3Cost, 2.75 );
                Ressource magicalProtectionLevel5Cost = AddMiltipliedRessource( context, magicalProtectionLevel4Cost, 2.75 );
                Ressource magicalProtectionLevel6Cost = AddMiltipliedRessource( context, magicalProtectionLevel5Cost, 2.75 );
                Ressource magicalProtectionLevel7Cost = AddMiltipliedRessource( context, magicalProtectionLevel6Cost, 3 );
                Ressource magicalProtectionLevel8Cost = AddMiltipliedRessource( context, magicalProtectionLevel7Cost, 3 );
                Ressource magicalProtectionLevel9Cost = AddMiltipliedRessource( context, magicalProtectionLevel8Cost, 3.25 );
                Ressource magicalProtectionLevel10Cost = AddMiltipliedRessource( context, magicalProtectionLevel9Cost, 3.5 );

                // Add magicalProtectionRequirements
                Requirement magicalProtectionLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 2 );
                Requirement magicalProtectionLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 1 );
                Requirement magicalProtectionLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 2 );
                Requirement magicalProtectionLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 3 );
                Requirement magicalProtectionLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 4 );
                Requirement magicalProtectionLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 5 );
                Requirement magicalProtectionLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 6 );
                Requirement magicalProtectionLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 7 );
                Requirement magicalProtectionLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 8 );
                Requirement magicalProtectionLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 9 );

                //Add Bonuses ( 1% defense bonus magical type units / magicalProtection level )
                BonusTechnologyOnUnit magicalProtectionBonusLevel1 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 1 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel2 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 2 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel3 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 3 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel4 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 4 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel5 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 5 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel6 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 6 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel7 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 7 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel8 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 8 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel9 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 9 );
                BonusTechnologyOnUnit magicalProtectionBonusLevel10 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.magic, 10 );

                // Set up Levels
                TechnologyLevel magicalProtectionLevel1 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 1, magicalProtectionLevel1Cost, 75,
                    CreateRequirementList( magicalProtectionLevel1Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel1 ) );
                TechnologyLevel magicalProtectionLevel2 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 2, magicalProtectionLevel2Cost, (int)( magicalProtectionLevel1.Duration * 2.5 ),
                    CreateRequirementList( magicalProtectionLevel2Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel2 ) );
                TechnologyLevel magicalProtectionLevel3 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 3, magicalProtectionLevel3Cost, (int)( magicalProtectionLevel2.Duration * 2.5 ),
                    CreateRequirementList( magicalProtectionLevel3Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel2 ) );
                TechnologyLevel magicalProtectionLevel4 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 4, magicalProtectionLevel3Cost, (int)( magicalProtectionLevel3.Duration * 2.75 ),
                    CreateRequirementList( magicalProtectionLevel4Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel3 ) );
                TechnologyLevel magicalProtectionLevel5 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 5, magicalProtectionLevel5Cost, (int)( magicalProtectionLevel4.Duration * 2.75 ),
                    CreateRequirementList( magicalProtectionLevel5Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel4 ) );
                TechnologyLevel magicalProtectionLevel6 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 6, magicalProtectionLevel6Cost, (int)( magicalProtectionLevel5.Duration * 2.75 ),
                    CreateRequirementList( magicalProtectionLevel6Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel5 ) );
                TechnologyLevel magicalProtectionLevel7 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 7, magicalProtectionLevel7Cost, (int)( magicalProtectionLevel6.Duration * 3.0 ),
                    CreateRequirementList( magicalProtectionLevel7Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel6 ) );
                TechnologyLevel magicalProtectionLevel8 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 8, magicalProtectionLevel8Cost, (int)( magicalProtectionLevel7.Duration * 3.0 ),
                    CreateRequirementList( magicalProtectionLevel8Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel7 ) );
                TechnologyLevel magicalProtectionLevel9 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 9, magicalProtectionLevel9Cost, (int)( magicalProtectionLevel8.Duration * 3.0 ),
                    CreateRequirementList( magicalProtectionLevel9Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel8 ) );
                TechnologyLevel magicalProtectionLevel10 = AddTechnologyLevel( context, TechnologyName.magicalProtection, 10, magicalProtectionLevel10Cost, (int)( magicalProtectionLevel9.Duration * 3.25 ),
                    CreateRequirementList( magicalProtectionLevel10Requirement ),
                    CreateBonusTechnologyList( magicalProtectionBonusLevel9 ) );

                context.SaveChanges();
            }
        }
        public void SeedmagicalStaffLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add magicalStaffCosts
                Ressource magicalStaffLevel1Cost = AddRessource( context, 0, 100, 50, 25 );
                Ressource magicalStaffLevel2Cost = AddMiltipliedRessource( context, magicalStaffLevel1Cost, 2.5 );
                Ressource magicalStaffLevel3Cost = AddMiltipliedRessource( context, magicalStaffLevel2Cost, 2.5 );
                Ressource magicalStaffLevel4Cost = AddMiltipliedRessource( context, magicalStaffLevel3Cost, 2.75 );
                Ressource magicalStaffLevel5Cost = AddMiltipliedRessource( context, magicalStaffLevel4Cost, 2.75 );
                Ressource magicalStaffLevel6Cost = AddMiltipliedRessource( context, magicalStaffLevel5Cost, 2.75 );
                Ressource magicalStaffLevel7Cost = AddMiltipliedRessource( context, magicalStaffLevel6Cost, 3 );
                Ressource magicalStaffLevel8Cost = AddMiltipliedRessource( context, magicalStaffLevel7Cost, 3 );
                Ressource magicalStaffLevel9Cost = AddMiltipliedRessource( context, magicalStaffLevel8Cost, 3.25 );
                Ressource magicalStaffLevel10Cost = AddMiltipliedRessource( context, magicalStaffLevel9Cost, 3.5 );

                // Add magicalStaffRequirements
                Requirement magicalStaffLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 2 );
                Requirement magicalStaffLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 1 );
                Requirement magicalStaffLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 2 );
                Requirement magicalStaffLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 3 );
                Requirement magicalStaffLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 4 );
                Requirement magicalStaffLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 5 );
                Requirement magicalStaffLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 6 );
                Requirement magicalStaffLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 7 );
                Requirement magicalStaffLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 8 );
                Requirement magicalStaffLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 9 );

                //Add Bonuses ( 1% attack bonus magical type units / magicalStaff level )
                BonusTechnologyOnUnit magicalStaffBonusLevel1 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 1 );
                BonusTechnologyOnUnit magicalStaffBonusLevel2 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 2 );
                BonusTechnologyOnUnit magicalStaffBonusLevel3 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 3 );
                BonusTechnologyOnUnit magicalStaffBonusLevel4 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 4 );
                BonusTechnologyOnUnit magicalStaffBonusLevel5 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 5 );
                BonusTechnologyOnUnit magicalStaffBonusLevel6 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 6 );
                BonusTechnologyOnUnit magicalStaffBonusLevel7 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 7 );
                BonusTechnologyOnUnit magicalStaffBonusLevel8 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 8 );
                BonusTechnologyOnUnit magicalStaffBonusLevel9 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 9 );
                BonusTechnologyOnUnit magicalStaffBonusLevel10 = AddBonusTechnology( context, BonusType.army_attack, UnitType.magic, 10 );

                // Set up Levels
                TechnologyLevel magicalStaffLevel1 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 1, magicalStaffLevel1Cost, 75,
                    CreateRequirementList( magicalStaffLevel1Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel1 ) );
                TechnologyLevel magicalStaffLevel2 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 2, magicalStaffLevel2Cost, (int)( magicalStaffLevel1.Duration * 2.5 ),
                    CreateRequirementList( magicalStaffLevel2Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel2 ) );
                TechnologyLevel magicalStaffLevel3 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 3, magicalStaffLevel3Cost, (int)( magicalStaffLevel2.Duration * 2.5 ),
                    CreateRequirementList( magicalStaffLevel3Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel2 ) );
                TechnologyLevel magicalStaffLevel4 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 4, magicalStaffLevel3Cost, (int)( magicalStaffLevel3.Duration * 2.75 ),
                    CreateRequirementList( magicalStaffLevel4Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel3 ) );
                TechnologyLevel magicalStaffLevel5 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 5, magicalStaffLevel5Cost, (int)( magicalStaffLevel4.Duration * 2.75 ),
                    CreateRequirementList( magicalStaffLevel5Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel4 ) );
                TechnologyLevel magicalStaffLevel6 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 6, magicalStaffLevel6Cost, (int)( magicalStaffLevel5.Duration * 2.75 ),
                    CreateRequirementList( magicalStaffLevel6Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel5 ) );
                TechnologyLevel magicalStaffLevel7 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 7, magicalStaffLevel7Cost, (int)( magicalStaffLevel6.Duration * 3.0 ),
                    CreateRequirementList( magicalStaffLevel7Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel6 ) );
                TechnologyLevel magicalStaffLevel8 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 8, magicalStaffLevel8Cost, (int)( magicalStaffLevel7.Duration * 3.0 ),
                    CreateRequirementList( magicalStaffLevel8Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel7 ) );
                TechnologyLevel magicalStaffLevel9 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 9, magicalStaffLevel9Cost, (int)( magicalStaffLevel8.Duration * 3.0 ),
                    CreateRequirementList( magicalStaffLevel9Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel8 ) );
                TechnologyLevel magicalStaffLevel10 = AddTechnologyLevel( context, TechnologyName.magicalStaff, 10, magicalStaffLevel10Cost, (int)( magicalStaffLevel9.Duration * 3.25 ),
                    CreateRequirementList( magicalStaffLevel10Requirement ),
                    CreateBonusTechnologyList( magicalStaffBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedLevitationLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add levitationCosts
                Ressource levitationLevel1Cost = AddRessource( context, 0, 100, 50, 25 );
                Ressource levitationLevel2Cost = AddMiltipliedRessource( context, levitationLevel1Cost, 2.5 );
                Ressource levitationLevel3Cost = AddMiltipliedRessource( context, levitationLevel2Cost, 2.5 );
                Ressource levitationLevel4Cost = AddMiltipliedRessource( context, levitationLevel3Cost, 2.75 );
                Ressource levitationLevel5Cost = AddMiltipliedRessource( context, levitationLevel4Cost, 2.75 );
                Ressource levitationLevel6Cost = AddMiltipliedRessource( context, levitationLevel5Cost, 2.75 );
                Ressource levitationLevel7Cost = AddMiltipliedRessource( context, levitationLevel6Cost, 3 );
                Ressource levitationLevel8Cost = AddMiltipliedRessource( context, levitationLevel7Cost, 3 );
                Ressource levitationLevel9Cost = AddMiltipliedRessource( context, levitationLevel8Cost, 3.25 );
                Ressource levitationLevel10Cost = AddMiltipliedRessource( context, levitationLevel9Cost, 3.5 );

                // Add levitationRequirements
                Requirement levitationLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 2 );
                Requirement levitationLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 1 );
                Requirement levitationLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 2 );
                Requirement levitationLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 3 );
                Requirement levitationLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 4 );
                Requirement levitationLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 5 );
                Requirement levitationLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 6 );
                Requirement levitationLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 7 );
                Requirement levitationLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 8 );
                Requirement levitationLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.levitation, 9 );

                //Add Bonuses ( 1% speed bonus magical type units / levitation level )
                BonusTechnologyOnUnit levitationBonusLevel1 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 1 );
                BonusTechnologyOnUnit levitationBonusLevel2 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 2 );
                BonusTechnologyOnUnit levitationBonusLevel3 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 3 );
                BonusTechnologyOnUnit levitationBonusLevel4 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 4 );
                BonusTechnologyOnUnit levitationBonusLevel5 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 5 );
                BonusTechnologyOnUnit levitationBonusLevel6 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 6 );
                BonusTechnologyOnUnit levitationBonusLevel7 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 7 );
                BonusTechnologyOnUnit levitationBonusLevel8 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 8 );
                BonusTechnologyOnUnit levitationBonusLevel9 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 9 );
                BonusTechnologyOnUnit levitationBonusLevel10 = AddBonusTechnology( context, BonusType.army_speed, UnitType.magic, 10 );

                // Set up Levels
                TechnologyLevel levitationLevel1 = AddTechnologyLevel( context, TechnologyName.levitation, 1, levitationLevel1Cost, 75,
                    CreateRequirementList( levitationLevel1Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel1 ) );
                TechnologyLevel levitationLevel2 = AddTechnologyLevel( context, TechnologyName.levitation, 2, levitationLevel2Cost, (int)( levitationLevel1.Duration * 2.5 ),
                    CreateRequirementList( levitationLevel2Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel2 ) );
                TechnologyLevel levitationLevel3 = AddTechnologyLevel( context, TechnologyName.levitation, 3, levitationLevel3Cost, (int)( levitationLevel2.Duration * 2.5 ),
                    CreateRequirementList( levitationLevel3Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel2 ) );
                TechnologyLevel levitationLevel4 = AddTechnologyLevel( context, TechnologyName.levitation, 4, levitationLevel3Cost, (int)( levitationLevel3.Duration * 2.75 ),
                    CreateRequirementList( levitationLevel4Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel3 ) );
                TechnologyLevel levitationLevel5 = AddTechnologyLevel( context, TechnologyName.levitation, 5, levitationLevel5Cost, (int)( levitationLevel4.Duration * 2.75 ),
                    CreateRequirementList( levitationLevel5Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel4 ) );
                TechnologyLevel levitationLevel6 = AddTechnologyLevel( context, TechnologyName.levitation, 6, levitationLevel6Cost, (int)( levitationLevel5.Duration * 2.75 ),
                    CreateRequirementList( levitationLevel6Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel5 ) );
                TechnologyLevel levitationLevel7 = AddTechnologyLevel( context, TechnologyName.levitation, 7, levitationLevel7Cost, (int)( levitationLevel6.Duration * 3.0 ),
                    CreateRequirementList( levitationLevel7Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel6 ) );
                TechnologyLevel levitationLevel8 = AddTechnologyLevel( context, TechnologyName.levitation, 8, levitationLevel8Cost, (int)( levitationLevel7.Duration * 3.0 ),
                    CreateRequirementList( levitationLevel8Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel7 ) );
                TechnologyLevel levitationLevel9 = AddTechnologyLevel( context, TechnologyName.levitation, 9, levitationLevel9Cost, (int)( levitationLevel8.Duration * 3.0 ),
                    CreateRequirementList( levitationLevel9Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel8 ) );
                TechnologyLevel levitationLevel10 = AddTechnologyLevel( context, TechnologyName.levitation, 10, levitationLevel10Cost, (int)( levitationLevel9.Duration * 3.25 ),
                    CreateRequirementList( levitationLevel10Requirement ),
                    CreateBonusTechnologyList( levitationBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedShellLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add shellCosts
                Ressource shellLevel1Cost = AddRessource( context, 100, 100, 100, 0 );
                Ressource shellLevel2Cost = AddMiltipliedRessource( context, shellLevel1Cost, 2.5 );
                Ressource shellLevel3Cost = AddMiltipliedRessource( context, shellLevel2Cost, 2.5 );
                Ressource shellLevel4Cost = AddMiltipliedRessource( context, shellLevel3Cost, 2.75 );
                Ressource shellLevel5Cost = AddMiltipliedRessource( context, shellLevel4Cost, 2.75 );
                Ressource shellLevel6Cost = AddMiltipliedRessource( context, shellLevel5Cost, 2.75 );
                Ressource shellLevel7Cost = AddMiltipliedRessource( context, shellLevel6Cost, 3 );
                Ressource shellLevel8Cost = AddMiltipliedRessource( context, shellLevel7Cost, 3 );
                Ressource shellLevel9Cost = AddMiltipliedRessource( context, shellLevel8Cost, 3.25 );
                Ressource shellLevel10Cost = AddMiltipliedRessource( context, shellLevel9Cost, 3.5 );

                // Add shellRequirements
                Requirement shellLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 3 );
                Requirement shellLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 1 );
                Requirement shellLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 2 );
                Requirement shellLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 3 );
                Requirement shellLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 4 );
                Requirement shellLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 5 );
                Requirement shellLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 6 );
                Requirement shellLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 7 );
                Requirement shellLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 8 );
                Requirement shellLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.shell, 9 );

                //Add Bonuses ( 1% physical and magical defense bonus monster type units / shell level )
                BonusTechnologyOnUnit shellMRBonusLevel1 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 1 );
                BonusTechnologyOnUnit shellMRBonusLevel2 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 2 );
                BonusTechnologyOnUnit shellMRBonusLevel3 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 3 );
                BonusTechnologyOnUnit shellMRBonusLevel4 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 4 );
                BonusTechnologyOnUnit shellMRBonusLevel5 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 5 );
                BonusTechnologyOnUnit shellMRBonusLevel6 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 6 );
                BonusTechnologyOnUnit shellMRBonusLevel7 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 7 );
                BonusTechnologyOnUnit shellMRBonusLevel8 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 8 );
                BonusTechnologyOnUnit shellMRBonusLevel9 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 9 );
                BonusTechnologyOnUnit shellMRBonusLevel10 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.monster, 10 );

                BonusTechnologyOnUnit shellArmorBonusLevel1 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 1 );
                BonusTechnologyOnUnit shellArmorBonusLevel2 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 2 );
                BonusTechnologyOnUnit shellArmorBonusLevel3 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 3 );
                BonusTechnologyOnUnit shellArmorBonusLevel4 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 4 );
                BonusTechnologyOnUnit shellArmorBonusLevel5 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 5 );
                BonusTechnologyOnUnit shellArmorBonusLevel6 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 6 );
                BonusTechnologyOnUnit shellArmorBonusLevel7 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 7 );
                BonusTechnologyOnUnit shellArmorBonusLevel8 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 8 );
                BonusTechnologyOnUnit shellArmorBonusLevel9 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 9 );
                BonusTechnologyOnUnit shellArmorBonusLevel10 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.monster, 10 );

                // Set up Levels
                TechnologyLevel shellLevel1 = AddTechnologyLevel( context, TechnologyName.shell, 1, shellLevel1Cost, 75,
                    CreateRequirementList( shellLevel1Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel1, shellArmorBonusLevel1 ) );
                TechnologyLevel shellLevel2 = AddTechnologyLevel( context, TechnologyName.shell, 2, shellLevel2Cost, (int)( shellLevel1.Duration * 2.5 ),
                    CreateRequirementList( shellLevel2Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel2, shellArmorBonusLevel2 ) );
                TechnologyLevel shellLevel3 = AddTechnologyLevel( context, TechnologyName.shell, 3, shellLevel3Cost, (int)( shellLevel2.Duration * 2.5 ),
                    CreateRequirementList( shellLevel3Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel3, shellArmorBonusLevel3 ) );
                TechnologyLevel shellLevel4 = AddTechnologyLevel( context, TechnologyName.shell, 4, shellLevel3Cost, (int)( shellLevel3.Duration * 2.75 ),
                    CreateRequirementList( shellLevel4Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel4, shellArmorBonusLevel3 ) );
                TechnologyLevel shellLevel5 = AddTechnologyLevel( context, TechnologyName.shell, 5, shellLevel5Cost, (int)( shellLevel4.Duration * 2.75 ),
                    CreateRequirementList( shellLevel5Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel5, shellArmorBonusLevel4 ) );
                TechnologyLevel shellLevel6 = AddTechnologyLevel( context, TechnologyName.shell, 6, shellLevel6Cost, (int)( shellLevel5.Duration * 2.75 ),
                    CreateRequirementList( shellLevel6Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel6, shellArmorBonusLevel5 ) );
                TechnologyLevel shellLevel7 = AddTechnologyLevel( context, TechnologyName.shell, 7, shellLevel7Cost, (int)( shellLevel6.Duration * 3.0 ),
                    CreateRequirementList( shellLevel7Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel7, shellArmorBonusLevel6 ) );
                TechnologyLevel shellLevel8 = AddTechnologyLevel( context, TechnologyName.shell, 8, shellLevel8Cost, (int)( shellLevel7.Duration * 3.0 ),
                    CreateRequirementList( shellLevel8Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel8, shellArmorBonusLevel7 ) );
                TechnologyLevel shellLevel9 = AddTechnologyLevel( context, TechnologyName.shell, 9, shellLevel9Cost, (int)( shellLevel8.Duration * 3.0 ),
                    CreateRequirementList( shellLevel9Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel9, shellArmorBonusLevel8 ) );
                TechnologyLevel shellLevel10 = AddTechnologyLevel( context, TechnologyName.shell, 10, shellLevel10Cost, (int)( shellLevel9.Duration * 3.25 ),
                    CreateRequirementList( shellLevel10Requirement ),
                    CreateBonusTechnologyList( shellMRBonusLevel10, shellArmorBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedClawsLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add clawsCosts
                Ressource clawsLevel1Cost = AddRessource( context, 100, 100, 100, 0 );
                Ressource clawsLevel2Cost = AddMiltipliedRessource( context, clawsLevel1Cost, 2.5 );
                Ressource clawsLevel3Cost = AddMiltipliedRessource( context, clawsLevel2Cost, 2.5 );
                Ressource clawsLevel4Cost = AddMiltipliedRessource( context, clawsLevel3Cost, 2.75 );
                Ressource clawsLevel5Cost = AddMiltipliedRessource( context, clawsLevel4Cost, 2.75 );
                Ressource clawsLevel6Cost = AddMiltipliedRessource( context, clawsLevel5Cost, 2.75 );
                Ressource clawsLevel7Cost = AddMiltipliedRessource( context, clawsLevel6Cost, 3 );
                Ressource clawsLevel8Cost = AddMiltipliedRessource( context, clawsLevel7Cost, 3 );
                Ressource clawsLevel9Cost = AddMiltipliedRessource( context, clawsLevel8Cost, 3.25 );
                Ressource clawsLevel10Cost = AddMiltipliedRessource( context, clawsLevel9Cost, 3.5 );

                // Add clawsRequirements
                Requirement clawsLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 3 );
                Requirement clawsLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 1 );
                Requirement clawsLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 2 );
                Requirement clawsLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 3 );
                Requirement clawsLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 4 );
                Requirement clawsLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 5 );
                Requirement clawsLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 6 );
                Requirement clawsLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 7 );
                Requirement clawsLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 8 );
                Requirement clawsLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.claws, 9 );

                //Add Bonuses ( 1% attack bonus monster type units / claws level )
                BonusTechnologyOnUnit clawsBonusLevel1 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 1 );
                BonusTechnologyOnUnit clawsBonusLevel2 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 2 );
                BonusTechnologyOnUnit clawsBonusLevel3 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 3 );
                BonusTechnologyOnUnit clawsBonusLevel4 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 4 );
                BonusTechnologyOnUnit clawsBonusLevel5 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 5 );
                BonusTechnologyOnUnit clawsBonusLevel6 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 6 );
                BonusTechnologyOnUnit clawsBonusLevel7 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 7 );
                BonusTechnologyOnUnit clawsBonusLevel8 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 8 );
                BonusTechnologyOnUnit clawsBonusLevel9 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 9 );
                BonusTechnologyOnUnit clawsBonusLevel10 = AddBonusTechnology( context, BonusType.army_attack, UnitType.monster, 10 );

                // Set up Levels
                TechnologyLevel clawsLevel1 = AddTechnologyLevel( context, TechnologyName.claws, 1, clawsLevel1Cost, 75,
                    CreateRequirementList( clawsLevel1Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel1 ) );
                TechnologyLevel clawsLevel2 = AddTechnologyLevel( context, TechnologyName.claws, 2, clawsLevel2Cost, (int)( clawsLevel1.Duration * 2.5 ),
                    CreateRequirementList( clawsLevel2Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel2 ) );
                TechnologyLevel clawsLevel3 = AddTechnologyLevel( context, TechnologyName.claws, 3, clawsLevel3Cost, (int)( clawsLevel2.Duration * 2.5 ),
                    CreateRequirementList( clawsLevel3Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel2 ) );
                TechnologyLevel clawsLevel4 = AddTechnologyLevel( context, TechnologyName.claws, 4, clawsLevel3Cost, (int)( clawsLevel3.Duration * 2.75 ),
                    CreateRequirementList( clawsLevel4Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel3 ) );
                TechnologyLevel clawsLevel5 = AddTechnologyLevel( context, TechnologyName.claws, 5, clawsLevel5Cost, (int)( clawsLevel4.Duration * 2.75 ),
                    CreateRequirementList( clawsLevel5Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel4 ) );
                TechnologyLevel clawsLevel6 = AddTechnologyLevel( context, TechnologyName.claws, 6, clawsLevel6Cost, (int)( clawsLevel5.Duration * 2.75 ),
                    CreateRequirementList( clawsLevel6Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel5 ) );
                TechnologyLevel clawsLevel7 = AddTechnologyLevel( context, TechnologyName.claws, 7, clawsLevel7Cost, (int)( clawsLevel6.Duration * 3.0 ),
                    CreateRequirementList( clawsLevel7Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel6 ) );
                TechnologyLevel clawsLevel8 = AddTechnologyLevel( context, TechnologyName.claws, 8, clawsLevel8Cost, (int)( clawsLevel7.Duration * 3.0 ),
                    CreateRequirementList( clawsLevel8Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel7 ) );
                TechnologyLevel clawsLevel9 = AddTechnologyLevel( context, TechnologyName.claws, 9, clawsLevel9Cost, (int)( clawsLevel8.Duration * 3.0 ),
                    CreateRequirementList( clawsLevel9Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel8 ) );
                TechnologyLevel clawsLevel10 = AddTechnologyLevel( context, TechnologyName.claws, 10, clawsLevel10Cost, (int)( clawsLevel9.Duration * 3.25 ),
                    CreateRequirementList( clawsLevel10Requirement ),
                    CreateBonusTechnologyList( clawsBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedWingsLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add wingsCosts
                Ressource wingsLevel1Cost = AddRessource( context, 100, 100, 100, 0 );
                Ressource wingsLevel2Cost = AddMiltipliedRessource( context, wingsLevel1Cost, 2.5 );
                Ressource wingsLevel3Cost = AddMiltipliedRessource( context, wingsLevel2Cost, 2.5 );
                Ressource wingsLevel4Cost = AddMiltipliedRessource( context, wingsLevel3Cost, 2.75 );
                Ressource wingsLevel5Cost = AddMiltipliedRessource( context, wingsLevel4Cost, 2.75 );
                Ressource wingsLevel6Cost = AddMiltipliedRessource( context, wingsLevel5Cost, 2.75 );
                Ressource wingsLevel7Cost = AddMiltipliedRessource( context, wingsLevel6Cost, 3 );
                Ressource wingsLevel8Cost = AddMiltipliedRessource( context, wingsLevel7Cost, 3 );
                Ressource wingsLevel9Cost = AddMiltipliedRessource( context, wingsLevel8Cost, 3.25 );
                Ressource wingsLevel10Cost = AddMiltipliedRessource( context, wingsLevel9Cost, 3.5 );

                // Add wingsRequirements
                Requirement wingsLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 3 );
                Requirement wingsLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 1 );
                Requirement wingsLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 2 );
                Requirement wingsLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 3 );
                Requirement wingsLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 4 );
                Requirement wingsLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 5 );
                Requirement wingsLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 6 );
                Requirement wingsLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 7 );
                Requirement wingsLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 8 );
                Requirement wingsLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.wings, 9 );

                //Add Bonuses ( 1% speed bonus monster type units / wings level )
                BonusTechnologyOnUnit wingsBonusLevel1 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 1 );
                BonusTechnologyOnUnit wingsBonusLevel2 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 2 );
                BonusTechnologyOnUnit wingsBonusLevel3 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 3 );
                BonusTechnologyOnUnit wingsBonusLevel4 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 4 );
                BonusTechnologyOnUnit wingsBonusLevel5 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 5 );
                BonusTechnologyOnUnit wingsBonusLevel6 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 6 );
                BonusTechnologyOnUnit wingsBonusLevel7 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 7 );
                BonusTechnologyOnUnit wingsBonusLevel8 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 8 );
                BonusTechnologyOnUnit wingsBonusLevel9 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 9 );
                BonusTechnologyOnUnit wingsBonusLevel10 = AddBonusTechnology( context, BonusType.army_speed, UnitType.monster, 10 );

                // Set up Levels
                TechnologyLevel wingsLevel1 = AddTechnologyLevel( context, TechnologyName.wings, 1, wingsLevel1Cost, 75,
                    CreateRequirementList( wingsLevel1Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel1 ) );
                TechnologyLevel wingsLevel2 = AddTechnologyLevel( context, TechnologyName.wings, 2, wingsLevel2Cost, (int)( wingsLevel1.Duration * 2.5 ),
                    CreateRequirementList( wingsLevel2Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel2 ) );
                TechnologyLevel wingsLevel3 = AddTechnologyLevel( context, TechnologyName.wings, 3, wingsLevel3Cost, (int)( wingsLevel2.Duration * 2.5 ),
                    CreateRequirementList( wingsLevel3Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel2 ) );
                TechnologyLevel wingsLevel4 = AddTechnologyLevel( context, TechnologyName.wings, 4, wingsLevel3Cost, (int)( wingsLevel3.Duration * 2.75 ),
                    CreateRequirementList( wingsLevel4Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel3 ) );
                TechnologyLevel wingsLevel5 = AddTechnologyLevel( context, TechnologyName.wings, 5, wingsLevel5Cost, (int)( wingsLevel4.Duration * 2.75 ),
                    CreateRequirementList( wingsLevel5Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel4 ) );
                TechnologyLevel wingsLevel6 = AddTechnologyLevel( context, TechnologyName.wings, 6, wingsLevel6Cost, (int)( wingsLevel5.Duration * 2.75 ),
                    CreateRequirementList( wingsLevel6Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel5 ) );
                TechnologyLevel wingsLevel7 = AddTechnologyLevel( context, TechnologyName.wings, 7, wingsLevel7Cost, (int)( wingsLevel6.Duration * 3.0 ),
                    CreateRequirementList( wingsLevel7Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel6 ) );
                TechnologyLevel wingsLevel8 = AddTechnologyLevel( context, TechnologyName.wings, 8, wingsLevel8Cost, (int)( wingsLevel7.Duration * 3.0 ),
                    CreateRequirementList( wingsLevel8Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel7 ) );
                TechnologyLevel wingsLevel9 = AddTechnologyLevel( context, TechnologyName.wings, 9, wingsLevel9Cost, (int)( wingsLevel8.Duration * 3.0 ),
                    CreateRequirementList( wingsLevel9Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel8 ) );
                TechnologyLevel wingsLevel10 = AddTechnologyLevel( context, TechnologyName.wings, 10, wingsLevel10Cost, (int)( wingsLevel9.Duration * 3.25 ),
                    CreateRequirementList( wingsLevel10Requirement ),
                    CreateBonusTechnologyList( wingsBonusLevel9 ) );

                context.SaveChanges();
            }

        }

        public void SeedPlatingLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add platingCosts
                Ressource platingLevel1Cost = AddRessource( context, 200, 150, 50, 100 );
                Ressource platingLevel2Cost = AddMiltipliedRessource( context, platingLevel1Cost, 2.5 );
                Ressource platingLevel3Cost = AddMiltipliedRessource( context, platingLevel2Cost, 2.5 );
                Ressource platingLevel4Cost = AddMiltipliedRessource( context, platingLevel3Cost, 2.75 );
                Ressource platingLevel5Cost = AddMiltipliedRessource( context, platingLevel4Cost, 2.75 );
                Ressource platingLevel6Cost = AddMiltipliedRessource( context, platingLevel5Cost, 2.75 );
                Ressource platingLevel7Cost = AddMiltipliedRessource( context, platingLevel6Cost, 3 );
                Ressource platingLevel8Cost = AddMiltipliedRessource( context, platingLevel7Cost, 3 );
                Ressource platingLevel9Cost = AddMiltipliedRessource( context, platingLevel8Cost, 3.25 );
                Ressource platingLevel10Cost = AddMiltipliedRessource( context, platingLevel9Cost, 3.5 );

                // Add platingRequirements
                Requirement platingLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 4 );
                Requirement platingLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 1 );
                Requirement platingLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 2 );
                Requirement platingLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 3 );
                Requirement platingLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 4 );
                Requirement platingLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 5 );
                Requirement platingLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 6 );
                Requirement platingLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 7 );
                Requirement platingLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 8 );
                Requirement platingLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.plating, 9 );

                //Add Bonuses ( 2% magicResist and armor bonus mecanic type units / plating level )
                BonusTechnologyOnUnit platingMRBonusLevel1 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 1 );
                BonusTechnologyOnUnit platingMRBonusLevel2 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 2 );
                BonusTechnologyOnUnit platingMRBonusLevel3 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 3 );
                BonusTechnologyOnUnit platingMRBonusLevel4 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 4 );
                BonusTechnologyOnUnit platingMRBonusLevel5 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 5 );
                BonusTechnologyOnUnit platingMRBonusLevel6 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 6 );
                BonusTechnologyOnUnit platingMRBonusLevel7 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 7 );
                BonusTechnologyOnUnit platingMRBonusLevel8 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 8 );
                BonusTechnologyOnUnit platingMRBonusLevel9 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 9 );
                BonusTechnologyOnUnit platingMRBonusLevel10 = AddBonusTechnology( context, BonusType.army_magicalDefense, UnitType.mecanical, 10 );

                BonusTechnologyOnUnit platingArmorBonusLevel1 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 1 );
                BonusTechnologyOnUnit platingArmorBonusLevel2 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 2 );
                BonusTechnologyOnUnit platingArmorBonusLevel3 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 3 );
                BonusTechnologyOnUnit platingArmorBonusLevel4 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 4 );
                BonusTechnologyOnUnit platingArmorBonusLevel5 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 5 );
                BonusTechnologyOnUnit platingArmorBonusLevel6 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 6 );
                BonusTechnologyOnUnit platingArmorBonusLevel7 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 7 );
                BonusTechnologyOnUnit platingArmorBonusLevel8 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 8 );
                BonusTechnologyOnUnit platingArmorBonusLevel9 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 9 );
                BonusTechnologyOnUnit platingArmorBonusLevel10 = AddBonusTechnology( context, BonusType.army_physicalDefense, UnitType.mecanical, 10 );

                // Set up Levels
                TechnologyLevel platingLevel1 = AddTechnologyLevel( context, TechnologyName.plating, 1, platingLevel1Cost, 75,
                       CreateRequirementList( platingLevel1Requirement ),
                       CreateBonusTechnologyList( platingMRBonusLevel1, platingArmorBonusLevel1 ) );
                TechnologyLevel platingLevel2 = AddTechnologyLevel( context, TechnologyName.plating, 2, platingLevel2Cost, (int)( platingLevel1.Duration * 2.5 ),
                    CreateRequirementList( platingLevel2Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel2, platingArmorBonusLevel2 ) );
                TechnologyLevel platingLevel3 = AddTechnologyLevel( context, TechnologyName.plating, 3, platingLevel3Cost, (int)( platingLevel2.Duration * 2.5 ),
                    CreateRequirementList( platingLevel3Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel3, platingArmorBonusLevel3 ) );
                TechnologyLevel platingLevel4 = AddTechnologyLevel( context, TechnologyName.plating, 4, platingLevel3Cost, (int)( platingLevel3.Duration * 2.75 ),
                    CreateRequirementList( platingLevel4Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel4, platingArmorBonusLevel3 ) );
                TechnologyLevel platingLevel5 = AddTechnologyLevel( context, TechnologyName.plating, 5, platingLevel5Cost, (int)( platingLevel4.Duration * 2.75 ),
                    CreateRequirementList( platingLevel5Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel5, platingArmorBonusLevel4 ) );
                TechnologyLevel platingLevel6 = AddTechnologyLevel( context, TechnologyName.plating, 6, platingLevel6Cost, (int)( platingLevel5.Duration * 2.75 ),
                    CreateRequirementList( platingLevel6Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel6, platingArmorBonusLevel5 ) );
                TechnologyLevel platingLevel7 = AddTechnologyLevel( context, TechnologyName.plating, 7, platingLevel7Cost, (int)( platingLevel6.Duration * 3.0 ),
                    CreateRequirementList( platingLevel7Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel7, platingArmorBonusLevel6 ) );
                TechnologyLevel platingLevel8 = AddTechnologyLevel( context, TechnologyName.plating, 8, platingLevel8Cost, (int)( platingLevel7.Duration * 3.0 ),
                    CreateRequirementList( platingLevel8Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel8, platingArmorBonusLevel7 ) );
                TechnologyLevel platingLevel9 = AddTechnologyLevel( context, TechnologyName.plating, 9, platingLevel9Cost, (int)( platingLevel8.Duration * 3.0 ),
                    CreateRequirementList( platingLevel9Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel9, platingArmorBonusLevel8 ) );
                TechnologyLevel platingLevel10 = AddTechnologyLevel( context, TechnologyName.plating, 10, platingLevel10Cost, (int)( platingLevel9.Duration * 3.25 ),
                    CreateRequirementList( platingLevel10Requirement ),
                    CreateBonusTechnologyList( platingMRBonusLevel10, platingArmorBonusLevel9 ) );

                context.SaveChanges();
            }
        }

        public void SeedHoldLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add holdCosts
                Ressource holdLevel1Cost = AddRessource( context, 150, 150, 75, 0 );
                Ressource holdLevel2Cost = AddMiltipliedRessource( context, holdLevel1Cost, 2.5 );
                Ressource holdLevel3Cost = AddMiltipliedRessource( context, holdLevel2Cost, 2.5 );
                Ressource holdLevel4Cost = AddMiltipliedRessource( context, holdLevel3Cost, 2.75 );
                Ressource holdLevel5Cost = AddMiltipliedRessource( context, holdLevel4Cost, 2.75 );
                Ressource holdLevel6Cost = AddMiltipliedRessource( context, holdLevel5Cost, 2.75 );
                Ressource holdLevel7Cost = AddMiltipliedRessource( context, holdLevel6Cost, 3 );
                Ressource holdLevel8Cost = AddMiltipliedRessource( context, holdLevel7Cost, 3 );
                Ressource holdLevel9Cost = AddMiltipliedRessource( context, holdLevel8Cost, 3.25 );
                Ressource holdLevel10Cost = AddMiltipliedRessource( context, holdLevel9Cost, 3.5 );

                // Add holdRequirements
                Requirement holdLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 2 );
                Requirement holdLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 1 );
                Requirement holdLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 2 );
                Requirement holdLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 3 );
                Requirement holdLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 4 );
                Requirement holdLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 5 );
                Requirement holdLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 6 );
                Requirement holdLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 7 );
                Requirement holdLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 8 );
                Requirement holdLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.hold, 9 );

                //Add Bonuses ( 1% speed bonus utility type units / hold level )
                BonusTechnologyOnUnit holdBonusLevel1 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 1 );
                BonusTechnologyOnUnit holdBonusLevel2 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 2 );
                BonusTechnologyOnUnit holdBonusLevel3 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 3 );
                BonusTechnologyOnUnit holdBonusLevel4 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 4 );
                BonusTechnologyOnUnit holdBonusLevel5 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 5 );
                BonusTechnologyOnUnit holdBonusLevel6 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 6 );
                BonusTechnologyOnUnit holdBonusLevel7 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 7 );
                BonusTechnologyOnUnit holdBonusLevel8 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 8 );
                BonusTechnologyOnUnit holdBonusLevel9 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 9 );
                BonusTechnologyOnUnit holdBonusLevel10 = AddBonusTechnology( context, BonusType.army_capacity, UnitType.utility, 10 );

                // Set up Levels
                TechnologyLevel holdLevel1 = AddTechnologyLevel( context, TechnologyName.hold, 1, holdLevel1Cost, 75,
                    CreateRequirementList( holdLevel1Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel1 ) );
                TechnologyLevel holdLevel2 = AddTechnologyLevel( context, TechnologyName.hold, 2, holdLevel2Cost, (int)( holdLevel1.Duration * 2.5 ),
                    CreateRequirementList( holdLevel2Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel2 ) );
                TechnologyLevel holdLevel3 = AddTechnologyLevel( context, TechnologyName.hold, 3, holdLevel3Cost, (int)( holdLevel2.Duration * 2.5 ),
                    CreateRequirementList( holdLevel3Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel2 ) );
                TechnologyLevel holdLevel4 = AddTechnologyLevel( context, TechnologyName.hold, 4, holdLevel3Cost, (int)( holdLevel3.Duration * 2.75 ),
                    CreateRequirementList( holdLevel4Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel3 ) );
                TechnologyLevel holdLevel5 = AddTechnologyLevel( context, TechnologyName.hold, 5, holdLevel5Cost, (int)( holdLevel4.Duration * 2.75 ),
                    CreateRequirementList( holdLevel5Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel4 ) );
                TechnologyLevel holdLevel6 = AddTechnologyLevel( context, TechnologyName.hold, 6, holdLevel6Cost, (int)( holdLevel5.Duration * 2.75 ),
                    CreateRequirementList( holdLevel6Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel5 ) );
                TechnologyLevel holdLevel7 = AddTechnologyLevel( context, TechnologyName.hold, 7, holdLevel7Cost, (int)( holdLevel6.Duration * 3.0 ),
                    CreateRequirementList( holdLevel7Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel6 ) );
                TechnologyLevel holdLevel8 = AddTechnologyLevel( context, TechnologyName.hold, 8, holdLevel8Cost, (int)( holdLevel7.Duration * 3.0 ),
                    CreateRequirementList( holdLevel8Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel7 ) );
                TechnologyLevel holdLevel9 = AddTechnologyLevel( context, TechnologyName.hold, 9, holdLevel9Cost, (int)( holdLevel8.Duration * 3.0 ),
                    CreateRequirementList( holdLevel9Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel8 ) );
                TechnologyLevel holdLevel10 = AddTechnologyLevel( context, TechnologyName.hold, 10, holdLevel10Cost, (int)( holdLevel9.Duration * 3.25 ),
                    CreateRequirementList( holdLevel10Requirement ),
                    CreateBonusTechnologyList( holdBonusLevel9 ) );

                context.SaveChanges();
            }

        }

        public void SeedPropulsionLevels()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Add propulsionCosts
                Ressource propulsionLevel1Cost = AddRessource( context, 150, 150, 75, 0 );
                Ressource propulsionLevel2Cost = AddMiltipliedRessource( context, propulsionLevel1Cost, 2.5 );
                Ressource propulsionLevel3Cost = AddMiltipliedRessource( context, propulsionLevel2Cost, 2.5 );
                Ressource propulsionLevel4Cost = AddMiltipliedRessource( context, propulsionLevel3Cost, 2.75 );
                Ressource propulsionLevel5Cost = AddMiltipliedRessource( context, propulsionLevel4Cost, 2.75 );
                Ressource propulsionLevel6Cost = AddMiltipliedRessource( context, propulsionLevel5Cost, 2.75 );
                Ressource propulsionLevel7Cost = AddMiltipliedRessource( context, propulsionLevel6Cost, 3 );
                Ressource propulsionLevel8Cost = AddMiltipliedRessource( context, propulsionLevel7Cost, 3 );
                Ressource propulsionLevel9Cost = AddMiltipliedRessource( context, propulsionLevel8Cost, 3.25 );
                Ressource propulsionLevel10Cost = AddMiltipliedRessource( context, propulsionLevel9Cost, 3.5 );

                // Add propulsionRequirements
                Requirement propulsionLevel1Requirement = AddBuildingRequirement( context, BuildingName.laboratory, 2 );
                Requirement propulsionLevel2Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 1 );
                Requirement propulsionLevel3Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 2 );
                Requirement propulsionLevel4Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 3 );
                Requirement propulsionLevel5Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 4 );
                Requirement propulsionLevel6Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 5 );
                Requirement propulsionLevel7Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 6 );
                Requirement propulsionLevel8Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 7 );
                Requirement propulsionLevel9Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 8 );
                Requirement propulsionLevel10Requirement = AddTechnologyRequirement( context, TechnologyName.propulsion, 9 );

                //Add Bonuses ( 1% speed bonus utility type units / propulsion level )
                BonusTechnologyOnUnit propulsionBonusLevel1 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 1 );
                BonusTechnologyOnUnit propulsionBonusLevel2 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 2 );
                BonusTechnologyOnUnit propulsionBonusLevel3 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 3 );
                BonusTechnologyOnUnit propulsionBonusLevel4 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 4 );
                BonusTechnologyOnUnit propulsionBonusLevel5 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 5 );
                BonusTechnologyOnUnit propulsionBonusLevel6 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 6 );
                BonusTechnologyOnUnit propulsionBonusLevel7 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 7 );
                BonusTechnologyOnUnit propulsionBonusLevel8 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 8 );
                BonusTechnologyOnUnit propulsionBonusLevel9 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 9 );
                BonusTechnologyOnUnit propulsionBonusLevel10 = AddBonusTechnology( context, BonusType.army_speed, UnitType.utility, 10 );

                // Set up Levels
                TechnologyLevel propulsionLevel1 = AddTechnologyLevel( context, TechnologyName.propulsion, 1, propulsionLevel1Cost, 75,
                    CreateRequirementList( propulsionLevel1Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel1 ) );
                TechnologyLevel propulsionLevel2 = AddTechnologyLevel( context, TechnologyName.propulsion, 2, propulsionLevel2Cost, (int)( propulsionLevel1.Duration * 2.5 ),
                    CreateRequirementList( propulsionLevel2Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel2 ) );
                TechnologyLevel propulsionLevel3 = AddTechnologyLevel( context, TechnologyName.propulsion, 3, propulsionLevel3Cost, (int)( propulsionLevel2.Duration * 2.5 ),
                    CreateRequirementList( propulsionLevel3Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel2 ) );
                TechnologyLevel propulsionLevel4 = AddTechnologyLevel( context, TechnologyName.propulsion, 4, propulsionLevel3Cost, (int)( propulsionLevel3.Duration * 2.75 ),
                    CreateRequirementList( propulsionLevel4Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel3 ) );
                TechnologyLevel propulsionLevel5 = AddTechnologyLevel( context, TechnologyName.propulsion, 5, propulsionLevel5Cost, (int)( propulsionLevel4.Duration * 2.75 ),
                    CreateRequirementList( propulsionLevel5Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel4 ) );
                TechnologyLevel propulsionLevel6 = AddTechnologyLevel( context, TechnologyName.propulsion, 6, propulsionLevel6Cost, (int)( propulsionLevel5.Duration * 2.75 ),
                    CreateRequirementList( propulsionLevel6Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel5 ) );
                TechnologyLevel propulsionLevel7 = AddTechnologyLevel( context, TechnologyName.propulsion, 7, propulsionLevel7Cost, (int)( propulsionLevel6.Duration * 3.0 ),
                    CreateRequirementList( propulsionLevel7Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel6 ) );
                TechnologyLevel propulsionLevel8 = AddTechnologyLevel( context, TechnologyName.propulsion, 8, propulsionLevel8Cost, (int)( propulsionLevel7.Duration * 3.0 ),
                    CreateRequirementList( propulsionLevel8Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel7 ) );
                TechnologyLevel propulsionLevel9 = AddTechnologyLevel( context, TechnologyName.propulsion, 9, propulsionLevel9Cost, (int)( propulsionLevel8.Duration * 3.0 ),
                    CreateRequirementList( propulsionLevel9Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel8 ) );
                TechnologyLevel propulsionLevel10 = AddTechnologyLevel( context, TechnologyName.propulsion, 10, propulsionLevel10Cost, (int)( propulsionLevel9.Duration * 3.25 ),
                    CreateRequirementList( propulsionLevel10Requirement ),
                    CreateBonusTechnologyList( propulsionBonusLevel9 ) );

                context.SaveChanges();
            }
        }
        #endregion

        #region Methods
        BonusBuildingOnUnit AddBonusBuilding( SetupContext context, BonusType bonusType, UnitType unitTarget, int modifier )
        {
            BonusBuildingOnUnit bonus = new BonusBuildingOnUnit
            {
                BonusType = bonusType,
                Modifier = modifier,
                TargetUnit = unitTarget
            };
            context.Add( bonus );

            return bonus;
        }

        BonusBuildingOnTechnology AddBonusBuilding( SetupContext context, BonusType bonusType, TechnologyName technologyTarget, int modifier )
        {
            BonusBuildingOnTechnology bonus = new BonusBuildingOnTechnology
            {
                BonusType = bonusType,
                Modifier = modifier,
                TargetTechnology = technologyTarget
            };
            context.Add( bonus );

            return bonus;
        }

        BonusBuildingOnBuilding AddBonusBuilding( SetupContext context, BonusType bonusType, BuildingName buildingsTarget, int modifier )
        {
            BonusBuildingOnBuilding bonus = new BonusBuildingOnBuilding
            {
                BonusType = bonusType,
                Modifier = modifier,
                TargetBuilding = buildingsTarget
            };
            context.Add( bonus );

            return bonus;
        }

        BonusTechnologyOnUnit AddBonusTechnology( SetupContext context, BonusType bonusType, UnitType unitTarget, int modifier )
        {
            BonusTechnologyOnUnit bonus = new BonusTechnologyOnUnit
            {
                BonusType = bonusType,
                Modifier = modifier,
                TargetUnit = unitTarget
            };
            context.Add( bonus );

            return bonus;
        }

        BonusTechnologyOnBuilding AddBonusTechnology( SetupContext context, BonusType bonusType, BuildingName buildingsTarget, int modifier )
        {
            BonusTechnologyOnBuilding bonus = new BonusTechnologyOnBuilding
            {
                BonusType = bonusType,
                Modifier = modifier,
                TargetBuilding = buildingsTarget
            };
            context.Add( bonus );

            return bonus;
        }

        BonusTechnologyOnTechnology AddBonusTechnology( SetupContext context, BonusType bonusType, TechnologyName technologyTarget, int modifier )
        {
            BonusTechnologyOnTechnology bonus = new BonusTechnologyOnTechnology
            {
                BonusType = bonusType,
                Modifier = modifier,
                TargetTechnology = technologyTarget
            };
            context.Add( bonus );

            return bonus;
        }

        BuildingLevel AddBuildingLevel( SetupContext context, BuildingName buildingName, int number, Ressource cost, int duration, List<Requirement> requirements, List<BonusBuilding> bonuses )
        {
            BuildingLevel buildingLevel = new BuildingLevel
            {
                Number = number,
                BuildingName = buildingName,
                Cost = cost,
                Requirements = requirements,
                BuildingBonuses = bonuses,
                Duration = duration
            };
            context.Add( buildingLevel );
            return buildingLevel;
        }

        TechnologyLevel AddTechnologyLevel( SetupContext context, TechnologyName technologyName, int number, Ressource cost, int duration, List<Requirement> requirements, List<BonusTechnology> bonuses )
        {
            TechnologyLevel technologyLevel = new TechnologyLevel
            {
                Number = number,
                TechnologyName = technologyName,
                Cost = cost,
                Requirements = requirements,
                TechnologyBonuses = bonuses,
                Duration = duration
            };
            context.Add( technologyLevel );
            return technologyLevel;
        }

        FieldLevel AddFieldLevel( SetupContext context, BuildingName buildingName, int number, Ressource cost, int duration, List<Requirement> requirements, int production )
        {
            FieldLevel fieldLevel = new FieldLevel
            {
                Number = number,
                BuildingName = buildingName,
                Cost = cost,
                Requirements = requirements,
                Production = production
            };
            context.Add( fieldLevel );
            return fieldLevel;

        }

        ShieldLevel AddShieldLevel( SetupContext context, BuildingName buildingName, int number, Ressource cost, int duration, List<Requirement> requirements, int defense, int offence )
        {
            ShieldLevel fieldLevel = new ShieldLevel
            {
                Number = number,
                BuildingName = buildingName,
                Cost = cost,
                Requirements = requirements,
                Defense = defense,
                Offense = offence
            };
            context.Add( fieldLevel );
            return fieldLevel;

        }

        Requirement AddBuildingRequirement( SetupContext context, BuildingName buildingName, int number )
        {
            Requirement requirement = new Requirement
            {
                BuildingName = buildingName,
                Number = number
            };
            context.Add( requirement );
            return requirement;
        }

        Requirement AddTechnologyRequirement( SetupContext context, TechnologyName technologyName, int number )
        {
            Requirement requirement = new Requirement
            {
                TechnologyName = technologyName,
                Number = number
            };
            context.Add( requirement );
            return requirement;
        }

        List<Requirement> CreateRequirementList( params Requirement[ ] args )
        {
            List<Requirement> requirements = new List<Requirement>();
            foreach ( Requirement r in args )
            {
                requirements.Add( r );
            }
            return requirements;
        }

        List<BonusBuilding> CreateBonusBuildingList( params BonusBuilding[ ] args )
        {
            List<BonusBuilding> bonuses = new List<BonusBuilding>();
            foreach ( BonusBuilding b in args )
            {
                bonuses.Add( b );
            }
            return bonuses;
        }

        List<BonusTechnology> CreateBonusTechnologyList( params BonusTechnology[ ] args )
        {
            List<BonusTechnology> bonuses = new List<BonusTechnology>();
            foreach ( BonusTechnology b in args )
            {
                bonuses.Add( b );
            }
            return bonuses;
        }

        Ressource AddRessource( SetupContext context, int wood, int metal, int cristal, int magic )
        {
            Ressource ressource = new Ressource
            {
                Wood = wood,
                Metal = wood,
                Cristal = cristal,
                Magic = magic
            };
            context.Add( ressource );
            return ressource;
        }

        Ressource AddMiltipliedRessource( SetupContext context, Ressource initialRessource, double factor )
        {
            Ressource multipliedRessoure = Multiplyressource( initialRessource, factor );
            context.Add( multipliedRessoure );
            return multipliedRessoure;
        }

        Ressource Multiplyressource( Ressource initialRessource, double factor )
        {
            return new Ressource
            {
                Wood = (int)( initialRessource.Wood * factor ),
                Metal = (int)( initialRessource.Metal * factor ),
                Cristal = (int)( initialRessource.Cristal * factor ),
                Magic = (int)( initialRessource.Magic * factor )
            };
        }
        #endregion
    }
}
