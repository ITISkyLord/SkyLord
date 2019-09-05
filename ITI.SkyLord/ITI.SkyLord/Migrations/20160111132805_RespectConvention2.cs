using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITI.SkyLord.Migrations
{
    public partial class RespectConvention2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_ArmyEvent_Army_armyArmyId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_ArmyEvent_Island_destinationIslandId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_TechnologyEvent_Technology_technologyTechnologyId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_UpgradeEvent_Building_buildingToUpgradeBuildingId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_Player_User_Player_PlayerId", table: "Player");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_IdentityUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_IdentityUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.AddForeignKey(
                name: "FK_ArmyEvent_Army_ArmyArmyId",
                table: "Event",
                column: "ArmyArmyId",
                principalTable: "Army",
                principalColumn: "ArmyId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ArmyEvent_Island_DestinationIslandId",
                table: "Event",
                column: "DestinationIslandId",
                principalTable: "Island",
                principalColumn: "IslandId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyEvent_Technology_TechnologyTechnologyId",
                table: "Event",
                column: "TechnologyTechnologyId",
                principalTable: "Technology",
                principalColumn: "TechnologyId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UpgradeEvent_Building_BuildingToUpgradeBuildingId",
                table: "Event",
                column: "BuildingToUpgradeBuildingId",
                principalTable: "Building",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Restrict);
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
            migrationBuilder.RenameColumn(
                name: "buildingToUpgradeBuildingId",
                table: "Event",
                newName: "BuildingToUpgradeBuildingId");
            migrationBuilder.RenameColumn(
                name: "technologyTechnologyId",
                table: "Event",
                newName: "TechnologyTechnologyId");
            migrationBuilder.RenameColumn(
                name: "destinationIslandId",
                table: "Event",
                newName: "DestinationIslandId");
            migrationBuilder.RenameColumn(
                name: "armyMovement",
                table: "Event",
                newName: "ArmyMovement");
            migrationBuilder.RenameColumn(
                name: "armyArmyId",
                table: "Event",
                newName: "ArmyArmyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_ArmyEvent_Army_ArmyArmyId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_ArmyEvent_Island_DestinationIslandId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_TechnologyEvent_Technology_TechnologyTechnologyId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_UpgradeEvent_Building_BuildingToUpgradeBuildingId", table: "Event");
            migrationBuilder.DropForeignKey(name: "FK_Player_User_Player_PlayerId", table: "Player");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_IdentityUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_IdentityUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.AddForeignKey(
                name: "FK_ArmyEvent_Army_armyArmyId",
                table: "Event",
                column: "armyArmyId",
                principalTable: "Army",
                principalColumn: "ArmyId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ArmyEvent_Island_destinationIslandId",
                table: "Event",
                column: "destinationIslandId",
                principalTable: "Island",
                principalColumn: "IslandId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_TechnologyEvent_Technology_technologyTechnologyId",
                table: "Event",
                column: "technologyTechnologyId",
                principalTable: "Technology",
                principalColumn: "TechnologyId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UpgradeEvent_Building_buildingToUpgradeBuildingId",
                table: "Event",
                column: "buildingToUpgradeBuildingId",
                principalTable: "Building",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Restrict);
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
            migrationBuilder.RenameColumn(
                name: "BuildingToUpgradeBuildingId",
                table: "Event",
                newName: "buildingToUpgradeBuildingId");
            migrationBuilder.RenameColumn(
                name: "TechnologyTechnologyId",
                table: "Event",
                newName: "technologyTechnologyId");
            migrationBuilder.RenameColumn(
                name: "DestinationIslandId",
                table: "Event",
                newName: "destinationIslandId");
            migrationBuilder.RenameColumn(
                name: "ArmyMovement",
                table: "Event",
                newName: "armyMovement");
            migrationBuilder.RenameColumn(
                name: "ArmyArmyId",
                table: "Event",
                newName: "armyArmyId");
        }
    }
}
