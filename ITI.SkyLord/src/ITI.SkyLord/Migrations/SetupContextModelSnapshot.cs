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
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("ITI.SkyLord.GuildRole", b =>
                {
                    b.Property<long>("GuildRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Rights");

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

            modelBuilder.Entity("ITI.SkyLord.Map", b =>
                {
                    b.Property<long>("MapId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("MapId");
                });

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.Entites.GuildMember", b =>
                {
                    b.Property<long>("GuildMemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("GuildGuildId");

                    b.Property<long?>("GuildRoleGuildRoleId");

                    b.HasKey("GuildMemberId");
                });

            modelBuilder.Entity("ITI.SkyLord.Player", b =>
                {
                    b.Property<long>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("GuildGuildId");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 20);

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

                    b.Property<string>("Password")
                        .HasAnnotation("MaxLength", 60);

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

                    b.Property<string>("Name");

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

            modelBuilder.Entity("ITI.SkyLord.Army", b =>
                {
                    b.HasOne("ITI.SkyLord.Island")
                        .WithMany()
                        .HasForeignKey("IslandIslandId");
                });

            modelBuilder.Entity("ITI.SkyLord.Building", b =>
                {
                    b.HasOne("ITI.SkyLord.Island")
                        .WithMany()
                        .HasForeignKey("IslandIslandId");

                    b.HasOne("ITI.SkyLord.BuildingLevel")
                        .WithMany()
                        .HasForeignKey("LevelLevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.BuildingLevel", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .HasForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Island", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .HasForeignKey("AllRessourcesRessourceId");

                    b.HasOne("ITI.SkyLord.Coordinate")
                        .WithMany()
                        .HasForeignKey("CoordinatesCoordinateId");

                    b.HasOne("ITI.SkyLord.Map")
                        .WithMany()
                        .HasForeignKey("MapMapId");

                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .HasForeignKey("OwnerPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.Entites.GuildMember", b =>
                {
                    b.HasOne("ITI.SkyLord.Guild")
                        .WithMany()
                        .HasForeignKey("GuildGuildId");

                    b.HasOne("ITI.SkyLord.GuildRole")
                        .WithMany()
                        .HasForeignKey("GuildRoleGuildRoleId");
                });

            modelBuilder.Entity("ITI.SkyLord.Player", b =>
                {
                    b.HasOne("ITI.SkyLord.Guild")
                        .WithMany()
                        .HasForeignKey("GuildGuildId");

                    b.HasOne("ITI.SkyLord.Profil")
                        .WithMany()
                        .HasForeignKey("ProfilProfilId");

                    b.HasOne("ITI.SkyLord.World")
                        .WithMany()
                        .HasForeignKey("WorldWorldId");
                });

            modelBuilder.Entity("ITI.SkyLord.Regiment", b =>
                {
                    b.HasOne("ITI.SkyLord.Army")
                        .WithMany()
                        .HasForeignKey("ArmyArmyId");

                    b.HasOne("ITI.SkyLord.Unit")
                        .WithMany()
                        .HasForeignKey("UnitUnitId");
                });

            modelBuilder.Entity("ITI.SkyLord.Technology", b =>
                {
                    b.HasOne("ITI.SkyLord.TechnologyLevel")
                        .WithMany()
                        .HasForeignKey("LevelLevelId");

                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .HasForeignKey("PlayerPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.TechnologyLevel", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .HasForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Unit", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .HasForeignKey("UnitCostRessourceId");

                    b.HasOne("ITI.SkyLord.UnitStatistics")
                        .WithMany()
                        .HasForeignKey("UnitStatisticsUnitStatisticsId");
                });

            modelBuilder.Entity("ITI.SkyLord.World", b =>
                {
                    b.HasOne("ITI.SkyLord.Map")
                        .WithMany()
                        .HasForeignKey("MapMapId");
                });
        }
    }
}
