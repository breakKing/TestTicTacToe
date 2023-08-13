using System.Net;
using Common.Api;
using FastEndpoints;
using Gaming.Application.Games;
using MediatR;

namespace Gaming.Presentation.Endpoints.Games.GetCurrent;

public sealed class GetGameForCurrentPlayerEndpoint : EndpointBase<EmptyRequest, GameDto>
{
    private readonly ISender _sender;

    public GetGameForCurrentPlayerEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Group<GameGroup>();
        Get("current");
        
        ConfigureSwaggerDescription(
            new GetGameForCurrentPlayerSummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError);
    }
}