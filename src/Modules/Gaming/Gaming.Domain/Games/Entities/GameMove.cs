using Gaming.Domain.Common;
using Gaming.Domain.Games.DomainEvents;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Games.Entities;

public sealed class GameMove : Entity<GameMoveId>
{
    public GameId GameId { get; private set; }
    
    public PlayerId PlayerId { get; private set; }
    
    public FieldCoordinates Coordinates { get; private set; }

    public DateTimeOffset MovedAt { get; private set; } = DateTimeOffset.UtcNow;
    
    /// <inheritdoc />
    public GameMove(GameId gameId, PlayerId playerId, FieldCoordinates coordinates) : base(GameMoveId.CreateNew())
    {
        GameId = gameId;
        PlayerId = playerId;
        Coordinates = coordinates;
        
        RaiseEvent(new GamePlayerMovedDomainEvent(GameId, PlayerId, Coordinates, MovedAt));
    }
}