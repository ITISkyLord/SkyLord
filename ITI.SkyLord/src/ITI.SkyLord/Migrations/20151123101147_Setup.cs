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
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildRole", x => x.GuildRoleId);
                });
            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    MapId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.MapId);
                });
            migrationBuilder.CreateTable(
                name: "Profil",
                columns: table => new
                {
                    ProfilId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
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
                        principalColumn: "GuildId");
                    table.ForeignKey(
                        name: "FK_GuildMember_GuildRole_GuildRoleGuildRoleId",
                        column: x => x.GuildRoleGuildRoleId,
                        principalTable: "GuildRole",
                        principalColumn: "GuildRoleId");
                });
            migrationBuilder.CreateTable(
                name: "World",
                columns: table => new
                {
                    WorldId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MapMapId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_World", x => x.WorldId);
                    table.ForeignKey(
                        name: "FK_World_Map_MapMapId",
                        column: x => x.MapMapId,
                        principalTable: "Map",
                        principalColumn: "MapId");
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
                        principalColumn: "RessourceId");
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
                        principalColumn: "RessourceId");
                });
            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        principalColumn: "RessourceId");
                    table.ForeignKey(
                        name: "FK_Unit_UnitStatistics_UnitStatisticsUnitStatisticsId",
                        column: x => x.UnitStatisticsUnitStatisticsId,
                        principalTable: "UnitStatistics",
                        principalColumn: "UnitStatisticsId");
                });
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuildGuildId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
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
                        principalColumn: "GuildId");
                    table.ForeignKey(
                        name: "FK_Player_Profil_ProfilProfilId",
                        column: x => x.ProfilProfilId,
                        principalTable: "Profil",
                        principalColumn: "ProfilId");
                    table.ForeignKey(
                        name: "FK_Player_World_WorldWorldId",
                        column: x => x.WorldWorldId,
                        principalTable: "World",
                        principalColumn: "WorldId");
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
                    MapMapId = table.Column<long>(nullable: true),
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
                        principalColumn: "RessourceId");
                    table.ForeignKey(
                        name: "FK_Island_Coordinate_CoordinatesCoordinateId",
                        column: x => x.CoordinatesCoordinateId,
                        principalTable: "Coordinate",
                        principalColumn: "CoordinateId");
                    table.ForeignKey(
                        name: "FK_Island_Map_MapMapId",
                        column: x => x.MapMapId,
                        principalTable: "Map",
                        principalColumn: "MapId");
                    table.ForeignKey(
                        name: "FK_Island_Player_OwnerPlayerId",
                        column: x => x.OwnerPlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
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
                        principalColumn: "LevelId");
                    table.ForeignKey(
                        name: "FK_Technology_Player_PlayerPlayerId",
                        column: x => x.PlayerPlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
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
                        principalColumn: "IslandId");
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
                        principalColumn: "IslandId");
                    table.ForeignKey(
                        name: "FK_Building_BuildingLevel_LevelLevelId",
                        column: x => x.LevelLevelId,
                        principalTable: "BuildingLevel",
                        principalColumn: "LevelId");
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
                        principalColumn: "ArmyId");
                    table.ForeignKey(
                        name: "FK_Regiment_Unit_UnitUnitId",
                        column: x => x.UnitUnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Building");
            migrationBuilder.DropTable("GuildMember");
            migrationBuilder.DropTable("Regiment");
            migrationBuilder.DropTable("Technology");
            migrationBuilder.DropTable("BuildingLevel");
            migrationBuilder.DropTable("GuildRole");
            migrationBuilder.DropTable("Army");
            migrationBuilder.DropTable("Unit");
            migrationBuilder.DropTable("TechnologyLevel");
            migrationBuilder.DropTable("Island");
            migrationBuilder.DropTable("UnitStatistics");
            migrationBuilder.DropTable("Ressource");
            migrationBuilder.DropTable("Coordinate");
            migrationBuilder.DropTable("Player");
            migrationBuilder.DropTable("Guild");
            migrationBuilder.DropTable("Profil");
            migrationBuilder.DropTable("World");
            migrationBuilder.DropTable("Map");
        }
    }
}
