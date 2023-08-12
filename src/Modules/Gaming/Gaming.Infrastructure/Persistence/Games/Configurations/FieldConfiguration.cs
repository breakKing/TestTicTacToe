using Gaming.Domain.Games.Entities;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Infrastructure.Persistence.Common;
using Gaming.Infrastructure.Persistence.Games.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.Infrastructure.Persistence.Games.Configurations;

internal sealed class FieldConfiguration : EntityTypeConfigurationBase<Field, FieldId>
{
    /// <inheritdoc />
    public override void ConfigureEntity(EntityTypeBuilder<Field> builder)
    {
        builder.ToTable("fields", "Game");
        
        builder.Property(f => f.Id)
            .HasConversion(new FieldIdConverter())
            .HasComment("Идентификатор");
        
        builder.HasKey(f => f.Id)
            .HasName("pk_fields");
        
        builder.Property(f => f.Cells)
            .HasField("_cells")
            .HasColumnType("jsonb")
            .IsRequired()
            .HasConversion(new FieldCellsConverter())
            .HasComment("Состояние игрового поля");
        
        builder.Property(f => f.GameId)
            .HasConversion(new GameIdConverter())
            .HasComment("Игра, к которой привязано данное поле");

        builder.HasOne<Game>()
            .WithOne(g => g.Field)
            .HasForeignKey<Field>(f => f.GameId)
            .HasConstraintName("fk_fields_games_game_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}