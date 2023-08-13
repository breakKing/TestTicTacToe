using System.Net;
using Common.Api;
using ErrorOr;
using FastEndpoints;
using Gaming.Application.Games;
using Gaming.Application.Games.GetForPlayer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Games.GetCurrent;

public sealed class GetGameForCurrentPlayerEndpoint : EndpointBase<
    GetGameForCurrentPlayerRequest, 
    Results<Ok<GameDto>, NotFound<ProblemDetails>, ProblemDetails>>
{
    private readonly ISender _sender;

    public GetGameForCurrentPlayerEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Get("{@gameId}", r => new { r.GameId });
        Group<GameGroup>();

        ConfigureSwaggerDescription(
            new GetGameForCurrentPlayerSummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok<GameDto>, NotFound<ProblemDetails>, ProblemDetails>> ExecuteAsync(
        GetGameForCurrentPlayerRequest req, 
        CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var command = new GetGameForPlayerQuery(req.GameId, userId.Value);

        var result = await _sender.Send(command, ct);

        if (result.IsError)
        {
            AddErrors(result.Errors);

            if (result.Errors.Any(e => e.Type == ErrorType.NotFound))
            {
                return TypedResults.NotFound(new ProblemDetails(ValidationFailures));
            }

            return new ProblemDetails(ValidationFailures);
        }

        return TypedResults.Ok(result.Value);
    }
}