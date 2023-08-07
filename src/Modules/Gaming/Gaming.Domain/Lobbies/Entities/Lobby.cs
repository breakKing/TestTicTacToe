using Common.Domain.Primitives;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;
using ErrorOr;
using Gaming.Domain.Lobbies.DomainEvents;

namespace Gaming.Domain.Lobbies.Entities;

public sealed class Lobby : AggregateRoot<LobbyId>
{
    private const string WrongPlayerJoinErrorDescription =
        "Данный игрок не может присоединиться к лобби, так как он уже является его владельцем";
    
    private const string WrongPlayerLeaveErrorDescription = "Данный игрок не состоит в этом лобби";
    
    private const string NoSpaceForPlayerJoinErrorDescription = "В данном лобби нет мест";
    
    private const string InsufficientPlayersForStartErrorDescription = "В данном лобби недостаточно игроков для старта игры";
    
    /// <summary>
    /// Идентификатор игрока, который создал лобби
    /// </summary>
    public PlayerId InitiatorPlayerId { get; private set; }
    
    /// <summary>
    /// Идентификатор присоединившегося игрока
    /// </summary>
    public PlayerId? JoinedPlayerId { get; private set; }
    
    public DateTimeOffset? GameStartedAt { get; private set; }
    
    /// <inheritdoc />
    public Lobby(PlayerId initiatorPlayerId) : base(LobbyId.Create())
    {
        InitiatorPlayerId = initiatorPlayerId;
        RaiseEvent(new LobbyCreatedDomainEvent(Id, InitiatorPlayerId));
    }

    public ErrorOr<bool> PlayerJoin(PlayerId playerId)
    {
        if (playerId == InitiatorPlayerId)
        {
            return Error.Validation(description: WrongPlayerJoinErrorDescription);
        }

        if (JoinedPlayerId is not null)
        {
            return Error.Validation(description: NoSpaceForPlayerJoinErrorDescription);
        }

        JoinedPlayerId = playerId;
        RaiseEvent(new LobbyPlayerJoinedDomainEvent(Id, playerId));

        return true;
    }

    public ErrorOr<bool> PlayerLeave(PlayerId playerId)
    {
        if (playerId == InitiatorPlayerId)
        {
            RaiseEvent(new LobbyDisbandedDomainEvent(Id));
        }
        
        else if (playerId == JoinedPlayerId)
        {
            JoinedPlayerId = null;
            RaiseEvent(new LobbyPlayerLeavedDomainEvent(Id, playerId));
        }

        else
        {
            return Error.Validation(description: WrongPlayerLeaveErrorDescription);
        }

        return true;
    }

    public ErrorOr<bool> LockAndStartGame()
    {
        if (JoinedPlayerId is null)
        {
            return Error.Validation(description: InsufficientPlayersForStartErrorDescription);
        }
        
        GameStartedAt = DateTimeOffset.UtcNow;
        
        RaiseEvent(new LobbyLockedAndStartedGame(
            Id,
            InitiatorPlayerId,
            JoinedPlayerId,
            GameStartedAt.Value));

        return true;
    }
}