using Gaming.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaming.Infrastructure.Persistence.Common;

internal abstract class EntityTypeConfigurationBase<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TKey> 
    where TKey : IEquatable<TKey>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureEntity(builder);
        
        builder.Ignore(e => e.DomainEvents);
        
        builder.Property<uint>("Version")
            .IsRowVersion();
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}