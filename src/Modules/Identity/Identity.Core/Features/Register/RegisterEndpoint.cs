using System.Net;
using Common.Api;
using FastEndpoints;
using Identity.Core.Common.Identity.Entites;
using Identity.IntegrationEvents;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Features.Register;

public sealed class RegisterEndpoint : EndpointBase<RegisterRequest, Results<Ok, ProblemDetails>>
{
    private const string UserAlreadyExistsErrorDescription = "Пользователь с таким именем уже существует";
    
    private readonly UserManager<User> _userManager;
    private readonly IPublishEndpoint _publishEndpoint;

    /// <inheritdoc />
    public RegisterEndpoint(UserManager<User> userManager, IPublishEndpoint publishEndpoint)
    {
        _userManager = userManager;
        _publishEndpoint = publishEndpoint;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("register");
        Group<UserGroup>();
        
        AllowAnonymous();

        ConfigureSwaggerDescription(
            new RegisterSummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok, ProblemDetails>> ExecuteAsync(RegisterRequest req, CancellationToken ct)
    {
        var existingUser = await _userManager.FindByNameAsync(req.Username);

        if (existingUser is not null)
        {
            AddError(UserAlreadyExistsErrorDescription);
            return new ProblemDetails(ValidationFailures);
        }

        var user = new User
        {
            UserName = req.Username
        };

        await _userManager.CreateAsync(user, req.Password);
        
        await _publishEndpoint.Publish(
            new UserRegisteredIntegrationEvent(user.Id, user.UserName!), 
            ct);

        return TypedResults.Ok();
    }
}