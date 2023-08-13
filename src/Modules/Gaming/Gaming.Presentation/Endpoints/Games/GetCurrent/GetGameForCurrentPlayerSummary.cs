using System.Net;
using Common.Api;
using Gaming.Application.Games;
using Gaming.Application.Players;
using Gaming.Domain.Games.ValueObjects;

namespace Gaming.Presentation.Endpoints.Games.GetCurrent;

public sealed class GetGameForCurrentPlayerSummary : EndpointSummaryBase
{
    private static readonly Guid FirstPlayerId = Guid.NewGuid();
    
    private static readonly GameDto SuccessResponseExample = new()
    {
        Id = Guid.NewGuid(),
        FirstPlayer = new PlayerDto
        {
            Id = FirstPlayerId,
            Username = "test_user_1"
        },
        SecondPlayer = new PlayerDto
        {
            Id = Guid.NewGuid(),
            Username = "test_user_2"
        },
        CellValues = new[]
        {
            new []{ 0, 0, 1 },
            new []{ 0, 1, 0 },
            new []{ 2, 2, 0 }
        },
        LastMovePlayerId = FirstPlayerId,
        StartedAt = DateTimeOffset.Now.AddMinutes(-2),
        FinishedAt = DateTimeOffset.Now.AddMinutes(-1),
        Result = GameResult.StillInProgress
    };
    
    public GetGameForCurrentPlayerSummary()
    {
        Summary = "Получение игры";
        Description = "Получение игры по идентификатору. Ответ будет пустым, если игрок не участвует в игре";
        
        AddSuccessResponseExample(HttpStatusCode.OK, SuccessResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.NotFound, HttpStatusCode.InternalServerError);
    }
}