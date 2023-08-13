using System.Net;
using Common.Api;
using ErrorOr;
using FastEndpoints;
using Gaming.Application.Lobbies.Join;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Lobbies.Join;

public sealed class JoinLobbyEndpoint : EndpointBase<
    JoinLobbyRequest, 
    Results<Ok, NotFound<ProblemDetails>, ProblemDetails>>
{
    private readonly ISender _sender;

    public JoinLobbyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Group<LobbyGroup>();
        Post("{@lobbyId}/join", r => new { r.LobbyId });
        
        ConfigureSwaggerDescription(
            new JoinLobbySummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok, NotFound<ProblemDetails>, ProblemDetails>> ExecuteAsync(
        JoinLobbyRequest req, 
        CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var command = new JoinLobbyCommand(userId.Value, req.LobbyId);

        var result = await _sender.Send(command, ct);

        if (result.IsError)
        {
            AddErrors(result.Errors);
            return new ProblemDetails(ValidationFailures);
        }

        return TypedResults.Ok();
    }
}