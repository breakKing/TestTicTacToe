using Common.Domain.Primitives;
using Gaming.Domain.Games.ValueObjects;
using ErrorOr;

namespace Gaming.Domain.Games.Entities;

public sealed class Field : Entity<FieldId>
{
    private const string CellDoesntExistsErrorDescription = "Такой клетки нет на поле";
    private const string CellAlreadyMarkedErrorDescription = "Данная клетка уже занята";
    
    private readonly FieldMark[][] _cells = new[]
    {
        new[] { FieldMark.NotMarked, FieldMark.NotMarked, FieldMark.NotMarked },
        new[] { FieldMark.NotMarked, FieldMark.NotMarked, FieldMark.NotMarked },
        new[] { FieldMark.NotMarked, FieldMark.NotMarked, FieldMark.NotMarked },
    };
    
    public GameId GameId { get; private set; }
    
    public IReadOnlyList<IReadOnlyList<FieldMark>> Cells => _cells.AsReadOnly();
    
    /// <inheritdoc />
    public Field(GameId gameId) : base(FieldId.Create())
    {
        GameId = gameId;
    }

    public ErrorOr<bool> FirstPlayerMove(int x, int y)
    {
        return PlayerMove(x, y, FieldMark.MarkedByFirstPlayer);
    }
    
    public ErrorOr<bool> SecondPlayerMove(int x, int y)
    {
        return PlayerMove(x, y, FieldMark.MarkedBySecondPlayer);
    }

    private ErrorOr<bool> PlayerMove(int x, int y, FieldMark markToSet)
    {
        if (!CellExists(x, y))
        {
            return Error.Validation(description: CellDoesntExistsErrorDescription);
        }

        if (_cells[x][y] != FieldMark.NotMarked)
        {
            return Error.Validation(description: CellAlreadyMarkedErrorDescription);
        }
        
        _cells[x][y] = markToSet;

        return true;
    }

    private static bool CellExists(int x, int y)
    {
        return (x is >= 0 and < 3) && (y is >= 0 and < 3);
    }
}