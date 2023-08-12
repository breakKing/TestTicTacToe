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

internal sealed class GameIdWithNullConverter : ValueConverter<GameId?, Guid?>
{
    public GameIdWithNullConverter() : 
        base(
            playerId => playerId == null ? null : playerId.Value,
            value => value.HasValue ? null : GameId.CreateFromGuid(value!.Value))
    {
    }
}