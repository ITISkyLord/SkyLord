using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ITI.SkyLord.Migrations
{
    public partial class AddTechnAndBuildingBonuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Player_User_Player_PlayerId", table: "Player");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_IdentityUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_IdentityUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropTable("Bonus");
            migrationBuilder.CreateTable(
                name: "BonusBuilding",
                columns: table => new
                {
                    BonusBuildingId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusType = table.Column<int>(nullable: false),
                    BuildingLevelLevelId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Modifier = table.Column<int>(nullable: false),
                    TargetBuilding = table.Column<int>(nullable: true),
                    TargetTechnology = table.Column<int>(nullable: true),
                    TargetUnit = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusBuilding", x => x.BonusBuildingId);
                    table.ForeignKey(
                        name: "FK_BonusBuilding_BuildingLevel_BuildingLevelLevelId",
                        column: x => x.BuildingLevelLevelId,
                        principalTable: "BuildingLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "BonusTechnology",
                columns: table => new
                {
                    BonusTechnologyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusType = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Modifier = table.Column<int>(nullable: false),
                    TechnologyLevelLevelId = table.Column<long>(nullable: true),
                    TargetBuilding = table.Column<int>(nullable: true),
                    TargetTechnology = table.Column<int>(nullable: true),
                    TargetUnit = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTechnology", x => x.BonusTechnologyId);
                    table.ForeignKey(
                        name: "FK_BonusTechnology_TechnologyLevel_TechnologyLevelLevelId",
                        column: x => x.TechnologyLevelLevelId,
                        principalTable: "TechnologyLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddColumn<bool>(
                name: "IsModel",
                table: "Unit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddForeignKey(
                name: "FK_Player_User_Player_PlayerId",
                table: "Player",
                column: "PlayerId",
                principalTable: "User_Player",
                principalColumn: "User_PlayerId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_IdentityUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_IdentityUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Player_User_Player_PlayerId", table: "Player");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_IdentityUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_IdentityUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "IsModel", table: "Unit");
            migrationBuilder.DropTable("BonusBuilding");
            migrationBuilder.DropTable("BonusTechnology");
            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    BonusId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusType = table.Column<int>(nullable: false),
                    BuildingLevelLevelId = table.Column<long>(nullable: true),
                    LevelLevelId = table.Column<long>(nullable: true),
                    Modifier = table.Column<int>(nullable: false),
                    TechnologyLevelLevelId = table.Column<long>(nullable: true),
                    UnitUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.BonusId);
                    table.ForeignKey(
                        name: "FK_Bonus_BuildingLevel_BuildingLevelLevelId",
                        column: x => x.BuildingLevelLevelId,
                        principalTable: "BuildingLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bonus_Level_LevelLevelId",
                        column: x => x.LevelLevelId,
                        principalTable: "Level",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bonus_TechnologyLevel_TechnologyLevelLevelId",
                        column: x => x.TechnologyLevelLevelId,
                        principalTable: "TechnologyLevel",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bonus_Unit_UnitUnitId",
                        column: x => x.UnitUnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_Player_User_Player_PlayerId",
                table: "Player",
                column: "PlayerId",
                principalTable: "User_Player",
                principalColumn: "User_PlayerId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_IdentityUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_IdentityUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
