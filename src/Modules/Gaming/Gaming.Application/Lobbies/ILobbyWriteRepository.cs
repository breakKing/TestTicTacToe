using Gaming.Domain.Lobbies.Entities;

namespace Gaming.Application.Lobbies;

public interface ILobbyWriteRepository
{
    void Add(Lobby lobby);
}