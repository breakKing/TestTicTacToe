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