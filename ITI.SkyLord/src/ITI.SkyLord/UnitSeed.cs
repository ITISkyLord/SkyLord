using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class UnitSeed
    {
        public void SeedUnits()
        {
            SeedSoldiers();
            SeedMonsters();
            SeedMagical();
            SeedMecanical();
            SeedUtility();
        }

        public void SeedSoldiers()
        {
            SeedArcher();
            SeedChampion();
            SeedKnight();
            SeedSoldier();
            SeedGriffin();
        }

        public void SeedMonsters()
        {
            SeedDirtGolem();
            SeedGobelin();
            SeedHarpy();
            SeedJuggernaut();
            SeedPhenix();
            SeedTroll();
        }

        public void SeedMagical()
        {
            SeedNovice();
            SeedProphet();
            SeedSummoner();
            SeedWisp();
            SeedNecromancer();
        }

        public void SeedMecanical()
        {
            //SeedCanoe();
            SeedFlameThrower();
            SeedIronGolem();
            SeedLivingArmor();
            SeedPrismaticGolem();
            SeedSaboteur();
            SeedScorpion();
        }
        public void SeedUtility()
        {
            SeedApprentice();
            SeedCarrier();
        }

        #region Soldiers

        public void SeedSoldier()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource soliderCost = new Ressource { Wood = 100, Metal = 50, Cristal = 0, Magic = 0 };
                context.Add( soliderCost );

                // Set up Statistics
                UnitStatistics soliderStatistics = new UnitStatistics
                {
                    Attack = 40,
                    PhysicResist = 40,
                    MagicResist = 20,
                    Speed = 100,
                    Capacity = 100,
                    Consumption = 5
                };
                context.Add( soliderStatistics );

                // Set up Requirements
                Requirement soldierRequirement = AddBuildingRequirement( context, BuildingName.barrack, 1 );
                Requirement soldierRequirement2 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit soldier = new Unit
                {
                    Name = "Soldat",
                    UnitName = UnitName.soldier,
                    UnitType = UnitType.soldier,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = soliderCost,
                    UnitStatistics = soliderStatistics,
                    Requirements = CreateRequirementList( soldierRequirement, soldierRequirement2 ),
                    Duration = 60,
                    IsModel = true,
                    Description = "Habillé de son armure toute neuve, il sait par quel bout prendre son épée et obéir aux ordres. Le soldat n'est pas très efficace mais il est peu cher à recruter et à entretenir."
                };
                context.Add( soldier );

                context.SaveChanges();
            }
        }

        public void SeedArcher()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource archerCost = new Ressource { Wood = 250, Metal = 150, Cristal = 0, Magic = 0 };
                context.Add( archerCost );

                // Set up Statistics
                UnitStatistics archerStatistics = new UnitStatistics
                {
                    Attack = 25,
                    PhysicResist = 125,
                    MagicResist = 100,
                    Speed = 75,
                    Capacity = 75,
                    Consumption = 10
                };
                context.Add( archerStatistics );

                // Set up Requirements
                Requirement archerRequirement = AddBuildingRequirement( context, BuildingName.barrack, 3 );
                Requirement archerRequirement2 = AddTechnologyRequirement( context, TechnologyName.weapons, 1 );
                Requirement archerRequirement3 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit archer = new Unit
                {
                    Name = "Arbaletrier",
                    UnitName = UnitName.archer,
                    UnitType = UnitType.soldier,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = archerCost,
                    UnitStatistics = archerStatistics,
                    Requirements = CreateRequirementList( archerRequirement, archerRequirement2, archerRequirement3 ),
                    Duration = 120,
                    IsModel = true,
                    Description = "Un soldat à qui on a confié une arbalète chargée et une armure plus légère. Efficace pour tenir à l'abri d'un rempart."
                };
                context.Add( archer );

                context.SaveChanges();
            }
        }

        public void SeedKnight()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource knightCost = new Ressource { Wood = 1000, Metal = 850, Cristal = 250, Magic = 0 };
                context.Add( knightCost );

                // Set up Statistics
                UnitStatistics knightStatistics = new UnitStatistics
                {
                    Attack = 250,
                    PhysicResist = 200,
                    MagicResist = 150,
                    Speed = 175,
                    Capacity = 100,
                    Consumption = 15
                };
                context.Add( knightStatistics );

                // Set up Requirements
                Requirement knightRequirement = AddBuildingRequirement( context, BuildingName.barrack, 5 );
                Requirement knightRequirement2 = AddTechnologyRequirement( context, TechnologyName.armor, 2 );
                Requirement knightRequirement3 = AddTechnologyRequirement( context, TechnologyName.magicBoots, 2 );
                Requirement knightRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit knight = new Unit
                {
                    Name = "Chevalier",
                    UnitName = UnitName.knight,
                    UnitType = UnitType.soldier,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = knightCost,
                    UnitStatistics = knightStatistics,
                    Requirements = CreateRequirementList( knightRequirement, knightRequirement2, knightRequirement3, knightRequirement4 ),
                    Duration = 450,
                    IsModel = true,
                    Description = "Sur son cheval et dans son armure étincelante, le chevalier fait fondre le cœur des dames à la maison et charge impitoyablement l'ennemi sur le champs de bataille. Unité rapide faite pour les assauts percutants."
                };
                context.Add( knight );

                context.SaveChanges();
            }
        }

        public void SeedGriffin()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource griffinCost = new Ressource { Wood = 2500, Metal = 2000, Cristal = 1250, Magic = 250 };
                context.Add( griffinCost );

                // Set up Statistics
                UnitStatistics griffinStatistics = new UnitStatistics
                {
                    Attack = 425,
                    PhysicResist = 300,
                    MagicResist = 200,
                    Speed = 150,
                    Capacity = 200,
                    Consumption = 30
                };
                context.Add( griffinStatistics );

                // Set up Requirements
                Requirement griffinRequirement = AddBuildingRequirement( context, BuildingName.barrack, 7 );
                Requirement griffinRequirement2 = AddTechnologyRequirement( context, TechnologyName.magicBoots, 5 );
                Requirement griffinRequirement3 = AddTechnologyRequirement( context, TechnologyName.weapons, 4 );
                Requirement griffinRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit griffin = new Unit
                {
                    Name = "Chevaucheur de Griffon",
                    UnitName = UnitName.griffin,
                    UnitType = UnitType.soldier,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = griffinCost,
                    UnitStatistics = griffinStatistics,
                    Requirements = CreateRequirementList( griffinRequirement, griffinRequirement2, griffinRequirement3, griffinRequirement4 ),
                    Duration = 160,
                    IsModel = true,
                    Description = "Seul les cavaliers les plus habiles - qui n'ont pas peur des coups de griffes - peuvent se targuer de monter un griffon au combat. Cela vaut bien quelques plaies car leur efficacité est phénoménale, surtout en attaque."
                };
                context.Add( griffin );

                context.SaveChanges();
            }
        }

        public void SeedChampion()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource championCost = new Ressource { Wood = 7500, Metal = 7500, Cristal = 5000, Magic = 750 };
                context.Add( championCost );

                // Set up Statistics
                UnitStatistics championStatistics = new UnitStatistics
                {
                    Attack = 750,
                    PhysicResist = 750,
                    MagicResist = 500,
                    Speed = 100,
                    Capacity = 100,
                    Consumption = 50
                };
                context.Add( championStatistics );

                // Set up Requirements
                Requirement championRequirement = AddBuildingRequirement( context, BuildingName.barrack, 9 );
                Requirement championRequirement2 = AddTechnologyRequirement( context, TechnologyName.magicBoots, 7 );
                Requirement championRequirement3 = AddTechnologyRequirement( context, TechnologyName.weapons, 7 );
                Requirement championRequirement44 = AddTechnologyRequirement( context, TechnologyName.armor, 8 );
                Requirement championRequirement5 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit champion = new Unit
                {
                    Name = "Champion",
                    UnitName = UnitName.champion,
                    UnitType = UnitType.soldier,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = championCost,
                    UnitStatistics = championStatistics,
                    Requirements = CreateRequirementList( championRequirement, championRequirement2, championRequirement3, championRequirement44, championRequirement5 ),
                    Duration = 1500,
                    IsModel = true,
                    Description = "Le meilleur entraînement, la plus grande expérience, les armes les plus rares, les champions sont les maîtres de guerre qui dirigent tous les soldats à la bataille. Il s'agit d'une unité puissante et équilibrée, le cœur de toute armée qui se respecte."
                };
                context.Add( champion );

                context.SaveChanges();
            }
        }

        #endregion

        #region Monsters

        public void SeedGobelin()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource gobelinCost = new Ressource { Wood = 75, Metal = 50, Cristal = 0, Magic = 0 };
                context.Add( gobelinCost );

                // Set up Statistics
                UnitStatistics gobelinStatistics = new UnitStatistics
                {
                    Attack = 30,
                    PhysicResist = 25,
                    MagicResist = 35,
                    Speed = 125,
                    Capacity = 75,
                    Consumption = 4
                };
                context.Add( gobelinStatistics );

                // Set up Requirements
                Requirement gobelinRequirement = AddBuildingRequirement( context, BuildingName.barrack, 2 );
                Requirement gobelinRequirement2 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit gobelin = new Unit
                {
                    Name = "Gobelin",
                    UnitName = UnitName.gobelin,
                    UnitType = UnitType.monster,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = gobelinCost,
                    UnitStatistics = gobelinStatistics,
                    Requirements = CreateRequirementList( gobelinRequirement, gobelinRequirement2 ),
                    Duration = 50,
                    IsModel = true,
                    Description = "Cette petite créature est stupide, méchante et agressive, surtout stupide, ce qui les rend facile à contrôler. Bons marché et rapides, c'est la chair à canon idéale."
                };
                context.Add( gobelin );

                context.SaveChanges();
            }
        }

        public void SeedHarpy()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource harpyCost = new Ressource { Wood = 200, Metal = 150, Cristal = 50, Magic = 0 };
                context.Add( harpyCost );

                // Set up Statistics
                UnitStatistics harpyStatistics = new UnitStatistics
                {
                    Attack = 125,
                    PhysicResist = 75,
                    MagicResist = 100,
                    Speed = 175,
                    Capacity = 150,
                    Consumption = 10
                };
                context.Add( harpyStatistics );

                // Set up Requirements
                Requirement harpyRequirement = AddBuildingRequirement( context, BuildingName.barrack, 2 );
                Requirement harpyRequirement2 = AddTechnologyRequirement( context, TechnologyName.claws, 1 );
                Requirement harpyRequirement3 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit harpy = new Unit
                {
                    Name = "Harpie",
                    UnitName = UnitName.harpy,
                    UnitType = UnitType.monster,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = harpyCost,
                    UnitStatistics = harpyStatistics,
                    Requirements = CreateRequirementList( harpyRequirement, harpyRequirement2, harpyRequirement3 ),
                    Duration = 90,
                    IsModel = true,
                    Description = "Entre les hurlements, les griffes et les verrues, les harpies sont terrifiantes, c'est pour cela qu'on les envoie habituellement sur les mages ennemi. Unité d'attaque efficace en quantité."
                };
                context.Add( harpy );

                context.SaveChanges();
            }
        }

        public void SeedDirtGolem()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource dirtGolemCost = new Ressource { Wood = 500, Metal = 500, Cristal = 250, Magic = 100 };
                context.Add( dirtGolemCost );

                // Set up Statistics
                UnitStatistics dirtGolemStatistics = new UnitStatistics
                {
                    Attack = 50,
                    PhysicResist = 200,
                    MagicResist = 275,
                    Speed = 25,
                    Capacity = 25,
                    Consumption = 50
                };
                context.Add( dirtGolemStatistics );

                // Set up Requirements
                Requirement dirtGolemRequirement = AddBuildingRequirement( context, BuildingName.barrack, 4 );
                Requirement dirtGolemRequirement2 = AddTechnologyRequirement( context, TechnologyName.shell, 2 );
                Requirement dirtGolemRequirement3 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit dirtGolem = new Unit
                {
                    Name = "Golem de terre",
                    UnitName = UnitName.dirtGolem,
                    UnitType = UnitType.monster,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = dirtGolemCost,
                    UnitStatistics = dirtGolemStatistics,
                    Requirements = CreateRequirementList( dirtGolemRequirement, dirtGolemRequirement2, dirtGolemRequirement3 ),
                    Duration = 180,
                    IsModel = true,
                    Description = "Un gros bonhomme de terre lent et maladroit, mais très résistant. Très efficace en défense, surtout contre les magiciens."
                };
                context.Add( dirtGolem );

                context.SaveChanges();
            }
        }

        public void SeedTroll()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource trollCost = new Ressource { Wood = 1500, Metal = 1250, Cristal = 750, Magic = 250 };
                context.Add( trollCost );

                // Set up Statistics
                UnitStatistics trollStatistics = new UnitStatistics
                {
                    Attack = 400,
                    PhysicResist = 100,
                    MagicResist = 250,
                    Speed = 125,
                    Capacity = 250,
                    Consumption = 30
                };
                context.Add( trollStatistics );

                // Set up Requirements
                Requirement trollRequirement = AddBuildingRequirement( context, BuildingName.barrack, 6 );
                Requirement trollRequirement2 = AddTechnologyRequirement( context, TechnologyName.shell, 4 );
                Requirement trollRequirement3 = AddTechnologyRequirement( context, TechnologyName.claws, 4 );
                Requirement trollRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit troll = new Unit
                {
                    Name = "Troll",
                    UnitName = UnitName.troll,
                    UnitType = UnitType.monster,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = trollCost,
                    UnitStatistics = trollStatistics,
                    Requirements = CreateRequirementList( trollRequirement, trollRequirement2, trollRequirement3, trollRequirement4 ),
                    Duration = 490,
                    IsModel = true,
                    Description = "Le troll est une créature repoussante d'une force et d'une résistance incroyable. Tant qu'on peut supporter l'odeur de leurs pieds, ils sont un investissement rentable."
                };
                context.Add( troll );

                context.SaveChanges();
            }
        }

        public void SeedPhenix()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource phenixCost = new Ressource { Wood = 3500, Metal = 3500, Cristal = 2750, Magic = 1250 };
                context.Add( phenixCost );

                // Set up Statistics
                UnitStatistics phenixStatistics = new UnitStatistics
                {
                    Attack = 850,
                    PhysicResist = 400,
                    MagicResist = 600,
                    Speed = 175,
                    Capacity = 150,
                    Consumption = 50
                };
                context.Add( phenixStatistics );

                // Set up Requirements
                Requirement phenixRequirement = AddBuildingRequirement( context, BuildingName.barrack, 8 );
                Requirement phenixRequirement2 = AddTechnologyRequirement( context, TechnologyName.wings, 6 );
                Requirement phenixRequirement3 = AddTechnologyRequirement( context, TechnologyName.claws, 7 );
                Requirement phenixRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit phenix = new Unit
                {
                    Name = "Phénix",
                    UnitName = UnitName.phenix,
                    UnitType = UnitType.monster,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = phenixCost,
                    UnitStatistics = phenixStatistics,
                    Requirements = CreateRequirementList( phenixRequirement, phenixRequirement2, phenixRequirement3, phenixRequirement4 ),
                    Duration = 900,
                    IsModel = true,
                    Description = "L'oiseau légendaire, révéré et craint de tous, supposé immortel car renaissant à l'infini de ses cendres selon les légendes. Il est particulièrement efficace en attaque."
                };
                context.Add( phenix );

                context.SaveChanges();
            }
        }

        public void SeedJuggernaut()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource juggernautCost = new Ressource { Wood = 250000, Metal = 250000, Cristal = 190000, Magic = 100000 };
                context.Add( juggernautCost );

                // Set up Statistics
                UnitStatistics juggernautStatistics = new UnitStatistics
                {
                    Attack = 5000,
                    PhysicResist = 2500,
                    MagicResist = 4000,
                    Speed = 75,
                    Capacity = 2000,
                    Consumption = 500
                };
                context.Add( juggernautStatistics );

                // Set up Requirements
                Requirement juggernautRequirement = AddBuildingRequirement( context, BuildingName.barrack, 10 );
                Requirement juggernautRequirement2 = AddTechnologyRequirement( context, TechnologyName.wings, 10 );
                Requirement juggernautRequirement3 = AddTechnologyRequirement( context, TechnologyName.claws, 10 );
                Requirement juggernautRequirement4 = AddTechnologyRequirement( context, TechnologyName.shell, 10 );
                Requirement juggernautRequirement5 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit juggernaut = new Unit
                {
                    Name = "Juggernaut",
                    UnitName = UnitName.juggernaut,
                    UnitType = UnitType.monster,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = juggernautCost,
                    UnitStatistics = juggernautStatistics,
                    Requirements = CreateRequirementList( juggernautRequirement, juggernautRequirement2, juggernautRequirement3, juggernautRequirement4, juggernautRequirement5 ),
                    Duration = 900,
                    IsModel = true,
                    Description = "Créé en mélangeant magie et sciences, le juggernaut est une monstruosité faite pour une chose et une seule : tout écraser sur son passage."
                };
                context.Add( juggernaut );

                context.SaveChanges();
            }
        }
        #endregion

        #region Magical

        public void SeedWisp()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource wispCost = new Ressource { Wood = 50, Metal = 25, Cristal = 75, Magic = 0 };
                context.Add( wispCost );

                // Set up Statistics
                UnitStatistics wispStatistics = new UnitStatistics
                {
                    Attack = 50,
                    PhysicResist = 50,
                    MagicResist = 50,
                    Speed = 200,
                    Capacity = 50,
                    Consumption = 4
                };
                context.Add( wispStatistics );

                // Set up Requirements
                Requirement wispRequirement = AddBuildingRequirement( context, BuildingName.academy, 1 );
                Requirement wispRequirement2 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit wisp = new Unit
                {
                    Name = "Feu follet",
                    UnitName = UnitName.wisp,
                    UnitType = UnitType.magic,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = wispCost,
                    UnitStatistics = wispStatistics,
                    Requirements = CreateRequirementList( wispRequirement, wispRequirement2 ),
                    Duration = 50,
                    IsModel = true,
                    Description = "Les feux follets sont des incarnations d'esprits magiques mineurs, qui courent les cieux par milliers. Ce sont des créatures conscientes qui pratiquent elle-même la magie et aident les mages au combat."
                };
                context.Add( wisp );

                context.SaveChanges();
            }
        }

        public void SeedNovice()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource noviceCost = new Ressource { Wood = 100, Metal = 125, Cristal = 150, Magic = 25 };
                context.Add( noviceCost );

                // Set up Statistics
                UnitStatistics noviceStatistics = new UnitStatistics
                {
                    Attack = 150,
                    PhysicResist = 100,
                    MagicResist = 100,
                    Speed = 100,
                    Capacity = 100,
                    Consumption = 10
                };
                context.Add( noviceStatistics );

                // Set up Requirements
                Requirement noviceRequirement = AddBuildingRequirement( context, BuildingName.academy, 2 );
                Requirement noviceRequirement2 = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 2 );
                Requirement noviceRequirement3 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit novice = new Unit
                {
                    Name = "Novice",
                    UnitName = UnitName.novice,
                    UnitType = UnitType.magic,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = noviceCost,
                    UnitStatistics = noviceStatistics,
                    Requirements = CreateRequirementList( noviceRequirement, noviceRequirement2, noviceRequirement3 ),
                    Duration = 100,
                    IsModel = true,
                    Description = " Le novice est un jeune mage tout juste sorti de l'académie, capable de lancer des sorts de base et de soutenir ses camarades sur le champs de bataille."
                };
                context.Add( novice );

                context.SaveChanges();
            }
        }

        public void SeedNecromancer()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource necromancerCost = new Ressource { Wood = 200, Metal = 200, Cristal = 500, Magic = 150 };
                context.Add( necromancerCost );

                // Set up Statistics
                UnitStatistics necromancerStatistics = new UnitStatistics
                {
                    Attack = 200,
                    PhysicResist = 350,
                    MagicResist = 150,
                    Speed = 100,
                    Capacity = 150,
                    Consumption = 25
                };
                context.Add( necromancerStatistics );

                // Set up Requirements
                Requirement necromancerRequirement = AddBuildingRequirement( context, BuildingName.academy, 4 );
                Requirement necromancerRequirement2 = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 2 );
                Requirement necromancerRequirement3 = AddTechnologyRequirement( context, TechnologyName.levitation, 1 );
                Requirement necromancerRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit necromancer = new Unit
                {
                    Name = "Nécromancien",
                    UnitName = UnitName.necromancer,
                    UnitType = UnitType.magic,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = necromancerCost,
                    UnitStatistics = necromancerStatistics,
                    Requirements = CreateRequirementList( necromancerRequirement, necromancerRequirement2, necromancerRequirement3, necromancerRequirement4 ),
                    Duration = 260,
                    IsModel = true,
                    Description = "Formé dans les arts sombres de la magie, le nécromancien ramène ceux tombés au combat pour qu'il se battent de son côté. C'est une unité équilibrée puissante, particulièrement forte en défense contre les soldats."
                };
                context.Add( necromancer );

                context.SaveChanges();
            }
        }

        public void SeedSummoner()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource summonerCost = new Ressource { Wood = 500, Metal = 500, Cristal = 2000, Magic = 750 };
                context.Add( summonerCost );

                // Set up Statistics
                UnitStatistics summonerStatistics = new UnitStatistics
                {
                    Attack = 750,
                    PhysicResist = 650,
                    MagicResist = 750,
                    Speed = 100,
                    Capacity = 250,
                    Consumption = 100
                };
                context.Add( summonerStatistics );

                // Set up Requirements
                Requirement summonerRequirement = AddBuildingRequirement( context, BuildingName.academy, 6 );
                Requirement summonerRequirement2 = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 4 );
                Requirement summonerRequirement3 = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 4 );
                Requirement summonerRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit summoner = new Unit
                {
                    Name = "Invocateur",
                    UnitName = UnitName.summoner,
                    UnitType = UnitType.magic,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = summonerCost,
                    UnitStatistics = summonerStatistics,
                    Requirements = CreateRequirementList( summonerRequirement, summonerRequirement2, summonerRequirement3, summonerRequirement4 ),
                    Duration = 800,
                    IsModel = true,
                    Description = "L'invocateur est un puissant maître des arcanes qui invoque des créatures éthérées qui combattent pour lui, il est particulièrement efficace en attaque comme en défense."
                };
                context.Add( summoner );

                context.SaveChanges();
            }
        }

        public void SeedProphet()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource prophetCost = new Ressource { Wood = 1500, Metal = 1500, Cristal = 7500, Magic = 1500 };
                context.Add( prophetCost );

                // Set up Statistics
                UnitStatistics prophetStatistics = new UnitStatistics
                {
                    Attack = 800,
                    PhysicResist = 1250,
                    MagicResist = 1500,
                    Speed = 75,
                    Capacity = 200,
                    Consumption = 250
                };
                context.Add( prophetStatistics );

                // Set up Requirements
                Requirement prophetRequirement = AddBuildingRequirement( context, BuildingName.academy, 8 );
                Requirement prophetRequirement2 = AddTechnologyRequirement( context, TechnologyName.magicalStaff, 7 );
                Requirement prophetRequirement3 = AddTechnologyRequirement( context, TechnologyName.magicalProtection, 7 );
                Requirement prophetRequirement4 = AddTechnologyRequirement( context, TechnologyName.levitation, 6 );
                Requirement prophetRequirement5 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit prophet = new Unit
                {
                    Name = "Prophète",
                    UnitName = UnitName.prophet,
                    UnitType = UnitType.magic,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = prophetCost,
                    UnitStatistics = prophetStatistics,
                    Requirements = CreateRequirementList( prophetRequirement, prophetRequirement2, prophetRequirement3, prophetRequirement4, prophetRequirement5 ),
                    Duration = 1850,
                    IsModel = true,
                    Description = "Le prophète est la sagesse et la puissance magique incarnée, il n'aime pas la violence mais sait utiliser ses pouvoirs prodigieux quand il le faut."
                };
                context.Add( prophet );

                context.SaveChanges();
            }
        }

        #endregion

        #region Mecanical

        public void SeedScorpion()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource scorpionCost = new Ressource { Wood = 250, Metal = 50, Cristal = 25, Magic = 0 };
                context.Add( scorpionCost );

                // Set up Statistics
                UnitStatistics scorpionStatistics = new UnitStatistics
                {
                    Attack = 75,
                    PhysicResist = 100,
                    MagicResist = 100,
                    Speed = 50,
                    Capacity = 25,
                    Consumption = 10
                };
                context.Add( scorpionStatistics );

                // Set up Requirements
                Requirement scorpionRequirement = AddBuildingRequirement( context, BuildingName.forge, 1 );
                Requirement scorpionRequirement2 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit scorpion = new Unit
                {
                    Name = "Scorpion",
                    UnitName = UnitName.scorpion,
                    UnitType = UnitType.mecanical,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = scorpionCost,
                    UnitStatistics = scorpionStatistics,
                    Requirements = CreateRequirementList( scorpionRequirement, scorpionRequirement2 ),
                    Duration = 75,
                    IsModel = true,
                    Description = "Cette petite machine de guerre envoit de gros carreaux à grande distance, avec plus ou moins de précision. C'est une machine basique surtout utile en défense."
                };
                context.Add( scorpion );

                context.SaveChanges();
            }
        }

        public void SeedFlameThrower()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource flameThrowerCost = new Ressource { Wood = 500, Metal = 350, Cristal = 100, Magic = 0 };
                context.Add( flameThrowerCost );

                // Set up Statistics
                UnitStatistics flameThrowerStatistics = new UnitStatistics
                {
                    Attack = 250,
                    PhysicResist = 150,
                    MagicResist = 200,
                    Speed = 50,
                    Capacity = 25,
                    Consumption = 20
                };
                context.Add( flameThrowerStatistics );

                // Set up Requirements
                Requirement flameThrowerRequirement = AddBuildingRequirement( context, BuildingName.forge, 2 );
                Requirement flameThrowerRequirement2 = AddTechnologyRequirement( context, TechnologyName.plating, 2 );
                Requirement flameThrowerRequirement3 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit flameThrower = new Unit
                {
                    Name = "Lanceur de feu",
                    UnitName = UnitName.flameThrower,
                    UnitType = UnitType.mecanical,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = flameThrowerCost,
                    UnitStatistics = flameThrowerStatistics,
                    Requirements = CreateRequirementList( flameThrowerRequirement, flameThrowerRequirement2, flameThrowerRequirement3 ),
                    Duration = 140,
                    IsModel = true,
                    Description = "Cette machine envoit de longues langues de feu pour enflammer les ennemis, très puissante en attaque."
                };
                context.Add( flameThrower );

                context.SaveChanges();
            }
        }

        public void SeedSaboteur()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource saboteurCost = new Ressource { Wood = 1250, Metal = 1850, Cristal = 750, Magic = 250 };
                context.Add( saboteurCost );

                // Set up Statistics
                UnitStatistics saboteurStatistics = new UnitStatistics
                {
                    Attack = 250,
                    PhysicResist = 400,
                    MagicResist = 250,
                    Speed = 50,
                    Capacity = 25,
                    Consumption = 150
                };
                context.Add( saboteurStatistics );

                // Set up Requirements
                Requirement saboteurRequirement = AddBuildingRequirement( context, BuildingName.forge, 4 );
                Requirement saboteurRequirement2 = AddTechnologyRequirement( context, TechnologyName.plating, 3 );
                Requirement saboteurRequirement3 = AddTechnologyRequirement( context, TechnologyName.propulsion, 3 );
                Requirement saboteurRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit saboteur = new Unit
                {
                    Name = "Désosseur",
                    UnitName = UnitName.saboteur,
                    UnitType = UnitType.mecanical,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = saboteurCost,
                    UnitStatistics = saboteurStatistics,
                    Requirements = CreateRequirementList( saboteurRequirement, saboteurRequirement2, saboteurRequirement3, saboteurRequirement4 ),
                    Duration = 260,
                    IsModel = true,
                    Description = "Un amas d'armes sur des bras articulés faits pour détruire tout ce peut l'être, particulièrement utile en attaque contre les soldats."
                };
                context.Add( saboteur );

                context.SaveChanges();
            }
        }

        public void SeedLivingArmor()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource livingArmorCost = new Ressource { Wood = 2500, Metal = 3500, Cristal = 1500, Magic = 250 };
                context.Add( livingArmorCost );

                // Set up Statistics
                UnitStatistics livingArmorStatistics = new UnitStatistics
                {
                    Attack = 350,
                    PhysicResist = 350,
                    MagicResist = 350,
                    Speed = 100,
                    Capacity = 150,
                    Consumption = 100
                };
                context.Add( livingArmorStatistics );

                // Set up Requirements
                Requirement livingArmorRequirement = AddBuildingRequirement( context, BuildingName.forge, 5 );
                Requirement livingArmorRequirement2 = AddTechnologyRequirement( context, TechnologyName.plating, 6 );
                Requirement livingArmorRequirement3 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit livingArmor = new Unit
                {
                    Name = "Armure vivante",
                    UnitName = UnitName.livingArmor,
                    UnitType = UnitType.mecanical,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = livingArmorCost,
                    UnitStatistics = livingArmorStatistics,
                    Requirements = CreateRequirementList( livingArmorRequirement, livingArmorRequirement2, livingArmorRequirement3 ),
                    Duration = 500,
                    IsModel = true,
                    Description = "Une grande armure vide animée par un mélange de mécanique et de magie, cette unité est aussi forte en attaque qu'en défense."
                };
                context.Add( livingArmor );

                context.SaveChanges();
            }
        }

        public void SeedIronGolem()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource ironGolemCost = new Ressource { Wood = 5000, Metal = 7500, Cristal = 2500, Magic = 1000 };
                context.Add( ironGolemCost );

                // Set up Statistics
                UnitStatistics ironGolemStatistics = new UnitStatistics
                {
                    Attack = 150,
                    PhysicResist = 1000,
                    MagicResist = 1000,
                    Speed = 25,
                    Capacity = 25,
                    Consumption = 300
                };
                context.Add( ironGolemStatistics );

                // Set up Requirements
                Requirement ironGolemRequirement = AddBuildingRequirement( context, BuildingName.forge, 7 );
                Requirement ironGolemRequirement2 = AddTechnologyRequirement( context, TechnologyName.plating, 8 );
                Requirement ironGolemRequirement3 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit ironGolem = new Unit
                {
                    Name = "Golem de fer",
                    UnitName = UnitName.ironGolem,
                    UnitType = UnitType.mecanical,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = ironGolemCost,
                    UnitStatistics = ironGolemStatistics,
                    Requirements = CreateRequirementList( ironGolemRequirement, ironGolemRequirement2, ironGolemRequirement3 ),
                    Duration = 1200,
                    IsModel = true,
                    Description = "Le golem de fer est une construction de métal brute qui résiste à presque tout, ce qui la rend très bonne en défense."
                };
                context.Add( ironGolem );

                context.SaveChanges();
            }
        }

        public void SeedPrismaticGolem()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource prismaticGolemCost = new Ressource { Wood = 15000, Metal = 25000, Cristal = 10000, Magic = 2500 };
                context.Add( prismaticGolemCost );

                // Set up Statistics
                UnitStatistics prismaticGolemStatistics = new UnitStatistics
                {
                    Attack = 500,
                    PhysicResist = 6000,
                    MagicResist = 5000,
                    Speed = 100,
                    Capacity = 200,
                    Consumption = 250
                };
                context.Add( prismaticGolemStatistics );

                // Set up Requirements
                Requirement prismaticGolemRequirement = AddBuildingRequirement( context, BuildingName.forge, 10 );
                Requirement prismaticGolemRequirement2 = AddTechnologyRequirement( context, TechnologyName.plating, 10 );
                Requirement prismaticGolemRequirement3 = AddTechnologyRequirement( context, TechnologyName.propulsion, 10 );
                Requirement prismaticGolemRequirement4 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit prismaticGolem = new Unit
                {
                    Name = "Golem  prismatique",
                    UnitName = UnitName.prismaticGolem,
                    UnitType = UnitType.mecanical,
                    UnitDamageType = UnitDamageType.magical,
                    UnitCost = prismaticGolemCost,
                    UnitStatistics = prismaticGolemStatistics,
                    Requirements = CreateRequirementList( prismaticGolemRequirement, prismaticGolemRequirement2, prismaticGolemRequirement3, prismaticGolemRequirement4 ),
                    Duration = 2600,
                    IsModel = true,
                    Description = "Seuls les plus puissants mages peuvent créer et contrôler cette machine fantastique, faite de lumière solidifiée, elle est presque indestructible."
                };
                context.Add( prismaticGolem );

                context.SaveChanges();
            }
        }

        //public void SeedCanoe()
        //{
        //    using ( SetupContext context = new SetupContext() )
        //    {
        //        // Set up Cost

        //        Ressource canoeCost = new Ressource { Wood = 500, Metal = 250, Cristal = 0, Magic = 0 };
        //        context.Add( canoeCost );

        //        // Set up Statistics
        //        UnitStatistics canoeStatistics = new UnitStatistics
        //        {
        //            Attack = 5,
        //            PhysicResist = 100,
        //            MagicResist = 100,
        //            Speed = 125,
        //            Capacity = 5000,
        //            Consumption = 25
        //        };
        //        context.Add( canoeStatistics );

        //        // Set up Requirements
        //        Requirement canoeRequirement = AddBuildingRequirement( context, BuildingName.tower, 2 );
        //        Requirement canoeRequirement2 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

        //        // Add Unit
        //        Unit canoe = new Unit
        //        {
        //            Name = "Pirogue",
        //            UnitName = UnitName.canoe,
        //            UnitType = UnitType.mecanical,
        //            UnitDamageType = UnitDamageType.physical,
        //            UnitCost = canoeCost,
        //            UnitStatistics = canoeStatistics,
        //            Requirements = CreateRequirementList( canoeRequirement, canoeRequirement2 ),
        //            Duration = 120,
        //            IsModel = true
        //        };
        //        context.Add( canoe );

        //        context.SaveChanges();
        //    }
        //}

        #endregion

        #region Utility

        public void SeedCarrier()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource carrierCost = new Ressource { Wood = 2000, Metal = 1250, Cristal = 500, Magic = 0 };
                context.Add( carrierCost );

                // Set up Statistics
                UnitStatistics carrierStatistics = new UnitStatistics
                {
                    Attack = 25,
                    PhysicResist = 200,
                    MagicResist = 200,
                    Speed = 80,
                    Capacity = 25000,
                    Consumption = 50
                };
                context.Add( carrierStatistics );

                // Set up Requirements
                Requirement carrierRequirement = AddTechnologyRequirement( context, TechnologyName.hold, 2 );
                Requirement carrierRequirement2 = AddBuildingRequirement( context, BuildingName.invocation, 1 );

                // Add Unit
                Unit carrier = new Unit
                {
                    Name = "Transporteur",
                    UnitName = UnitName.carrier,
                    UnitType = UnitType.utility,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = carrierCost,
                    UnitStatistics = carrierStatistics,
                    Requirements = CreateRequirementList( carrierRequirement, carrierRequirement2 ),
                    Duration = 250,
                    IsModel = true,
                    Description = "Fait pour transporter des ressources en grandes quantité, cette grande montgolfière n'est pas faire pour le combat et doit être protégé."
                };
                context.Add( carrier );

                context.SaveChanges();
            }
        }
        public void SeedApprentice()
        {
            using ( SetupContext context = new SetupContext() )
            {
                // Set up Cost

                Ressource apprenticeCost = new Ressource { Wood = 2000, Metal = 2000, Cristal = 2000, Magic = 500 };
                context.Add( apprenticeCost );

                // Set up Statistics
                UnitStatistics apprenticeStatistics = new UnitStatistics
                {
                    Attack = 0,
                    PhysicResist = 0,
                    MagicResist = 0,
                    Speed = 25,
                    Capacity = 5000,
                    Consumption = 50
                };
                context.Add( apprenticeStatistics );

                // Set up Requirements
                Requirement apprenticeRequirement = AddTechnologyRequirement( context, TechnologyName.conquest, 2 );

                // Add Unit
                Unit apprentice = new Unit
                {
                    Name = "Apprenti",
                    UnitName = UnitName.apprentice,
                    UnitType = UnitType.utility,
                    UnitDamageType = UnitDamageType.physical,
                    UnitCost = apprenticeCost,
                    UnitStatistics = apprenticeStatistics,
                   // Requirements = CreateRequirementList( apprenticeRequirement ),
                    //Duration = 600,
                    Duration = 10,
                    IsModel = true,
                    Description = "L'apprenti est formé non pas pour le combat, mais pour construire sa propre tour de mage et prendre le contrôle d'une nouvelle île. Il est faible en attaque et en défense, il doit donc être protégé."
                };
                context.Add( apprentice );

                context.SaveChanges();
            }
        }

        #endregion

        #region Methods
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
        #endregion
    }
}
