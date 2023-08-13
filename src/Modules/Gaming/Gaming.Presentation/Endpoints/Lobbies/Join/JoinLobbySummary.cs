using System.Net;
using Common.Api;
using FastEndpoints;

namespace Gaming.Presentation.Endpoints.Lobbies.Join;

public sealed class JoinLobbySummary : EndpointSummaryBase
{
    public JoinLobbySummary()
    {
        Summary = "Присоединение к лобби";
        Description = "Присоединение текущего пользователя к заданному лобби";
        
        AddSuccessResponseExample(HttpStatusCode.OK, new EmptyResponse());
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.NotFound, HttpStatusCode.InternalServerError);
    }
}