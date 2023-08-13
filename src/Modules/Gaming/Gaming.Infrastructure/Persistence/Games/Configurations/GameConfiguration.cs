using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Lobbies.Entities;
using Gaming.Domain.Players.Entities;
using Gaming.Infrastructure.Persistence.Common;
using Gaming.Infrastructure.Persistence.Games.Converters;
using Gaming.Infrastructure.Persistence.Lobbies.Converters;
using Gaming.Infrastructure.Persistence.Players.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.Infrastructure.Persistence.Games.Configurations;

internal sealed class GameConfiguration : EntityTypeConfigurationBase<Game, GameId>
{
    /// <inheritdoc />
    public override void ConfigureEntity(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games", "Game");
        
        builder.Property(g => g.Id)
            .HasConversion(new GameIdConverter())
            .HasComment("Идентификатор");
        
        builder.HasKey(g => g.Id);
        
        builder.Property(g => g.FirstPlayerId)
            .HasConversion(new PlayerIdConverter())
            .HasComment("Первый игрок");

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(g => g.FirstPlayerId)
            .HasConstraintName("fk_games_players_first_player_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(g => g.SecondPlayerId)
            .HasConversion(new PlayerIdConverter())
            .HasComment("Второй игрок");

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(l => l.SecondPlayerId)
            .HasConstraintName("fk_games_players_second_player_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(g => g.LastMovePlayerId)
            .HasConversion(new PlayerIdConverter())
            .HasComment("Игрок, сделавший последний ход");

        builder.Ignore(g => g.Field);

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(l => l.LastMovePlayerId)
            .HasConstraintName("fk_games_players_last_move_player_id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(g => g.StartedAt)
            .IsRequired()
            .HasComment("Дата и время начала игры");
        
        builder.Property(g => g.FinishedAt)
            .IsRequired(false)
            .HasComment("Дата и время окончания игры");

        builder.Property(g => g.Result)
            .HasConversion(new GameResultConverter())
            .HasComment("Результат игры");

        builder.Navigation(g => g.Moves)
            .HasField("_moves")
            .AutoInclude();

        builder.HasMany(g => g.Moves)
            .WithOne()
            .HasForeignKey(m => m.GameId)
            .HasConstraintName("fk_moves_games_game_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Ignore(g => g.WinnerPlayerId);

        builder.Ignore(g => g.LoserPlayerId);
    }
}