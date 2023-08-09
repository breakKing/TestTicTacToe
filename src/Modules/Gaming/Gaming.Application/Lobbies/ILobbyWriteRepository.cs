using Gaming.Domain.Lobbies.Entities;
using Gaming.Domain.Lobbies.ValueObjects;

namespace Gaming.Application.Lobbies;

public interface ILobbyWriteRepository
{
    ValueTask<Lobby?> LoadAsync(LobbyId lobbyId, CancellationToken ct = default);
    
    void Add(Lobby lobby);

    void Update(Lobby lobby);
    
    void Delete(Lobby lobby);
}