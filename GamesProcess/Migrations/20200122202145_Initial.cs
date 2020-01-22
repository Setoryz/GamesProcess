using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesProcess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamesClass",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesClass", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    GamesClassID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Game_GamesClass_GamesClassID",
                        column: x => x.GamesClassID,
                        principalTable: "GamesClass",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EventNumber = table.Column<int>(nullable: false),
                    GameID = table.Column<int>(nullable: false),
                    MachineValues = table.Column<string>(nullable: true),
                    WinningValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Event_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_GameID",
                table: "Event",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GamesClassID",
                table: "Game",
                column: "GamesClassID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "GamesClass");
        }
    }
}
