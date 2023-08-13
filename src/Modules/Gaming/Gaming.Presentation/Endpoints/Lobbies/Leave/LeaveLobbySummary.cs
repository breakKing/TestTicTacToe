using System.Net;
using Common.Api;
using FastEndpoints;

namespace Gaming.Presentation.Endpoints.Lobbies.Leave;

public sealed class LeaveLobbySummary : EndpointSummaryBase
{
    public LeaveLobbySummary()
    {
        Summary = "Выход из лобби";
        Description = "Выход текущего пользователя из заданного лобби";
        
        AddSuccessResponseExample(HttpStatusCode.OK, new EmptyResponse());
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}