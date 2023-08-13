using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaming.Infrastructure.Persistence.Common.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFieldTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fields",
                schema: "Game");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fields",
                schema: "Game",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор"),
                    cells = table.Column<List<int>>(type: "jsonb", nullable: false, comment: "Состояние игрового поля"),
                    game_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Игра, к которой привязано данное поле"),
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

            migrationBuilder.CreateIndex(
                name: "ix_fields_game_id",
                schema: "Game",
                table: "fields",
                column: "game_id",
                unique: true);
        }
    }
}
