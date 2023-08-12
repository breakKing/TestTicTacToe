using Gaming.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaming.Infrastructure.Persistence.Games.Converters;

internal sealed class FieldIdConverter : ValueConverter<FieldId, Guid>
{
    public FieldIdConverter() : 
        base(
            playerId => playerId.Value,
            value => FieldId.CreateFromGuid(value))
    {
    }
}

internal sealed class FieldIdWithNullConverter : ValueConverter<FieldId?, Guid?>
{
    public FieldIdWithNullConverter() : 
        base(
            playerId => playerId == null ? null : playerId.Value,
            value => value.HasValue ? null : FieldId.CreateFromGuid(value!.Value))
    {
    }
}