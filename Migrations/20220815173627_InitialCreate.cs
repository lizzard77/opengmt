using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrlDM = table.Column<string>(type: "TEXT", nullable: true),
                    ImageWidth = table.Column<long>(type: "INTEGER", nullable: false),
                    ImageHeight = table.Column<long>(type: "INTEGER", nullable: false),
                    WidthInSquares = table.Column<decimal>(type: "TEXT", nullable: false),
                    HeightInSquares = table.Column<decimal>(type: "TEXT", nullable: false),
                    FootPerSquare = table.Column<decimal>(type: "TEXT", nullable: false),
                    GridOpacity = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scenes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MapId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scenes_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Creatures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HPMax = table.Column<int>(type: "INTEGER", nullable: false),
                    HPTemporary = table.Column<int>(type: "INTEGER", nullable: false),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Reach = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    SheetURL = table.Column<string>(type: "TEXT", nullable: true),
                    SceneId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creatures_Scenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "Scenes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CurrentSceneId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Scenes_CurrentSceneId",
                        column: x => x.CurrentSceneId,
                        principalTable: "Scenes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HitModifier = table.Column<string>(type: "TEXT", nullable: true),
                    Damage = table.Column<string>(type: "TEXT", nullable: true),
                    DamageType = table.Column<string>(type: "TEXT", nullable: true),
                    Range = table.Column<string>(type: "TEXT", nullable: true),
                    CreatureId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attack_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CreatureState",
                columns: table => new
                {
                    CreatureStateId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatureId = table.Column<long>(type: "INTEGER", nullable: true),
                    MapId = table.Column<long>(type: "INTEGER", nullable: true),
                    X = table.Column<long>(type: "INTEGER", nullable: false),
                    Y = table.Column<long>(type: "INTEGER", nullable: false),
                    Initiative = table.Column<long>(type: "INTEGER", nullable: false),
                    Visible = table.Column<bool>(type: "INTEGER", nullable: false),
                    VisionNormal = table.Column<long>(type: "INTEGER", nullable: false),
                    VisionDim = table.Column<long>(type: "INTEGER", nullable: false),
                    SceneId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureState", x => x.CreatureStateId);
                    table.ForeignKey(
                        name: "FK_CreatureState_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreatureState_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CreatureState_Scenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "Scenes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attack_CreatureId",
                table: "Attack",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_SceneId",
                table: "Creatures",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureState_CreatureId",
                table: "CreatureState",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureState_MapId",
                table: "CreatureState",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureState_SceneId",
                table: "CreatureState",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_MapId",
                table: "Scenes",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CurrentSceneId",
                table: "Sessions",
                column: "CurrentSceneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attack");

            migrationBuilder.DropTable(
                name: "CreatureState");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Creatures");

            migrationBuilder.DropTable(
                name: "Scenes");

            migrationBuilder.DropTable(
                name: "Map");
        }
    }
}
