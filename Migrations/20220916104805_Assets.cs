using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class Assets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Handouts",
                table: "Scenes");

            migrationBuilder.RenameColumn(
                name: "Sounds",
                table: "Scenes",
                newName: "ScenesAndEncounters");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Loop = table.Column<bool>(type: "INTEGER", nullable: true),
                    OffsetSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    Volume = table.Column<float>(type: "REAL", nullable: true),
                    SceneId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Scenes_SceneId",
                        column: x => x.SceneId,
                        principalTable: "Scenes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SceneId",
                table: "Assets",
                column: "SceneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.RenameColumn(
                name: "ScenesAndEncounters",
                table: "Scenes",
                newName: "Sounds");

            migrationBuilder.AddColumn<string>(
                name: "Handouts",
                table: "Scenes",
                type: "TEXT",
                nullable: true);
        }
    }
}
