using Common.Api;

namespace Gaming.Presentation.Endpoints.Lobbies;

public sealed class LobbyGroup : EndpointGroupBase
{
    /// <inheritdoc />
    public LobbyGroup() : base("Lobby", "lobbies")
    {
    }
}