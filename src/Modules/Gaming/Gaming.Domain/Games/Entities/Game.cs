using Common.Domain.Primitives;
using Gaming.Domain.Games.ValueObjects;
using Gaming.Domain.Players.ValueObjects;
using ErrorOr;

namespace Gaming.Domain.Games.Entities;

public sealed class Game : AggregateRoot<GameId>
{
    private const string PlayerNotInGameErrorDescription = "Игрок не является участником данной игры"; 
    private const string PlayerCantMoveErrorDescription = "Игрок не может сделать свой ход сейчас"; 
    
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

    /// <inheritdoc />
    public Game(PlayerId firstPlayerId, PlayerId secondPlayerId) : base(GameId.Create())
    {
        FirstPlayerId = firstPlayerId;
        SecondPlayerId = secondPlayerId;
        Field = new Field(Id);
    }

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

    public ErrorOr<bool> Move(PlayerId playerId, int x, int y)
    {
        if (!IsPlayerAllowedForGame(playerId))
        {
            return Error.Validation(description: PlayerNotInGameErrorDescription);
        }

        if (!IsPlayerAllowedToMove(playerId))
        {
            return Error.Validation(description: PlayerCantMoveErrorDescription);
        }

        var moveResult = PlayerMove(playerId, x, y);

        if (moveResult.IsError)
        {
            return moveResult.Errors;
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

    private ErrorOr<bool> PlayerMove(PlayerId playerId, int x, int y)
    {
        if (playerId == FirstPlayerId)
        {
            return Field.FirstPlayerMove(x, y);
        }

        return Field.SecondPlayerMove(x, y);
    }
}