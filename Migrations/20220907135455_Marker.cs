using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class Marker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapMarker_Session_SessionId",
                table: "MapMarker");

            migrationBuilder.DropIndex(
                name: "IX_MapMarker_SessionId",
                table: "MapMarker");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "MapMarker");
        }
    }
}
