using System.Net;
using Common.Api;
using ErrorOr;
using FastEndpoints;
using Gaming.Application.Lobbies.Leave;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Lobbies.Leave;

public sealed class LeaveLobbyEndpoint : EndpointBase<
    LeaveLobbyRequest, 
    Results<Ok, NotFound<ProblemDetails>, ProblemDetails>>
{
    private readonly ISender _sender;

    public LeaveLobbyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Group<LobbyGroup>();
        Post("{@lobbyId}/leave", r => new { r.LobbyId });
        
        ConfigureSwaggerDescription(
            new LeaveLobbySummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok, NotFound<ProblemDetails>, ProblemDetails>> ExecuteAsync(
        LeaveLobbyRequest req, 
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