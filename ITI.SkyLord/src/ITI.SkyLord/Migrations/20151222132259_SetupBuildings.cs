using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ITI.SkyLord.Migrations
{
    public partial class SetupBuildings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Building_BuildingLevel_LevelLevelId", table: "Building");
            migrationBuilder.DropForeignKey(name: "FK_Player_User_Player_PlayerId", table: "Player");
            migrationBuilder.DropForeignKey(name: "FK_Technology_TechnologyLevel_LevelLevelId", table: "Technology");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_IdentityUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_IdentityUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropTable("BuildingLevel");
            migrationBuilder.DropTable("TechnologyLevel");
            migrationBuilder.CreateTable(
                name: "BuildingDefinition",
                columns: table => new
                {
                    BuildingDefinitionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingDefinition", x => x.BuildingDefinitionId);
                });
            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    RequirementId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.RequirementId);
                    table.ForeignKey(
                        name: "FK_Requirement_Level_RequirementId",
                        column: x => x.RequirementId,
                        principalTable: "Level",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.AddColumn<long>(
                name: "BuildingDefinitionBuildingDefinitionId",
                table: "Level",
                nullable: true);
            migrationBuilder.AddColumn<long>(
                name: "RequirementRequirementId",
                table: "ApprenticeLevel",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_ApprenticeLevel_Requirement_RequirementRequirementId",
                table: "ApprenticeLevel",
                column: "RequirementRequirementId",
                principalTable: "Requirement",
                principalColumn: "RequirementId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Building_BuildingLevel_LevelLevelId",
                table: "Building",
                column: "LevelLevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_BuildingLevel_BuildingDefinition_BuildingDefinitionBuildingDefinitionId",
                table: "Level",
                column: "BuildingDefinitionBuildingDefinitionId",
                principalTable: "BuildingDefinition",
                principalColumn: "BuildingDefinitionId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Player_User_Player_PlayerId",
                table: "Player",
                column: "PlayerId",
                principalTable: "User_Player",
                principalColumn: "User_PlayerId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Technology_TechnologyLevel_LevelLevelId",
                table: "Technology",
                column: "LevelLevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
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
            migrationBuilder.DropForeignKey(name: "FK_ApprenticeLevel_Requirement_RequirementRequirementId", table: "ApprenticeLevel");
            migrationBuilder.DropForeignKey(name: "FK_Building_BuildingLevel_LevelLevelId", table: "Building");
            migrationBuilder.DropForeignKey(name: "FK_BuildingLevel_BuildingDefinition_BuildingDefinitionBuildingDefinitionId", table: "Level");
            migrationBuilder.DropForeignKey(name: "FK_Player_User_Player_PlayerId", table: "Player");
            migrationBuilder.DropForeignKey(name: "FK_Technology_TechnologyLevel_LevelLevelId", table: "Technology");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_IdentityUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_IdentityUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "BuildingDefinitionBuildingDefinitionId", table: "Level");
            migrationBuilder.DropColumn(name: "RequirementRequirementId", table: "ApprenticeLevel");
            migrationBuilder.DropTable("BuildingDefinition");
            migrationBuilder.DropTable("Requirement");
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
            migrationBuilder.AddForeignKey(
                name: "FK_Building_BuildingLevel_LevelLevelId",
                table: "Building",
                column: "LevelLevelId",
                principalTable: "BuildingLevel",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Player_User_Player_PlayerId",
                table: "Player",
                column: "PlayerId",
                principalTable: "User_Player",
                principalColumn: "User_PlayerId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Technology_TechnologyLevel_LevelLevelId",
                table: "Technology",
                column: "LevelLevelId",
                principalTable: "TechnologyLevel",
                principalColumn: "LevelId",
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
