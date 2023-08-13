using Gaming.Domain.Lobbies.Entities;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.Entities;
using Gaming.Infrastructure.Persistence.Common;
using Gaming.Infrastructure.Persistence.Lobbies.Converters;
using Gaming.Infrastructure.Persistence.Players.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.Infrastructure.Persistence.Lobbies.Configurations;

internal sealed class LobbyConfiguration : EntityTypeConfigurationBase<Lobby, LobbyId>
{
    /// <inheritdoc />
    public override void ConfigureEntity(EntityTypeBuilder<Lobby> builder)
    {
        builder.ToTable("lobbies", "Lobby");
        
        builder.Property(l => l.Id)
            .HasConversion(new LobbyIdConverter())
            .HasComment("Идентификатор");
        
        builder.HasKey(l => l.Id);
        
        builder.Property(l => l.InitiatorPlayerId)
            .HasConversion(new PlayerIdConverter())
            .HasComment("Лидер лобби");

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(l => l.InitiatorPlayerId)
            .HasConstraintName("fk_lobbies_players_initiator_player_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(l => l.JoinedPlayerId)
            .HasConversion(new PlayerIdConverter())
            .IsRequired(false)
            .HasComment("Присоединившийся игрок");

        builder.HasOne<Player>()
            .WithMany()
            .HasForeignKey(l => l.JoinedPlayerId)
            .HasConstraintName("fk_lobbies_players_joined_player_id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(l => l.IsLocked)
            .IsRequired()
            .HasComment("Заблокировано ли лобби его лидером");
    }
}