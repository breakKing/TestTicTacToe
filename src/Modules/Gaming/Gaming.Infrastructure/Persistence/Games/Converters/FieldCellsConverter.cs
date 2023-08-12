using Gaming.Domain.Games.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gaming.Infrastructure.Persistence.Games.Converters;

internal sealed class FieldCellsConverter : ValueConverter<IReadOnlyList<IReadOnlyList<FieldMark>>, List<int>>
{
    public FieldCellsConverter() : 
        base(
            cells => cells
                .SelectMany(c => c
                    .Select(cIn => cIn.Value)
                    .ToList())
                .ToList(),
            values => values
                .Select(FieldMark.CreateFromValue)
                .Chunk(FieldCoordinates.FieldSize)
                .ToArray()
                .AsReadOnly())
    {
    }
}