using Gaming.Domain.Lobbies.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaming.Infrastructure.Persistence.Lobbies.Converters;

internal sealed class LobbyIdConverter : ValueConverter<LobbyId, Guid>
{
    public LobbyIdConverter() : 
        base(
            playerId => playerId.Value,
            value => LobbyId.CreateFromGuid(value))
    {
    }
}

internal sealed class LobbyIdWithNullConverter : ValueConverter<LobbyId?, Guid?>
{
    public LobbyIdWithNullConverter() : 
        base(
            playerId => playerId == null ? null : playerId.Value,
            value => value.HasValue ? null : LobbyId.CreateFromGuid(value!.Value))
    {
    }
}