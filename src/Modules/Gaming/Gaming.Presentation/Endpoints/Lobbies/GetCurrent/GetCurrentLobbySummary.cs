using System.Net;
using Common.Api;
using Gaming.Application.Lobbies;
using Gaming.Application.Players;

namespace Gaming.Presentation.Endpoints.Lobbies.GetCurrent;

public sealed class GetCurrentLobbySummary : EndpointSummaryBase
{
    private static readonly LobbyDto SuccessResponseExample = new()
    {
        Id = Guid.NewGuid(),
        InitiatorPlayer = new PlayerDto
        {
            Id = Guid.NewGuid(),
            Username = "test_user_1"
        },
        JoinedPlayer = new PlayerDto
        {
            Id = Guid.NewGuid(),
            Username = "test_user_2"
        }
    };
    public GetCurrentLobbySummary()
    {
        Summary = "Получение лобби, в котором находится текущий пользователь";
        Description = "Получение информации о лобби, в котором состоит текущий пользователь";
        
        AddSuccessResponseExample(HttpStatusCode.OK, SuccessResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.NotFound, HttpStatusCode.InternalServerError);
    }
}