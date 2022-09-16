using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class AssetsUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uri",
                table: "Assets",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uri",
                table: "Assets");
        }
    }
}
