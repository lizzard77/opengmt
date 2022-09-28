using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenGMT.Migrations
{
    public partial class Campaigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerSession");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentSceneId = table.Column<long>(type: "INTEGER", nullable: false),
                    DMPlayerId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_Scenes_CurrentSceneId",
                        column: x => x.CurrentSceneId,
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignPlayer",
                columns: table => new
                {
                    CampaignsId = table.Column<long>(type: "INTEGER", nullable: false),
                    PlayersId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignPlayer", x => new { x.CampaignsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_CampaignPlayer_Campaigns_CampaignsId",
                        column: x => x.CampaignsId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignPlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPlayer_PlayersId",
                table: "CampaignPlayer",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CurrentSceneId",
                table: "Campaigns",
                column: "CurrentSceneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignPlayer");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SceneId = table.Column<long>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DMPlayerId = table.Column<long>(type: "INTEGER", nullable: false)
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
                name: "IX_PlayerSession_SessionsId",
                table: "PlayerSession",
                column: "SessionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SceneId",
                table: "Session",
                column: "SceneId");
        }
    }
}
