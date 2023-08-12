using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.Entities;
using Gaming.Infrastructure.Persistence.Common;
using Gaming.Infrastructure.Persistence.Games.Converters;
using Gaming.Infrastructure.Persistence.Players.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.Infrastructure.Persistence.Games.Configurations;

internal sealed class GameMoveConfiguration : EntityTypeConfigurationBase<GameMove, GameMoveId>
{
    /// <inheritdoc />
    public override void ConfigureEntity(EntityTypeBuilder<GameMove> builder)
    {
        builder.ToTable("moves", "Game");
        
        builder.Property(m => m.Id)
            .HasConversion(new GameMoveIdConverter())
            .HasComment("Идентификатор");
        
        builder.HasKey(m => m.Id);

        builder.Property(m => m.GameId)
            .HasConversion(new GameIdConverter())
            .HasComment("Игра");

        builder.HasOne<Game>()
            .WithMany(g => g.Moves)
            .HasForeignKey(m => m.GameId)
            .HasConstraintName("fk_moves_games_game_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(m => m.PlayerId)
            .HasConversion(new PlayerIdConverter())
            .HasComment("Игрок");

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(m => m.PlayerId)
            .HasConstraintName("fk_moves_players_player_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(
            m => m.Coordinates,
            mb =>
            {
                mb.Property(c => c.Value1)
                    .HasColumnName("x")
                    .HasComment("X-координата хода");
                
                mb.Property(c => c.Value2)
                    .HasColumnName("y")
                    .HasComment("Y-координата хода");
            });

        builder.Property(m => m.MovedAt)
            .IsRequired()
            .HasComment("Время хода");
    }
}