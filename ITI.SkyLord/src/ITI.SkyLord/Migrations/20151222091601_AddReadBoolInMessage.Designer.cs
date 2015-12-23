using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.Migrations
{
    [DbContext(typeof(SetupContext))]
    [Migration("20151222091601_AddReadBoolInMessage")]
    partial class AddReadBoolInMessage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Name");

                    b.Property<long?>("OwnerPlayerId");

                    b.HasKey("IslandId");
                });

            modelBuilder.Entity("ITI.SkyLord.Level", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CostRessourceId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.HasKey("LevelId");

                    b.HasAnnotation("Relational:DiscriminatorProperty", "Discriminator");

                    b.HasAnnotation("Relational:DiscriminatorValue", "Level");
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

            modelBuilder.Entity("ITI.SkyLord.Message", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CoreMessage");

                    b.Property<string>("MessageObject");

                    b.Property<bool>("Read");

                    b.Property<long?>("ReceiverPlayerId");

                    b.Property<long?>("SenderPlayerId");

                    b.HasKey("MessageId");
                });

            modelBuilder.Entity("ITI.SkyLord.Player", b =>
                {
                    b.Property<long>("PlayerId");

                    b.Property<long?>("GuildGuildId");

                    b.Property<string>("Mail");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Password");

                    b.Property<long?>("ProfilProfilId");

                    b.Property<long?>("WorldWorldId");

                    b.HasKey("PlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Profil", b =>
                {
                    b.Property<long>("ProfilId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ProfilId");
                });

            modelBuilder.Entity("ITI.SkyLord.Regiment", b =>
                {
                    b.Property<long>("RegimentId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ArmyId")
                        .HasAnnotation("Relational:ColumnName", "ArmyArmyId");

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

            modelBuilder.Entity("ITI.SkyLord.User_Player", b =>
                {
                    b.Property<long>("User_PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.HasKey("User_PlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.World", b =>
                {
                    b.Property<long>("WorldId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("WorldId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:DiscriminatorProperty", "Discriminator");

                    b.HasAnnotation("Relational:DiscriminatorValue", "IdentityUser");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("ITI.SkyLord.MageLevel", b =>
                {
                    b.HasBaseType("ITI.SkyLord.Level");


                    b.HasAnnotation("Relational:DiscriminatorValue", "MageLevel");
                });

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNet.Identity.EntityFramework.IdentityUser");


                    b.HasAnnotation("Relational:DiscriminatorValue", "ApplicationUser");
                });

            modelBuilder.Entity("ITI.SkyLord.Apprentice", b =>
                {
                    b.HasOne("ITI.SkyLord.ApprenticeLevel")
                        .WithMany()
                        .HasForeignKey("LevelLevelId");

                    b.HasOne("ITI.SkyLord.Mage")
                        .WithMany()
                        .HasForeignKey("MageMageId");
                });

            modelBuilder.Entity("ITI.SkyLord.ApprenticeLevel", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .HasForeignKey("CostRessourceId");
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

            modelBuilder.Entity("ITI.SkyLord.CombatReport", b =>
                {
                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .HasForeignKey("ReceiverPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.GuildMember", b =>
                {
                    b.HasOne("ITI.SkyLord.Guild")
                        .WithMany()
                        .HasForeignKey("GuildGuildId");

                    b.HasOne("ITI.SkyLord.GuildRole")
                        .WithMany()
                        .HasForeignKey("GuildRoleGuildRoleId");
                });

            modelBuilder.Entity("ITI.SkyLord.Island", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .HasForeignKey("AllRessourcesRessourceId");

                    b.HasOne("ITI.SkyLord.Coordinate")
                        .WithMany()
                        .HasForeignKey("CoordinatesCoordinateId");

                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .HasForeignKey("OwnerPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Level", b =>
                {
                    b.HasOne("ITI.SkyLord.Ressource")
                        .WithMany()
                        .HasForeignKey("CostRessourceId");
                });

            modelBuilder.Entity("ITI.SkyLord.Mage", b =>
                {
                    b.HasOne("ITI.SkyLord.Island")
                        .WithMany()
                        .HasForeignKey("IslandIslandId");

                    b.HasOne("ITI.SkyLord.MageLevel")
                        .WithMany()
                        .HasForeignKey("MageLevelLevelId");
                });

            modelBuilder.Entity("ITI.SkyLord.Message", b =>
                {
                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .HasForeignKey("ReceiverPlayerId");

                    b.HasOne("ITI.SkyLord.Player")
                        .WithMany()
                        .HasForeignKey("SenderPlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Player", b =>
                {
                    b.HasOne("ITI.SkyLord.Guild")
                        .WithMany()
                        .HasForeignKey("GuildGuildId");

                    b.HasOne("ITI.SkyLord.User_Player")
                        .WithOne()
                        .HasForeignKey("ITI.SkyLord.Player", "PlayerId");

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
                        .HasForeignKey("ArmyId");

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

            modelBuilder.Entity("ITI.SkyLord.User_Player", b =>
                {
                    b.HasOne("ITI.SkyLord.Models.Entity_Framework.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ITI.SkyLord.MageLevel", b =>
                {
                });
        }
    }
}
