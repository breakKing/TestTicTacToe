using Gaming.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaming.Infrastructure.Persistence.Games.Converters;

internal sealed class GameResultConverter : ValueConverter<GameResult, int>
{
    public GameResultConverter() : 
        base(
            playerId => playerId.Value,
            value => GameResult.CreateFromValue(value))
    {
    }
}