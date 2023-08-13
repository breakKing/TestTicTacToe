using System.Net;
using Common.Api;
using ErrorOr;
using FastEndpoints;
using Gaming.Application.Lobbies;
using Gaming.Application.Lobbies.GetCurrent;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Lobbies.GetCurrent;

public sealed class GetCurrentLobbyEndpoint : EndpointBase<
    EmptyRequest, 
    Results<Ok<LobbyDto>, NotFound<ProblemDetails>, ProblemDetails>>
{
    private readonly ISender _sender;

    public GetCurrentLobbyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Group<LobbyGroup>();
        Get("current");
        
        ConfigureSwaggerDescription(
            new GetCurrentLobbySummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok<LobbyDto>, NotFound<ProblemDetails>, ProblemDetails>> ExecuteAsync(
        EmptyRequest req, 
        CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var command = new GetPlayerCurrentLobbyQuery(userId.Value);

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