using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ITI.SkyLord.Migrations
{
    public partial class Setup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinate",
                columns: table => new
                {
                    CoordinateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinate", x => x.CoordinateId);
                });
            migrationBuilder.CreateTable(
                name: "Guild",
                columns: table => new
                {
                    GuildId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    InternalMessage = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PublicMessage = table.Column<string>(nullable: true),
                    Recrutement = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guild", x => x.GuildId);
                });
            migrationBuilder.CreateTable(
                name: "GuildRole",
                columns: table => new
                {
                    GuildRoleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Rights = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildRole", x => x.GuildRoleId);
                });
            migrationBuilder.CreateTable(
                name: "Profil",
                columns: table => new
                {
                    ProfilId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profil", x => x.ProfilId);
                });
            migrationBuilder.CreateTable(
                name: "Ressource",
                columns: table => new
                {
                    RessourceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cristal = table.Column<int>(nullable: false),
                    Magic = table.Column<int>(nullable: false),
                    Metal = table.Column<int>(nullable: false),
                    Wood = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressource", x => x.RessourceId);
                });
            migrationBuilder.CreateTable(
                name: "Spell",
                columns: table => new
                {
                    SpellId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spell", x => x.SpellId);
                });
            migrationBuilder.CreateTable(
                name: "UnitStatistics",
                columns: table => new
                {
                    UnitStatisticsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attack = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Consumption = table.Column<int>(nullable: false),
                    MagicResist = table.Column<int>(nullable: false),
                    PhysicResist = table.Column<int>(nullable: false),
                    Speed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStatistics", x => x.UnitStatisticsId);
                });
            migrationBuilder.CreateTable(
                name: "World",
                columns: table => new
                {
                    WorldId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_World", x => x.WorldId);
                });
            migrationBuilder.CreateTable(
                name: "GuildMember",
                columns: table => new
                {
                    GuildMemberId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuildGuildId = table.Column<long>(nullable: true),
                    GuildRoleGuildRoleId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMember", x => x.GuildMemberId);
                    table.ForeignKey(
                        name: "FK_GuildMember_Guild_GuildGuildId",
                        column: x => x.GuildGuildId,
                        principalTable: "Guild",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildMember_GuildRole_GuildRoleGuildRoleId",
                        column: x => x.GuildRoleGuildRoleId,
                        principalTable: "GuildRole",
                        principalColumn: "GuildRoleId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ApprenticeLevel",
                columns: table => new
                {
                    LevelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostRessourceId = table.Column<long>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprenticeLevel", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_ApprenticeLevel_Ressource_CostRessourceId",
                        column: x => x.CostRessourceId,
                        principalTable: "Ressource",
                        principalColumn: "RessourceId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "BuildingLevel",
                columns: table => new
                {
                    LevelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostRessourceId = table.Column<long>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingLevel", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_BuildingLevel_Ressource_CostRessourceId",
                        column: x => x.CostRessourceId,
                        principalTable: "Ressource",
                        principalColumn: "RessourceId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    LevelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostRessourceId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_Level_Ressource_CostRessourceId",
                        column: x => x.CostRessourceId,
                        principalTable: "Ressource",
                        principalColumn: "RessourceId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "TechnologyLevel",
                columns: table => new
                {
                    LevelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostRessourceId = table.Column<long>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologyLevel", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_TechnologyLevel_Ressource_CostRessourceId",
                        column: x => x.CostRessourceId,
                        principalTable: "Ressource",
                        principalColumn: "RessourceId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UnitCostRessourceId = table.Column<long>(nullable: true),
                    UnitDamageType = table.Column<int>(nullable: false),
                    UnitName = table.Column<int>(nullable: false),
                    UnitStatisticsUnitStatisticsId = table.Column<long>(nullable: true),
                    UnitType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Unit_Ressource_UnitCostRessourceId",
                        column: x => x.UnitCostRessourceId,
                        principalTable: "Ressource",
                        principalColumn: "RessourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unit_UnitStatistics_UnitStatisticsUnitStatisticsId",
                        column: x => x.UnitStatisticsUnitStatisticsId,
                        principalTable: "UnitStatistics",
                        principalColumn: "UnitStatisticsId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuildGuildId = table.Column<long>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ProfilProfilId = table.Column<long>(nullable: true),
                    WorldWorldId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Guild_GuildGuildId",
                        column: x => x.GuildGuildId,
                        principalTable: "Guild",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Player_Profil_ProfilProfilId",
                        column: x => x.ProfilProfilId,
                        principalTable: "Profil",
                        principalColumn: "ProfilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Player_World_WorldWorldId",
                        column: x => x.WorldWorldId,
                        principalTable: "World",
                        principalColumn: "WorldId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "CombatReport",
                columns: table => new
                {
                    CombatreportId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ObjectReport = table.Column<string>(nullable: true),
                    ReceiverPlayerId = table.Column<long>(nullable: true),
                    Report = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatReport", x => x.CombatreportId);
                    table.ForeignKey(
                        name: "FK_CombatReport_Player_ReceiverPlayerId",
                        column: x => x.ReceiverPlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Island",
                columns: table => new
                {
                    IslandId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllRessourcesRessourceId = table.Column<long>(nullable: true),
                    CoordinatesCoordinateId = table.Column<long>(nullable: true),
                    IsCapital = table.Column<bool>(nullable: false),
                    Loyalty = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OwnerPlayerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Island", x => x.IslandId);
                    table.ForeignKey(
                        name: "FK_Island_Ressource_AllRessourcesRessourceId",
                        column: x => x.AllRessourcesRessourceId,
                        principalTable: "Ressource",
                        principalColumn: "RessourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Island_Coordinate_CoordinatesCoordinateId",
                        column: x => x.CoordinatesCoordinateId,
                        principalTable: "Coordinate",
                        principalColumn: "CoordinateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Island_Player_OwnerPlayerId",
                        column: x => x.OwnerPlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoreMessage = table.Column<string>(nullable: true),
                    MessageObject = table.Column<string>(nullable: true),
                    ReceiverPlayerId = table.Column<long>(nullable: true),
                    SenderPlayerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_Player_ReceiverPlayerId",
                        column: x => x.ReceiverPlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Player_SenderPlayerId",
                        column: x => x.SenderPlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Technology",
                columns: table => new
                {
                    TechnologyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelLevelId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PlayerPlayerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.TechnologyId);
                    table.ForeignKey(
                        name: "FK_Technology_TechnologyLevel_LevelLevelId",
                        column: x => x.LevelLevelId,
                        principalTable: "TechnologyLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Technology_Player_PlayerPlayerId",
                        column: x => x.PlayerPlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Army",
                columns: table => new
                {
                    ArmyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArmyState = table.Column<int>(nullable: false),
                    IslandIslandId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Army", x => x.ArmyId);
                    table.ForeignKey(
                        name: "FK_Army_Island_IslandIslandId",
                        column: x => x.IslandIslandId,
                        principalTable: "Island",
                        principalColumn: "IslandId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IslandIslandId = table.Column<long>(nullable: true),
                    LevelLevelId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingId);
                    table.ForeignKey(
                        name: "FK_Building_Island_IslandIslandId",
                        column: x => x.IslandIslandId,
                        principalTable: "Island",
                        principalColumn: "IslandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Building_BuildingLevel_LevelLevelId",
                        column: x => x.LevelLevelId,
                        principalTable: "BuildingLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Mage",
                columns: table => new
                {
                    MageId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IslandIslandId = table.Column<long>(nullable: true),
                    MageLevelLevelId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mage", x => x.MageId);
                    table.ForeignKey(
                        name: "FK_Mage_Island_IslandIslandId",
                        column: x => x.IslandIslandId,
                        principalTable: "Island",
                        principalColumn: "IslandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mage_MageLevel_MageLevelLevelId",
                        column: x => x.MageLevelLevelId,
                        principalTable: "Level",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Regiment",
                columns: table => new
                {
                    RegimentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArmyArmyId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    UnitUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiment", x => x.RegimentId);
                    table.ForeignKey(
                        name: "FK_Regiment_Army_ArmyArmyId",
                        column: x => x.ArmyArmyId,
                        principalTable: "Army",
                        principalColumn: "ArmyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Regiment_Unit_UnitUnitId",
                        column: x => x.UnitUnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Apprentice",
                columns: table => new
                {
                    ApprenticeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelLevelId = table.Column<long>(nullable: true),
                    MageMageId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apprentice", x => x.ApprenticeId);
                    table.ForeignKey(
                        name: "FK_Apprentice_ApprenticeLevel_LevelLevelId",
                        column: x => x.LevelLevelId,
                        principalTable: "ApprenticeLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apprentice_Mage_MageMageId",
                        column: x => x.MageMageId,
                        principalTable: "Mage",
                        principalColumn: "MageId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Apprentice");
            migrationBuilder.DropTable("Building");
            migrationBuilder.DropTable("CombatReport");
            migrationBuilder.DropTable("GuildMember");
            migrationBuilder.DropTable("Message");
            migrationBuilder.DropTable("Regiment");
            migrationBuilder.DropTable("Spell");
            migrationBuilder.DropTable("Technology");
            migrationBuilder.DropTable("ApprenticeLevel");
            migrationBuilder.DropTable("Mage");
            migrationBuilder.DropTable("BuildingLevel");
            migrationBuilder.DropTable("GuildRole");
            migrationBuilder.DropTable("Army");
            migrationBuilder.DropTable("Unit");
            migrationBuilder.DropTable("TechnologyLevel");
            migrationBuilder.DropTable("Level");
            migrationBuilder.DropTable("Island");
            migrationBuilder.DropTable("UnitStatistics");
            migrationBuilder.DropTable("Ressource");
            migrationBuilder.DropTable("Coordinate");
            migrationBuilder.DropTable("Player");
            migrationBuilder.DropTable("Guild");
            migrationBuilder.DropTable("Profil");
            migrationBuilder.DropTable("World");
        }
    }
}
