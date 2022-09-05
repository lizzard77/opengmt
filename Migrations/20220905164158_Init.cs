using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ArmorClass = table.Column<int>(type: "INTEGER", nullable: false),
                    WalkingSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    AdditionalSpeed = table.Column<string>(type: "TEXT", nullable: true),
                    SkillsText = table.Column<string>(type: "TEXT", nullable: true),
                    DamageResitances = table.Column<string>(type: "TEXT", nullable: true),
                    ConditionImmunities = table.Column<string>(type: "TEXT", nullable: true),
                    Senses = table.Column<string>(type: "TEXT", nullable: true),
                    Languages = table.Column<string>(type: "TEXT", nullable: true),
                    Challenge = table.Column<string>(type: "TEXT", nullable: true),
                    XP = table.Column<int>(type: "INTEGER", nullable: false),
                    ProficiencyBonus = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialTraits = table.Column<string>(type: "TEXT", nullable: true),
                    Actions = table.Column<string>(type: "TEXT", nullable: true),
                    Initiative = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilitiesString = table.Column<string>(type: "TEXT", nullable: true),
                    SkillsString = table.Column<string>(type: "TEXT", nullable: true),
                    InventoryString = table.Column<string>(type: "TEXT", nullable: true),
                    MoneyString = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<string>(type: "TEXT", nullable: true),
                    EyeColor = table.Column<string>(type: "TEXT", nullable: true),
                    SkinColor = table.Column<string>(type: "TEXT", nullable: true),
                    HairColor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
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
                    table.PrimaryKey("PK_Maps", x => x.Id);
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
                name: "MapMarker",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatureId = table.Column<long>(type: "INTEGER", nullable: false),
                    MapId = table.Column<long>(type: "INTEGER", nullable: false),
                    X = table.Column<decimal>(type: "TEXT", nullable: false),
                    Y = table.Column<decimal>(type: "TEXT", nullable: false),
                    Initiative = table.Column<long>(type: "INTEGER", nullable: false),
                    Visible = table.Column<bool>(type: "INTEGER", nullable: false),
                    Light = table.Column<bool>(type: "INTEGER", nullable: false),
                    VisionNormal = table.Column<long>(type: "INTEGER", nullable: false),
                    VisionDim = table.Column<long>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Reach = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapMarker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapMarker_Creatures_CreatureId",
                        column: x => x.CreatureId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapMarker_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scenes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MapId = table.Column<long>(type: "INTEGER", nullable: true),
                    StrongStart = table.Column<string>(type: "TEXT", nullable: true),
                    SecretsAndHints = table.Column<string>(type: "TEXT", nullable: true),
                    PhantasticLocations = table.Column<string>(type: "TEXT", nullable: true),
                    MagicItems = table.Column<string>(type: "TEXT", nullable: true),
                    Handouts = table.Column<string>(type: "TEXT", nullable: true),
                    Sounds = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scenes_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CreatureScene",
                columns: table => new
                {
                    CreaturesId = table.Column<long>(type: "INTEGER", nullable: false),
                    ScenesId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureScene", x => new { x.CreaturesId, x.ScenesId });
                    table.ForeignKey(
                        name: "FK_CreatureScene_Creatures_CreaturesId",
                        column: x => x.CreaturesId,
                        principalTable: "Creatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreatureScene_Scenes_ScenesId",
                        column: x => x.ScenesId,
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SceneId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_Scenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attack_CreatureId",
                table: "Attack",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureScene_ScenesId",
                table: "CreatureScene",
                column: "ScenesId");

            migrationBuilder.CreateIndex(
                name: "IX_MapMarker_CreatureId",
                table: "MapMarker",
                column: "CreatureId");

            migrationBuilder.CreateIndex(
                name: "IX_MapMarker_MapId",
                table: "MapMarker",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_MapId",
                table: "Scenes",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SceneId",
                table: "Session",
                column: "SceneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attack");

            migrationBuilder.DropTable(
                name: "CreatureScene");

            migrationBuilder.DropTable(
                name: "MapMarker");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Creatures");

            migrationBuilder.DropTable(
                name: "Scenes");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
