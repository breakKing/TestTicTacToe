using Common.Api;

namespace Gaming.Presentation.Endpoints.Games;

public sealed class GameGroup : EndpointGroupBase
{
    /// <inheritdoc />
    public GameGroup() : base("Game", "games")
    {
    }
}