using System.Net;
using Common.Api;
using Gaming.Application.Lobbies;
using Gaming.Application.Players;

namespace Gaming.Presentation.Endpoints.Lobbies.GetAvailable;

public sealed class GetAvailableLobbiesSummary : EndpointSummaryBase
{
    private static readonly List<LobbyDto> SuccessResponseExample = new()
    {
        new LobbyDto
        {
            Id = Guid.NewGuid(),
            InitiatorPlayer = new PlayerDto
            {
                Id = Guid.NewGuid(),
                Username = "test_user"
            }
        }
    };
    
    public GetAvailableLobbiesSummary()
    {
        Summary = "Выгрузка списка доступных лобби для присоединения";
        Description = "Выгрузка списка доступных лобби, к которым может присоединиться пользователей. Список выводится в пагинированном виде";
        
        AddSuccessResponseExample(HttpStatusCode.OK, SuccessResponseExample);
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}