using Common.Domain.Primitives;
using Gaming.Domain.Games.ValueObjects;
using ErrorOr;

namespace Gaming.Domain.Games.Entities;

public sealed class Field : Entity<FieldId>
{
    private const string CellAlreadyMarkedErrorDescription = "Данная клетка уже занята";

    private readonly FieldMark[][] _cells = Enumerable.Range(0, FieldCoordinates.FieldSize)
        .Select(_ =>
            Enumerable.Range(0, FieldCoordinates.FieldSize)
                .Select(_ => FieldMark.NotMarked)
                .ToArray())
        .ToArray();
    
    public GameId GameId { get; private set; }
    
    public IReadOnlyList<IReadOnlyList<FieldMark>> Cells => _cells.AsReadOnly();
    
    /// <inheritdoc />
    public Field(GameId gameId) : base(FieldId.CreateNew())
    {
        GameId = gameId;
    }

    public ErrorOr<bool> FirstPlayerMove(FieldCoordinates coordinates)
    {
        return PlayerMove(coordinates, FieldMark.MarkedByFirstPlayer);
    }
    
    public ErrorOr<bool> SecondPlayerMove(FieldCoordinates coordinates)
    {
        return PlayerMove(coordinates, FieldMark.MarkedBySecondPlayer);
    }

    public bool HasFirstPlayerWon()
    {
        return PlayerWon(FieldMark.MarkedByFirstPlayer);
    }

    public bool HasSecondPlayerWon()
    {
        return PlayerWon(FieldMark.MarkedBySecondPlayer);
    }
    
    private ErrorOr<bool> PlayerMove(FieldCoordinates coordinates, FieldMark markToSet)
    {
        var x = coordinates.Value.X;
        var y = coordinates.Value.Y; 
        
        if (_cells[x][y] != FieldMark.NotMarked)
        {
            return Error.Validation(description: CellAlreadyMarkedErrorDescription);
        }
        
        _cells[x][y] = markToSet;

        return true;
    }

    private bool PlayerWon(FieldMark fieldMarkToCheck)
    {
        return CheckRows(fieldMarkToCheck)
               || CheckColumns(fieldMarkToCheck)
               || CheckDiagonals(fieldMarkToCheck);
    }

    private bool CheckRows(FieldMark fieldMarkToCheck)
    {
        return _cells.Any(
            row => 
                row.All(cell => cell == fieldMarkToCheck));
    }
    
    private bool CheckColumns(FieldMark fieldMarkToCheck)
    {
        for (int col = 0; col < FieldCoordinates.FieldSize; col++)
        {
            bool isWinningColumn = Enumerable.Range(0, FieldCoordinates.FieldSize)
                .All(j => _cells[j][col] == fieldMarkToCheck);
            
            if (isWinningColumn)
            {
                return true;
            }
        }

        return false;
    }
    
    private bool CheckDiagonals(FieldMark fieldMarkToCheck)
    {
        return Enumerable.Range(0, FieldCoordinates.FieldSize)
                   .All(j => _cells[j][j] == fieldMarkToCheck)
               || Enumerable.Range(0, FieldCoordinates.FieldSize)
                   .All(j => _cells[FieldCoordinates.FieldSize - 1 - j][FieldCoordinates.FieldSize - 1 - j] == fieldMarkToCheck);
    }
}