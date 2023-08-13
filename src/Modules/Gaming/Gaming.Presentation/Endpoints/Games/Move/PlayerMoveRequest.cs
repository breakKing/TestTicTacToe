namespace Gaming.Presentation.Endpoints.Games.Move;

public sealed record PlayerMoveRequest(Guid GameId, int X, int Y);
