using ErrorOr;
using Gaming.Domain.Common;
using Gaming.Domain.Lobbies.DomainEvents;
using Gaming.Domain.Lobbies.ValueObjects;
using Gaming.Domain.Players.ValueObjects;

namespace Gaming.Domain.Lobbies.Entities;

public sealed class Lobby : AggregateRoot<LobbyId>
{
    private const string WrongPlayerJoinErrorDescription =
        "Данный игрок не может присоединиться к лобби, так как он уже является его владельцем";
    
    private const string WrongPlayerLeaveErrorDescription = "Данный игрок не состоит в этом лобби";
    
    private const string NoSpaceForPlayerJoinErrorDescription = "В данном лобби нет мест";
    
    private const string InsufficientPlayersForStartErrorDescription = "В данном лобби недостаточно игроков для старта игры";
    
    private const string WrongPlayerToStartGameErrorDescription = "Данный игрок не может запустить игру в заданном лобби";
    
    private const string GameAlreadyStartedErrorDescription = "В данном лобби игра уже была запущена ранее";

    private const string PlayerCantDisbandLobbyErrorDescription = "Данный игрок не может расформировать лобби";
    
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
    public Lobby(PlayerId initiatorPlayerId) : base(LobbyId.CreateNew())
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
            Disband(playerId);
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

    public ErrorOr<bool> LockAndStartGame(PlayerId playerId)
    {
        if (JoinedPlayerId is null)
        {
            return Error.Validation(description: InsufficientPlayersForStartErrorDescription);
        }

        if (playerId != InitiatorPlayerId)
        {
            return Error.Validation(description: WrongPlayerToStartGameErrorDescription);
        }

        if (GameStartedAt.HasValue)
        {
            return Error.Validation(description: GameAlreadyStartedErrorDescription);
        }
        
        GameStartedAt = DateTimeOffset.UtcNow;
        
        RaiseEvent(new LobbyLockedForGameStartDomainEvent(
            Id,
            InitiatorPlayerId,
            JoinedPlayerId,
            GameStartedAt.Value));

        return true;
    }

    public ErrorOr<bool> Disband(PlayerId playerId)
    {
        if (playerId != InitiatorPlayerId)
        {
            return Error.Validation(description: PlayerCantDisbandLobbyErrorDescription);
        }

        if (JoinedPlayerId is not null)
        {
            PlayerLeave(JoinedPlayerId);
        }
        
        RaiseEvent(new LobbyDisbandedDomainEvent(Id));

        return true;
    }
}