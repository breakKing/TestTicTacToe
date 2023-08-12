using Gaming.Application.Lobbies;
using Gaming.Domain.Lobbies.Entities;
using Gaming.Domain.Lobbies.ValueObjects;

namespace Gaming.Infrastructure.Persistence.Lobbies.Repositories;

internal sealed class LobbyWriteRepository : ILobbyWriteRepository
{
    /// <inheritdoc />
    public async ValueTask<Lobby?> LoadAsync(LobbyId lobbyId, CancellationToken ct = default)
    {
        return null;
    }

    /// <inheritdoc />
    public void Add(Lobby lobby)
    {
    }

    /// <inheritdoc />
    public void Update(Lobby lobby)
    {
    }

    /// <inheritdoc />
    public void Delete(Lobby lobby)
    {
    }
}