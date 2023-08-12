using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaming.Infrastructure.Persistence.Common.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Game");

            migrationBuilder.EnsureSchema(
                name: "Lobby");

            migrationBuilder.EnsureSchema(
                name: "Player");

            migrationBuilder.CreateTable(
                name: "players",
                schema: "Player",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор"),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Имя пользователя"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                schema: "Game",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор"),
                    first_player_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Первый игрок"),
                    second_player_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Второй игрок"),
                    last_move_player_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Игрок, сделавший последний ход"),
                    started_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата и время начала игры"),
                    finished_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: "Дата и время окончания игры"),
                    result = table.Column<int>(type: "integer", nullable: false, comment: "Результат игры"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.id);
                    table.ForeignKey(
                        name: "fk_games_players_first_player_id",
                        column: x => x.first_player_id,
                        principalSchema: "Player",
                        principalTable: "players",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_games_players_last_move_player_id",
                        column: x => x.last_move_player_id,
                        principalSchema: "Player",
                        principalTable: "players",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_games_players_second_player_id",
                        column: x => x.second_player_id,
                        principalSchema: "Player",
                        principalTable: "players",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lobbies",
                schema: "Lobby",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор"),
                    initiator_player_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Лидер лобби"),
                    joined_player_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Присоединившийся игрок"),
                    is_locked = table.Column<bool>(type: "boolean", nullable: false, comment: "Заблокировано ли лобби его лидером"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lobbies", x => x.id);
                    table.ForeignKey(
                        name: "fk_lobbies_players_initiator_player_id",
                        column: x => x.initiator_player_id,
                        principalSchema: "Player",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lobbies_players_joined_player_id",
                        column: x => x.joined_player_id,
                        principalSchema: "Player",
                        principalTable: "players",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "fields",
                schema: "Game",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор"),
                    game_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Игра, к которой привязано данное поле"),
                    cells = table.Column<List<int>>(type: "jsonb", nullable: false, comment: "Состояние игрового поля"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fields", x => x.id);
                    table.ForeignKey(
                        name: "fk_fields_games_game_id",
                        column: x => x.game_id,
                        principalSchema: "Game",
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "moves",
                schema: "Game",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор"),
                    game_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Игра"),
                    player_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Игрок"),
                    x = table.Column<int>(type: "integer", nullable: false, comment: "X-координата хода"),
                    y = table.Column<int>(type: "integer", nullable: false, comment: "Y-координата хода"),
                    moved_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Время хода"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_moves", x => x.id);
                    table.ForeignKey(
                        name: "fk_moves_games_game_id",
                        column: x => x.game_id,
                        principalSchema: "Game",
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_moves_players_player_id",
                        column: x => x.player_id,
                        principalSchema: "Player",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_fields_game_id",
                schema: "Game",
                table: "fields",
                column: "game_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_games_first_player_id",
                schema: "Game",
                table: "games",
                column: "first_player_id");

            migrationBuilder.CreateIndex(
                name: "ix_games_last_move_player_id",
                schema: "Game",
                table: "games",
                column: "last_move_player_id");

            migrationBuilder.CreateIndex(
                name: "ix_games_second_player_id",
                schema: "Game",
                table: "games",
                column: "second_player_id");

            migrationBuilder.CreateIndex(
                name: "ix_lobbies_initiator_player_id",
                schema: "Lobby",
                table: "lobbies",
                column: "initiator_player_id");

            migrationBuilder.CreateIndex(
                name: "ix_lobbies_joined_player_id",
                schema: "Lobby",
                table: "lobbies",
                column: "joined_player_id");

            migrationBuilder.CreateIndex(
                name: "ix_moves_game_id",
                schema: "Game",
                table: "moves",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_moves_player_id",
                schema: "Game",
                table: "moves",
                column: "player_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fields",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "lobbies",
                schema: "Lobby");

            migrationBuilder.DropTable(
                name: "moves",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "games",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "players",
                schema: "Player");
        }
    }
}
