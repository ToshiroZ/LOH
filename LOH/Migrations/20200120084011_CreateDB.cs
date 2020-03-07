using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LOH.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monster",
                columns: table => new
                {
                    MonsterID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Gold = table.Column<int>(nullable: false),
                    Experience = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Health = table.Column<int>(nullable: false),
                    Damage = table.Column<int>(nullable: false),
                    Defense = table.Column<int>(nullable: false),
                    Mana = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monster", x => x.MonsterID);
                });

            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    BattleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(nullable: false),
                    Turn = table.Column<int>(nullable: false),
                    MonsterID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.BattleID);
                    table.ForeignKey(
                        name: "FK_Battle_Monster_MonsterID",
                        column: x => x.MonsterID,
                        principalTable: "Monster",
                        principalColumn: "MonsterID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    PlayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gold = table.Column<int>(nullable: false),
                    Experience = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Health = table.Column<int>(nullable: false),
                    Damage = table.Column<int>(nullable: false),
                    Defense = table.Column<int>(nullable: false),
                    Mana = table.Column<int>(nullable: false),
                    PlayerName = table.Column<string>(nullable: true),
                    LastWorkTime = table.Column<DateTime>(nullable: false),
                    BattleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_User_Battle_BattleID",
                        column: x => x.BattleID,
                        principalTable: "Battle",
                        principalColumn: "BattleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battle_MonsterID",
                table: "Battle",
                column: "MonsterID");

            migrationBuilder.CreateIndex(
                name: "IX_User_BattleID",
                table: "User",
                column: "BattleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Battle");

            migrationBuilder.DropTable(
                name: "Monster");
        }
    }
}
