//using ITI.SkyLord.Models.Entity_Framework.Contexts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ITI.SkyLord
//{
//    public class LevelSeed
//    {
//        public void SeedTowerLevels()
//        {
//            using ( SetupContext context = new SetupContext() )
//            {
//                // Add towerCosts
//                Ressource towerLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
//                Ressource towerLevel2Cost = AddRessourceTimes2( context, towerLevel1Cost );
//                Ressource towerLevel3Cost = AddRessourceTimes2( context, towerLevel2Cost );
//                Ressource towerLevel4Cost = AddRessourceTimes2( context, towerLevel3Cost );
//                Ressource towerLevel5Cost = AddRessourceTimes2( context, towerLevel4Cost );
//                Ressource towerLevel6Cost = AddRessourceTimes2( context, towerLevel5Cost );
//                Ressource towerLevel7Cost = AddRessourceTimes2( context, towerLevel6Cost );
//                Ressource towerLevel8Cost = AddRessourceTimes2( context, towerLevel7Cost );
//                Ressource towerLevel9Cost = AddRessourceTimes2( context, towerLevel8Cost );
//                Ressource towerLevel10Cost = AddRessourceTimes2( context, towerLevel9Cost );

//                // Add towerRequirements
//                Requirement towerLevel2Requirement = AddBuildingRequirement( context, BuildingName.tower, 1 );
//                Requirement towerLevel3Requirement = AddBuildingRequirement( context, BuildingName.tower, 2 );
//                Requirement towerLevel4Requirement = AddBuildingRequirement( context, BuildingName.tower, 3 );
//                Requirement towerLevel5Requirement = AddBuildingRequirement( context, BuildingName.tower, 4 );
//                Requirement towerLevel6Requirement = AddBuildingRequirement( context, BuildingName.tower, 5 );
//                Requirement towerLevel7Requirement = AddBuildingRequirement( context, BuildingName.tower, 6 );
//                Requirement towerLevel8Requirement = AddBuildingRequirement( context, BuildingName.tower, 7 );
//                Requirement towerLevel9Requirement = AddBuildingRequirement( context, BuildingName.tower, 8 );
//                Requirement towerLevel10Requirement = AddBuildingRequirement( context, BuildingName.tower, 9 );

//                // Add Bonuses ( 2% duration bonus sur tous les buildings / level tower )
//                BonusBuildingOnBuilding buildingsDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 2 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 4 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 6 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 8 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 10 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 12 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 14 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 16 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 18 );
//                BonusBuildingOnBuilding buildingsDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, BuildingName.none, 20 );

//                // Set up Levels
//                BuildingLevel towerLevel1 = AddBuildingLevel( context, BuildingName.tower, 1, towerLevel1Cost, 0, null, CreateBonusBuildingList( buildingsDurationBonusLevel1)  );
//                BuildingLevel towerLevel2 = AddBuildingLevel( context, BuildingName.tower, 2, towerLevel2Cost, 60,
//                    CreateRequirementList( towerLevel2Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel2 ) );
//                BuildingLevel towerLevel3 = AddBuildingLevel( context, BuildingName.tower, 3, towerLevel3Cost, 350,
//                    CreateRequirementList( towerLevel3Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel3 ) );
//                BuildingLevel towerLevel4 = AddBuildingLevel( context, BuildingName.tower, 4, towerLevel3Cost, 900,
//                    CreateRequirementList( towerLevel4Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel4 ) );
//                BuildingLevel towerLevel5 = AddBuildingLevel( context, BuildingName.tower, 5, towerLevel5Cost, 1800,
//                    CreateRequirementList( towerLevel5Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel5 ) );
//                BuildingLevel towerLevel6 = AddBuildingLevel( context, BuildingName.tower, 6, towerLevel6Cost, 3800,
//                    CreateRequirementList( towerLevel6Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel6 ) );
//                BuildingLevel towerLevel7 = AddBuildingLevel( context, BuildingName.tower, 7, towerLevel7Cost, 8000,
//                    CreateRequirementList( towerLevel7Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel7 ) );
//                BuildingLevel towerLevel8 = AddBuildingLevel( context, BuildingName.tower, 8, towerLevel8Cost, 20000,
//                    CreateRequirementList( towerLevel8Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel8) );
//                BuildingLevel towerLevel9 = AddBuildingLevel( context, BuildingName.tower, 9, towerLevel9Cost, 50000,
//                    CreateRequirementList( towerLevel9Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel9 ) );
//                BuildingLevel towerLevel10 = AddBuildingLevel( context, BuildingName.tower, 10, towerLevel10Cost, 120000,
//                    CreateRequirementList( towerLevel10Requirement ),
//                    CreateBonusBuildingList( buildingsDurationBonusLevel10 ) );

//                context.SaveChanges();
//            }
//        }

//        public void SeedBarrackLevels()
//        {
//            using ( SetupContext context = new SetupContext() )
//            {
//                // Add BarrackCosts
//                Ressource barrackLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
//                Ressource barrackLevel2Cost = AddRessourceTimes2( context, barrackLevel1Cost );
//                Ressource barrackLevel3Cost = AddRessourceTimes2( context, barrackLevel2Cost );
//                Ressource barrackLevel4Cost = AddRessourceTimes2( context, barrackLevel3Cost );
//                Ressource barrackLevel5Cost = AddRessourceTimes2( context, barrackLevel4Cost );
//                Ressource barrackLevel6Cost = AddRessourceTimes2( context, barrackLevel5Cost );
//                Ressource barrackLevel7Cost = AddRessourceTimes2( context, barrackLevel6Cost );
//                Ressource barrackLevel8Cost = AddRessourceTimes2( context, barrackLevel7Cost );
//                Ressource barrackLevel9Cost = AddRessourceTimes2( context, barrackLevel8Cost );
//                Ressource barrackLevel10Cost = AddRessourceTimes2( context, barrackLevel9Cost );

//                // Add BarrackRequirements
//                Requirement barrackLevel1Requirement = AddBuildingRequirement( context, BuildingName.tower, 1 );
//                Requirement barrackLevel1Requirement2 = AddBuildingRequirement( context, BuildingName.barrack, 1 );
//                Requirement barrackLevel2Requirement = AddBuildingRequirement( context, BuildingName.barrack, 1 );
//                Requirement barrackLevel3Requirement = AddBuildingRequirement( context, BuildingName.barrack, 2 );
//                Requirement barrackLevel4Requirement = AddBuildingRequirement( context, BuildingName.barrack, 3 );
//                Requirement barrackLevel5Requirement = AddBuildingRequirement( context, BuildingName.barrack, 4 );
//                Requirement barrackLevel6Requirement = AddBuildingRequirement( context, BuildingName.barrack, 5 );
//                Requirement barrackLevel7Requirement = AddBuildingRequirement( context, BuildingName.barrack, 6 );
//                Requirement barrackLevel8Requirement = AddBuildingRequirement( context, BuildingName.barrack, 7 );
//                Requirement barrackLevel9Requirement = AddBuildingRequirement( context, BuildingName.barrack, 8 );
//                Requirement barrackLevel10Requirement = AddBuildingRequirement( context, BuildingName.barrack, 9 );

//                //Add Bonuses ( 1% duration bonus sur les unités de type monstre et soldats / barrack level )
//                BonusBuildingOnUnit monsterDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 1 );
//                BonusBuildingOnUnit monsterDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 2 );
//                BonusBuildingOnUnit monsterDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 3 );
//                BonusBuildingOnUnit monsterDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 4 );
//                BonusBuildingOnUnit monsterDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 5 );
//                BonusBuildingOnUnit monsterDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 6 );
//                BonusBuildingOnUnit monsterDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 7 );
//                BonusBuildingOnUnit monsterDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 8 );
//                BonusBuildingOnUnit monsterDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 9 );
//                BonusBuildingOnUnit monsterDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, UnitType.monster, 10 );

//                BonusBuildingOnUnit soldierDurationBonusLevel1 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 1 );
//                BonusBuildingOnUnit soldierDurationBonusLevel2 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 2 );
//                BonusBuildingOnUnit soldierDurationBonusLevel3 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 3 );
//                BonusBuildingOnUnit soldierDurationBonusLevel4 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 4 );
//                BonusBuildingOnUnit soldierDurationBonusLevel5 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 5 );
//                BonusBuildingOnUnit soldierDurationBonusLevel6 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 6 );
//                BonusBuildingOnUnit soldierDurationBonusLevel7 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 7 );
//                BonusBuildingOnUnit soldierDurationBonusLevel8 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 8 );
//                BonusBuildingOnUnit soldierDurationBonusLevel9 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 9 );
//                BonusBuildingOnUnit soldierDurationBonusLevel10 = AddBonusBuilding( context, BonusType.duration, UnitType.soldier, 10 );

//                // Set up Levels
//                BuildingLevel barrackLevel1 = AddBuildingLevel( context, BuildingName.barrack, 1, barrackLevel1Cost, 20, 
//                    CreateRequirementList( barrackLevel1Requirement, barrackLevel1Requirement2 ), 
//                    CreateBonusBuildingList( monsterDurationBonusLevel1, soldierDurationBonusLevel1 ) );
//                BuildingLevel barrackLevel2 = AddBuildingLevel( context, BuildingName.barrack, 2, barrackLevel2Cost, 100,
//                    CreateRequirementList( barrackLevel2Requirement),
//                    CreateBonusBuildingList( monsterDurationBonusLevel2, soldierDurationBonusLevel2 ) );
//                BuildingLevel barrackLevel3 = AddBuildingLevel( context, BuildingName.barrack, 3, barrackLevel3Cost, 500,
//                    CreateRequirementList( barrackLevel3Requirement),
//                    CreateBonusBuildingList( monsterDurationBonusLevel3, soldierDurationBonusLevel3 ) );
//                BuildingLevel barrackLevel4 = AddBuildingLevel( context, BuildingName.barrack, 4, barrackLevel3Cost, 1500,
//                    CreateRequirementList( barrackLevel4Requirement ),
//                    CreateBonusBuildingList( monsterDurationBonusLevel4, soldierDurationBonusLevel4 ) );
//                BuildingLevel barrackLevel5 = AddBuildingLevel( context, BuildingName.barrack, 5, barrackLevel5Cost, 3500,
//                    CreateRequirementList( barrackLevel5Requirement ),
//                    CreateBonusBuildingList( monsterDurationBonusLevel5, soldierDurationBonusLevel5 ) );
//                BuildingLevel barrackLevel6 = AddBuildingLevel( context, BuildingName.barrack, 6, barrackLevel6Cost, 7000,
//                    CreateRequirementList( barrackLevel6Requirement ),
//                    CreateBonusBuildingList( monsterDurationBonusLevel6, soldierDurationBonusLevel6 ) );
//                BuildingLevel barrackLevel7 = AddBuildingLevel( context, BuildingName.barrack, 7, barrackLevel7Cost, 15000,
//                    CreateRequirementList( barrackLevel7Requirement ),
//                    CreateBonusBuildingList( monsterDurationBonusLevel7, soldierDurationBonusLevel7 ) );
//                BuildingLevel barrackLevel8 = AddBuildingLevel( context, BuildingName.barrack, 8, barrackLevel8Cost, 35000,
//                    CreateRequirementList( barrackLevel8Requirement ),
//                    CreateBonusBuildingList( monsterDurationBonusLevel8, soldierDurationBonusLevel8 ) );
//                BuildingLevel barrackLevel9 = AddBuildingLevel( context, BuildingName.barrack, 9, barrackLevel9Cost, 60000,
//                    CreateRequirementList( barrackLevel9Requirement ),
//                    CreateBonusBuildingList( monsterDurationBonusLevel9, soldierDurationBonusLevel9 ) );
//                BuildingLevel barrackLevel10 = AddBuildingLevel( context, BuildingName.barrack, 10, barrackLevel10Cost, 120000,
//                    CreateRequirementList( barrackLevel10Requirement ),
//                    CreateBonusBuildingList( monsterDurationBonusLevel10, soldierDurationBonusLevel10 ) );

//                context.SaveChanges();
//            }
//        }

//        public void SeedWoodLevels()
//        {
//            using ( SetupContext context = new SetupContext() )
//            {
//                // Add woodFieldCosts
//                Ressource woodFieldLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
//                Ressource woodFieldLevel2Cost = AddRessourceTimes2( context, woodFieldLevel1Cost );
//                Ressource woodFieldLevel3Cost = AddRessourceTimes2( context, woodFieldLevel2Cost );
//                Ressource woodFieldLevel4Cost = AddRessourceTimes2( context, woodFieldLevel3Cost );
//                Ressource woodFieldLevel5Cost = AddRessourceTimes2( context, woodFieldLevel4Cost );
//                Ressource woodFieldLevel6Cost = AddRessourceTimes2( context, woodFieldLevel5Cost );
//                Ressource woodFieldLevel7Cost = AddRessourceTimes2( context, woodFieldLevel6Cost );
//                Ressource woodFieldLevel8Cost = AddRessourceTimes2( context, woodFieldLevel7Cost );
//                Ressource woodFieldLevel9Cost = AddRessourceTimes2( context, woodFieldLevel8Cost );
//                Ressource woodFieldLevel10Cost = AddRessourceTimes2( context, woodFieldLevel9Cost );

//                // Add woodFieldRequirements
//                Requirement woodFieldLevel2Requirement = AddBuildingRequirement( context, BuildingName.woodField, 1 );
//                Requirement woodFieldLevel3Requirement = AddBuildingRequirement( context, BuildingName.woodField, 2 );
//                Requirement woodFieldLevel4Requirement = AddBuildingRequirement( context, BuildingName.woodField, 3 );
//                Requirement woodFieldLevel5Requirement = AddBuildingRequirement( context, BuildingName.woodField, 4 );
//                Requirement woodFieldLevel6Requirement = AddBuildingRequirement( context, BuildingName.woodField, 5 );
//                Requirement woodFieldLevel7Requirement = AddBuildingRequirement( context, BuildingName.woodField, 6 );
//                Requirement woodFieldLevel8Requirement = AddBuildingRequirement( context, BuildingName.woodField, 7 );
//                Requirement woodFieldLevel9Requirement = AddBuildingRequirement( context, BuildingName.woodField, 8 );
//                Requirement woodFieldLevel10Requirement = AddBuildingRequirement( context, BuildingName.woodField, 9 );

//                // Set up Levels
//                FieldLevel woodFieldLevel1 = AddFieldLevel( context, BuildingName.woodField, 1, woodFieldLevel1Cost, null, 100 );
//                FieldLevel woodFieldLevel2 = AddFieldLevel( context, BuildingName.woodField, 2, woodFieldLevel2Cost, CreateRequirementList( woodFieldLevel2Requirement ), 200 );
//                FieldLevel woodFieldLevel3 = AddFieldLevel( context, BuildingName.woodField, 3, woodFieldLevel3Cost, CreateRequirementList( woodFieldLevel3Requirement ), 400 );
//                FieldLevel woodFieldLevel4 = AddFieldLevel( context, BuildingName.woodField, 4, woodFieldLevel3Cost, CreateRequirementList( woodFieldLevel4Requirement ), 600 );
//                FieldLevel woodFieldLevel5 = AddFieldLevel( context, BuildingName.woodField, 5, woodFieldLevel5Cost, CreateRequirementList( woodFieldLevel5Requirement ), 1000 );
//                FieldLevel woodFieldLevel6 = AddFieldLevel( context, BuildingName.woodField, 6, woodFieldLevel6Cost, CreateRequirementList( woodFieldLevel6Requirement ), 1500 );
//                FieldLevel woodFieldLevel7 = AddFieldLevel( context, BuildingName.woodField, 7, woodFieldLevel7Cost, CreateRequirementList( woodFieldLevel7Requirement ), 2000 );
//                FieldLevel woodFieldLevel8 = AddFieldLevel( context, BuildingName.woodField, 8, woodFieldLevel8Cost, CreateRequirementList( woodFieldLevel8Requirement ), 3000);
//                FieldLevel woodFieldLevel9 = AddFieldLevel( context, BuildingName.woodField, 9, woodFieldLevel9Cost, CreateRequirementList( woodFieldLevel9Requirement ), 5000 );
//                FieldLevel woodFieldLevel10 = AddFieldLevel( context, BuildingName.woodField, 10, woodFieldLevel10Cost, CreateRequirementList( woodFieldLevel10Requirement ), 10000 );

//                context.SaveChanges();
//            }
//        }

//        public void SeedCristalLevels()
//        {
//            using ( SetupContext context = new SetupContext() )
//            {
//                // Add cristalFieldCosts
//                Ressource cristalFieldLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
//                Ressource cristalFieldLevel2Cost = AddRessourceTimes2( context, cristalFieldLevel1Cost );
//                Ressource cristalFieldLevel3Cost = AddRessourceTimes2( context, cristalFieldLevel2Cost );
//                Ressource cristalFieldLevel4Cost = AddRessourceTimes2( context, cristalFieldLevel3Cost );
//                Ressource cristalFieldLevel5Cost = AddRessourceTimes2( context, cristalFieldLevel4Cost );
//                Ressource cristalFieldLevel6Cost = AddRessourceTimes2( context, cristalFieldLevel5Cost );
//                Ressource cristalFieldLevel7Cost = AddRessourceTimes2( context, cristalFieldLevel6Cost );
//                Ressource cristalFieldLevel8Cost = AddRessourceTimes2( context, cristalFieldLevel7Cost );
//                Ressource cristalFieldLevel9Cost = AddRessourceTimes2( context, cristalFieldLevel8Cost );
//                Ressource cristalFieldLevel10Cost = AddRessourceTimes2( context, cristalFieldLevel9Cost );

//                // Add cristalFieldRequirements
//                Requirement cristalFieldLevel2Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 1 );
//                Requirement cristalFieldLevel3Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 2 );
//                Requirement cristalFieldLevel4Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 3 );
//                Requirement cristalFieldLevel5Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 4 );
//                Requirement cristalFieldLevel6Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 5 );
//                Requirement cristalFieldLevel7Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 6 );
//                Requirement cristalFieldLevel8Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 7 );
//                Requirement cristalFieldLevel9Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 8 );
//                Requirement cristalFieldLevel10Requirement = AddBuildingRequirement( context, BuildingName.cristalField, 9 );

//                // Set up Levels
//                FieldLevel cristalFieldLevel1 = AddFieldLevel( context, BuildingName.cristalField, 1, cristalFieldLevel1Cost, null, 100 );
//                FieldLevel cristalFieldLevel2 = AddFieldLevel( context, BuildingName.cristalField, 2, cristalFieldLevel2Cost, CreateRequirementList( cristalFieldLevel2Requirement ), 200 );
//                FieldLevel cristalFieldLevel3 = AddFieldLevel( context, BuildingName.cristalField, 3, cristalFieldLevel3Cost, CreateRequirementList( cristalFieldLevel3Requirement ), 400 );
//                FieldLevel cristalFieldLevel4 = AddFieldLevel( context, BuildingName.cristalField, 4, cristalFieldLevel3Cost, CreateRequirementList( cristalFieldLevel4Requirement ), 600 );
//                FieldLevel cristalFieldLevel5 = AddFieldLevel( context, BuildingName.cristalField, 5, cristalFieldLevel5Cost, CreateRequirementList( cristalFieldLevel5Requirement ), 1000 );
//                FieldLevel cristalFieldLevel6 = AddFieldLevel( context, BuildingName.cristalField, 6, cristalFieldLevel6Cost, CreateRequirementList( cristalFieldLevel6Requirement ), 1500 );
//                FieldLevel cristalFieldLevel7 = AddFieldLevel( context, BuildingName.cristalField, 7, cristalFieldLevel7Cost, CreateRequirementList( cristalFieldLevel7Requirement ), 2000 );
//                FieldLevel cristalFieldLevel8 = AddFieldLevel( context, BuildingName.cristalField, 8, cristalFieldLevel8Cost, CreateRequirementList( cristalFieldLevel8Requirement ), 3000 );
//                FieldLevel cristalFieldLevel9 = AddFieldLevel( context, BuildingName.cristalField, 9, cristalFieldLevel9Cost, CreateRequirementList( cristalFieldLevel9Requirement ), 5000 );
//                FieldLevel cristalFieldLevel10 = AddFieldLevel( context, BuildingName.cristalField, 10, cristalFieldLevel10Cost, CreateRequirementList( cristalFieldLevel10Requirement ), 10000 );

//                context.SaveChanges();
//            }
//        }
//        public void SeedMetalLevels()
//        {
//            using ( SetupContext context = new SetupContext() )
//            {
//                // Add metalFieldCosts
//                Ressource metalFieldLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
//                Ressource metalFieldLevel2Cost = AddRessourceTimes2( context, metalFieldLevel1Cost );
//                Ressource metalFieldLevel3Cost = AddRessourceTimes2( context, metalFieldLevel2Cost );
//                Ressource metalFieldLevel4Cost = AddRessourceTimes2( context, metalFieldLevel3Cost );
//                Ressource metalFieldLevel5Cost = AddRessourceTimes2( context, metalFieldLevel4Cost );
//                Ressource metalFieldLevel6Cost = AddRessourceTimes2( context, metalFieldLevel5Cost );
//                Ressource metalFieldLevel7Cost = AddRessourceTimes2( context, metalFieldLevel6Cost );
//                Ressource metalFieldLevel8Cost = AddRessourceTimes2( context, metalFieldLevel7Cost );
//                Ressource metalFieldLevel9Cost = AddRessourceTimes2( context, metalFieldLevel8Cost );
//                Ressource metalFieldLevel10Cost = AddRessourceTimes2( context, metalFieldLevel9Cost );

//                // Add metalFieldRequirements
//                Requirement metalFieldLevel2Requirement = AddBuildingRequirement( context, BuildingName.metalField, 1 );
//                Requirement metalFieldLevel3Requirement = AddBuildingRequirement( context, BuildingName.metalField, 2 );
//                Requirement metalFieldLevel4Requirement = AddBuildingRequirement( context, BuildingName.metalField, 3 );
//                Requirement metalFieldLevel5Requirement = AddBuildingRequirement( context, BuildingName.metalField, 4 );
//                Requirement metalFieldLevel6Requirement = AddBuildingRequirement( context, BuildingName.metalField, 5 );
//                Requirement metalFieldLevel7Requirement = AddBuildingRequirement( context, BuildingName.metalField, 6 );
//                Requirement metalFieldLevel8Requirement = AddBuildingRequirement( context, BuildingName.metalField, 7 );
//                Requirement metalFieldLevel9Requirement = AddBuildingRequirement( context, BuildingName.metalField, 8 );
//                Requirement metalFieldLevel10Requirement = AddBuildingRequirement( context, BuildingName.metalField, 9 );

//                // Set up Levels
//                FieldLevel metalFieldLevel1 = AddFieldLevel( context, BuildingName.metalField, 1, metalFieldLevel1Cost, null, 100 );
//                FieldLevel metalFieldLevel2 = AddFieldLevel( context, BuildingName.metalField, 2, metalFieldLevel2Cost, CreateRequirementList( metalFieldLevel2Requirement ), 200 );
//                FieldLevel metalFieldLevel3 = AddFieldLevel( context, BuildingName.metalField, 3, metalFieldLevel3Cost, CreateRequirementList( metalFieldLevel3Requirement ), 400 );
//                FieldLevel metalFieldLevel4 = AddFieldLevel( context, BuildingName.metalField, 4, metalFieldLevel3Cost, CreateRequirementList( metalFieldLevel4Requirement ), 600 );
//                FieldLevel metalFieldLevel5 = AddFieldLevel( context, BuildingName.metalField, 5, metalFieldLevel5Cost, CreateRequirementList( metalFieldLevel5Requirement ), 1000 );
//                FieldLevel metalFieldLevel6 = AddFieldLevel( context, BuildingName.metalField, 6, metalFieldLevel6Cost, CreateRequirementList( metalFieldLevel6Requirement ), 1500 );
//                FieldLevel metalFieldLevel7 = AddFieldLevel( context, BuildingName.metalField, 7, metalFieldLevel7Cost, CreateRequirementList( metalFieldLevel7Requirement ), 2000 );
//                FieldLevel metalFieldLevel8 = AddFieldLevel( context, BuildingName.metalField, 8, metalFieldLevel8Cost, CreateRequirementList( metalFieldLevel8Requirement ), 3000 );
//                FieldLevel metalFieldLevel9 = AddFieldLevel( context, BuildingName.metalField, 9, metalFieldLevel9Cost, CreateRequirementList( metalFieldLevel9Requirement ), 5000 );
//                FieldLevel metalFieldLevel10 = AddFieldLevel( context, BuildingName.metalField, 10, metalFieldLevel10Cost, CreateRequirementList( metalFieldLevel10Requirement ), 10000 );

//                context.SaveChanges();
//            }
//        }

//        public void SeedMagicLevels()
//        {
//            using ( SetupContext context = new SetupContext() )
//            {
//                // Add magicFieldCosts
//                Ressource magicFieldLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
//                Ressource magicFieldLevel2Cost = AddRessourceTimes2( context, magicFieldLevel1Cost );
//                Ressource magicFieldLevel3Cost = AddRessourceTimes2( context, magicFieldLevel2Cost );
//                Ressource magicFieldLevel4Cost = AddRessourceTimes2( context, magicFieldLevel3Cost );
//                Ressource magicFieldLevel5Cost = AddRessourceTimes2( context, magicFieldLevel4Cost );
//                Ressource magicFieldLevel6Cost = AddRessourceTimes2( context, magicFieldLevel5Cost );
//                Ressource magicFieldLevel7Cost = AddRessourceTimes2( context, magicFieldLevel6Cost );
//                Ressource magicFieldLevel8Cost = AddRessourceTimes2( context, magicFieldLevel7Cost );
//                Ressource magicFieldLevel9Cost = AddRessourceTimes2( context, magicFieldLevel8Cost );
//                Ressource magicFieldLevel10Cost = AddRessourceTimes2( context, magicFieldLevel9Cost );

//                // Add magicFieldRequirements
//                Requirement magicFieldLevel2Requirement = AddBuildingRequirement( context, BuildingName.magicField, 1 );
//                Requirement magicFieldLevel3Requirement = AddBuildingRequirement( context, BuildingName.magicField, 2 );
//                Requirement magicFieldLevel4Requirement = AddBuildingRequirement( context, BuildingName.magicField, 3 );
//                Requirement magicFieldLevel5Requirement = AddBuildingRequirement( context, BuildingName.magicField, 4 );
//                Requirement magicFieldLevel6Requirement = AddBuildingRequirement( context, BuildingName.magicField, 5 );
//                Requirement magicFieldLevel7Requirement = AddBuildingRequirement( context, BuildingName.magicField, 6 );
//                Requirement magicFieldLevel8Requirement = AddBuildingRequirement( context, BuildingName.magicField, 7 );
//                Requirement magicFieldLevel9Requirement = AddBuildingRequirement( context, BuildingName.magicField, 8 );
//                Requirement magicFieldLevel10Requirement = AddBuildingRequirement( context, BuildingName.magicField, 9 );

//                // Set up Levels
//                FieldLevel magicFieldLevel1 = AddFieldLevel( context, BuildingName.magicField, 1, magicFieldLevel1Cost, null, 100 );
//                FieldLevel magicFieldLevel2 = AddFieldLevel( context, BuildingName.magicField, 2, magicFieldLevel2Cost, CreateRequirementList( magicFieldLevel2Requirement ), 200 );
//                FieldLevel magicFieldLevel3 = AddFieldLevel( context, BuildingName.magicField, 3, magicFieldLevel3Cost, CreateRequirementList( magicFieldLevel3Requirement ), 400 );
//                FieldLevel magicFieldLevel4 = AddFieldLevel( context, BuildingName.magicField, 4, magicFieldLevel3Cost, CreateRequirementList( magicFieldLevel4Requirement ), 600 );
//                FieldLevel magicFieldLevel5 = AddFieldLevel( context, BuildingName.magicField, 5, magicFieldLevel5Cost, CreateRequirementList( magicFieldLevel5Requirement ), 1000 );
//                FieldLevel magicFieldLevel6 = AddFieldLevel( context, BuildingName.magicField, 6, magicFieldLevel6Cost, CreateRequirementList( magicFieldLevel6Requirement ), 1500 );
//                FieldLevel magicFieldLevel7 = AddFieldLevel( context, BuildingName.magicField, 7, magicFieldLevel7Cost, CreateRequirementList( magicFieldLevel7Requirement ), 2000 );
//                FieldLevel magicFieldLevel8 = AddFieldLevel( context, BuildingName.magicField, 8, magicFieldLevel8Cost, CreateRequirementList( magicFieldLevel8Requirement ), 3000 );
//                FieldLevel magicFieldLevel9 = AddFieldLevel( context, BuildingName.magicField, 9, magicFieldLevel9Cost, CreateRequirementList( magicFieldLevel9Requirement ), 5000 );
//                FieldLevel magicFieldLevel10 = AddFieldLevel( context, BuildingName.magicField, 10, magicFieldLevel10Cost, CreateRequirementList( magicFieldLevel10Requirement ), 10000 );

//                context.SaveChanges();
//            }
//        }
//        public void SeedInvocationLevels()
//        {
//            using ( SetupContext context = new SetupContext() )
//            {
//                // Add invocationCosts
//                Ressource invocationLevel1Cost = AddRessource( context, 100, 50, 0, 0 );
//                Ressource invocationLevel2Cost = AddRessourceTimes2( context, invocationLevel1Cost );
//                Ressource invocationLevel3Cost = AddRessourceTimes2( context, invocationLevel2Cost );
//                Ressource invocationLevel4Cost = AddRessourceTimes2( context, invocationLevel3Cost );
//                Ressource invocationLevel5Cost = AddRessourceTimes2( context, invocationLevel4Cost );
//                Ressource invocationLevel6Cost = AddRessourceTimes2( context, invocationLevel5Cost );
//                Ressource invocationLevel7Cost = AddRessourceTimes2( context, invocationLevel6Cost );
//                Ressource invocationLevel8Cost = AddRessourceTimes2( context, invocationLevel7Cost );
//                Ressource invocationLevel9Cost = AddRessourceTimes2( context, invocationLevel8Cost );
//                Ressource invocationLevel10Cost = AddRessourceTimes2( context, invocationLevel9Cost );

//                // Add invocationRequirements
//                Requirement invocationLevel2Requirement = AddBuildingRequirement( context, BuildingName.invocation, 1 );
//                Requirement invocationLevel3Requirement = AddBuildingRequirement( context, BuildingName.invocation, 2 );
//                Requirement invocationLevel4Requirement = AddBuildingRequirement( context, BuildingName.invocation, 3 );
//                Requirement invocationLevel5Requirement = AddBuildingRequirement( context, BuildingName.invocation, 4 );
//                Requirement invocationLevel6Requirement = AddBuildingRequirement( context, BuildingName.invocation, 5 );
//                Requirement invocationLevel7Requirement = AddBuildingRequirement( context, BuildingName.invocation, 6 );
//                Requirement invocationLevel8Requirement = AddBuildingRequirement( context, BuildingName.invocation, 7 );
//                Requirement invocationLevel9Requirement = AddBuildingRequirement( context, BuildingName.invocation, 8 );
//                Requirement invocationLevel10Requirement = AddBuildingRequirement( context, BuildingName.invocation, 9 );

//                // Set up Levels
//                BuildingLevel invocationLevel1 = AddBuildingLevel( context, BuildingName.invocation, 1, invocationLevel1Cost, null );
//                BuildingLevel invocationLevel2 = AddBuildingLevel( context, BuildingName.invocation, 2, invocationLevel2Cost, CreateRequirementList( invocationLevel2Requirement ) );
//                BuildingLevel invocationLevel3 = AddBuildingLevel( context, BuildingName.invocation, 3, invocationLevel3Cost, CreateRequirementList( invocationLevel3Requirement ) );
//                BuildingLevel invocationLevel4 = AddBuildingLevel( context, BuildingName.invocation, 4, invocationLevel3Cost, CreateRequirementList( invocationLevel4Requirement ) );
//                BuildingLevel invocationLevel5 = AddBuildingLevel( context, BuildingName.invocation, 5, invocationLevel5Cost, CreateRequirementList( invocationLevel5Requirement ) );
//                BuildingLevel invocationLevel6 = AddBuildingLevel( context, BuildingName.invocation, 6, invocationLevel6Cost, CreateRequirementList( invocationLevel6Requirement ) );
//                BuildingLevel invocationLevel7 = AddBuildingLevel( context, BuildingName.invocation, 7, invocationLevel7Cost, CreateRequirementList( invocationLevel7Requirement ) );
//                BuildingLevel invocationLevel8 = AddBuildingLevel( context, BuildingName.invocation, 8, invocationLevel8Cost, CreateRequirementList( invocationLevel8Requirement ) );
//                BuildingLevel invocationLevel9 = AddBuildingLevel( context, BuildingName.invocation, 9, invocationLevel9Cost, CreateRequirementList( invocationLevel9Requirement ) );
//                BuildingLevel invocationLevel10 = AddBuildingLevel( context, BuildingName.invocation, 10, invocationLevel10Cost, CreateRequirementList( invocationLevel10Requirement ) );

//                context.SaveChanges();
//            }
//        }

//        BonusBuildingOnUnit AddBonusBuilding( SetupContext context, BonusType bonusType, UnitType unitTarget, int modifier )
//        {
//            BonusBuildingOnUnit bonus = new BonusBuildingOnUnit
//            {
//                BonusType = bonusType,
//                Modifier = modifier,
//                TargetUnit = unitTarget
//            };
//            context.Add( bonus );

//            return bonus;
//        }

//        BonusBuildingOnTechnology AddBonusBuilding( SetupContext context, BonusType bonusType, TechnologyName technologyTarget, int modifier )
//        {
//            BonusBuildingOnTechnology bonus = new BonusBuildingOnTechnology
//            {
//                BonusType = bonusType,
//                Modifier = modifier,
//                TargetTechnology = technologyTarget
//            };
//            context.Add( bonus );

//            return bonus;
//        }

//        BonusBuildingOnBuilding AddBonusBuilding( SetupContext context, BonusType bonusType, BuildingName buildingsTarget, int modifier )
//        {
//            BonusBuildingOnBuilding bonus = new BonusBuildingOnBuilding
//            {
//                BonusType = bonusType,
//                Modifier = modifier,
//                TargetBuilding = buildingsTarget
//            };
//            context.Add( bonus );

//            return bonus;
//        }

//        BonusTechnologyOnUnit AddBonusTechnology( SetupContext context, BonusType bonusType, UnitType unitTarget, int modifier )
//        {
//            BonusTechnologyOnUnit bonus = new BonusTechnologyOnUnit
//            {
//                BonusType = bonusType,
//                Modifier = modifier,
//                TargetUnit = unitTarget
//            };
//            context.Add( bonus );

//            return bonus;
//        }

//        BonusTechnologyOnBuilding AddBonusTechnology( SetupContext context, BonusType bonusType, BuildingName buildingsTarget, int modifier )
//        {
//            BonusTechnologyOnBuilding bonus = new BonusTechnologyOnBuilding
//            {
//                BonusType = bonusType,
//                Modifier = modifier,
//                TargetBuilding = buildingsTarget
//            };
//            context.Add( bonus );

//            return bonus;
//        }

//        BonusTechnologyOnTechnology AddBonusTechnology( SetupContext context, BonusType bonusType, TechnologyName technologyTarget, int modifier )
//        {
//            BonusTechnologyOnTechnology bonus = new BonusTechnologyOnTechnology
//            {
//                BonusType = bonusType,
//                Modifier = modifier,
//                TargetTechnology = technologyTarget
//            };
//            context.Add( bonus );

//            return bonus;
//        }

//        //BuildingLevel AddBuildingLevel( SetupContext context, BuildingName buildingName, int number, int duration, Ressource cost, List<Requirement> requirements )
//        //{
//        //    BuildingLevel buildingLevel = new BuildingLevel
//        //    {
//        //        Number = number,
//        //        BuildingName = buildingName,
//        //        Cost = cost,
//        //        Requirements = requirements,
//        //        Duration = duration
//        //    };
//        //    context.Add( buildingLevel );
//        //    return buildingLevel;
//        //}
//        BuildingLevel AddBuildingLevel( SetupContext context, BuildingName buildingName, int number, Ressource cost, int duration, List<Requirement> requirements, List<BonusBuilding> bonuses )
//        {
//            BuildingLevel buildingLevel = new BuildingLevel
//            {
//                Number = number,
//                BuildingName = buildingName,
//                Cost = cost,
//                Requirements = requirements,
//                BuildingBonuses = bonuses, 
//                Duration = duration
//            };
//            context.Add( buildingLevel );
//            return buildingLevel;
//        }

//        FieldLevel AddFieldLevel( SetupContext context, BuildingName buildingName, int number, Ressource cost, List<Requirement> requirements, int production )
//        {
//            FieldLevel fieldLevel = new FieldLevel
//            {
//                Number = number,
//                BuildingName = buildingName,
//                Cost = cost,
//                Requirements = requirements,
//                Production = production
//            };
//            context.Add( fieldLevel );
//            return fieldLevel;

//        }
//        Requirement AddBuildingRequirement( SetupContext context, BuildingName buildingName, int number )
//        {
//            Requirement requirement = new Requirement
//            {
//                BuildingName = buildingName,
//                Number = number
//            };
//            context.Add( requirement );
//            return requirement;
//        }

//        List<Requirement> CreateRequirementList( params Requirement[] args )
//        {
//            List<Requirement> requirements = new List<Requirement>();
//            foreach( Requirement r in args )
//            {
//                requirements.Add( r );
//            }
//            return requirements;
//        }

//        List<BonusBuilding> CreateBonusBuildingList( params BonusBuilding[ ] args )
//        {
//            List<BonusBuilding> bonuses = new List<BonusBuilding>();
//            foreach ( BonusBuilding b in args )
//            {
//                bonuses.Add( b );
//            }
//            return bonuses;
//        }

//        List<BonusTechnology> CreateBonusTechnologyList( params BonusTechnology[ ] args )
//        {
//            List<BonusTechnology> bonuses = new List<BonusTechnology>();
//            foreach ( BonusTechnology b in args )
//            {
//                bonuses.Add( b );
//            }
//            return bonuses;
//        }

//        Ressource AddRessource( SetupContext context, int wood, int metal, int cristal, int magic )
//        {
//            Ressource ressource = new Ressource {
//                Wood = wood,
//                Metal = wood,
//                Cristal = cristal,
//                Magic = magic
//            };
//            context.Add( ressource );
//            return ressource;
//        }

//        Ressource AddRessourceTimes2( SetupContext context, Ressource initialRessource )
//        {
//            Ressource multipliedRessoure = Multiplyressource( initialRessource, 2 );
//            context.Add( multipliedRessoure );
//            return multipliedRessoure;
//        }

//        Ressource Multiplyressource( Ressource initialRessource, int factor )
//        {
//            return new Ressource
//            {
//                Wood = initialRessource.Wood * factor,
//                Metal = initialRessource.Metal * factor,
//                Cristal = initialRessource.Cristal * factor,
//                Magic = initialRessource.Magic * factor
//            };
//        }
//    }
//}
