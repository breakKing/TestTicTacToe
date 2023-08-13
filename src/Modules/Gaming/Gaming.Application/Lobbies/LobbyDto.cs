using Gaming.Application.Players;

namespace Gaming.Application.Lobbies;

public sealed class LobbyDto
{
    public required Guid Id { get; init; }
    public required PlayerDto InitiatorPlayer { get; init; }
    public PlayerDto? JoinedPlayer { get; init; }
}