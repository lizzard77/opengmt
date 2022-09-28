using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class ForeignKeysAdjusted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Scenes_CurrentSceneId",
                table: "Campaigns");

            migrationBuilder.AlterColumn<long>(
                name: "CurrentSceneId",
                table: "Campaigns",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Scenes_CurrentSceneId",
                table: "Campaigns",
                column: "CurrentSceneId",
                principalTable: "Scenes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Scenes_CurrentSceneId",
                table: "Campaigns");

            migrationBuilder.AlterColumn<long>(
                name: "CurrentSceneId",
                table: "Campaigns",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Scenes_CurrentSceneId",
                table: "Campaigns",
                column: "CurrentSceneId",
                principalTable: "Scenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
