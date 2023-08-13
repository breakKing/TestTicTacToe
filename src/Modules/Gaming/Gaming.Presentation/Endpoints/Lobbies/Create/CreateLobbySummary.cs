using System.Net;
using Common.Api;
using FastEndpoints;

namespace Gaming.Presentation.Endpoints.Lobbies.Create;

public sealed class CreateLobbySummary : EndpointSummaryBase
{
    public CreateLobbySummary()
    {
        Summary = "Создание лобби";
        Description = "Создание нового лобби и присоединения к нему";
        
        AddSuccessResponseExample(HttpStatusCode.OK, new EmptyResponse());
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}