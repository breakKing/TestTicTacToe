using System.Net;
using Common.Api;
using FastEndpoints;

namespace Gaming.Presentation.Endpoints.Lobbies.Lock;

public sealed class LockLobbySummary : EndpointSummaryBase
{
    public LockLobbySummary()
    {
        Summary = "Блокирование лобби и старт игры";
        Description = "Блокирование лобби перед стартом игры. После этого действия оба игрока не смогут сменить лобби до конца игры";
        
        AddSuccessResponseExample(HttpStatusCode.OK, new EmptyResponse());
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.NotFound, HttpStatusCode.InternalServerError);
    }
}