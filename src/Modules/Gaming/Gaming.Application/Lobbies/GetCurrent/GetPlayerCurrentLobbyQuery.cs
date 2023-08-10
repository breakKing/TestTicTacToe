using Gaming.Application.Common.Handling;

namespace Gaming.Application.Lobbies.GetCurrent;

public sealed record GetPlayerCurrentLobbyQuery(Guid PlayerId) : IQuery<LobbyDto>;