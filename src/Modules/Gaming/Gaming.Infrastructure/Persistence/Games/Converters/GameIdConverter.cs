using Gaming.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaming.Infrastructure.Persistence.Games.Converters;

internal sealed class GameIdConverter : ValueConverter<GameId, Guid>
{
    public GameIdConverter() : 
        base(
            playerId => playerId.Value,
            value => GameId.CreateFromGuid(value))
    {
    }
}