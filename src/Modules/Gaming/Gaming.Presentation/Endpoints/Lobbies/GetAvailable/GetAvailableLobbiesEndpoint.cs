using System.Net;
using Common.Api;
using FastEndpoints;
using Gaming.Application.Lobbies;
using Gaming.Application.Lobbies.GetAvailable;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gaming.Presentation.Endpoints.Lobbies.GetAvailable;

public sealed class GetAvailableLobbiesEndpoint : EndpointBase<GetAvailableLobbiesRequest, Results<Ok<List<LobbyDto>>, ProblemDetails>>
{
    private readonly ISender _sender;

    public GetAvailableLobbiesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Get("available");
        Group<LobbyGroup>();

        ConfigureSwaggerDescription(
            new GetAvailableLobbiesSummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok<List<LobbyDto>>, ProblemDetails>> ExecuteAsync(
        GetAvailableLobbiesRequest req,
        CancellationToken ct)
    {
        var userId = GetCurrentUserId()!;

        var command = new GetAvailableLobbiesQuery(
            userId.Value, 
            new(req.PageNumber, req.PageSize));

        var result = await _sender.Send(command, ct);

        if (result.IsError)
        {
            AddErrors(result.Errors);
            return new ProblemDetails(ValidationFailures);
        }
        
        this.PreparePaginationResult(result.Value);

        return TypedResults.Ok(result.Value.Items);
    }
}