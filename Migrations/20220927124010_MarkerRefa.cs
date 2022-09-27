using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class MarkerRefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MapMarker;");

            migrationBuilder.DropForeignKey(
                name: "FK_MapMarker_Session_SessionId",
                table: "MapMarker");

            migrationBuilder.DropIndex(
                name: "IX_MapMarker_SessionId",
                table: "MapMarker");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "MapMarker");

            migrationBuilder.AddColumn<long>(
                name: "DMPlayerId",
                table: "Session",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SceneId",
                table: "MapMarker",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSession",
                columns: table => new
                {
                    PlayersId = table.Column<long>(type: "INTEGER", nullable: false),
                    SessionsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSession", x => new { x.PlayersId, x.SessionsId });
                    table.ForeignKey(
                        name: "FK_PlayerSession_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSession_Session_SessionsId",
                        column: x => x.SessionsId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapMarker_SceneId",
                table: "MapMarker",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSession_SessionsId",
                table: "PlayerSession",
                column: "SessionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarker_Scenes_SceneId",
                table: "MapMarker",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarker_Scenes_SceneId",
                table: "MapMarker");

            migrationBuilder.DropTable(
                name: "PlayerSession");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropIndex(
                name: "IX_MapMarker_SceneId",
                table: "MapMarker");

            migrationBuilder.DropColumn(
                name: "DMPlayerId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "SceneId",
                table: "MapMarker");

            migrationBuilder.AddColumn<long>(
                name: "SessionId",
                table: "MapMarker",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MapMarker_SessionId",
                table: "MapMarker",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapMarker_Session_SessionId",
                table: "MapMarker",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id");
        }
    }
}
