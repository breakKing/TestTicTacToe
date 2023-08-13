namespace Gaming.Application.Players;

public sealed class PlayerDto
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
}