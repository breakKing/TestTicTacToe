using System.Net;
using Common.Api;
using ErrorOr;
using FastEndpoints;
using Gaming.Application.Lobbies.Leave;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Lobbies.Lock;

public sealed class LockLobbyEndpoint : EndpointBase<LockLobbyRequest, Results<Ok, ProblemDetails>>
{
    private readonly ISender _sender;

    public LockLobbyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("{@lobbyId}/lock", r => new { r.LobbyId });
        Group<LobbyGroup>();

        ConfigureSwaggerDescription(
            new LockLobbySummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok, ProblemDetails>> ExecuteAsync(
        LockLobbyRequest req, 
        CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var command = new LeaveLobbyCommand(userId.Value, req.LobbyId);

        var result = await _sender.Send(command, ct);

        if (result.IsError)
        {
            AddErrors(result.Errors);
            return new ProblemDetails(ValidationFailures);
        }

        return TypedResults.Ok();
    }
}