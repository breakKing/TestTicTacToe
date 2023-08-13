using System.Net;
using Common.Api;
using FastEndpoints;
using Gaming.Application.Games.Move;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Games.Move;

public sealed class PlayerMoveEndpoint : EndpointBase<PlayerMoveRequest, Results<Ok, ProblemDetails>>
{
    private readonly ISender _sender;

    public PlayerMoveEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("{@gameId}/move", r => new { r.GameId });
        Group<GameGroup>();

        ConfigureSwaggerDescription(
            new PlayerMoveSummary(), 
            false,
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok, ProblemDetails>> ExecuteAsync(PlayerMoveRequest req, CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var command = new PlayerMoveCommand(userId.Value, req.GameId, req.X, req.Y);

        var result = await _sender.Send(command, ct);

        if (result.IsError)
        {
            AddErrors(result.Errors);
            return new ProblemDetails(ValidationFailures);
        }

        return TypedResults.Ok();
    }
}