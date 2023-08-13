using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;
using ErrorOr;
using Gaming.Domain.Common;
using Gaming.Domain.Games.DomainEvents;
using Gaming.Domain.Lobbies.ValueObjects;

namespace Gaming.Domain.Games.Entities;

public sealed class Game : AggregateRoot<GameId>
{
    private const string GameNotInProgressErrorDescription = "Игра уже окончена";
    private const string PlayerNotInGameErrorDescription = "Игрок не является участником данной игры";
    private const string PlayerCantMoveErrorDescription = "Игрок не может сделать свой ход сейчас";

    private readonly List<GameMove> _moves = new();

    public LobbyId StartedFromLobbyId { get; private set; }

    public PlayerId FirstPlayerId { get; private set; }

    public PlayerId SecondPlayerId { get; private set; }

    public Field Field { get; private set; }

    /// <summary>
    /// Последний ходивший игрок
    /// </summary>
    public PlayerId? LastMovePlayerId { get; private set; }

    public DateTimeOffset StartedAt { get; private set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? FinishedAt { get; private set; }

    public GameResult Result { get; private set; } = GameResult.StillInProgress;

    public IReadOnlyList<GameMove> Moves => _moves.AsReadOnly();

    public PlayerId? WinnerPlayerId
    {
        get
        {
            if (Result == GameResult.FirstPlayerVictory)
            {
                return FirstPlayerId;
            }

            if (Result == GameResult.SecondPlayerVictory)
            {
                return SecondPlayerId;
            }

            return null;
        }
    }

    public PlayerId? LoserPlayerId
    {
        get
        {
            if (WinnerPlayerId == FirstPlayerId)
            {
                return SecondPlayerId;
            }

            if (WinnerPlayerId == SecondPlayerId)
            {
                return FirstPlayerId;
            }

            return null;
        }
    }

    /// <summary>
    /// Не использовать, необходим для обхода ограничений EF Core
    /// </summary>
    private Game() : base(GameId.CreateNew())
    {
        FirstPlayerId = PlayerId.CreateNew();
        SecondPlayerId = PlayerId.CreateNew();
        StartedFromLobbyId = LobbyId.CreateNew();
        Field = new Field(Id);
    }

/// <inheritdoc />
    public Game(PlayerId firstPlayerId, PlayerId secondPlayerId, LobbyId startedFromLobbyId) : base(GameId.CreateNew())
    {
        FirstPlayerId = firstPlayerId;
        SecondPlayerId = secondPlayerId;
        StartedFromLobbyId = startedFromLobbyId;
        Field = new Field(Id);
        
        RaiseEvent(new GameStartedDomainEvent(Id, FirstPlayerId, SecondPlayerId, StartedAt));
    }

    public ErrorOr<bool> Move(PlayerId playerId, FieldCoordinates coordinates)
    {
        if (Result != GameResult.StillInProgress)
        {
            return Error.Validation(description: GameNotInProgressErrorDescription);
        }
        
        if (!IsPlayerAllowedForGame(playerId))
        {
            return Error.Validation(description: PlayerNotInGameErrorDescription);
        }

        if (!IsPlayerAllowedToMove(playerId))
        {
            return Error.Validation(description: PlayerCantMoveErrorDescription);
        }

        var moveResult = PlayerMove(playerId, coordinates);

        if (moveResult.IsError)
        {
            return moveResult.Errors;
        }

        LastMovePlayerId = playerId;
        _moves.Add(new GameMove(Id, playerId, coordinates));
        
        var checkResult = CheckResult();

        if (checkResult != GameResult.StillInProgress)
        {
            HandleFinalGameResult(checkResult);
        }

        return true;
    }

    private bool IsPlayerAllowedForGame(PlayerId playerId)
    {
        return playerId == FirstPlayerId || playerId == SecondPlayerId;
    }
    
    private bool IsPlayerAllowedToMove(PlayerId playerId)
    {
        if (LastMovePlayerId is null)
        {
            return playerId == FirstPlayerId;
        }

        return playerId != LastMovePlayerId;
    }

    private ErrorOr<bool> PlayerMove(PlayerId playerId, FieldCoordinates coordinates)
    {
        if (playerId == FirstPlayerId)
        {
            return Field.FirstPlayerMove(coordinates);
        }

        return Field.SecondPlayerMove(coordinates);
    }

    private GameResult CheckResult()
    {
        if (Field.HasFirstPlayerWon())
        {
            return GameResult.FirstPlayerVictory;
        }

        if (Field.HasSecondPlayerWon())
        {
            return GameResult.SecondPlayerVictory;
        }
        
        var allCellsAreMarked = Field.Cells
            .All(row => row
                .All(cell => cell == FieldMark.NotMarked));

        if (allCellsAreMarked)
        {
            return GameResult.Draw;
        }

        return GameResult.StillInProgress;
    }

    private void HandleFinalGameResult(GameResult result)
    {
        Result = result;
        FinishedAt = DateTimeOffset.UtcNow;
        
        RaiseEvent(new GameEndedDomainEvent(Id, FinishedAt.Value, WinnerPlayerId, LoserPlayerId));
    }
}