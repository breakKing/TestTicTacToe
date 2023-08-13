using System.Net;
using Common.Api;
using FastEndpoints;
using Gaming.Application.Lobbies.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Lobbies.Create;

public sealed class CreateLobbyEndpoint : EndpointBase<EmptyRequest, Results<Ok, ProblemDetails>>
{
    private readonly ISender _sender;

    public CreateLobbyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("");
        Group<LobbyGroup>();

        ConfigureSwaggerDescription(
            new CreateLobbySummary(), 
            true,
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok, ProblemDetails>> ExecuteAsync(
        EmptyRequest req, 
        CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var command = new CreateLobbyCommand(userId.Value);

        var result = await _sender.Send(command, ct);

        if (result.IsError)
        {
            AddErrors(result.Errors);
            return new ProblemDetails(ValidationFailures);
        }

        return TypedResults.Ok();
    }
}