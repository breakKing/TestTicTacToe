using Gaming.Domain.Players.Entities;
using Gaming.Domain.Players.ValueObjects;
using Gaming.Infrastructure.Persistence.Common;
using Gaming.Infrastructure.Persistence.Players.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.Infrastructure.Persistence.Players.Configurations;

internal sealed class PlayerConfiguration : EntityTypeConfigurationBase<Player, PlayerId>
{
    /// <inheritdoc />
    public override void ConfigureEntity(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players", "Player");
        
        builder.Property(p => p.Id)
            .HasConversion(new PlayerIdConverter())
            .HasComment("Идентификатор");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Username)
            .HasMaxLength(100)
            .HasComment("Имя пользователя");
    }
}