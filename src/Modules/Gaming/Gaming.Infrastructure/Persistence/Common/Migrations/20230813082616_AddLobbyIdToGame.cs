using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaming.Infrastructure.Persistence.Common.Migrations
{
    /// <inheritdoc />
    public partial class AddLobbyIdToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "started_from_lobby_id",
                schema: "Game",
                table: "games",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Лобби, из которого игра была запущена");

            migrationBuilder.CreateIndex(
                name: "ix_games_started_from_lobby_id",
                schema: "Game",
                table: "games",
                column: "started_from_lobby_id");

            migrationBuilder.AddForeignKey(
                name: "fk_games_lobbies_started_from_lobby_id",
                schema: "Game",
                table: "games",
                column: "started_from_lobby_id",
                principalSchema: "Lobby",
                principalTable: "lobbies",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_games_lobbies_started_from_lobby_id",
                schema: "Game",
                table: "games");

            migrationBuilder.DropIndex(
                name: "ix_games_started_from_lobby_id",
                schema: "Game",
                table: "games");

            migrationBuilder.DropColumn(
                name: "started_from_lobby_id",
                schema: "Game",
                table: "games");
        }
    }
}
