using Gaming.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaming.Infrastructure.Persistence.Games.Converters;

internal sealed class GameMoveIdConverter : ValueConverter<GameMoveId, Guid>
{
    public GameMoveIdConverter() : 
        base(
            playerId => playerId.Value,
            value => GameMoveId.CreateFromGuid(value))
    {
    }
}