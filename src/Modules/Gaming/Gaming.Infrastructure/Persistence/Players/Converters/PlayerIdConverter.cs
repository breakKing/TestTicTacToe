using Gaming.Domain.Players.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaming.Infrastructure.Persistence.Players.Converters;

internal sealed class PlayerIdConverter : ValueConverter<PlayerId, Guid>
{
    public PlayerIdConverter() : 
        base(
            playerId => playerId.Value,
            value => PlayerId.CreateFromGuid(value))
    {
    }
}