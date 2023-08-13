using System.Net;
using Common.Api;
using FastEndpoints;

namespace Gaming.Presentation.Endpoints.Games.Move;

public sealed class PlayerMoveSummary : EndpointSummaryBase
{
    public PlayerMoveSummary()
    {
        Summary = "Ход в рамках игры";
        Description = "Ход текущего игрока в рамках указанной игры";
        
        AddSuccessResponseExample(HttpStatusCode.OK, new EmptyResponse());
        AddFailResponseExamples(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }
}