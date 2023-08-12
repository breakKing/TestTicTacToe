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

    /// <summary>
    /// Не использовать, нужно для обхода ограничений EF Core 
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="playerId"></param>
    private GameMove(GameId gameId, PlayerId playerId) : base(GameMoveId.CreateNew())
    {
        GameId = gameId;
        PlayerId = playerId;

        FieldCoordinates.TryCreate(0, 0, out var coordinates);

        Coordinates = coordinates;
    }
    
    /// <inheritdoc />
    public GameMove(GameId gameId, PlayerId playerId, FieldCoordinates coordinates) : this(gameId, playerId)
    {
        Coordinates = coordinates;
        
        RaiseEvent(new GamePlayerMovedDomainEvent(GameId, PlayerId, Coordinates, MovedAt));
    }
}