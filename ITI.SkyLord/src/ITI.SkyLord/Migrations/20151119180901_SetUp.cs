using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ITI.SkyLord.Migrations
{
    public partial class SetUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EWorld",
                columns: table => new
                {
                    WorldId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EWorld", x => x.WorldId);
                });
            migrationBuilder.CreateTable(
                name: "EPlayer",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    WorldWorldId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPlayer", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_EPlayer_EWorld_WorldWorldId",
                        column: x => x.WorldWorldId,
                        principalTable: "EWorld",
                        principalColumn: "WorldId");
                });
            migrationBuilder.CreateTable(
                name: "EProfil",
                columns: table => new
                {
                    ProfilId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EProfil", x => x.ProfilId);
                    table.ForeignKey(
                        name: "FK_EProfil_EPlayer_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "EPlayer",
                        principalColumn: "PlayerId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("EProfil");
            migrationBuilder.DropTable("EPlayer");
            migrationBuilder.DropTable("EWorld");
        }
    }
}
