using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.Migrations
{
    [DbContext(typeof(SetupContext))]
    partial class SetupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITI.SkyLord.Apprentice", b =>
                {
                    b.Property<long>("ApprenticeId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("LevelLevelId");

                    b.Property<long?>("MageMageId");

                    b.HasKey("ApprenticeId");
                });

            modelBuilder.Entity("ITI.SkyLord.ApprenticeLevel", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CostRessourceId");

                    b.Property<int>("Number");

                    b.HasKey("LevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.Army", b =>
                {
                    b.Property<long>("ArmyId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArmyState");

                    b.Property<long?>("IslandIslandId");

                    b.HasKey("ArmyId");
                });

            modelBuilder.Entity("ITI.SkyLord.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("IslandIslandId");

                    b.Property<long?>("LevelLevelId");

                    b.Property<string>("Name");

                    b.HasKey("BuildingId");
                });

            modelBuilder.Entity("ITI.SkyLord.BuildingLevel", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CostRessourceId");

                    b.Property<int>("Number");

                    b.HasKey("LevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.CombatReport", b =>
                {
                    b.Property<long>("CombatreportId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ObjectReport");

                    b.Property<long?>("ReceiverPlayerId");

                    b.Property<string>("Report");

                    b.HasKey("CombatreportId");
                });

            modelBuilder.Entity("ITI.SkyLord.Coordinate", b =>
                {
                    b.Property<long>("CoordinateId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("X");

                    b.Property<int>("Y");

                    b.HasKey("CoordinateId");
                });

            modelBuilder.Entity("ITI.SkyLord.Guild", b =>
                {
                    b.Property<long>("GuildId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("InternalMessage");

                    b.Property<string>("Name");

                    b.Property<string>("PublicMessage");

                    b.Property<bool>("Recrutement");

                    b.HasKey("GuildId");
                });

            modelBuilder.Entity("ITI.SkyLord.GuildMember", b =>
                {
                    b.Property<long>("GuildMemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("GuildGuildId");

                    b.Property<long?>("GuildRoleGuildRoleId");

                    b.HasKey("GuildMemberId");
                });

            modelBuilder.Entity("ITI.SkyLord.GuildRole", b =>
                {
                    b.Property<long>("GuildRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("GuildRoleId");
                });

            modelBuilder.Entity("ITI.SkyLord.Island", b =>
                {
                    b.Property<long>("IslandId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AllRessourcesRessourceId");

                    b.Property<long?>("CoordinatesCoordinateId");

                    b.Property<bool>("IsCapital");

                    b.Property<int>("Loyalty");

                    b.Property<long?>("MapMapId");

                    b.Property<string>("Name");

                    b.Property<long?>("OwnerPlayerId");

                    b.HasKey("IslandId");
                });

            modelBuilder.Entity("ITI.SkyLord.Level", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CostRessourceId");

                    b.Property<int>("Number");

                    b.HasKey("LevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.Mage", b =>
                {
                    b.Property<long>("MageId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("IslandIslandId");

                    b.Property<long?>("MageLevelLevelId");

                    b.Property<string>("Name");

                    b.HasKey("MageId");
                });

            modelBuilder.Entity("ITI.SkyLord.MageLevel", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CostRessourceId");

                    b.Property<int>("Number");

                    b.HasKey("LevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.Map", b =>
                {
                    b.Property<long>("MapId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("MapId");
                });

            modelBuilder.Entity("ITI.SkyLord.Message", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CoreMessage");

                    b.Property<string>("MessageObject");

                    b.Property<long?>("ReceiverPlayerId");

                    b.Property<long?>("SenderPlayerId");

                    b.HasKey("MessageId");
                });

            modelBuilder.Entity("ITI.SkyLord.Player", b =>
                {
                    b.Property<long>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("GuildGuildId");

                    b.Property<string>("Name");

                    b.Property<long?>("ProfilProfilId");

                    b.Property<long?>("WorldWorldId");

                    b.HasKey("PlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Profil", b =>
                {
                    b.Property<long>("ProfilId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Mail");

                    b.Property<string>("Password");

                    b.HasKey("ProfilId");
                });

            modelBuilder.Entity("ITI.SkyLord.Regiment", b =>
                {
                    b.Property<long>("RegimentId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ArmyArmyId");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.Property<int?>("UnitUnitId");

                    b.HasKey("RegimentId");
                });

            modelBuilder.Entity("ITI.SkyLord.Ressource", b =>
                {
                    b.Property<long>("RessourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cristal");

                    b.Property<int>("Magic");

                    b.Property<int>("Metal");

                    b.Property<int>("Wood");

                    b.HasKey("RessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Spell", b =>
                {
                    b.Property<long>("SpellId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("SpellId");
                });

            modelBuilder.Entity("ITI.SkyLord.Technology", b =>
                {
                    b.Property<long>("TechnologyId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("LevelLevelId");

                    b.Property<string>("Name");

                    b.Property<long?>("PlayerPlayerId");

                    b.HasKey("TechnologyId");
                });

            modelBuilder.Entity("ITI.SkyLord.TechnologyLevel", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CostRessourceId");

                    b.Property<int>("Number");

                    b.HasKey("LevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("UnitCostRessourceId");

                    b.Property<int>("UnitDamageType");

                    b.Property<int>("UnitName");

                    b.Property<long?>("UnitStatisticsUnitStatisticsId");

                    b.Property<int>("UnitType");

                    b.HasKey("UnitId");
                });

            modelBuilder.Entity("ITI.SkyLord.UnitStatistics", b =>
                {
                    b.Property<long>("UnitStatisticsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Attack");

                    b.Property<int>("Capacity");

                    b.Property<int>("Consumption");

                    b.Property<int>("MagicResist");

                    b.Property<int>("PhysicResist");

                    b.Property<int>("Speed");

                    b.HasKey("UnitStatisticsId");
                });

            modelBuilder.Entity("ITI.SkyLord.World", b =>
                {
                    b.Property<long>("WorldId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("MapMapId");

                    b.HasKey("WorldId");
                });

            modelBuilder.Entity("ITI.SkyLord.Apprentice", b =>
                {
                    b.HasOne("ITI.SkyLord.ApprenticeLevel")
                        .WithMany()
                        .ForeignKey("LevelLevelId");

                    b.HasOne("ITI.SkyLord.Mage")
                        .WithMany()
                        .ForeignKey("MageMageId");
                });

            modelBuilder.Entity("ITI.SkyLord.ApprenticeLevel", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .ForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Army", b =>
                {
                    b.HasOne("ITI.SkyLord.Island")
                        .WithMany()
                        .ForeignKey("IslandIslandId");
                });

            modelBuilder.Entity("ITI.SkyLord.Building", b =>
                {
                    b.HasOne("ITI.SkyLord.Island")
                        .WithMany()
                        .ForeignKey("IslandIslandId");

                    b.HasOne("ITI.SkyLord.BuildingLevel")
                        .WithMany()
                        .ForeignKey("LevelLevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.BuildingLevel", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .ForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.CombatReport", b =>
                {
                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .ForeignKey("ReceiverPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.GuildMember", b =>
                {
                    b.HasOne("ITI.SkyLord.Guild")
                        .WithMany()
                        .ForeignKey("GuildGuildId");

                    b.HasOne("ITI.SkyLord.GuildRole")
                        .WithMany()
                        .ForeignKey("GuildRoleGuildRoleId");
                });

            modelBuilder.Entity("ITI.SkyLord.Island", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .ForeignKey("AllRessourcesRessourceId");

                    b.HasOne("ITI.SkyLord.Coordinate")
                        .WithMany()
                        .ForeignKey("CoordinatesCoordinateId");

                    b.HasOne("ITI.SkyLord.Map")
                        .WithMany()
                        .ForeignKey("MapMapId");

                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .ForeignKey("OwnerPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Level", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .ForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Mage", b =>
                {
                    b.HasOne("ITI.SkyLord.Island")
                        .WithMany()
                        .ForeignKey("IslandIslandId");

                    b.HasOne("ITI.SkyLord.MageLevel")
                        .WithMany()
                        .ForeignKey("MageLevelLevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.MageLevel", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .ForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Message", b =>
                {
                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .ForeignKey("ReceiverPlayerId");

                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .ForeignKey("SenderPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Player", b =>
                {
                    b.HasOne("ITI.SkyLord.Guild")
                        .WithMany()
                        .ForeignKey("GuildGuildId");

                    b.HasOne("ITI.SkyLord.Profil")
                        .WithMany()
                        .ForeignKey("ProfilProfilId");

                    b.HasOne("ITI.SkyLord.World")
                        .WithMany()
                        .ForeignKey("WorldWorldId");
                });

            modelBuilder.Entity("ITI.SkyLord.Regiment", b =>
                {
                    b.HasOne("ITI.SkyLord.Army")
                        .WithMany()
                        .ForeignKey("ArmyArmyId");

                    b.HasOne("ITI.SkyLord.Unit")
                        .WithMany()
                        .ForeignKey("UnitUnitId");
                });

            modelBuilder.Entity("ITI.SkyLord.Technology", b =>
                {
                    b.HasOne("ITI.SkyLord.TechnologyLevel")
                        .WithMany()
                        .ForeignKey("LevelLevelId");

                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .ForeignKey("PlayerPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.TechnologyLevel", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .ForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Unit", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .ForeignKey("UnitCostRessourceId");

                    b.HasOne("ITI.SkyLord.UnitStatistics")
                        .WithMany()
                        .ForeignKey("UnitStatisticsUnitStatisticsId");
                });

            modelBuilder.Entity("ITI.SkyLord.World", b =>
                {
                    b.HasOne("ITI.SkyLord.Map")
                        .WithMany()
                        .ForeignKey("MapMapId");
                });
        }
    }
}
